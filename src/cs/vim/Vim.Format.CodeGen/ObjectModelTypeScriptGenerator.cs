using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.DotNetUtilities;
using Vim.Format.ObjectModel;

namespace Vim.Format.CodeGen;

public static class ObjectModelTypeScriptGenerator
{
    private static string ToLowerFirstLetter(string str) => string.Concat(str[..1].ToLower(), str.AsSpan(1));

    private static string SanitizeFieldName(string fieldName)
        => fieldName == "new" ? "_new" : fieldName;

    private static string ToTypeScriptType(string type) =>
        type switch
        {
            "Boolean" or "bool" => "boolean",
            "Single" or "float" or "Double" or "double" or "Int32" or "int" => "number",
            "String" or "string" => "string",
            "Vector2" or "DVector2" => "Vector2",
            "Vector3" or "DVector3" => "Vector3",
            "Vector4" or "DVector4" => "Vector4",
            "AABox" or "DAABox" => "AABox",
            "AABox2D" or "DAABox2D" => "AABox2D",
            "AABox4D" or "DAABox4D" => "AABox4D",
            "Matrix4x4" => type,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, $"Type {type} not supported")
        };

    private static void WriteEntityInterface(CodeBuilder cb, Type entity)
    {
        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"export interface I{entity.Name} {{");
        cb.AppendLine("index: number");

        foreach (var field in fields)
        {
            cb.AppendLine($"{SanitizeFieldName(ToLowerFirstLetter(field.Name))}?: {ToTypeScriptType(field.FieldType.Name)}");
        }

        if (relations.Length > 0)
            cb.AppendLine();

        foreach (var relation in relations)
        {
            var name = ToLowerFirstLetter(relation.Name.Replace("_", ""));

            cb.AppendLine($"{name}Index?: number");
            cb.AppendLine($"{name}?: I{relation.FieldType.RelationTypeParameter().Name}");
        }

        cb.AppendLine("}");
    }

    private static void WriteEntityTableInterface(CodeBuilder cb, Type entity)
    {
        var name = entity.Name;
        var lowerName = ToLowerFirstLetter(name);

        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"export interface I{name}Table {{");
        cb.AppendLine("getCount(): Promise<number>");
        cb.AppendLine($"get({lowerName}Index: number): Promise<I{name}>");
        cb.AppendLine($"getAll(): Promise<I{name}[]>");
        
        if (fields.Length > 0 || relations.Length > 0)
            cb.AppendLine();

        foreach (var field in fields)
        {
            cb.AppendLine($"get{field.Name}({lowerName}Index: number): Promise<{ToTypeScriptType(field.FieldType.Name)} | undefined>");
            cb.AppendLine($"getAll{field.Name}(): Promise<{ToTypeScriptType(field.FieldType.Name)}[] | undefined>");
        }

        if (fields.Length > 0 && relations.Length > 0)
            cb.AppendLine();

        foreach (var relation in relations)
        {
            var relationName = relation.Name.Replace("_", "");

            cb.AppendLine($"get{relationName}Index({lowerName}Index: number): Promise<number | undefined>");
            cb.AppendLine($"getAll{relationName}Index(): Promise<number[] | undefined>");
            cb.AppendLine($"get{relationName}({lowerName}Index: number): Promise<I{relation.FieldType.RelationTypeParameter().Name} | undefined>");
        }

        cb.AppendLine("}");
    }

    private static void WriteEntityClass(CodeBuilder cb, Type entity)
    {
        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"export class {entity.Name} implements I{entity.Name} {{");
        cb.AppendLine("index: number");

        foreach (var field in fields)
        {
            cb.AppendLine($"{SanitizeFieldName(ToLowerFirstLetter(field.Name))}?: {ToTypeScriptType(field.FieldType.Name)}");
        }

        if (relations.Length > 0)
            cb.AppendLine();

        foreach (var relation in relations)
        {
            var name = ToLowerFirstLetter(relation.Name.Replace("_", ""));

            cb.AppendLine($"{name}Index?: number");
            cb.AppendLine($"{name}?: I{relation.FieldType.RelationTypeParameter().Name}");
        }

        cb.AppendLine();

        cb.AppendLine($"static async createFromTable(table: I{entity.Name}Table, index: number): Promise<I{entity.Name}> {{");

        cb.AppendLine($"let result = new {entity.Name}()");
        cb.AppendLine("result.index = index");
        cb.AppendLine();

        cb.AppendLine("await Promise.all([");
        cb.Indent();

        foreach (var field in fields)
        {
            cb.AppendLine($"table.get{field.Name}(index).then(v => result.{SanitizeFieldName(ToLowerFirstLetter(field.Name))} = v),");
        }

        foreach (var relation in relations)
        {
            var name = relation.Name.Replace("_", "");
            cb.AppendLine($"table.get{name}Index(index).then(v => result.{ToLowerFirstLetter(name)}Index = v),");
        }
        
        cb.Unindent();
        cb.AppendLine("])");
        cb.AppendLine();

        cb.AppendLine("return result");

        cb.AppendLine("}");
        cb.AppendLine("}");
    }

    private static string CompositeTypePrefix(Type type) => type.Name.StartsWith("D") ? "double:" : "float:"; 

    private static void WriteEntityTableClass(CodeBuilder cb, Type entity)
    {
        var name = entity.Name;
        var lowerName = ToLowerFirstLetter(name);

        var fields = entity.GetEntityFields().ToArray();
        var relations = entity.GetRelationFields().ToArray();
        
        cb.AppendLine($"export class {name}Table implements I{name}Table {{");

        if (relations.Length > 0)
            cb.AppendLine("private document: VimDocument");

        cb.AppendLine("private entityTable: EntityTable");
        cb.AppendLine();

        WriteCreateFromDocument(cb, entity, name, relations);
        cb.AppendLine();

        WriteGetCountGetter(cb, fields, relations);
        cb.AppendLine();

        cb.AppendLine($"async get({lowerName}Index: number): Promise<I{name}> {{");
        cb.AppendLine($"return await {name}.createFromTable(this, {lowerName}Index)");
        cb.AppendLine("}");
        cb.AppendLine();

        WriteGetAllGetter(cb, fields, relations, name, lowerName);

        if (fields.Length > 0 || relations.Length > 0)
            cb.AppendLine();
        
        foreach (var field in fields)
        {
            var strategy = WriteGetFieldGetter(cb, field, lowerName, out var prefix);
            cb.AppendLine();
            
            WriteGetAllFieldGetter(cb, field, strategy, prefix);
            cb.AppendLine();
        }

        WriteRelationGetters(cb, relations, lowerName);

        cb.AppendLine("}");
    }

    private static void WriteCreateFromDocument(CodeBuilder cb, Type entity, string name, FieldInfo[] relations)
    {
        cb.AppendLine($"static async createFromDocument(document: VimDocument): Promise<I{name}Table | undefined> {{");
        cb.AppendLine($"const entity = await document.entities.getBfast(\"{entity.GetEntityTableName()}\")");
        cb.AppendLine();

        cb.AppendLine("if (!entity) {");
        cb.AppendLine("return undefined");
        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine($"let table = new {name}Table()");

        if (relations.Length > 0)
            cb.AppendLine("table.document = document");

        cb.AppendLine("table.entityTable = new EntityTable(entity, document.strings)");
        cb.AppendLine();

        cb.AppendLine("return table");
        cb.AppendLine("}");
    }

    private static void WriteGetCountGetter(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations)
    {
        cb.AppendLine("async getCount(): Promise<number> {");

        if (fields.Length > 0)
        {
            var first = fields[0];
            var (strategy, prefix) = first.FieldType.GetValueSerializationStrategyAndTypePrefix();

            if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
            {
                cb.AppendLine($"return (await this.entityTable.getArray(\"{prefix}{first.Name}\"))?.length ?? 0");
            }
            else
            {
                cb.AppendLine($"return (await this.entityTable.getArray(\"{CompositeTypePrefix(first.FieldType)}{first.Name}\"" +
                              $" + new Converters.{ToTypeScriptType(first.FieldType.Name)}Converter().columns[0]))?.length ?? 0");
            }
        }
        else if (relations.Length > 0)
        {
            var first = relations[0].GetIndexColumnInfo();
            cb.AppendLine($"return (await this.entityTable.getArray(\"{first.IndexColumnName}\"))?.length ?? 0");
        }

        cb.AppendLine("}");
    }

    private static ValueSerializationStrategy WriteGetFieldGetter(CodeBuilder cb, FieldInfo field, string lowerName,
                                                                  out string prefix)
    {
        cb.AppendLine($"async get{field.Name}({lowerName}Index: number): Promise<{ToTypeScriptType(field.FieldType.Name)} | undefined>{{");
        (var strategy, prefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

        if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
        {
            var getterName = ToTypeScriptType(field.FieldType.Name) switch
            {
                "boolean" => "getBoolean",
                "number" => "getNumber",
                "string" => "getString",
                _ => throw new ArgumentOutOfRangeException($"There's no getter function for {field.FieldType.Name}")
            };

            cb.AppendLine($"return await this.entityTable.{getterName}({lowerName}Index, \"{prefix}{field.Name}\")");
        }
        else
        {
            cb.AppendLine($"const converter = new Converters.{ToTypeScriptType(field.FieldType.Name)}Converter()");
            cb.AppendLine();

            cb.AppendLine("let numbers = await Promise.all(converter.columns.map(c" +
                          $" => this.entityTable.getNumber({lowerName}Index, \"{CompositeTypePrefix(field.FieldType)}{field.Name}\" + c)))");
            cb.AppendLine();

            cb.AppendLine("return Converters.convert(converter, numbers)");
        }

        cb.AppendLine("}");
        return strategy;
    }

    private static string GetArrayGetterName(FieldInfo field) =>
        ToTypeScriptType(field.FieldType.Name) switch
        {
            "boolean" => "getBooleanArray",
            "number" => "getArray",
            "string" => "getStringArray",
            _ => throw new ArgumentOutOfRangeException($"There's no getter function for {field.FieldType.Name}")
        };

    private static void WriteGetAllFieldGetter(CodeBuilder cb, FieldInfo field, ValueSerializationStrategy strategy,
                                               string prefix)
    {
        cb.AppendLine($"async getAll{field.Name}(): Promise<{ToTypeScriptType(field.FieldType.Name)}[] | undefined>{{");

        if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
        {
            cb.AppendLine($"return await this.entityTable.{GetArrayGetterName(field)}(\"{prefix}{field.Name}\")");
        }
        else
        {
            cb.AppendLine($"const converter = new Converters.{ToTypeScriptType(field.FieldType.Name)}Converter()");
            cb.AppendLine();

            cb.AppendLine("let numbers = await Promise.all(converter.columns.map(c" +
                          $" => this.entityTable.getArray(\"{CompositeTypePrefix(field.FieldType)}{field.Name}\" + c)))");
            cb.AppendLine();

            cb.AppendLine("return Converters.convertArray(converter, numbers)");
        }

        cb.AppendLine("}");
    }

    private static void WriteRelationGetters(CodeBuilder cb, FieldInfo[] relations, string lowerName)
    {
        foreach (var relation in relations)
        {
            var relationName = relation.Name.Replace("_", "");
            var typeName = relation.FieldType.RelationTypeParameter().Name;

            cb.AppendLine($"async get{relationName}Index({lowerName}Index: number): Promise<number | undefined> {{");
            cb.AppendLine($"return await this.entityTable.getNumber({lowerName}Index, \"{relation.GetIndexColumnInfo().IndexColumnName}\")");
            cb.AppendLine("}");
            cb.AppendLine();

            cb.AppendLine($"async getAll{relationName}Index(): Promise<number[] | undefined> {{");
            cb.AppendLine($"return await this.entityTable.getArray(\"{relation.GetIndexColumnInfo().IndexColumnName}\")");
            cb.AppendLine("}");
            cb.AppendLine();

            cb.AppendLine($"async get{relationName}({lowerName}Index: number):" +
                          $" Promise<I{typeName} | undefined> {{");
            cb.AppendLine($"const index = await this.get{relationName}Index({lowerName}Index)");
            cb.AppendLine();

            cb.AppendLine("if (index === undefined) {");
            cb.AppendLine("return undefined");
            cb.AppendLine("}");
            cb.AppendLine();

            cb.AppendLine($"return await this.document.{ToLowerFirstLetter(typeName)}?.get(index)");
            cb.AppendLine("}");
            cb.AppendLine();
        }
    }

    private static void WriteGetAllGetter(CodeBuilder cb, FieldInfo[] fields, FieldInfo[] relations,
                                          string name, string lowerName)
    {
        cb.AppendLine($"async getAll(): Promise<I{name}[]> {{");
        cb.AppendLine("const localTable = await this.entityTable.getLocal()");
        cb.AppendLine();

        var arrays = new List<string>();

        foreach (var field in fields)
        {
            var lowerFieldName = SanitizeFieldName(ToLowerFirstLetter(field.Name));
            var (strategy, _) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

            if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
            {
                cb.AppendLine($"let {lowerFieldName}: {ToTypeScriptType(field.FieldType.Name)}[] | undefined");
            }
            else
            {
                cb.AppendLine($"const {lowerFieldName}Converter = new Converters.{ToTypeScriptType(field.FieldType.Name)}Converter()");
                cb.AppendLine($"let {lowerFieldName}: {ToTypeScriptType(field.FieldType.Name)}[] | undefined");
            }
        }

        foreach (var relation in relations)
        {
            cb.AppendLine($"let {ToLowerFirstLetter(relation.Name.Replace("_", ""))}Index: number[] | undefined");
        }

        cb.AppendLine();
        
        cb.AppendLine("await Promise.all([");
        cb.Indent();
        
        foreach (var field in fields)
        {
            var lowerFieldName = SanitizeFieldName(ToLowerFirstLetter(field.Name));
            var (strategy, prefix) = field.FieldType.GetValueSerializationStrategyAndTypePrefix();

            if (strategy != ValueSerializationStrategy.SerializeAsCompositeDataColumns)
            {
                cb.AppendLine($"localTable.{GetArrayGetterName(field)}(\"{prefix}{field.Name}\").then(a => {lowerFieldName} = a),");
            }
            else
            {
                cb.AppendLine($"Promise.all({lowerFieldName}Converter.columns.map(" +
                              $"c => this.entityTable.getArray(\"{CompositeTypePrefix(field.FieldType)}{field.Name}\" + c)))");
                cb.Indent();
                cb.AppendLine($".then(a => {lowerFieldName} = Converters.convertArray({lowerFieldName}Converter, a)),");
                cb.Unindent();
            }

            arrays.Add(lowerFieldName);
        }

        foreach (var relation in relations)
        {
            var lowerRelationName = ToLowerFirstLetter(relation.Name.Replace("_", ""));

            cb.AppendLine($"localTable.getArray(\"{relation.GetIndexColumnInfo().IndexColumnName}\").then(a => {lowerRelationName}Index = a),");

            arrays.Add(lowerRelationName + "Index");
        }
        
        cb.Unindent();
        cb.AppendLine("])");

        cb.AppendLine();

        cb.AppendLine($"let {lowerName}: I{name}[] = []");
        cb.AppendLine();

        cb.AppendLine($"for (let i = 0; i < {arrays[0]}!.length; i++) {{");
        cb.AppendLine($"{lowerName}.push({{");
        cb.AppendLine("index: i,");

        for (var i = 0; i < arrays.Count; i++)
        {
            cb.AppendLine($"{arrays[i]}: {arrays[i]} ? {arrays[i]}[i] : undefined{(i == arrays.Count - 1 ? "" : ",")}");
        }

        cb.AppendLine("})");
        cb.AppendLine("}");
        cb.AppendLine();

        cb.AppendLine($"return {lowerName}");
        cb.AppendLine("}");
    }

    private static void WriteVimDocument(CodeBuilder cb, Type[] entityTypes)
    {
        cb.AppendLine("export class VimDocument {");

        foreach (var type in entityTypes)
        {
            cb.AppendLine($"{ToLowerFirstLetter(type.Name)}: I{type.Name}Table | undefined");
        }

        cb.AppendLine();

        cb.AppendLine("entities: BFast");
        cb.AppendLine("strings: string[] | undefined");
        cb.AppendLine();

        cb.AppendLine("private constructor(entities: BFast, strings: string[] | undefined) {");
        cb.AppendLine("this.entities = entities");
        cb.AppendLine("this.strings = strings");
        cb.AppendLine("}");
        cb.AppendLine();
        
        cb.AppendLine("static async createFromBfast(bfast: BFast, ignoreStrings: boolean = false): Promise<VimDocument | undefined> {");
        cb.AppendLine("const loaded = await VimLoader.loadFromBfast(bfast, ignoreStrings)");
        cb.AppendLine();
        cb.AppendLine("if (loaded[0] === undefined)");
        cb.AppendLine("    return undefined");
        cb.AppendLine();
        cb.AppendLine("let doc = new VimDocument(loaded[0]!, loaded[1])");
        cb.AppendLine();

        foreach (var type in entityTypes)
        {
            cb.AppendLine($"doc.{ToLowerFirstLetter(type.Name)} = await " +
                          $"{type.Name}Table.createFromDocument(doc)");
        }

        cb.AppendLine();
        cb.AppendLine("return doc");
        cb.AppendLine("}");
        cb.AppendLine("}");
    }
    
    private static void WriteDocument(CodeBuilder file)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        foreach (var entity in entityTypes)
        {
            WriteEntityInterface(file, entity);
            file.AppendLine();

            WriteEntityTableInterface(file, entity);
            file.AppendLine();

            WriteEntityClass(file, entity);
            file.AppendLine();

            WriteEntityTableClass(file, entity);
            file.AppendLine();
        }

        WriteVimDocument(file, entityTypes);
    }

    public static void WriteDocument(string file)
    {
        try
        {
            var cb = new CodeBuilder();

            cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
            cb.AppendLine("/**");
            cb.AppendLine(" * @module vim-ts");
            cb.AppendLine(" */");
            cb.AppendLine("import { BFast } from \"./bfast\"");
            cb.AppendLine("import { EntityTable } from \"./entityTable\"");
            cb.AppendLine("import { VimLoader } from \"./vimLoader\"");
            cb.AppendLine("import { Vector2, Vector3, Vector4, AABox, AABox2D, AABox4D, Matrix4x4 } from \"./structures\"");
            cb.AppendLine("import * as Converters from \"./converters\"");

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
