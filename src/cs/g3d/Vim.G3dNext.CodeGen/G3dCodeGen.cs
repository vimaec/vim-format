using System;
using System.IO;
using System.Linq;

namespace Vim.G3dNext.CodeGen
{
    public static class G3dCodeGen
    {
        public static void WriteDocument(string filePath)
        {
            try
            {
                var cb = new CodeBuilder();

                cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY.");
                cb.AppendLine("// ReSharper disable All");
                cb.AppendLine("using Vim.BFastLib;");
                cb.AppendLine();
                cb.AppendLine("namespace Vim.G3dNext");
                cb.AppendLine("{");
                WriteEntities(cb);
                cb.AppendLine("}");
                var content = cb.ToString();
                File.WriteAllText(filePath, content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void WriteEntities(CodeBuilder cb)
        {
            foreach(var entity in Definitions.GetEntities())
            {
                cb.AppendLine(EntityToCode(entity));
            }
        }

        public static string EntityToCode(G3dEntity entity)
        {
            return $@"// Please provide an explicit implementation in another partial class file.
    public partial class {entity.ClassName} : ISetup
    {{
        {string.Join("\n \t\t", entity.Buffers.Select(b =>
            {
                return $"public {b.ValueType}[] {b.MemberName};";
            })).TrimStart()}

        public {entity.ClassName}(
            {string.Join(", \n \t\t\t", entity.Buffers.Select(b =>
            {
                return $"{b.ValueType}[] {b.ArgumentName}";
            })).TrimStart()}
        )
        {{
            {string.Join("\n \t\t\t", entity.Buffers.Select(b =>
            {
                return $"{b.MemberName} = {b.ArgumentName};";
            })).TrimStart()}

            (this as ISetup).Setup();
        }}

        public {entity.ClassName}(BFast bfast)
        {{
            {string.Join("\n \t\t\t", entity.Buffers.Select(b =>
            {
                return $"{b.MemberName} = bfast.GetArray<{b.ValueType}>(\"{b.BufferName}\");";
            })).TrimStart()}

            (this as ISetup).Setup();
        }}

        public BFast ToBFast()
        {{
            var bfast = new BFast();

            {string.Join("\n \t\t\t", entity.Buffers.Select(b =>
            {
                return $"bfast.SetArray<{b.ValueType}>(\"{b.BufferName}\", {b.MemberName});";
            })).TrimStart()}

            return bfast;
        }}

        public bool Equals({entity.ClassName} other )
        {{
            return {string.Join(" && \n \t\t\t", entity.Buffers.Select(b =>
            {
                return $"BufferMethods.SafeEquals({b.MemberName}, other.{b.MemberName})";
            }))};
        }}

        public {entity.ClassName} Merge({entity.ClassName} other)
        {{
            return new {entity.ClassName}(
                {string.Join(", \n \t\t\t\t", entity.Buffers.Select(b => {

                switch (b.BufferType)
                {
                    case BufferType.Singleton:
                        return $"{b.MemberName}";
                    case BufferType.Data:
                        return $"BufferMethods.MergeData({b.MemberName}, other.{b.MemberName})";
                    case BufferType.Index:
                        return $"BufferMethods.MergeIndex({b.MemberName}, other.{b.MemberName}, {b.IndexInto}?.Length ?? 0)";
                    default:
                        return "";
                }
            }))}
            );
        }}

        public void Validate() 
        {{
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
            {string.Join("\n \t\t\t", entity.Buffers.Select(c =>
            {
                if (c.BufferType == BufferType.Index)
                {
                    return $"BufferMethods.ValidateIndex({c.MemberName}, {c.IndexInto}, \"{c.MemberName}\");";
                }
                return null;
            }).Where(s => s != null))}
        }}
    }}
";
        }
    }
}


