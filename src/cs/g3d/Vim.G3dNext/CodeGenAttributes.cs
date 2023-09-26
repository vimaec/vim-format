using System;
using System.Linq.Expressions;

namespace Vim.G3dNext
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeDescriptorAttribute : Attribute
    {
        public string Name { get; set; }
        public Type ArrayType { get; set; } = null;
        public AttributeType AttributeType { get; set; } = AttributeType.Data;
        public Type IndexInto { get; set; } = null;

        public AttributeDescriptorAttribute(string name, AttributeType attributeType)
        {
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