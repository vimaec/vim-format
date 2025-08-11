using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.CodeGen;

public static class ObjectModelGenerator
{
    public static string GetEntityTableGetterFunctionName(this ValueSerializationStrategy strategy, Type type)
    {
        return strategy switch
        {
            ValueSerializationStrategy.SerializeAsStringColumn
                => nameof(EntityTable.GetStringColumnValues),
            ValueSerializationStrategy.SerializeAsDataColumn
                => $"{nameof(EntityTable.GetDataColumnValues)}<{type.Name}>",
            _ => throw new Exception($"{nameof(GetEntityTableGetterFunctionName)} error - unknown strategy {strategy:G}")
        };
    }

    public static string GetEntityTable_v2GetterFunctionName(this ValueSerializationStrategy strategy, Type type)
    {
        return strategy switch
        {
            ValueSerializationStrategy.SerializeAsStringColumn
                => nameof(EntityTable_v2.GetStringColumnValues),
            ValueSerializationStrategy.SerializeAsDataColumn
                => $"{nameof(EntityTable_v2.GetDataColumnValues)}<{type.Name}>",
            _ => throw new Exception($"{nameof(GetEntityTable_v2GetterFunctionName)} error - unknown strategy {strategy:G}")
        };
    }

    private static string GetEntityTableBuilderAddFunctionName(this ValueSerializationStrategy strategy, Type typeName)
    {
        return strategy switch
        {
            ValueSerializationStrategy.SerializeAsStringColumn
                => nameof(EntityTableBuilder.AddStringColumn),
            ValueSerializationStrategy.SerializeAsDataColumn
                => $"{nameof(EntityTableBuilder.AddDataColumn)}",
            _ => throw new Exception($"{nameof(GetEntityTableBuilderAddFunctionName)} error - unknown strategy {strategy:G}")
        };
    }

    private class EntityFields
    {
        public readonly List<string> TableInitializers = new();
        public readonly List<string> ArraysInitializers = new();
        public readonly List<string> RelationalColumns = new();
    }

    private static CodeBuilder WriteDocumentEntityData(Type t, CodeBuilder cb, EntityFields constructor)
    {
        cb.AppendLine("");
        cb.AppendLine($"// {t.Name}");
        cb.AppendLine("");

        var relationFields = t.GetRelationFields().ToArray();
        var entityFields = t.GetEntityFields().ToArray();

        // EntityTables
        cb.AppendLine($"public EntityTable {t.Name}EntityTable {{ get; }}");
        constructor.TableInitializers.Add($"{t.Name}EntityTable = Document.GetTable(\"{t.GetEntityTableName()}\");");
        cb.AppendLine("");

        // Get each non-relational columns for each element
        foreach (var fieldInfo in entityFields)
        {
            var fieldName = fieldInfo.Name;
            var fieldType = fieldInfo.FieldType;
            var fieldTypeName = fieldInfo.FieldType.Name;

            var loadingInfos = fieldInfo.GetEntityColumnLoadingInfo();

            var baseStrategy = loadingInfos[0].Strategy; // Invariant: there is always at least one entityColumnInfo (the default one)
            
            var dataColumnGetters = loadingInfos.Select(eci =>
            {
                var functionName = eci.Strategy.GetEntityTableGetterFunctionName(eci.EntityColumnAttribute.SerializedType);
                var dataColumnGetter = $"{t.Name}EntityTable?.{functionName}(\"{eci.SerializedValueColumnName}\")";
                if (eci.EntityColumnAttribute.SerializedType != fieldType)
                {
                    dataColumnGetter += $"?.Select(v => ({fieldTypeName}) v)";
                }
                return dataColumnGetter;
            }).ToArray();

            var dataColumnGetterString = dataColumnGetters.Length > 1
                ? $"({string.Join(" ?? ", dataColumnGetters)})"
                : dataColumnGetters[0];

            cb.AppendLine($"public IArray<{fieldTypeName}> {t.Name}{fieldName} {{ get; }}");
            constructor.ArraysInitializers
                       .Add($"{t.Name}{fieldName} = {dataColumnGetterString} ?? Array.Empty<{fieldTypeName}>().ToIArray();");

            // Safe accessor.
            var defaultValue = baseStrategy == ValueSerializationStrategy.SerializeAsStringColumn ? "\"\"" : "default";
            cb.AppendLine($"public {fieldTypeName} Get{t.Name}{fieldName}(int index, {fieldTypeName} defaultValue = {defaultValue}) => {t.Name}{fieldName}?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;");
        }

        // Get each relational column
        foreach (var fieldInfo in relationFields)
        {
            var (indexColumnName, localFieldName) = fieldInfo.GetIndexColumnInfo();

            cb.AppendLine($"public IArray<int> {t.Name}{localFieldName}Index {{ get; }}");
            constructor.RelationalColumns
                       .Add($"{t.Name}{localFieldName}Index = {t.Name}EntityTable?.GetIndexColumnValues(\"{indexColumnName}\") ?? Array.Empty<int>().ToIArray();");

            cb.AppendLine($"public int Get{t.Name}{localFieldName}Index(int index) => {t.Name}{localFieldName}Index?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;");
        }

        // Num Count
        cb.AppendLine($"public int Num{t.Name} => {t.Name}EntityTable?.NumRows ?? 0;");

        // Entity lists
        cb.AppendLine($"public IArray<{t.Name}> {t.Name}List {{ get; }}");

        // Element getter function
        cb.AppendLine($"public {t.Name} Get{t.Name}(int n)");
        cb.AppendLine("{");

        // Get the entity retrieval function
        cb.AppendLine("if (n < 0) return null;");
        cb.AppendLine($"var r = new {t.Name}();");
        cb.AppendLine("r.Document = Document;");
        cb.AppendLine("r.Index = n;");
        foreach (var fieldInfo in entityFields)
        {
            cb.AppendLine($"r.{fieldInfo.Name} = {t.Name}{fieldInfo.Name}.ElementAtOrDefault(n);");
        }

        foreach (var fieldInfo in relationFields)
        {
            var relType = fieldInfo.FieldType.RelationTypeParameter();
            cb.AppendLine($"r.{fieldInfo.Name} = new Relation<{relType}>(Get{t.Name}{fieldInfo.Name.Substring(1)}Index(n), Get{relType.Name});");
        }

        cb.AppendLine("return r;");
        cb.AppendLine("}");
        cb.AppendLine();

        return cb;
    }

    private static CodeBuilder WriteEntityClass(Type t, CodeBuilder cb = null)
    {
        var relationFields = t.GetRelationFields().ToArray();

        cb ??= new CodeBuilder();
        cb.AppendLine("// AUTO-GENERATED");
        cb.AppendLine($"public partial class {t.Name}").AppendLine("{");
        foreach (var fieldInfo in relationFields)
        {
            var relationFieldName = fieldInfo.Name.Substring(1);
            cb.AppendLine($"public {fieldInfo.FieldType.RelationTypeParameter()} {relationFieldName} => {fieldInfo.Name}?.Value;");
            cb.AppendLine($"public int {relationFieldName}Index => {fieldInfo.Name}?.Index ?? EntityRelation.None;");
        }

        cb.AppendLine($"public {t.Name}()");
        cb.AppendLine("{");
        foreach (var fieldInfo in relationFields)
        {
            cb.AppendLine($"{fieldInfo.Name} = new Relation<{fieldInfo.FieldType.RelationTypeParameter()}>();");
        }

        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine("public override bool FieldsAreEqual(object obj)");
        cb.AppendLine("{");

        cb.WriteFieldsAreEqualsType(t);

        cb.AppendLine("return false;");
        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine("} // end of class");
        cb.AppendLine();
        return cb;
    }

    private static CodeBuilder WriteFieldsAreEqualsType(this CodeBuilder cb, Type t,
        (string @namespace, string variable)? modifier = null)
    {
        var entityFields = t.GetEntityFields().ToArray();
        var relationFields = t.GetRelationFields().ToArray();

        var type = (modifier?.@namespace ?? string.Empty) + t.Name;
        var variable = (modifier?.variable ?? string.Empty) + "other";

        cb.AppendLine($"if ((obj is {type} {variable}))");
        cb.AppendLine("{");
        cb.AppendLine("var fieldsAreEqual =");

        IEnumerable<FieldInfo> GetEquatableFields(FieldInfo[] fis)
            => fis.Where(fi => !fi.GetCustomAttributes().Any(a => a is IgnoreInEquality));

        var entityFieldComparisons = GetEquatableFields(entityFields).Select(f => $"({f.Name} == {variable}.{f.Name})")
                                                                     .Prepend($"(Index == {variable}.Index)");
        var relationFieldComparisons = GetEquatableFields(relationFields)
           .Select(f => $"({f.Name}?.Index == {variable}.{f.Name}?.Index)");

        var comparisons = entityFieldComparisons.Concat(relationFieldComparisons).ToArray();
        for (var i = 0; i < comparisons.Length; ++i)
        {
            var comparison = comparisons[i];
            cb.AppendLine($"    {comparison}{(i == comparisons.Length - 1 ? ";" : " &&")}");
        }

        cb.AppendLine("if (!fieldsAreEqual)");
        cb.AppendLine("{");
        cb.AppendLine("return false;");
        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine("return true;");
        cb.AppendLine("}");

        return cb;
    }

    private static CodeBuilder WriteDocument(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        foreach (var et in entityTypes)
            WriteEntityClass(et, cb);

        cb.AppendLine("public partial class DocumentModel");
        cb.AppendLine("{");

        cb.AppendLine("public ElementIndexMaps ElementIndexMaps { get; }");

        var entityFields = new EntityFields();
        foreach (var et in entityTypes)
            WriteDocumentEntityData(et, cb, entityFields);

        cb.AppendLine("// All entity collections");
        cb.AppendLine("public Dictionary<string, IEnumerable<Entity>> AllEntities => new Dictionary<string, IEnumerable<Entity>>() {");
        foreach (var t in entityTypes)
            cb.AppendLine($"{{\"{t.GetEntityTableName()}\", {t.Name}List.ToEnumerable()}},");
        cb.AppendLine("};");
        cb.AppendLine();

        cb.AppendLine("// Entity types from table names");
        cb.AppendLine("public Dictionary<string, Type> EntityTypes => new Dictionary<string, Type>() {");
        foreach (var t in entityTypes)
            cb.AppendLine($"{{\"{t.GetEntityTableName()}\", typeof({t.Name})}},");
        cb.AppendLine("};");

        // Write the constructor
        cb.AppendLine("public DocumentModel(Document d, bool inParallel = true)");
        cb.AppendLine("{");
        cb.AppendLine("Document = d;");
        cb.AppendLine();

        cb.AppendLine("// Initialize entity tables");
        foreach (var line in entityFields.TableInitializers)
            cb.AppendLine(line);
        cb.AppendLine("");

        cb.AppendLine("// Initialize entity arrays");
        foreach (var line in entityFields.ArraysInitializers)
            cb.AppendLine(line);
        cb.AppendLine("");

        cb.AppendLine("// Initialize entity relational columns");
        foreach (var line in entityFields.RelationalColumns)
            cb.AppendLine(line);
        cb.AppendLine("");


        cb.AppendLine("// Initialize entity collections");
        foreach (var t in entityTypes)
            cb.AppendLine($"{t.Name}List = Num{t.Name}.Select(i => Get{t.Name}(i));");
        cb.AppendLine();

        cb.AppendLine("// Initialize element index maps");
        cb.AppendLine("ElementIndexMaps = new ElementIndexMaps(this, inParallel);");

        cb.AppendLine("}");
        cb.AppendLine("} // Document class");
        cb.AppendLine();
        return cb;
    }

    private static void WriteEntityTableSet(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public partial class EntityTableSet");
        cb.AppendLine("{");
        cb.AppendLine("public string[] StringTable { get; }");
        cb.AppendLine();
        cb.AppendLine("public Dictionary<string, SerializableEntityTable> RawTableMap { get; } = new Dictionary<string, SerializableEntityTable>();");
        cb.AppendLine();
        cb.AppendLine("public SerializableEntityTable GetRawTableOrDefault(string tableName)");
        cb.AppendLine("    => RawTableMap.TryGetValue(tableName, out var result) ? result : null;");
        cb.AppendLine();
        cb.AppendLine("public ElementIndexMaps ElementIndexMaps { get; }");
        cb.AppendLine();

        cb.AppendLine("public EntityTableSet(SerializableEntityTable[] rawTables, string[] stringTable, bool inParallel = true)");
        cb.AppendLine("{");
        cb.AppendLine("StringTable = stringTable;");
        cb.AppendLine();
        cb.AppendLine("foreach (var rawTable in rawTables)");
        cb.AppendLine("    RawTableMap[rawTable.Name] = rawTable;");
        cb.AppendLine();
        cb.AppendLine("// Populate the entity tables.");
        foreach (var t in entityTypes)
        {
            var etName = t.GetEntityTableName();
            var tmp = $"{t.Name.ToLowerInvariant()}Table";
            cb.AppendLine($"if (GetRawTableOrDefault(\"{etName}\") is SerializableEntityTable {tmp})");
            cb.AppendLine($"    {t.Name}Table = new {t.Name}Table({tmp}, stringTable, this);");
            cb.AppendLine();
        }
        cb.AppendLine("// Initialize element index maps");
        cb.AppendLine("ElementIndexMaps = new ElementIndexMaps(this, inParallel);");
        cb.AppendLine();
        cb.AppendLine("} // EntityTableSet constructor");

        cb.AppendLine();
        foreach (var t in entityTypes)
        {
            cb.AppendLine($"public {t.Name}Table {t.Name}Table {{ get; }} // can be null");
            cb.AppendLine($"public {t.Name} Get{t.Name}(int index) => {t.Name}Table?.Get(index);");
        }

        var elementKindEntityTypes = entityTypes
            .Select(t => (t, t.GetElementKind()))
            .Where(tuple => tuple.Item2 != ElementKind.Unknown)
            .ToArray();
        cb.AppendLine();
        cb.AppendLine("public static HashSet<string> GetElementKindTableNames()");
        cb.AppendLine("    => new HashSet<string>()");
        cb.AppendLine("    {");
        foreach (var (t, _) in elementKindEntityTypes)
        {
            cb.AppendLine(        $"\"{t.GetEntityTableName()}\",");
        }
        cb.AppendLine("    };");

        cb.AppendLine();
        cb.AppendLine("// Returns an array defining a 1:1 association of Element to its ElementKind");
        cb.AppendLine("public ElementKind[] GetElementKinds()");
        cb.AppendLine("{");
        cb.AppendLine("var elementKinds = new ElementKind[ElementTable?.RowCount ?? 0];");
        cb.AppendLine();
        cb.AppendLine("if (elementKinds.Length == 0) return elementKinds;");
        cb.AppendLine();
        cb.AppendLine("// Initialize all element kinds to unknown");
        cb.AppendLine("for (var i = 0; i < elementKinds.Length; ++i) { elementKinds[i] = ElementKind.Unknown; }");
        cb.AppendLine();
        cb.AppendLine("// Populate the element kinds from the relevant entity tables");
        foreach (var (t, elementKind) in elementKindEntityTypes)
        {
            var etPropertyName = $"{t.Name}Table";
            cb.AppendLine($"for (var i = 0; i < {etPropertyName}.RowCount; ++i)");
            cb.AppendLine("{");
            cb.AppendLine($"var elementIndex = {etPropertyName}?.Column_ElementIndex[i] ?? EntityRelation.None;");
            cb.AppendLine("if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;");
            cb.AppendLine();
            cb.AppendLine("var currentElementKind = elementKinds[elementIndex];");
            cb.AppendLine($"var candidateElementKind = ElementKind.{elementKind:G};");
            cb.AppendLine();
            // NOTE: In some cases, a Group entity shares the same element as a FamilyInstance entity.
            // Likewise, sometimes a System entity shares the same element as a FamilyInstance entity.
            // In these cases, the ElementKind.FamilyInstance takes priority because of the comparison between the currentElementKind and the candidateElementKind.
            cb.AppendLine("// Only update the element kind if it is unknown or if it is less than the current kind.");
            cb.AppendLine("if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;");
            cb.AppendLine();
            cb.AppendLine("elementKinds[elementIndex] = candidateElementKind;");
            cb.AppendLine("}");
            cb.AppendLine();
        }
        cb.AppendLine("return elementKinds;");
        cb.AppendLine("} // GetElementKinds()");

        cb.AppendLine("} // class EntityTableSet");
        cb.AppendLine();

        foreach (var t in entityTypes)
            WriteEntityTable(cb, t);
    }

    private static void WriteEntityTable(CodeBuilder cb, Type t)
    {
        var entityFields = t.GetEntityFields().ToArray();
        var relationFields = t.GetRelationFields().ToArray();

        cb.AppendLine($"public partial class {t.Name}Table : EntityTable_v2, IEnumerable<{t.Name}>");
        cb.AppendLine("{");
        cb.AppendLine();
        cb.AppendLine($"public const string TableName = \"{t.GetEntityTableName()}\";");
        cb.AppendLine();
        cb.AppendLine("public EntityTableSet ParentTableSet { get; } // can be null");
        cb.AppendLine();
        cb.AppendLine($"public {t.Name}Table(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)");
        cb.AppendLine("{");
        cb.AppendLine("ParentTableSet = parentTableSet;");
        foreach (var f in entityFields)
        {
            var fieldName = f.Name;
            var fieldType = f.FieldType;
            var fieldTypeName = f.FieldType.Name;
            var loadingInfos = f.GetEntityColumnLoadingInfo();
            
            var dataColumnGetters = loadingInfos.Select(eci =>
            {
                var functionName = eci.Strategy.GetEntityTable_v2GetterFunctionName(eci.EntityColumnAttribute.SerializedType);
                var dataColumnGetter = $"{functionName}(\"{eci.SerializedValueColumnName}\")";
                if (eci.EntityColumnAttribute.SerializedType != fieldType)
                {
                    dataColumnGetter += $"?.Select(v => ({fieldTypeName}) v).ToArray()";
                }
                return dataColumnGetter;
            }).ToArray();

            var dataColumnGetterString = dataColumnGetters.Length > 1
                ? $"({string.Join(" ?? ", dataColumnGetters)})"
                : dataColumnGetters[0];

            cb.AppendLine($"Column_{fieldName} = {dataColumnGetterString} ?? Array.Empty<{fieldTypeName}>();");
        }
        foreach (var f in relationFields)
        {
            var (indexColumnName, localFieldName) = f.GetIndexColumnInfo();
            cb.AppendLine($"Column_{localFieldName}Index = GetIndexColumnValues(\"{indexColumnName}\") ?? Array.Empty<int>();");
        }
        cb.AppendLine("}");
        cb.AppendLine();

        foreach (var f in entityFields)
        {
            var fieldName = f.Name;
            var fieldTypeName = f.FieldType.Name;
            var loadingInfos = f.GetEntityColumnLoadingInfo();
            var baseStrategy = loadingInfos[0].Strategy; // Invariant: there is always at least one entityColumnInfo (the default one)
            var defaultValue = baseStrategy == ValueSerializationStrategy.SerializeAsStringColumn ? "\"\"" : "default";

            cb.AppendLine($"public {fieldTypeName}[] Column_{fieldName} {{ get; }}");
            cb.AppendLine($"public {fieldTypeName} Get{fieldName}(int index, {fieldTypeName} @default = {defaultValue}) => Column_{fieldName}.ElementAtOrDefault(index, @default);");
        }
        foreach (var f in relationFields)
        {
            var (_, localFieldName) = f.GetIndexColumnInfo();
            var relType = f.FieldType.RelationTypeParameter();
            cb.AppendLine($"public int[] Column_{localFieldName}Index {{ get; }}");
            cb.AppendLine($"public int Get{localFieldName}Index(int index) => Column_{localFieldName}Index.ElementAtOrDefault(index, EntityRelation.None);");
            cb.AppendLine($"public {relType.Name} Get{localFieldName}(int index) => _GetReferenced{localFieldName}(Get{localFieldName}Index(index));");
            cb.AppendLine($"private {relType.Name} _GetReferenced{localFieldName}(int referencedIndex) => ParentTableSet.Get{relType.Name}(referencedIndex);");
        }

        cb.AppendLine("// Object Getter");
        cb.AppendLine($"public {t.Name} Get(int index)");
        cb.AppendLine("{");
        cb.AppendLine("if (index < 0) return null;");
        cb.AppendLine($"var r = new {t.Name}();");
        cb.AppendLine("r.Index = index;");
        foreach (var f in entityFields)
        {
            cb.AppendLine($"r.{f.Name} = Get{f.Name}(index);");
        }
        foreach (var f in relationFields)
        {
            var (_, localFieldName) = f.GetIndexColumnInfo();
            var relType = f.FieldType.RelationTypeParameter();
            cb.AppendLine($"r.{f.Name} = new Relation<{relType}>(Get{f.Name.Substring(1)}Index(index), _GetReferenced{localFieldName});");
        }
        cb.AppendLine("return r;");
        cb.AppendLine("}");

        cb.AppendLine("// Enumerator");
        cb.AppendLine("IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();");
        cb.AppendLine($"public IEnumerator<{t.Name}> GetEnumerator()");
        cb.AppendLine("{");
        cb.AppendLine("for (var i = 0; i < RowCount; ++i)");
        cb.AppendLine("    yield return Get(i);");
        cb.AppendLine("}");

        cb.AppendLine($"}} // class {t.Name}Table ");
        cb.AppendLine();
    }

    private static void WriteDocumentBuilder(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public static class DocumentBuilderExtensions");
        cb.AppendLine("{");

        foreach (var et in entityTypes)
        {
            var entityType = et.Name;
            cb.AppendLine($"public static EntityTableBuilder To{entityType}TableBuilder(this EntitySetBuilder<{entityType}> entitySet)");
            cb.AppendLine("{");

            //cb.AppendLine($"var typedEntities = entities?.Cast<{entityType}>() ?? Enumerable.Empty<{entityType}>();");
            var tableName = et.GetEntityTableName();
            cb.AppendLine($"var tb = new EntityTableBuilder(\"{tableName}\");");
            cb.AppendLine("var entities = entitySet.Entities;");
            cb.AppendLine("var entityCount = entities.Count;");

            var entityFields = et.GetEntityFields().ToArray();
            var relationFields = et.GetRelationFields().ToArray();

            if ((entityFields.Length + relationFields.Length) == 0)
                throw new Exception($"Entity table {tableName} does not contain any fields.");

            foreach (var fieldInfo in entityFields)
            {
                var (strategy, _) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
                var functionName = strategy.GetEntityTableBuilderAddFunctionName(fieldInfo.FieldType);
                cb.AppendLine("{");
                cb.AppendLine($"var columnData = new {fieldInfo.FieldType.Name}[entityCount];");
                cb.AppendLine($"for (var i = 0; i < columnData.Length; ++i) {{ columnData[i] = entities[i].{fieldInfo.Name}; }}");
                cb.AppendLine($"tb.{functionName}(\"{fieldInfo.GetSerializedValueColumnName()}\", columnData);");
                cb.AppendLine("}");
            }

            foreach (var fieldInfo in relationFields)
            {
                var (indexColumnName, localFieldName) = fieldInfo.GetIndexColumnInfo();
                cb.AppendLine("{");
                cb.AppendLine("var columnData = new int[entityCount];");
                cb.AppendLine($"for (var i = 0; i < columnData.Length; ++i) {{ columnData[i] = entities[i]._{localFieldName}?.Index ?? EntityRelation.None; }}");
                cb.AppendLine($"tb.AddIndexColumn(\"{indexColumnName}\", columnData);");
                cb.AppendLine("}");
            }

            cb.AppendLine("return tb;");
            cb.AppendLine("}");
        }

        cb.AppendLine("} // DocumentBuilderExtensions");
        cb.AppendLine();

        cb.AppendLine("public partial class ObjectModelBuilder");
        cb.AppendLine("{");
        // NOTE: the following lines must not be made static since the ObjectModelBuilder is instantiated upon each new export.
        // Making this static will cause the contained EntitySetBuilders to accumulate data from previous exports during the lifetime of the program.

        // Instantiates a named entity table builder for each type.
        foreach (var et in entityTypes)
            cb.AppendLine($"public readonly EntitySetBuilder<{et.Name}> {et.Name}Builder = new EntitySetBuilder<{et.Name}>(\"{et.GetEntityTableName()}\");");

        cb.AppendLine();
        cb.AppendLine("public DocumentBuilder AddEntityTableSets(DocumentBuilder db)");
        cb.AppendLine("{");
        foreach (var et in entityTypes)
        {
            cb.AppendLine($"db.Tables.Add({et.Name}Builder.EntityTableName, {et.Name}Builder.To{et.Name}TableBuilder());");
        }
        cb.AppendLine();
        cb.AppendLine("return db;");
        cb.AppendLine("} // AddEntityTableSets");

        cb.AppendLine();
        cb.AppendLine("public void Clear()");
        cb.AppendLine("{");
        foreach (var et in entityTypes)
        {
            cb.AppendLine($"{et.Name}Builder.Clear();");
        }
        cb.AppendLine("} // Clear");

        cb.AppendLine("} // ObjectModelBuilder");
    }

    public static void WriteDocument(string file)
    {
        try
        {
            var cb = new CodeBuilder();

            cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
            cb.AppendLine("// ReSharper disable All");
            cb.AppendLine("using System;");
            cb.AppendLine("using System.Collections;");
            cb.AppendLine("using System.Collections.Generic;");
            cb.AppendLine("using System.Linq;");
            cb.AppendLine("using Vim.Math3d;");
            cb.AppendLine("using Vim.LinqArray;");
            cb.AppendLine("using Vim.Format.ObjectModel;");
            cb.AppendLine("using Vim.Util;");

            cb.AppendLine();

            cb.AppendLine("namespace Vim.Format.ObjectModel {");

            WriteDocument(cb);

            WriteEntityTableSet(cb);

            WriteDocumentBuilder(cb);

            cb.AppendLine("} // namespace");
            var content = cb.ToString();
            File.WriteAllText(file, content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
