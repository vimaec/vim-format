static inline bool startsWith(const std::string& base, const std::string& value)
{
    return base.rfind(value, 0) == 0;
}

static size_t getTypeSize(const std::string& colName)
{
    if (startsWith(colName, "index:") ||
        startsWith(colName, "string:") ||
        startsWith(colName, "int:") ||
        startsWith(colName, "uint:") ||
        startsWith(colName, "float:"))
    {
        return 4; // 4 bytes
    }

    if (startsWith(colName, "double:") ||
        startsWith(colName, "long:") ||
        startsWith(colName, "ulong:"))
    {
        return 8; // 8 bytes
    }

    if (startsWith(colName, "byte:") ||
        startsWith(colName, "ubyte:"))
    {
        return 1; // 1 byte
    }

    return 1; // default to 1 byte
}

static bool columnExists(const EntityTable& table, const std::string& colName)
{
    if (startsWith(colName, "index:"))
    {
        return table.mIndexColumns.find(colName) != table.mIndexColumns.end();
    }

    if (startsWith(colName, "string:"))
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