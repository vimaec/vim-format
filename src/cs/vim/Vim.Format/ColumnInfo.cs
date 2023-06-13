using System;
using System.Collections.Generic;
using System.Linq;

namespace Vim.DataFormat
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
        public readonly Type SerializedType;
        public readonly ISet<Type> CastTypes;

        public ColumnInfo(ColumnType columnType, string typePrefix, Type serializedType, params Type[] castTypes)
        {
            (ColumnType, TypePrefix, SerializedType) = (columnType, typePrefix, serializedType);
            CastTypes = new HashSet<Type>(castTypes);
        }

        public IEnumerable<Type> RelatedTypes
            => CastTypes.Prepend(SerializedType);
    }
}
