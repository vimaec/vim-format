using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.DataFormat;
using Vim.DotNetUtilities;

namespace Vim.ObjectModel.CodeGen;

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
            ValueSerializationStrategy.SerializeAsCompositeDataColumns
                => $"{nameof(ColumnExtensions.GetCompositeDataColumnValues)}<{type.Name}>",
            _ => throw new Exception($"{nameof(GetEntityTableGetterFunctionName)} error - unknown strategy {strategy:G}")
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
            ValueSerializationStrategy.SerializeAsCompositeDataColumns
                => $"{nameof(ColumnExtensions.AddCompositeDataColumns)}",
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
            var (strategy, typePrefix) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
            var functionName = strategy.GetEntityTableGetterFunctionName(fieldInfo.FieldType);

            cb.AppendLine($"public IArray<{fieldInfo.FieldType.Name}> {t.Name}{fieldInfo.Name} {{ get; }}");
            constructor.ArraysInitializers
                       .Add($"{t.Name}{fieldInfo.Name} = {t.Name}EntityTable?.{functionName}(\"{typePrefix}{fieldInfo.Name}\") ?? Array.Empty<{fieldInfo.FieldType.Name}>().ToIArray();");

            // Safe accessor.
            var defaultValue = strategy == ValueSerializationStrategy.SerializeAsStringColumn ? "\"\"" : "default";
            cb.AppendLine($"public {fieldInfo.FieldType.Name} Get{t.Name}{fieldInfo.Name}(int index, {fieldInfo.FieldType.Name} defaultValue = {defaultValue}) => {t.Name}{fieldInfo.Name}?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;");
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
            cb.AppendLine($"public {fieldInfo.FieldType.RelationTypeParameter()} {fieldInfo.Name.Substring(1)} => {fieldInfo.Name}.Value;");
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

    private static CodeBuilder WriteDocument(CodeBuilder cb = null)
    {
        cb = cb ?? new CodeBuilder();

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
        return cb;
    }

    private static void WriteDocumentBuilder(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public static class DocumentBuilderExtensions");
        cb.AppendLine("{");

        cb.AppendLine("public static Func<IEnumerable<Entity>, EntityTableBuilder> GetTableBuilderFunc(this Type type)");
        cb.AppendLine("{");
        foreach (var et in entityTypes)
            cb.AppendLine($"if (type == typeof({et.Name})) return To{et.Name}TableBuilder;");
        cb.AppendLine("throw new ArgumentException(nameof(type));");
        cb.AppendLine("}");

        foreach (var et in entityTypes)
        {
            var entityType = et.Name;
            cb.AppendLine($"public static EntityTableBuilder To{entityType}TableBuilder(this IEnumerable<Entity> entities)");
            cb.AppendLine("{");

            cb.AppendLine($"var typedEntities = entities?.Cast<{entityType}>() ?? Enumerable.Empty<{entityType}>();");
            var tableName = et.GetEntityTableName();
            cb.AppendLine($"var tb = new EntityTableBuilder(\"{tableName}\");");

            var entityFields = et.GetEntityFields().ToArray();
            var relationFields = et.GetRelationFields().ToArray();

            if ((entityFields.Length + relationFields.Length) == 0)
                throw new Exception($"Entity table {tableName} does not contain any fields.");

            foreach (var fieldInfo in entityFields)
            {
                var (strategy, typePrefix) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
                var functionName = strategy.GetEntityTableBuilderAddFunctionName(fieldInfo.FieldType);
                var name = fieldInfo.Name;
                cb.AppendLine($"tb.{functionName}(\"{typePrefix}{name}\", typedEntities.Select(x => x.{name}));");
            }

            foreach (var fieldInfo in relationFields)
            {
                var (indexColumnName, localFieldName) = fieldInfo.GetIndexColumnInfo();
                cb.AppendLine($"tb.AddIndexColumn(\"{indexColumnName}\", typedEntities.Select(x => x._{localFieldName}.Index));");
            }

            cb.AppendLine("return tb;");
            cb.AppendLine("}");
        }

        cb.AppendLine("} // DocumentBuilderExtensions");
        cb.AppendLine();

        cb.AppendLine("public partial class ObjectModelBuilder");
        cb.AppendLine("{");
        // NOTE: the following line must not be made static since the ObjectModelBuilder is instantiated upon each new export.
        // Making this static will cause the contained EntityTableBuilders to accumulate data from previous exports during the lifetime of the program.
        cb.AppendLine("public readonly Dictionary<Type, EntityTableBuilder> EntityTableBuilders = new Dictionary<Type, EntityTableBuilder>()");
        cb.AppendLine("{");
        foreach (var et in entityTypes)
            cb.AppendLine($"{{typeof({et.Name}), new EntityTableBuilder()}},");
        cb.AppendLine("};");
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
            cb.AppendLine("using System.Collections.Generic;");
            cb.AppendLine("using System.Linq;");
            cb.AppendLine("using Vim.DataFormat;");
            cb.AppendLine("using Vim.Math3d;");
            cb.AppendLine("using Vim.LinqArray;");
            cb.AppendLine("using Vim.DotNetUtilities;");

            cb.AppendLine();

            cb.AppendLine("namespace Vim.ObjectModel {");

            WriteDocument(cb);

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
