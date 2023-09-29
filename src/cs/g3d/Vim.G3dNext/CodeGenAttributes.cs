using System;

namespace Vim.G3dNext
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeDescriptorAttribute : Attribute
    {
        public string Name { get; set; }
        public string Prefix { get; set; } = null;
        public Type ArrayType { get; set; } = null;
        public AttributeType AttributeType { get; set; } = AttributeType.Data;
        public Type IndexInto { get; set; } = null;

        public string FormatClassName(string name)
        {
            var result = name.Replace("Attribute", "");
            return Prefix != null
                ? result.Replace(Prefix,"") 
                : result;
        }
        public AttributeDescriptorAttribute(string name, AttributeType attributeType)
        {
            Name = name;
            AttributeType = attributeType;
        }
        public AttributeDescriptorAttribute(string prefix, string name, AttributeType attributeType)
        {
            Prefix = prefix;
            Name = name;
            AttributeType = attributeType;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeCollectionAttribute : Attribute
    {
        public Type[] AttributeClasses { get; }

        public AttributeCollectionAttribute(params Type[] attributeClasses)
            => AttributeClasses = attributeClasses;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class G3dDefinitionAttribute : Attribute
    {
        public Type[] AttributeClasses { get; }

        public G3dDefinitionAttribute(params Type[] attributeClasses)
            => AttributeClasses = attributeClasses;
    }
}
