using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Vim.G3dNext.CodeGen
{

    public static class G3dAttributeCollectionGenerator
    {
        public static void WriteDocument(string filePath)
        {
            try
            {
                var cb = new CodeBuilder();

                cb.AppendLine("// AUTO-GENERATED FILE, DO NOT MODIFY WACKO3.");
                cb.AppendLine("// ReSharper disable All");
                cb.AppendLine("using System;");
                cb.AppendLine("using System.IO;");
                cb.AppendLine("using System.Collections.Generic;");
                cb.AppendLine("using System.Linq;");
                cb.AppendLine("using Vim.BFast;");
                cb.AppendLine("using Vim.BFastNextNS;");
                cb.AppendLine();
                cb.AppendLine("namespace Vim.G3dNext.Attributes");
                cb.AppendLine("{");
                WriteVimG3dAttributes(cb);
                WriteVimG3dAttributeCollections(cb);
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

        public static IEnumerable<Type> GetAllClassesWithAttribute<T>() where T: Attribute
        {
            // Load the assembly containing the classes you want to examine
            var assembly = Assembly.GetExecutingAssembly();

            // Find all the types in the assembly that have the MyAttribute attribute
            return assembly.GetTypes()
                .Where(type => type.GetCustomAttributes<T>(false).Any());
        } 

        public static void WriteVimG3dAttributes(CodeBuilder cb)
        {
            var adClasses = GetAllClassesWithAttribute<AttributeDescriptorAttribute>();

            foreach (var adClass in adClasses)
                WriteVimG3dAttribute(cb, adClass);
        }

        public static string GetTypedDataType(this AttributeDescriptorAttribute ada)
        {
            if (ada.ArrayType != null)
                return ada.ArrayType.ToString();

            if (!AttributeDescriptor.TryParse(ada.Name, out var ad))
                throw new Exception($"Could not parse attribute name {ada.Name}");

            return ad.DataType.GetManagedType().ToString();
        }

        public static void WriteVimG3dAttribute(CodeBuilder cb, Type adClass)
        {
            var className = adClass.Name;
            var ada = adClass.GetCustomAttribute<AttributeDescriptorAttribute>();
            if (ada == null)
                throw new Exception($"No attribute of type {nameof(AttributeDescriptorAttribute)} found on {className}");

            var attributeName = ada.Name;
            if (!AttributeDescriptor.TryParse(attributeName, out var ad))
                throw new Exception($"Invalid attribute descriptor: {attributeName}");

            var typedDataType = ada.GetTypedDataType();
            var attributeType = ada.AttributeType;
            var indexInto = ada.IndexInto;

            cb.AppendLine($@"
    public partial class {className} : {nameof(IAttribute)}<{typedDataType}>
    {{
        public const string AttributeName = ""{attributeName}"";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {{
            if(TypedData != null)
            {{
                bfast.SetArray(Name, TypedData);
            }}
        }}

        public void ReadBFast(BFastNext bfast)
        {{
            TypedData = bfast.GetArray<{typedDataType}>(""{attributeName}"");
        }}

        public {nameof(IAttributeDescriptor)} {nameof(AttributeDescriptor)} {{ get; }}
            = new {nameof(AttributeDescriptor)}(AttributeName);

        public {nameof(AttributeType)} {nameof(IAttribute.AttributeType)} {{ get; }}
            = {nameof(AttributeType)}.{attributeType};

        public Type {nameof(IAttribute.IndexInto)} {{ get; }}
            = {(indexInto == default ? "null" : $"typeof({indexInto})")};

        public {typedDataType}[] TypedData {{ get; set; }}
            = Array.Empty<{typedDataType}>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {{
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }}
    }}");
        }

        public static void WriteVimG3dAttributeCollections(CodeBuilder cb)
        {
            var acClasses = GetAllClassesWithAttribute<AttributeCollectionAttribute>();

            foreach (var acClass in acClasses)
                WriteVimG3dAttributeCollection(cb, acClass);
        }

        public static void WriteVimG3dAttributeCollection(CodeBuilder cb, Type acClass)
        {
            var className = acClass.Name;

            var ac = acClass.GetCustomAttribute<AttributeCollectionAttribute>();
            if (ac == null)
                throw new Exception($"No attribute of type {(nameof(AttributeCollectionAttribute))} found on {className}");

            var attributeClasses = ac.AttributeClasses;
            var g3dName = "G3d" + className.Replace("AttributeCollection", "");

            cb.AppendLine($@"
    // Please provide an explicit implementation in another partial class file.
    public partial class {g3dName} : ISetup
    {{
        public {className} Attributes;

        public {g3dName}() : this(new {className}())
        {{
            // empty
        }}

        public {g3dName}(BFastNext bfast) : this(new {className}(bfast))
        {{
            // empty
        }}

        public {g3dName}({className} attributes)
        {{
            Attributes = attributes;

            // Method to implement in another partial class
            (this as ISetup).Setup();
        }}

        public BFastNext ToBFast()
            => Attributes.ToBFast();

        {
            string.Join(Environment.NewLine, attributeClasses.Select(c =>
            {
                var attribute = c.GetCustomAttribute<AttributeDescriptorAttribute>();
                var typedDataType = attribute.GetTypedDataType();
                var name = attribute.FormatClassName(c.Name);
                return $@"
        public {typedDataType}[] {attribute.FormatClassName(name)}
        {{
            get => Attributes.{name}.TypedData;
            set => Attributes.{name}.TypedData = value;
        }}";}))
        }
    }}
");


            cb.AppendLine($@"
    public partial class {className} : IAttributeCollection
    {{
        public {className}()
        {{
            // empty
        }}

        public {className}(BFastNext bfast)
        {{
            this.ReadAttributes(bfast);
        }}

        public void ReadAttributes(BFastNext bfast)
        {{
            foreach (var attribute in Map.Values)
            {{
                attribute.ReadBFast(bfast);
            }}
        }}

        public IDictionary<string, IAttribute> Map {{ get; }}
            = new Dictionary<string, IAttribute>
            {{
{string.Join(Environment.NewLine, attributeClasses.Select(c =>
    $"                [{c}.AttributeName] = new {c}(),"))}
            }};

        // Named Attributes.
{string.Join(Environment.NewLine, attributeClasses.Select(c =>
    {
        var attribute = c.GetCustomAttribute<AttributeDescriptorAttribute>();
        var name = attribute.FormatClassName(c.Name);
        return $@"
        public {c} {name}
        {{
            get => Map.TryGetValue({c}.AttributeName, out var attr) ? attr as {c} : default;
            set => Map[{c}.AttributeName] = value as IAttribute;
        }}";
    })
)}

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {{
{string.Join(Environment.NewLine, attributeClasses.Select(c => {
    var attribute = c.GetCustomAttribute<AttributeDescriptorAttribute>();
    var name = attribute.FormatClassName(c.Name);
    return $@"

                if (attributeType == typeof({c}))
                    return {name};";

    }))}

            throw new ArgumentException(""Type {{attributeType.ToString()}} is not supported."");
        }}

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {{
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {{
{string.Join(Environment.NewLine, attributeClasses.Select(c => {
    var ada = c.GetCustomAttribute<AttributeDescriptorAttribute>();
    var typedDataType = ada.GetTypedDataType();
    string caseBody = null;
    switch (ada.AttributeType)
    {
        case AttributeType.Singleton:
            caseBody = $@"// Singleton Attribute (no merging)
                    return {c.Name};";
            break;
        case AttributeType.Data:
            caseBody = $@"// Data Attribute
                    return collections.{nameof(AttributeCollectionExtensions.GetAttributesOfType)}<{c}>().ToArray().{nameof(AttributeExtensions.MergeDataAttributes)}<{c}, {typedDataType}>();";
            break;
        case AttributeType.Index:
            caseBody = $@"// Index Attribute
                    return collections.{nameof(AttributeCollectionExtensions.GetIndexedAttributesOfType)}<{c}>().{nameof(AttributeExtensions.MergeIndexAttributes)}();";
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(ada.AttributeType));
    }

    return $@"
                case {c}.AttributeName:
                {{
                    {caseBody}
                }}";
}))}

                default:
                    throw new ArgumentException(nameof(attributeName));
            }}
        }}

        public void Validate() 
        {{
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.
{string.Join(Environment.NewLine, attributeClasses.Select(c =>
{
    var ada = c.GetCustomAttribute<AttributeDescriptorAttribute>();
    var typedDataType = ada.GetTypedDataType();
    var name = ada.FormatClassName(c.Name);

    switch (ada.AttributeType)
    {
        case AttributeType.Singleton:
        case AttributeType.Data:
            return null;
        case AttributeType.Index:
            return $@"
            {{
                var maxIndex = GetAttribute({name}.IndexInto).Data.Length - 1;
                for (var i = 0; i < {name}.TypedData.Length; ++i)
                {{
                    var index = {name}.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($""Index out of range in {c} at position {{i}}. Expected either -1 for no relation, or a maximum of {{maxIndex}} but got {{index}}"");
                }}
            }}";
        default:
            throw new ArgumentOutOfRangeException(nameof(ada.AttributeType));
    }
}).Where(s => !string.IsNullOrEmpty(s)))}
        }}
    }}");
        }
    }
}
