using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.DotNetUtilities;
using Vim.Format.ObjectModel;

namespace Vim.Format.CodeGen;

public static class ObjectModelCppGenerator
{
    private static string ToLowerFirstLetter(string str) => string.Concat(str[..1].ToLower(), str.AsSpan(1));
    private static string ToUpperFirstLetter(string str) => string.Concat(str[..1].ToUpper(), str.AsSpan(1));
    private static string ToFieldName(string name) => 'm' + ToUpperFirstLetter(name);
    private static string ToArgumentName(string name)
    {
        string argument = ToLowerFirstLetter(name);

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
            "Vector2" or "DVector2" or
            "Vector3" or "DVector3" or
            "Vector4" or "DVector4" or
            "AABox" or "DAABox" or
            "AABox2D" or "DAABox2D" or
            "AABox4D" or "DAABox4D" or
            "Matrix4x4" => type,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} not supported")
        };

    private static string ColumnNameToVariableName(string column)
        => column.Substring(column.LastIndexOf(':') + 1).Replace(".", "");

    private static void WriteColumnCheck(CodeBuilder cb, (string collection, string column)[] columns, string returnValue)
    {
        if (columns.Length == 0)
            return;

        cb.AppendLine($"if (mEntityTable.{columns[0].collection}.find(\"{columns[0].column}\")" +
                      $" == mEntityTable.{columns[0].collection}.end(){(columns.Length == 1 ? ")" : " ||")}");

        for (int i = 1; i < columns.Length; i++)
        {
            cb.IndentOneLine($"mEntityTable.{columns[i].collection}.find(\"{columns[i].column}\")" +
                             $" == mEntityTable.{columns[i].collection}.end(){(i == columns.Length - 1 ? ")" : " ||")}");
        }

        cb.IndentOneLine($"return {returnValue};");
        cb.AppendLine();
    }

    private static void WriteColumnCheckFlags(CodeBuilder cb, (string collection, string column)[] columns)
    {
        if (columns.Length == 0)
            return;

        for (int i = 0; i < columns.Length; i++)
        {
            cb.AppendLine($"bool exists{ColumnNameToVariableName(columns[i].column)}" +
                          $" = mEntityTable.{columns[i].collection}.find(\"{columns[i].column}\")" +
                          $" == mEntityTable.{columns[i].collection}.end();");
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

    private static string CompositeTypePrefix(Type type) => type.Name.StartsWith("D") ? "double:" : "float:"; 
    
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
    
        WriteGetCount(cb, fields, relations);
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
        string name = ToUpperFirstLetter(entity.Name);
        string lowerName = ToLowerFirstLetter(name);

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
    
    private static void WriteGetCount(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations)
    {
        cb.AppendLine("int GetCount()");
        cb.AppendLine("{");

        string mapName = null;
        string columnName = null;
        string sizeType = "int";
    
        if (fields.Length > 0)
        {
            var first = fields[0];
            var (strategy, prefix) = first.FieldType.GetValueSerializationStrategyAndTypePrefix();
    
            if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
            {
                switch (prefix)
                {
                    case VimConstants.StringColumnNameTypePrefix:
                        mapName = "mStringColumns";
                        break;
                    case VimConstants.IndexColumnNameTypePrefix:
                        mapName = "mIndexColumns";
                        break;
                    case VimConstants.IntColumnNameTypePrefix or
                         VimConstants.ByteColumnNameTypePrefix or
                         VimConstants.FloatColumnNameTypePrefix or
                         VimConstants.DoubleColumnNameTypePrefix:
                        mapName = "mDataColumns";

                        sizeType = prefix switch
                        {
                            VimConstants.ByteColumnNameTypePrefix => "byte",
                            VimConstants.FloatColumnNameTypePrefix => "float",
                            VimConstants.DoubleColumnNameTypePrefix => "double",
                            _ => sizeType
                        };

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(prefix), prefix, $"Prefix {prefix} not expected");
                }

                columnName = $"\"{prefix}{first.Name}\"";
            }
            else
            {
                cb.AppendLine($"{ToCppType(first.FieldType.Name)}Converter converter;");

                mapName = "mDataColumns";
                columnName = $"\"{CompositeTypePrefix(first.FieldType)}{first.Name}\" + converter.GetColumns()[0]";
                sizeType = CompositeTypePrefix(first.FieldType).TrimEnd(':');
            }
        }
        else if (relations.Length > 0)
        {
            var first = relations[0].GetIndexColumnInfo();

            mapName = "mIndexColumns";
            columnName = $"\"{first.IndexColumnName}\"";
        }
        
        WriteNotFoundReturn(cb, "mEntityTable." + mapName, columnName, "0");
        cb.AppendLine($"return mEntityTable.{mapName}[{columnName}].size() / sizeof({ToCppType(sizeType)});");
    
        cb.AppendLine("}");
    }

    private static void WriteGetAll(CodeBuilder cb, Type entity, FieldInfo[] fields, FieldInfo[] relations)
    {
        string name = ToUpperFirstLetter(entity.Name);
        string lowerName = ToLowerFirstLetter(name);

        cb.AppendLine($"std::vector<{name}>* GetAll()");
        cb.AppendLine("{");

        WriteColumnCheckFlags(cb, GetColumnMap(fields, relations).ToArray());

        cb.AppendLine("const int count = GetCount();");
        cb.AppendLine();

        cb.AppendLine($"std::vector<{name}>* {lowerName} = new std::vector<{name}>();");
        cb.AppendLine($"{lowerName}->reserve(count);");
        cb.AppendLine();
        
        var arrays = WriteGetAllDataVars(cb, fields, relations, true);
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
                case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                    var converter = ToLowerFirstLetter(field.Name) + "Converter";
                    cb.AppendLine($"if ({GetCompositeSuffixes(field).Select(s => $"exists{field.Name}{s.Replace(".", "")}").Aggregate((s1, s2) => $"{s1} && {s2}")})");
                    cb.IndentOneLine($"{converter}.ConvertFromColumns(&entity.{fieldName}, {dataName}, i);");
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

    private static List<string> WriteGetAllDataVars(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations, bool checkExists)
    {
        var arrays = new List<string>();

        foreach (var field in fields)
        {
            var fieldName = ToLowerFirstLetter(field.Name);
            var (strategy, typePrefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                    cb.AppendLine($"const std::vector<int>& {fieldName}Data = " + (checkExists
                                   ? $"exists{field.Name} ? mEntityTable.mStringColumns[\"{typePrefix}{field.Name}\"] : std::vector<int>();"
                                   : $"mEntityTable.mStringColumns[\"{typePrefix}{field.Name}\"];"));
                    break;
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    var type = ToCppType(typePrefix.Replace(":", ""));
                    cb.AppendLine($"{type}* {fieldName}Data = new {type}[count];");

                    cb.AppendLine((checkExists ? $"if (exists{field.Name}) " : "") +
                                  $"memcpy({fieldName}Data, mEntityTable.mDataColumns[\"{typePrefix}{field.Name}\"].begin(), count * sizeof({type}));");

                    arrays.Add($"{fieldName}Data");
                    break;
                case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                    cb.AppendLine($"{ToCppType(field.FieldType.Name)}Converter {fieldName}Converter;");
                    cb.AppendLine($"ByteRangePtr* {fieldName}Data = new ByteRangePtr[{fieldName}Converter.GetSize()];");
                    cb.AppendLine((checkExists ? $"if ({GetCompositeSuffixes(field).Select(s => $"exists{field.Name}{s.Replace(".", "")}").Aggregate((s1, s2) => $"{s1} && {s2}")}) " : "") +
                                  $"for (int i = 0; i < {fieldName}Converter.GetSize(); i++)");
                    cb.IndentOneLine($"{fieldName}Data[i] = &mEntityTable.mDataColumns[\"{CompositeTypePrefix(field.FieldType)}{field.Name}\" + {fieldName}Converter.GetColumns()[i]];");

                    arrays.Add($"{fieldName}Data");
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Strategy {strategy} not expected");
            }

            cb.AppendLine();
        }

        foreach (var relation in relations)
        {
            cb.AppendLine($"const std::vector<int>& {ToLowerFirstLetter(relation.Name.Replace("_", ""))}Data = " +
                          (checkExists ?
                          $"exists{relation.Name.Replace("_", "")} ? mEntityTable.mIndexColumns[\"{relation.GetIndexColumnInfo().IndexColumnName}\"] : std::vector<int>();" :
                          $"mEntityTable.mIndexColumns[\"{relation.GetIndexColumnInfo().IndexColumnName}\"];"));
        }

        if (relations.Length > 0)
            cb.AppendLine();

        return arrays;
    }
    
    private static List<(string, string)> GetColumnMap(FieldInfo[] fields, FieldInfo[] relations)
    {
        var result = new List<(string, string)>();

        foreach (var field in fields)
        {
            var (strategy, typePrefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                    result.Add(("mStringColumns", $"{typePrefix}{field.Name}"));
                    break;
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    result.Add(("mDataColumns", $"{typePrefix}{field.Name}"));
                    break;
                case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                    GetCompositeTypeColumnsToCheck(field, ref result);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Strategy {strategy} not expected");
            }
        }

        foreach (var relation in relations)
        {
            result.Add(("mIndexColumns", relation.GetIndexColumnInfo().IndexColumnName));
        }

        return result;
    }

    private static string[] GetCompositeSuffixes(FieldInfo field)
    {
        var compositeTypesAssembly = new ColumnExtensions.Vector3CompositeDataColumn().GetType().Assembly;
        var composite = compositeTypesAssembly.GetType(
            $"Vim.Format.ColumnExtensions+{field.FieldType.Name}CompositeDataColumn", true, false);

        if (composite == null)
            throw new InvalidOperationException($"Type {field.FieldType.Name}CompositeDataColumn not found");

        var compositeInstance = Activator.CreateInstance(composite);

        var suffixes = (string[]) composite
                                 .GetProperty("Suffixes", BindingFlags.Public | BindingFlags.Instance)
                                ?.GetValue(compositeInstance);

        if (suffixes == null)
            throw new InvalidOperationException("Suffixes are null");

        return suffixes;
    }

    private static void GetCompositeTypeColumnsToCheck(FieldInfo field, ref List<(string, string)> columnsToCheck)
    {
        var suffixes = GetCompositeSuffixes(field);

        foreach (var suffix in suffixes)
        {
            columnsToCheck.Add(("mDataColumns", $"{CompositeTypePrefix(field.FieldType)}{field.Name}{suffix}"));
        }
    }

    private static void WriteGetFieldGetter(CodeBuilder cb, FieldInfo field, string lowerName)
    {
        var argumentName = ToArgumentName(field.Name);
        var type = ToCppType(field.FieldType.Name);

        cb.AppendLine($"{type} Get{ToUpperFirstLetter(field.Name)}(int {lowerName}Index)");
        cb.AppendLine("{");
        cb.AppendLine($"if ({lowerName}Index < 0 || {lowerName}Index >= GetCount())");
        cb.IndentOneLine("return {};");
        cb.AppendLine();

        var (strategy, prefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

        switch (strategy)
        {
            case ValueSerializationStrategy.SerializeAsStringColumn:
                WriteColumnCheck(cb, new[] { ("mStringColumns", $"{prefix}{field.Name}") }, "{}");
                cb.AppendLine($"return &mStrings[mEntityTable.mStringColumns[\"{prefix}{field.Name}\"][{lowerName}Index]];");
                break;
            case ValueSerializationStrategy.SerializeAsDataColumn:
                WriteColumnCheck(cb, new[] { ("mDataColumns", $"{prefix}{field.Name}") }, "{}");
                cb.AppendLine($"return *reinterpret_cast<{type}*>(const_cast<bfast::byte*>(" +
                              $"mEntityTable.mDataColumns[\"{prefix}{field.Name}\"].begin() + {lowerName}Index * sizeof({type})));");
                break;
            case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                var columnsToCheck = new List<(string, string)>();
                GetCompositeTypeColumnsToCheck(field, ref columnsToCheck);
                WriteColumnCheck(cb, columnsToCheck.ToArray(), "{}");

                cb.AppendLine($"{type}Converter {argumentName}Converter;");
                cb.AppendLine($"bfast::byte* bytes = new bfast::byte[{argumentName}Converter.GetSize() * {argumentName}Converter.GetBytes()];");
                
                cb.AppendLine($"for (int i = 0; i < {argumentName}Converter.GetSize(); ++i)");
                cb.AppendLine("{");
                cb.AppendLine($"memcpy(bytes + i * {argumentName}Converter.GetBytes(),");
                cb.IndentOneLine($"mEntityTable.mDataColumns[\"{CompositeTypePrefix(field.FieldType)}{field.Name}\"" +
                                 $" + {argumentName}Converter.GetColumns()[i]].begin()");
                cb.IndentOneLine($" + {lowerName}Index * {argumentName}Converter.GetBytes(),");
                cb.IndentOneLine($"{argumentName}Converter.GetBytes());");
                cb.AppendLine("}");

                cb.AppendLine($"{type} {argumentName} = {argumentName}Converter.ConvertFromArray(bytes);");
                cb.AppendLine("delete[] bytes;");
                cb.AppendLine($"return {argumentName};");
                break;
            default:
                throw new ArgumentOutOfRangeException($"Strategy {strategy} not expected");
        }

        cb.AppendLine("}");
    }

    private static void WriteGetAllFieldGetter(CodeBuilder cb, FieldInfo field)
    {
        var cppType = ToCppType(field.FieldType.Name);

        cb.AppendLine($"const std::vector<{ToCppType(field.FieldType.Name)}>* GetAll{field.Name}()");
        cb.AppendLine("{");

        WriteColumnCheck(cb, GetColumnMap(new[] { field }, Array.Empty<FieldInfo>()).ToArray(), "{}");

        cb.AppendLine("const int count = GetCount();");
        
        var arrays = WriteGetAllDataVars(cb, new[] { field }, Array.Empty<FieldInfo>(), false);
        
        var (strategy, _) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();
        var lowerName = ToLowerFirstLetter(field.Name);

        switch (strategy)
        {
            case ValueSerializationStrategy.SerializeAsDataColumn:
                cb.AppendLine($"std::vector<{cppType}>* result = new std::vector<{cppType}>({arrays[0]}, {arrays[0]} + count);");
                cb.AppendLine();

                cb.AppendLine($"delete[] {arrays[0]};");
                break;
            case ValueSerializationStrategy.SerializeAsStringColumn:
                cb.AppendLine("std::vector<const std::string*>* result = new std::vector<const std::string*>();");
                cb.AppendLine("result->reserve(count);");
                cb.AppendLine();

                cb.AppendLine("for (int i = 0; i < count; ++i)");
                cb.AppendLine("{");
                cb.AppendLine($"result->push_back(&mStrings[{lowerName}Data[i]]);");
                cb.AppendLine("}");
                break;
            case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                cb.AppendLine($"std::vector<{ToCppType(field.FieldType.Name)}>* result = new std::vector<{ToCppType(field.FieldType.Name)}>();");
                cb.AppendLine("result->reserve(count);");
                cb.AppendLine();

                cb.AppendLine("for (int i = 0; i < count; ++i)");
                cb.AppendLine("{");
                cb.AppendLine($"{ToCppType(field.FieldType.Name)} value;");
                cb.AppendLine($"{lowerName}Converter.ConvertFromColumns(&value, {arrays[0]}, i);");
                cb.AppendLine("result->push_back(value);");
                cb.AppendLine("}");
                cb.AppendLine();

                cb.AppendLine($"delete[] {arrays[0]};");
                break;
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
            WriteColumnCheck(cb, new[] { ("mIndexColumns", relation.GetIndexColumnInfo().IndexColumnName) }, "-1");
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