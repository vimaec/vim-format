using System;
using System.Reflection;
using Vim.DotNetUtilities;

namespace Vim.DataFormat
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public partial class EntityColumnLoaderAttribute : Attribute
    {
        /// <summary>
        /// The name of the serialized column.
        /// </summary>
        public string SerializedValueColumnName { get; }

        /// <summary>
        /// The serialized type.
        /// </summary>
        public Type SerializedType { get; }

        /// <summary>
        /// The object model version of this loader. This is used to sort the loaders in descending order.
        /// The default entity column loader of the current schema version always has top priority.
        /// </summary>
        public SerializableVersion ObjectModelVersion { get; }

        /// <summary>
        /// Base loader
        /// </summary>
        private EntityColumnLoaderAttribute(string serializedValueColumnName, Type serializedType, SerializableVersion objectModelVersion)
        {
            SerializedValueColumnName = serializedValueColumnName;
            SerializedType = serializedType;
            ObjectModelVersion = objectModelVersion;
        }

        /// <summary>
        /// Optional loader (used as an attribute)
        /// </summary>
        public EntityColumnLoaderAttribute(string serializedValueColumnName, Type serializedType, string objectModelVersion)
            : this(serializedValueColumnName, serializedType, SerializableVersion.Parse(objectModelVersion))
        { }

        /// <summary>
        /// Default loader.
        /// </summary>
        public EntityColumnLoaderAttribute(FieldInfo fieldInfo, SerializableVersion objectModelVersion)
            : this(fieldInfo.GetSerializedValueColumnName(), fieldInfo.FieldType, objectModelVersion)
        { }
    }
}
