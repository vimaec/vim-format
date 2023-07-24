typedef bfast::ByteRange* ByteRangePtr;

enum class ColumnType
{
    indexColumn,
    stringColumn,
    dataColumn,
};

// Buffer names
static const std::string bufferNameHeader = "header";
static const std::string bufferNameGeometry = "geometry";
static const std::string bufferNameAssets = "assets";
static const std::string bufferNameStrings = "strings";
static const std::string bufferNameEntities = "entities";

// Entity column prefixes
static const std::string indexColumnPrefix = "index:";
static const std::string stringColumnPrefix = "string:";
static const std::string dataColumnBytePrefix = "byte:";
static const std::string dataColumnUbytePrefix = "ubyte:";
static const std::string dataColumnIntPrefix = "int:";
static const std::string dataColumnUintPrefix = "uint:";
static const std::string dataColumnLongPrefix = "long:";
static const std::string dataColumnUlongPrefix = "ulong:";
static const std::string dataColumnFloatPrefix = "float:";
static const std::string dataColumnDoublePrefix = "double:";

static inline bool startsWith(const std::string& base, const std::string& value)
{
    return base.rfind(value, 0) == 0;
}

static ColumnType getColumnType(const std::string& colName)
{
    if (startsWith(colName, "index:"))
        return ColumnType::indexColumn;

    if (startsWith(colName, "string:"))
        return ColumnType::stringColumn;

    return ColumnType::dataColumn; // default to data column.
}

static size_t getTypeSize(const std::string& colName)
{
    if (startsWith(colName, indexColumnPrefix) ||
        startsWith(colName, stringColumnPrefix) ||
        startsWith(colName, dataColumnIntPrefix) ||
        startsWith(colName, dataColumnUintPrefix) ||
        startsWith(colName, dataColumnFloatPrefix))
    {
        return 4; // 4 bytes
    }

    if (startsWith(colName, dataColumnDoublePrefix) ||
        startsWith(colName, dataColumnLongPrefix) ||
        startsWith(colName, dataColumnUlongPrefix))
    {
        return 8; // 8 bytes
    }

    if (startsWith(colName, dataColumnBytePrefix) ||
        startsWith(colName, dataColumnUbytePrefix))
    {
        return 1; // 1 byte
    }

    return 1; // default to 1 byte
}

static bool columnExists(const EntityTable& table, const std::string& colName)
{
    if (startsWith(colName, indexColumnPrefix))
    {
        return table.mIndexColumns.find(colName) != table.mIndexColumns.end();
    }

    if (startsWith(colName, stringColumnPrefix))
    {
        return table.mStringColumns.find(colName) != table.mStringColumns.end();
    }

    return table.mDataColumns.find(colName) != table.mDataColumns.end();
}

static size_t getCount(const EntityTable& table)
{
    if (!table.mIndexColumns.empty())
    {
        return table.mIndexColumns.begin()->second.size();
    }

    if (!table.mStringColumns.empty())
    {
        return table.mStringColumns.begin()->second.size();
    }

    if (!table.mDataColumns.empty())
    {
        const auto first_data_column = table.mDataColumns.begin();
        const auto& col_name = first_data_column->first;
        const auto bytes = first_data_column->second;
        return bytes.size() / getTypeSize(col_name);
    }

    return 0;
}