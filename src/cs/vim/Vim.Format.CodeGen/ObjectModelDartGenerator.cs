using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.DotNetUtilities;

namespace Vim.ObjectModel.CodeGen;

public static class ObjectModelDartGenerator
{
    private static string ToLowerFirstLetter(string str) => string.Concat(str[..1].ToLower(), str.AsSpan(1));

    private static string ToDartType(string type) =>
        type switch
        {
            "Boolean" or "bool" => "bool",
            "Single" or "float" or "Double" or "double" => "double",
            "String" or "string" => "String",
            "Int32" or "int" => "int",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} not supported")
        };

    private static void WriteFlattenedStructRecursive(string prefix, FieldInfo field, ref List<(string, string)> fields)
    {
        var newPrefix = $"{prefix}{field.Name}";

        var subfields = field.FieldType.GetFields(BindingFlags.Instance | BindingFlags.Public);

        if (subfields.Length == 0)
        {
            fields.Add((ToDartType(field.FieldType.Name), ToLowerFirstLetter(newPrefix)));
            return;
        }

        foreach (var subfield in subfields)
        {
            WriteFlattenedStructRecursive($"{newPrefix}", subfield, ref fields);
        }
    }
    
    private static void WriteClasses(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        foreach (var entity in entityTypes)
        {
            cb.AppendLine($"class {entity.Name} {{");

            var fields = entity.GetEntityFields().ToArray();
            var fieldsCode = new List<(string, string)>();

            foreach (var field in fields)
            {
                WriteFlattenedStructRecursive("", field, ref fieldsCode);
            }

            fieldsCode.AddRange(entity.GetRelationFields()
                                      .Select(relation => ("int", ToLowerFirstLetter(relation.Name.Trim('_')) + "Index")));

            foreach (var (type, field) in fieldsCode)
            {
                cb.AppendLine($"final {type} {field};");
            }

            cb.AppendLine();

            cb.AppendLine($"const {entity.Name}(");
            cb.Indent();

            foreach (var (_, name) in fieldsCode)
            {
                cb.AppendLine($"this.{name},");
            }

            cb.Unindent();
            cb.AppendLine(");");

            cb.AppendLine("}");
            cb.AppendLine();
        }
    }
    
    private static void WriteDocument(CodeBuilder file)
    {
        WriteClasses(file);
    }

    public static void WriteDocument(string file)
    {
        try
        {
            var cb = new CodeBuilder();

            cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
            cb.AppendLine("import 'dart:core';");
            cb.AppendLine("import 'dart:typed_data';");

            cb.AppendLine();

            WriteDocument(cb);

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