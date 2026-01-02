using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.CodeGen;

public static class VimEntityCodeGen
{

    public const string EntityNamespace = "Vim.Format.ObjectModel";

    public static string GetVimEntityTableGetterFunctionName(this ValueSerializationStrategy strategy, Type type)
    {
        return strategy switch
        {
            ValueSerializationStrategy.SerializeAsStringColumn
                => nameof(api_v2.VimEntityTable.GetStringColumnValues),
            ValueSerializationStrategy.SerializeAsDataColumn
                => $"{nameof(api_v2.VimEntityTable.GetDataColumnValues)}<{type.Name}>",
            _ => throw new Exception($"{nameof(GetVimEntityTableGetterFunctionName)} error - unknown strategy {strategy:G}")
        };
    }

    private static string GetVimEntityTableBuilderAddFunctionName(this ValueSerializationStrategy strategy, Type typeName)
    {
        return strategy switch
        {
            ValueSerializationStrategy.SerializeAsStringColumn
                => nameof(api_v2.VimEntityTableBuilder.AddStringColumn),
            ValueSerializationStrategy.SerializeAsDataColumn
                => $"{nameof(api_v2.VimEntityTableBuilder.AddDataColumn)}",
            _ => throw new Exception($"{nameof(GetVimEntityTableBuilderAddFunctionName)} error - unknown strategy {strategy:G}")
        };
    }

    private static CodeBuilder WriteEntityClasses(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        foreach (var et in entityTypes)
            WriteEntityClass(et, cb);

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

    private static void WriteVimEntityTableSet(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public partial class VimEntityTableSet");
        cb.AppendLine("{");
        cb.AppendLine("public void Initialize(bool inParallel = true)");
        cb.AppendLine("{");
        foreach (var t in entityTypes)
        {
            cb.AppendLine($"Tables[VimEntityTableNames.{t.Name}] = {t.Name}Table = new {t.Name}Table(GetEntityTableDataOrEmpty(VimEntityTableNames.{t.Name}), StringTable, this);");
        }
        cb.AppendLine();
        cb.AppendLine("// Initialize element index maps");
        cb.AppendLine("ElementIndexMaps = new VimElementIndexMaps(this, inParallel);");
        cb.AppendLine();
        cb.AppendLine("} // end of Initialize");

        cb.AppendLine();
        foreach (var t in entityTypes)
        {
            cb.AppendLine($"public {t.Name}Table {t.Name}Table {{ get; private set; }} // can be null");
            cb.AppendLine($"public {EntityNamespace}.{t.Name} Get{t.Name}(int index) => {t.Name}Table?.Get(index);");
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
            cb.AppendLine(        $"VimEntityTableNames.{t.Name},");
        }
        cb.AppendLine("    };");

        var joiningTableTypes = entityTypes
            .Select(t => (t, t.HasJoiningTable()))
            .Where(tuple => tuple.Item2 != false)
            .ToArray();
        cb.AppendLine();
        cb.AppendLine("public static HashSet<string> GetJoiningTableNames()");
        cb.AppendLine("    => new HashSet<string>()");
        cb.AppendLine("    {");
        foreach (var (t, _) in joiningTableTypes)
        {
            cb.AppendLine(        $"VimEntityTableNames.{t.Name},");
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
            cb.AppendLine($"for (var i = 0; i < ({etPropertyName}?.RowCount ?? 0); ++i)");
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

        cb.AppendLine("} // end of partial class VimEntityTableSet");
        cb.AppendLine();

        foreach (var t in entityTypes)
            WriteVimEntityTable(cb, t);
    }

    private static void WriteVimEntityTable(CodeBuilder cb, Type t)
    {
        var entityFields = t.GetEntityFields().ToArray();
        var relationFields = t.GetRelationFields().ToArray();

        var elementKind = t.GetElementKind();

        cb.AppendLine($"public partial class {t.Name}Table : VimEntityTable, IEnumerable<{EntityNamespace}.{t.Name}>{(elementKind != ElementKind.Unknown ? ", IElementKindTable" : "")}");
        cb.AppendLine("{");
        cb.AppendLine();
        cb.AppendLine($"public const string TableName = VimEntityTableNames.{t.Name};");
        cb.AppendLine();
        cb.AppendLine("public VimEntityTableSet ParentTableSet { get; } // can be null");
        cb.AppendLine();
        cb.AppendLine($"public {t.Name}Table(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)");
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
                var functionName = eci.Strategy.GetVimEntityTableGetterFunctionName(eci.EntityColumnAttribute.SerializedType);
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
            cb.AppendLine($"public {EntityNamespace}.{relType.Name} Get{localFieldName}(int index) => _GetReferenced{localFieldName}(Get{localFieldName}Index(index));");
            cb.AppendLine($"private {EntityNamespace}.{relType.Name} _GetReferenced{localFieldName}(int referencedIndex) => ParentTableSet.Get{relType.Name}(referencedIndex);");
        }

        cb.AppendLine("// Object Getter");
        cb.AppendLine($"public {EntityNamespace}.{t.Name} Get(int index)");
        cb.AppendLine("{");
        cb.AppendLine("if (index < 0) return null;");
        cb.AppendLine($"var r = new {EntityNamespace}.{t.Name}();");
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
        cb.AppendLine($"public IEnumerator<{EntityNamespace}.{t.Name}> GetEnumerator()");
        cb.AppendLine("{");
        cb.AppendLine("for (var i = 0; i < RowCount; ++i)");
        cb.AppendLine("    yield return Get(i);");
        cb.AppendLine("}");

        cb.AppendLine($"}} // class {t.Name}Table ");
        cb.AppendLine();
    }

    private static void WriteVimBuilder(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public static class VimEntitySetBuilderExtensions");
        cb.AppendLine("{");

        foreach (var et in entityTypes)
        {
            var entityType = et.Name;
            cb.AppendLine($"public static VimEntityTableBuilder To{entityType}TableBuilder(this VimEntitySetBuilder<{EntityNamespace}.{entityType}> entitySetBuilder)");
            cb.AppendLine("{");

            //cb.AppendLine($"var typedEntities = entities?.Cast<{entityType}>() ?? Enumerable.Empty<{entityType}>();");
            var tableName = et.GetEntityTableName();
            cb.AppendLine($"var tb = new VimEntityTableBuilder(VimEntityTableNames.{et.Name});");
            cb.AppendLine("var entities = entitySetBuilder.Entities;");
            cb.AppendLine("var entityCount = entities.Count;");

            var entityFields = et.GetEntityFields().ToArray();
            var relationFields = et.GetRelationFields().ToArray();

            if ((entityFields.Length + relationFields.Length) == 0)
                throw new Exception($"Entity table {tableName} does not contain any fields.");

            foreach (var fieldInfo in entityFields)
            {
                var (strategy, _) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
                var functionName = strategy.GetVimEntityTableBuilderAddFunctionName(fieldInfo.FieldType);
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

        cb.AppendLine("} // VimEntitySetBuilderExtensions");
        cb.AppendLine();

        cb.AppendLine("public partial class VimBuilder");
        cb.AppendLine("{");
        // NOTE: the following lines must not be made static since the ObjectModelBuilder is instantiated upon each new export.
        // Making this static will cause the contained EntitySetBuilders to accumulate data from previous exports during the lifetime of the program.

        // Instantiates a named entity table builder for each type.
        foreach (var et in entityTypes)
            cb.AppendLine($"public readonly VimEntitySetBuilder<{EntityNamespace}.{et.Name}> {et.Name}Builder = new VimEntitySetBuilder<{EntityNamespace}.{et.Name}>(VimEntityTableNames.{et.Name});");

        cb.AppendLine();
        cb.AppendLine("public List<VimEntityTableBuilder> GetVimEntityTableBuilders()");
        cb.AppendLine("{");
        cb.AppendLine("var tableBuilders = new List<VimEntityTableBuilder>();");
        foreach (var et in entityTypes)
        {
            cb.AppendLine($"tableBuilders.Add({et.Name}Builder.To{et.Name}TableBuilder());");
        }
        cb.AppendLine();
        cb.AppendLine("return tableBuilders;");
        cb.AppendLine("} // GetVimEntityTableBuilders");

        cb.AppendLine();
        cb.AppendLine("public void Clear()");
        cb.AppendLine("{");
        foreach (var et in entityTypes)
        {
            cb.AppendLine($"{et.Name}Builder.Clear();");
        }
        cb.AppendLine("} // Clear");

        cb.AppendLine("} // VimBuilder");
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
            cb.AppendLine("using Vim.Format.ObjectModel;");
            cb.AppendLine("using Vim.Util;");

            cb.AppendLine();

            cb.AppendLine("namespace Vim.Format.api_v2");
            cb.AppendLine("{");

            // WriteEntityClasses(cb); // TODO: re-enable this once the other C# object model generator has been purged. 

            WriteVimEntityTableSet(cb);

            WriteVimBuilder(cb);

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
