/*
    VIM Data Format
    Copyright 2023, VIMaec LLC
    Copyright 2018, Ara 3D, Inc.
    Usage licensed under terms of MIT License.
*/
#ifndef __VIM_H__
#define __VIM_H__

#include <vector>
#include <sstream>
#include <unordered_map>
#include <tuple>
#include <stdexcept>

#include "../g3d.h"

namespace Vim
{
    enum class ColumnType
    {
        index_column,
        string_column,
        data_column,
    };

    // Buffer names
    static const std::string buffer_name_header = "header";
    static const std::string buffer_name_geometry = "geometry";
    static const std::string buffer_name_assets = "assets";
    static const std::string buffer_name_strings = "strings";
    static const std::string buffer_name_entities = "entities";

    // Entity column prefixes
    static const std::string index_column_prefix = "index:";
    static const std::string string_column_prefix = "string:";
    static const std::string data_column_byte_prefix = "byte:";
    static const std::string data_column_ubyte_prefix = "ubyte:";
    static const std::string data_column_int_prefix = "int:";
    static const std::string data_column_uint_prefix = "uint:";
    static const std::string data_column_long_prefix = "long:";
    static const std::string data_column_ulong_prefix = "ulong:";
    static const std::string data_column_float_prefix = "float:";
    static const std::string data_column_double_prefix = "double:";

    class EntityTable
    {
    public:
        std::string mName;

        std::unordered_map<std::string, std::vector<int>> mIndexColumns;
        std::unordered_map<std::string, std::vector<int>> mStringColumns;
        std::unordered_map<std::string, bfast::ByteRange> mDataColumns;

        static bool starts_with(const std::string& base, const std::string& value)
        {
            return base.rfind(value, 0) == 0;
        }

        static ColumnType get_column_type(const std::string& col_name)
        {
            if (starts_with(col_name, index_column_prefix))
                return ColumnType::index_column;

            if (starts_with(col_name, string_column_prefix))
                return ColumnType::string_column;

            return ColumnType::data_column; // default to data column.
        }

        static size_t get_type_size(const std::string& col_name)
        {
            if (starts_with(col_name, index_column_prefix) ||
                starts_with(col_name, string_column_prefix) ||
                starts_with(col_name, data_column_int_prefix) ||
                starts_with(col_name, data_column_uint_prefix) ||
                starts_with(col_name, data_column_float_prefix))
            {
                return 4; // 4 bytes
            }

            if (starts_with(col_name, data_column_double_prefix) ||
                starts_with(col_name, data_column_long_prefix) ||
                starts_with(col_name, data_column_ulong_prefix))
            {
                return 8; // 8 bytes
            }

            if (starts_with(col_name, data_column_byte_prefix) ||
                starts_with(col_name, data_column_ubyte_prefix))
            {
                return 1; // 1 byte
            }

            return 1; // default to 1 byte
        }

        bool column_exists(const std::string& col_name)
        {
            if (starts_with(col_name, index_column_prefix))
            {
                return mIndexColumns.find(col_name) != mIndexColumns.end();
            }

            if (starts_with(col_name, string_column_prefix))
            {
                return mStringColumns.find(col_name) != mStringColumns.end();
            }

            return mDataColumns.find(col_name) != mDataColumns.end();
        }

        size_t get_count() const
        {
            if (!mIndexColumns.empty())
            {
                return mIndexColumns.begin()->second.size();
            }

            if (!mStringColumns.empty())
            {
                return mStringColumns.begin()->second.size();
            }

            if (!mDataColumns.empty())
            {
                const auto first_data_column = mDataColumns.begin();
                const auto& col_name = first_data_column->first;
                const auto bytes = first_data_column->second;
                return bytes.size() / get_type_size(col_name);
            }

            return 0;
        }
    };

    inline std::vector<std::string> split(const std::string& str, const std::string& delim)
    {
        std::vector<std::string> tokens;
        size_t prev = 0, pos = 0;
        do
        {
            pos = str.find(delim, prev);
            if (pos == std::string::npos)
            {
                pos = str.length();
            }

            std::string token = str.substr(prev, pos - prev);
            if (!token.empty())
            {
                tokens.push_back(token);
            }

            prev = pos + delim.length();
        } while (pos < str.length() && prev < str.length());

        return tokens;
    }

    enum class VimErrorCodes
    {
        Success = 0,
        Failed = -1,
        NoVersionInfo = -2,
        FileNotRecognized = -3,
        GeometryLoadingException = -4,
        AssetLoadingException = -5,
        EntityLoadingException = -6
    };

    enum class VimLoadFlags
    {
        Geometry = 1,
        Assets = 2,
        Strings = 4,
        Entities = 8,
        All = Geometry | Assets | Strings | Entities
    };

    class Scene
    {
    public:
        bfast::Bfast mBfast;
        bfast::Bfast mGeometryBFast;
        bfast::Bfast mAssetsBFast;
        bfast::Bfast mEntitiesBFast;
        std::vector<const bfast::byte*> mStrings;
        g3d::G3d mGeometry;
        std::unordered_map<std::string, EntityTable> mEntityTables;
        std::unordered_map<std::string, std::string> mHeader;

        uint32_t mVersionMajor = 0xffffffff;
        uint32_t mVersionMinor = 0xffffffff;
        uint32_t mVersionPatch = 0xffffffff;

        VimErrorCodes ReadFile(std::string file_name, VimLoadFlags load_flags = VimLoadFlags::All)
        {
#ifdef DISABLE_EXCEPTIONS
            mBfast = bfast::Bfast::read_file(fileName);
#else
            try
            {
                mBfast = bfast::Bfast::read_file(file_name);
            }
            catch (std::exception& e)
            {
                e;
                return VimErrorCodes::FileNotRecognized;
            }
#endif

            auto ui_load_flags = static_cast<uint32_t>(load_flags);

            for (auto& b : mBfast.buffers)
            {
                if (b.name == buffer_name_header)
                {
                    std::vector<std::string> version_parts;

                    std::string header = reinterpret_cast<const char*>(b.data.begin());
                    std::vector<std::string> tokens = split(header, "\n");

                    for (auto& token : tokens)
                    {
                        std::vector<std::string> key_value = split(token, "=");

                        if (key_value.size() == 2)
                        {
                            mHeader[key_value[0]] = key_value[1];
                        }
                    }

                    if (mHeader.end() != mHeader.find("vim"))
                    {
                        version_parts = split(mHeader["vim"], ".");
                    }
                    else
                    {
                        // No vim version found
                        return VimErrorCodes::NoVersionInfo;
                    }

                    if (version_parts.size() > 0) mVersionMajor = std::stoi(version_parts[0]);
                    if (version_parts.size() > 1) mVersionMinor = std::stoi(version_parts[1]);
                    if (version_parts.size() > 2) mVersionPatch = std::stoi(version_parts[2]);
                }
                else if (b.name == buffer_name_geometry && (ui_load_flags & static_cast<uint32_t>(VimLoadFlags::Geometry)) != 0)
                {
#ifdef DISABLE_EXCEPTIONS
                    mGeometryBFast = bfast::Bfast::unpack(b.data);
                    mGeometry = std::move(g3d::G3d(mGeometryBFast));
#else
                    try
                    {
                        mGeometryBFast = bfast::Bfast::unpack(b.data);
                        mGeometry = std::move(g3d::G3d(mGeometryBFast));
                    }
                    catch (std::exception& e)
                    {
                        e;
                        return Vim::VimErrorCodes::GeometryLoadingException;
                    }
#endif
                }
                else if (b.name == buffer_name_assets && (ui_load_flags & static_cast<uint32_t>(VimLoadFlags::Assets)) != 0)
                {
#ifdef DISABLE_EXCEPTIONS
                    mAssetsBFast = bfast::Bfast::unpack(b.data);
#else
                    try
                    {
                        mAssetsBFast = bfast::Bfast::unpack(b.data);
                    }
                    catch (std::exception& e)
                    {
                        e;
                        return Vim::VimErrorCodes::AssetLoadingException;
                    }
#endif
                }
                else if (b.name == buffer_name_strings && (ui_load_flags & static_cast<uint32_t>(VimLoadFlags::Strings)) != 0)
                {
                    const bfast::byte* data = b.data.begin();
                    size_t count = 0;
                    while (data < b.data.end())
                    {
                        count++;
                        data += strlen(reinterpret_cast<const char*>(data)) + 1;
                    }

                    mStrings.resize(count);
                    count = 0;
                    data = b.data.begin();
                    while (data < b.data.end())
                    {
                        mStrings[count++] = data;
                        data += strlen(reinterpret_cast<const char*>(data)) + 1;
                    }
                }
                else if (b.name == buffer_name_entities && (ui_load_flags & static_cast<uint32_t>(VimLoadFlags::Entities)) != 0)
                {
#ifndef DISABLE_EXCEPTIONS
                    try
#endif
                    {
                        mEntitiesBFast = bfast::Bfast::unpack(b.data);
                        for (auto& entity_buffer : mEntitiesBFast.buffers)
                        {
                            EntityTable entity_table = { entity_buffer.name };
                            bfast::Bfast table_bfast = bfast::Bfast::unpack(entity_buffer.data);

                            for (auto& table_buffer : table_bfast.buffers)
                            {
                                auto column_type = EntityTable::get_column_type(table_buffer.name);
                                switch (column_type)
                                {
                                case ColumnType::index_column:
                                    entity_table.mIndexColumns[table_buffer.name] = std::vector<int>((int*)table_buffer.data.begin(), (int*)table_buffer.data.end());
                                    break;
                                case ColumnType::string_column:
                                    entity_table.mStringColumns[table_buffer.name] = std::vector<int>((int*)table_buffer.data.begin(), (int*)table_buffer.data.end());
                                    break;
                                case ColumnType::data_column:
                                    entity_table.mDataColumns[table_buffer.name] = table_buffer.data;
                                    break;
                                }
                            }

                            mEntityTables[entity_table.mName] = entity_table;
                        }
                    }
#ifndef DISABLE_EXCEPTIONS
                    catch (std::exception& e)
                    {
                        e;
                        return Vim::VimErrorCodes::EntityLoadingException;
                    }
#endif
                }
            }
            return VimErrorCodes::Success;
        }
    };

}

#endif
