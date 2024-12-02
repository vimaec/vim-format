using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.Format
{
    public enum ColumnType
    {
        IndexColumn,
        StringColumn,
        DataColumn,
    }

    public class ColumnInfo
    {
        public readonly ColumnType ColumnType;
        public readonly string TypePrefix;
        private readonly Type _serializedType;
        private readonly ISet<Type> _castTypes;

        public ColumnInfo(ColumnType columnType, string typePrefix, Type serializedType, params Type[] castTypes)
        {
            (ColumnType, TypePrefix, _serializedType) = (columnType, typePrefix, serializedType);
            _castTypes = new HashSet<Type>(castTypes);
        }

        public IEnumerable<Type> RelatedTypes
            => _castTypes.Prepend(_serializedType);
    }
}
