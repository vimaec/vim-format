namespace Vim.Format
{
    public class EntityColumnLoadingInfo
    {
        public readonly ValueSerializationStrategy Strategy;
        public readonly string TypePrefix;
        public readonly EntityColumnLoaderAttribute EntityColumnAttribute;
        public string SerializedValueColumnName
            => EntityColumnAttribute.SerializedValueColumnName;

        public EntityColumnLoadingInfo(
            ValueSerializationStrategy strategy,
            string typePrefix,
            EntityColumnLoaderAttribute entityColumnAttribute)
        {
            Strategy = strategy;
            TypePrefix = typePrefix;
            EntityColumnAttribute = entityColumnAttribute;
        }
    }
}
