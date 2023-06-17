using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.Format.ObjectModel;
using Vim.Format.Utils;

namespace Vim.Format.CodeGen;

public static class ObjectModelCppGenerator
{
    private static string ToLowerFirstLetter(string str) => string.Concat(str[..1].ToLower(), str.AsSpan(1));
    private static string ToUpperFirstLetter(string str) => string.Concat(str[..1].ToUpper(), str.AsSpan(1));
    private static string ToFieldName(string name) => 'm' + ToUpperFirstLetter(name);
    private static string ToArgumentName(string name)
    {
        var argument = ToLowerFirstLetter(name);

        return argument switch
        {
            "new" => "_new",
            _ => argument
        };
    }

    private static string ToCppType(string type) =>
        type switch
        {
            "Boolean" or "bool" => "bool",
            "Byte" or "byte" => "bfast::byte",
            "Single" or "float" => "float",
            "Double" or "double" => "double",
            "String" or "string" => "const std::string*",
            "Int32" or "int" => "int",
            "Int64" or "Long" or "long" => "long long",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} not supported")
        };

    private static void WriteColumnCheck(CodeBuilder cb, string columnName, string conditionalCode, bool negate)
    {
        cb.AppendLine($"if ({(negate ? "!" : "")}mEntityTable.column_exists(\"{columnName}\")) {{");
        cb.AppendLine(conditionalCode);
        cb.AppendLine("}");
        cb.AppendLine();
    }

    private static void WriteColumnCheckFlags(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations)
    {
        foreach (var fieldInfo in fields)
        {
            var loadingInfos = fieldInfo.GetEntityColumnLoadingInfo();
            var exists = string.Join(" || ", loadingInfos.Select(li => $"mEntityTable.column_exists(\"{li.SerializedValueColumnName}\")"));
            cb.AppendLine($"bool exists{fieldInfo.Name} = {exists};");
        }

        foreach (var fieldInfo in relations)
        {
            cb.AppendLine($"bool exists{fieldInfo.Name.Trim('_')} = mEntityTable.column_exists(\"{fieldInfo.GetSerializedIndexColumnName()}\");");
        }

        cb.AppendLine();
    }

    private static void WriteEntityClass(CodeBuilder cb, Type entity)
    {
        var className = ToUpperFirstLetter(entity.Name);
        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"class {className}");
        cb.AppendLine("{");
        cb.UnindentOneLine("public:");
        cb.AppendLine($"int {ToFieldName("Index")};");

        foreach (var field in fields)
        {
            cb.AppendLine($"{ToCppType(field.FieldType.Name)} {ToFieldName(field.Name)};");
        }

        if (relations.Length > 0)
            cb.AppendLine();

        foreach (var relation in relations)
        {
            var name = ToFieldName(relation.Name.Replace("_", ""));

            cb.AppendLine($"int {name}Index;");
            cb.AppendLine($"{relation.FieldType.RelationTypeParameter().Name}* {name};");
        }

        cb.AppendLine();

        cb.AppendLine($"{className}() {{}}");
        cb.AppendLine("};");
    }

    private static void WriteEntityTableClass(CodeBuilder cb, Type entity)
    {
        var name = ToUpperFirstLetter(entity.Name);
        var lowerName = ToLowerFirstLetter(entity.Name);
        var className = $"{name}Table";
    
        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"class {className}");
        cb.AppendLine("{");
    
        cb.AppendLine("EntityTable& mEntityTable;");
        cb.AppendLine("std::vector<std::string>& mStrings;");

        cb.UnindentOneLine("public:");

        cb.AppendLine($"{className}(EntityTable& entityTable, std::vector<std::string>& strings):");
        cb.IndentOneLine("mEntityTable(entityTable), mStrings(strings) {}");
        cb.AppendLine();
    
        WriteGetCount(cb);
        cb.AppendLine();

        WriteGet(cb, entity, fields, relations);
        cb.AppendLine();
    
        WriteGetAll(cb, entity, fields, relations);
        
        if (fields.Length > 0 || relations.Length > 0)
            cb.AppendLine();
        
        foreach (var field in fields)
        {
            WriteGetFieldGetter(cb, field, lowerName);
            cb.AppendLine();
            
            WriteGetAllFieldGetter(cb, field);
            cb.AppendLine();
        }
        
        WriteRelationGetters(cb, relations, lowerName);
    
        cb.AppendLine("};");
    }

    private static void WriteGet(CodeBuilder cb, Type entity, FieldInfo[] fields, FieldInfo[] relations)
    {
        var name = ToUpperFirstLetter(entity.Name);
        var lowerName = ToLowerFirstLetter(name);

        cb.AppendLine($"{name}* Get(int {lowerName}Index)");
        cb.AppendLine("{");
        cb.AppendLine($"{name}* {lowerName} = new {name}();");
        cb.AppendLine($"{lowerName}->{ToFieldName("Index")} = {lowerName}Index;");

        foreach (var field in fields)
        {
            cb.AppendLine($"{lowerName}->{ToFieldName(field.Name)} = Get{field.Name}({lowerName}Index);");
        }

        foreach (var relation in relations)
        {
            var relName = ToUpperFirstLetter(relation.Name.Replace("_", ""));
            cb.AppendLine($"{lowerName}->{ToFieldName(relName)}Index = Get{relName}Index({lowerName}Index);");
        }

        cb.AppendLine($"return {lowerName};");
        cb.AppendLine("}");
    }

    private static void WriteNotFoundReturn(CodeBuilder cb, string map, string value, string returnValue)
    {
        cb.AppendLine($"if ({map}.find({value}) == {map}.end())");
        cb.IndentOneLine($"return {returnValue};");
        cb.AppendLine();
    }
    
    private static void WriteGetEntityTable(CodeBuilder cb, Type entity)
    {
        cb.AppendLine($"static {entity.Name}Table* Get{entity.Name}Table(VimScene& scene)");
        cb.AppendLine("{");
        WriteNotFoundReturn(cb, "scene.mEntityTables", '"' + entity.GetEntityTableName() + '"', "{}");
        cb.AppendLine($"return new {entity.Name}Table(scene.mEntityTables[\"{entity.GetEntityTableName()}\"], scene.mStrings);");
        cb.AppendLine("}");
    }

    private static void WriteGetCount(CodeBuilder cb)
    {
        cb.AppendLine("size_t GetCount()");
        cb.AppendLine("{");
        cb.AppendLine("return mEntityTable.get_count();");
        cb.AppendLine("}");
    }

    private static void WriteGetAll(CodeBuilder cb, Type entity, FieldInfo[] fields, FieldInfo[] relations)
    {
        var name = ToUpperFirstLetter(entity.Name);
        var lowerName = ToLowerFirstLetter(name);

        cb.AppendLine($"std::vector<{name}>* GetAll()");
        cb.AppendLine("{");

        WriteColumnCheckFlags(cb, fields, relations);

        cb.AppendLine("const auto count = GetCount();");
        cb.AppendLine();

        cb.AppendLine($"std::vector<{name}>* {lowerName} = new std::vector<{name}>();");
        cb.AppendLine($"{lowerName}->reserve(count);");
        cb.AppendLine();
        
        var arrays = WriteGetAllDataVars(cb, fields, relations);
        WriteGetAllEntities(cb, fields, relations, name);

        cb.AppendLine($"{lowerName}->push_back(entity);");
        cb.AppendLine("}");
        cb.AppendLine();

        foreach (var array in arrays)
        {
            cb.AppendLine($"delete[] {array};");
        }

        if (arrays.Count > 0)
            cb.AppendLine();

        cb.AppendLine($"return {lowerName};");
        cb.AppendLine("}");
    }

    private static void WriteGetAllEntities(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations, string name)
    {
        cb.AppendLine("for (int i = 0; i < count; ++i)");
        cb.AppendLine("{");
        cb.AppendLine($"{name} entity;");
        cb.AppendLine("entity.mIndex = i;");

        foreach (var field in fields)
        {
            var fieldName = ToFieldName(field.Name);
            var dataName = ToLowerFirstLetter(field.Name) + "Data";
            var (strategy, _) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                    cb.AppendLine($"if (exists{field.Name})");
                    cb.IndentOneLine($"entity.{fieldName} = &mStrings[{dataName}[i]];");
                    break;
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    cb.AppendLine($"if (exists{field.Name})");
                    cb.IndentOneLine($"entity.{fieldName} = {dataName}[i];");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Strategy {strategy} not expected");
            }
        }

        foreach (var relation in relations)
        {
            var relName = relation.Name.Replace("_", "");
            cb.AppendLine($"entity.{ToFieldName(relation.Name.Replace("_", ""))}Index" +
                          $" = exists{relName} ? {ToLowerFirstLetter(relName)}Data[i] : -1;");
        }
    }

    private static List<string> WriteGetAllDataVars(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations)
    {
        var arrays = new List<string>();

        foreach (var field in fields)
        {
            var fieldName = ToLowerFirstLetter(field.Name);
            var loadingInfos = field.GetEntityColumnLoadingInfo();

            //var (strategy, typePrefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();
            var baseLoadingInfo = loadingInfos[0];
            var strategy = baseLoadingInfo.Strategy;
            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                    cb.AppendLine($"const std::vector<int>& {fieldName}Data = mEntityTable.column_exists(\"{baseLoadingInfo.SerializedValueColumnName}\") ? mEntityTable.mStringColumns[\"{baseLoadingInfo.SerializedValueColumnName}\"] : std::vector<int>();");
                    break;
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    var cppType = ToCppType(baseLoadingInfo.TypePrefix.Replace(":", ""));
                    cb.AppendLine($"{cppType}* {fieldName}Data = new {cppType}[count];");

                    for (var i = 0; i < loadingInfos.Length; ++i)
                    {
                        var li = loadingInfos[i];
                        cb.AppendLine($"{(i > 0 ? "else " : "")}if (mEntityTable.column_exists(\"{li.SerializedValueColumnName}\")) {{");

                        if (i == 0)
                        {
                            // memcpy
                            cb.AppendLine($"memcpy({fieldName}Data, mEntityTable.mDataColumns[\"{li.SerializedValueColumnName}\"].begin(), count * sizeof({cppType}));");
                        }
                        else
                        {
                            // Cast to the target C++ type.
                            var thisCppType = ToCppType(li.TypePrefix.Replace(":", ""));
                            cb.AppendLine("for (int i = 0; i < count; ++i) {");
                            cb.AppendLine($"{fieldName}Data[i] = {GetDataAsStaticCast(cppType, li.SerializedValueColumnName, "i", thisCppType)};");
                            cb.AppendLine("}");
                        }

                        cb.AppendLine("}");
                    }

                    arrays.Add($"{fieldName}Data");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Strategy {strategy} not expected");
            }

            cb.AppendLine();
        }

        foreach (var relation in relations)
        {
            var indexColumnName = relation.GetIndexColumnInfo().IndexColumnName;
            cb.AppendLine($"const std::vector<int>& {ToLowerFirstLetter(relation.Name.Replace("_", ""))}Data = mEntityTable.column_exists(\"{indexColumnName}\") ? mEntityTable.mIndexColumns[\"{indexColumnName}\"] : std::vector<int>();");
        }

        if (relations.Length > 0)
            cb.AppendLine();

        return arrays;
    }

    private static string GetDataAsStaticCast(string cppType, string serializedColumnName, string offset,
        string serializedCppType)
    {
        var getBytes = $"const_cast<bfast::byte*>(mEntityTable.mDataColumns[\"{serializedColumnName}\"].begin() + {offset} * sizeof({serializedCppType}))";
        return cppType == serializedCppType
            ? $"*reinterpret_cast<{cppType}*>({getBytes})"
            : $"static_cast<{cppType}>(*reinterpret_cast<{serializedCppType}*>({getBytes}))";
    }

    private static void WriteGetFieldGetter(CodeBuilder cb, FieldInfo field, string lowerName)
    {
        var argumentName = ToArgumentName(field.Name);
        var cppType = ToCppType(field.FieldType.Name);

        cb.AppendLine($"{cppType} Get{ToUpperFirstLetter(field.Name)}(int {lowerName}Index)");
        cb.AppendLine("{");
        cb.AppendLine($"if ({lowerName}Index < 0 || {lowerName}Index >= GetCount())");
        cb.IndentOneLine("return {};");
        cb.AppendLine();

        var loadingInfos = field.GetEntityColumnLoadingInfo();
        var baseLoadingInfo = loadingInfos[0];

        switch (baseLoadingInfo.Strategy)
        {
            case ValueSerializationStrategy.SerializeAsStringColumn:
                WriteColumnCheck(
                    cb,
                    baseLoadingInfo.SerializedValueColumnName,
                    $"return &mStrings[mEntityTable.mStringColumns[\"{baseLoadingInfo.SerializedValueColumnName}\"][{lowerName}Index]];",
                    false);
                cb.AppendLine("return {};");
                break;
            case ValueSerializationStrategy.SerializeAsDataColumn:
                foreach (var li in loadingInfos)
                {
                    var thisCppType = ToCppType(li.TypePrefix.Replace(":", ""));
                    WriteColumnCheck(
                        cb,
                        li.SerializedValueColumnName,
                        $"return {GetDataAsStaticCast(cppType, li.SerializedValueColumnName, $"{lowerName}Index", thisCppType)};",
                        false);
                }

                cb.AppendLine("return {};");
                break;
            default:
                throw new ArgumentOutOfRangeException($"Strategy {baseLoadingInfo.Strategy} not expected");
        }

        cb.AppendLine("}");
    }

    private static void WriteGetAllFieldGetter(CodeBuilder cb, FieldInfo field)
    {
        var cppType = ToCppType(field.FieldType.Name);

        cb.AppendLine($"std::vector<{cppType}>* GetAll{field.Name}()");
        cb.AppendLine("{");

        cb.AppendLine("const auto count = GetCount();");
        cb.AppendLine();

        var (strategy, _) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();
        var lowerName = ToLowerFirstLetter(field.Name);
        var arrays = WriteGetAllDataVars(cb, new[] { field }, Array.Empty<FieldInfo>());
        switch (strategy)
        {
            case ValueSerializationStrategy.SerializeAsDataColumn:
                {
                    cb.AppendLine($"std::vector<{cppType}>* result = new std::vector<{cppType}>({arrays[0]}, {arrays[0]} + count);");
                    cb.AppendLine();
                    cb.AppendLine($"delete[] {arrays[0]};");
                    break;
                }

            case ValueSerializationStrategy.SerializeAsStringColumn:
                {
                    cb.AppendLine("std::vector<const std::string*>* result = new std::vector<const std::string*>();");
                    cb.AppendLine("result->reserve(count);");
                    cb.AppendLine();
                    cb.AppendLine("for (int i = 0; i < count; ++i)");
                    cb.AppendLine("{");
                    cb.AppendLine($"result->push_back(&mStrings[{lowerName}Data[i]]);");
                    cb.AppendLine("}");
                    break;
                }
        }

        cb.AppendLine();
        cb.AppendLine("return result;");
        cb.AppendLine("}");
    }

    private static void WriteRelationGetters(CodeBuilder cb, FieldInfo[] relations, string lowerName)
    {
        foreach (var relation in relations)
        {
            var relationName = relation.Name.Replace("_", "");

            cb.AppendLine($"int Get{relationName}Index(int {lowerName}Index)");
            cb.AppendLine("{");
            WriteColumnCheck(cb, relation.GetIndexColumnInfo().IndexColumnName, "return -1;", true);
            cb.AppendLine($"if ({lowerName}Index < 0 || {lowerName}Index >= GetCount())");
            cb.IndentOneLine("return -1;");
            cb.AppendLine();
            cb.AppendLine($"return mEntityTable.mIndexColumns[\"{relation.GetIndexColumnInfo().IndexColumnName}\"]" +
                          $"[{lowerName}Index];");
            cb.AppendLine("}");
            cb.AppendLine();
        }
    }
    
    private static void WriteDocumentModel(CodeBuilder cb, Type[] entityTypes)
    {
        cb.AppendLine("class DocumentModel");
        cb.AppendLine("{");
        cb.UnindentOneLine("public:");
    
        foreach (var type in entityTypes)
        {
            cb.AppendLine($"{type.Name}Table* {ToFieldName(type.Name)};");
        }
    
        cb.AppendLine();
        
        cb.AppendLine("DocumentModel(VimScene& scene);");
        cb.AppendLine("~DocumentModel();");

        cb.AppendLine("};");
    }

    private static void WriteDocumentModelImplementation(CodeBuilder cb, Type[] entityTypes)
    {
        cb.AppendLine("DocumentModel::DocumentModel(VimScene& scene)");
        cb.AppendLine("{");

        foreach (var type in entityTypes)
        {
            cb.AppendLine($"{ToFieldName(type.Name)} = Get{type.Name}Table(scene);");
        }

        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine("DocumentModel::~DocumentModel()");
        cb.AppendLine("{");

        foreach (var type in entityTypes)
        {
            cb.AppendLine($"delete {ToFieldName(type.Name)};");
        }

        cb.AppendLine("}");
    }

    private static void WriteDocument(CodeBuilder file)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        var code = File.ReadAllLines("HandWrittenCpp.cpp");

        foreach (var line in code)
        {
            file.AppendRaw(line);
        }

        file.AppendLine();
        
        foreach (var entity in entityTypes)
        {
            file.AppendLine($"class {entity.Name};");
            file.AppendLine($"class {entity.Name}Table;");
        }

        file.AppendLine();

        WriteDocumentModel(file, entityTypes);
        file.AppendLine();

        foreach (var entity in entityTypes)
        {
            WriteEntityClass(file, entity);
            file.AppendLine();

            WriteEntityTableClass(file, entity);
            file.AppendLine();
            
            WriteGetEntityTable(file, entity);
            file.AppendLine();
        }

        WriteDocumentModelImplementation(file, entityTypes);
    }

    public static void WriteDocument(string file)
    {
        try
        {
            var cb = new CodeBuilder();

            cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
            cb.AppendLine();
            cb.AppendLine("#ifndef __OBJECT_MODEL_H__");
            cb.AppendLine("#define __OBJECT_MODEL_H__");
            cb.AppendLine();
            cb.AppendLine("#include <string>");
            cb.AppendLine("#include <vector>");
            cb.AppendLine("#include \"bfast.h\"");
            cb.AppendLine("#include \"vim.h\"");

            cb.AppendLine();
            cb.AppendLine("namespace Vim");
            cb.AppendLine("{");

            WriteDocument(cb);

            cb.AppendLine("}");
            cb.AppendLine();
            cb.AppendLine("#endif");

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
