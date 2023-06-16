using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Vim.DotNetUtilities;
using Vim.Format.ObjectModel;

namespace Vim.Format.CodeGen;

public static class ObjectModelDbContextGenerator
{
    private const string PocoClassPrefix = "Poco";
    private const string ForeignKeySuffix = "Object";
    
    private static void WriteFlattenedStructRecursive(string prefix, FieldInfo field, CodeBuilder cb)
    {
        var newPrefix = $"{prefix}{(prefix.Length > 0 ? "." : "")}{field.Name}";

        var subfields = field.FieldType.GetFields(BindingFlags.Instance | BindingFlags.Public);

        if (subfields.Length == 0)
        {
            if (!newPrefix.Equals(field.Name, StringComparison.Ordinal))
            {
                cb.AppendLine($"[Column(\"{newPrefix}\")]");
                cb.AppendLine($"public {field.FieldType.Name} {newPrefix.Replace(".", "")} {{ get; set; }}");
            }
            else
            {
                cb.AppendLine($"public {field.FieldType.Name} {field.Name} {{ get; set; }}");
            }

            return;
        }

        foreach (var subfield in subfields)
        {
            WriteFlattenedStructRecursive($"{newPrefix}", subfield, cb);
        }
    }

    private static void WriteDbContextEntities(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        foreach (var entity in entityTypes)
        {
            cb.AppendLine("[Table(TableName)]");
            cb.AppendLine($"public partial class {PocoClassPrefix}{entity.Name}");
            cb.AppendLine("{");
            cb.AppendLine($"public const string TableName = \"{entity.Name}\";");
            cb.AppendLine();
            cb.AppendLine("[Key, Column(ObjectModelContext.KeyColumnName), DatabaseGenerated(DatabaseGeneratedOption.None)]");
            cb.AppendLine("public long Key { get; set; }");
            cb.AppendLine();
            cb.AppendLine("[Column(ObjectModelContext.IndexColumnName)]");
            cb.AppendLine("public int Index { get; set; }");
            cb.AppendLine();

            var fields = entity.GetEntityFields().ToArray();

            foreach (var field in fields)
            {
                WriteFlattenedStructRecursive("", field, cb);
            }

            var relations = entity.GetRelationFields().ToArray();

            foreach (var relation in relations)
            {
                var relType = relation.FieldType.RelationTypeParameter();
                var name = relation.Name.Trim('_');

                cb.AppendLine($"public long? {name} {{ get; set; }}");

                cb.AppendLine($"[ForeignKey(nameof({name}))]");
                cb.AppendLine($"public {PocoClassPrefix}{relType.Name} {name}{ForeignKeySuffix} {{ get; set; }}");
            }

            cb.AppendLine("}");
            cb.AppendLine();
        }
    }

    private static void WriteDbContext(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public partial class ObjectModelContext : DbContext");
        cb.AppendLine("{");
        cb.AppendLine("// All entities have a _key representing their primary key in the database.");
        cb.AppendLine("public const string KeyColumnName = \"_key\";");
        cb.AppendLine();
        cb.AppendLine("// All entities have an Index representing the entity index within the originating VIM file.");
        cb.AppendLine("public const string IndexColumnName = \"Index\";");
        cb.AppendLine();

        foreach (var entity in entityTypes)
        {
            cb.AppendLine($"public DbSet<{PocoClassPrefix}{entity.Name}> {entity.Name} {{ get; set; }}");
        }
        cb.AppendLine();
        cb.AppendLine("public ObjectModelContext(DbContextOptions<ObjectModelContext> options) : base(options) { }");
        cb.AppendLine();
        cb.AppendLine("protected override void OnModelCreating (ModelBuilder builder)");
        cb.AppendLine("{");
        cb.AppendLine("OnModelCreatingCustom(builder);");

        foreach (var entity in entityTypes)
        {
            var relations = entity.GetRelationFields().ToArray();

            foreach (var relation in relations)
            {
                var relType = relation.FieldType.RelationTypeParameter();

                if (relType.GetRelationFields().Any(f => f.FieldType.RelationTypeParameter() == entity))
                {
                    cb.AppendLine($"builder.Entity<{PocoClassPrefix}{entity.Name}>()");
                    cb.AppendLine($"       .HasOne(e => e.{relation.Name.Trim('_')}{ForeignKeySuffix})");
                    cb.AppendLine($"       .WithMany()");
                    cb.AppendLine($"       .OnDelete(DeleteBehavior.NoAction);");
                }
            }
        }

        cb.AppendLine("} // OnModelCreating");
        cb.AppendLine("} // ObjectModelContext");
        cb.AppendLine();
    }

    private static void WriteDbContextExtensions(CodeBuilder cb)
    {
        var entityTypes = ObjectModelReflection.GetEntityTypes().ToArray();

        cb.AppendLine("public static partial class ObjectModelContextExtensions");
        cb.AppendLine("{");

        cb.AppendLine(
            "public static IReadOnlyDictionary<string, string> EntityTableNameToDatabaseTableNameMap = new Dictionary<string, string>()");
        cb.AppendLine("{");
        foreach (var entity in entityTypes)
        {
            cb.AppendLine($"{{ DataFormat.TableNames.{entity.Name}, {PocoClassPrefix}{entity.Name}.TableName }},");
        }
        cb.AppendLine("};");
        cb.AppendLine("} // ObjectModelContextExtensions");
    }

    public static void WriteDbContext(string file)
    {
        try
        {
            var cb = new CodeBuilder();

            cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
            cb.AppendLine("// ReSharper disable All");
            cb.AppendLine("#pragma warning disable 659");
            cb.AppendLine("using System;");
            cb.AppendLine("using System.Collections.Generic;");
            cb.AppendLine("using System.ComponentModel.DataAnnotations;");
            cb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            cb.AppendLine("using Microsoft.EntityFrameworkCore;");

            cb.AppendLine();

            cb.AppendLine("namespace Vim.Sql");
            cb.AppendLine("{");

            WriteDbContextEntities(cb);
            WriteDbContext(cb);
            WriteDbContextExtensions(cb);

            cb.AppendLine("} // namespace");
            cb.AppendLine("#pragma warning restore 659");
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
