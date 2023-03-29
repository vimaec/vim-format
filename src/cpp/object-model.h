// AUTO-GENERATED FILE, DO NOT MODIFY.

#ifndef __OBJECT_MODEL_H__
#define __OBJECT_MODEL_H__

#include <string>
#include <vector>
#include "bfast.h"
#include "vim.h"

namespace Vim
{
    struct Vector2 { float x, y; };
    struct Vector3 { float x, y, z; };
    struct Vector4 { float x, y, z, w; };
    struct AABox { Vector3 min, max; };
    struct AABox2D { Vector2 min, max; };
    struct AABox4D { Vector4 min, max; };
    struct DVector2 { double x, y; };
    struct DVector3 { double x, y, z; };
    struct DVector4 { double x, y, z, w; };
    struct DAABox { DVector3 min, max; };
    struct DAABox2D { DVector2 min, max; };
    struct DAABox4D { DVector4 min, max; };
    struct Matrix4x4 { float m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44; };

    typedef bfast::ByteRange* ByteRangePtr;

    template<class T>
    class Converter
    {
    public:
        virtual int GetSize() = 0;
        virtual int GetBytes() = 0;
        virtual const std::string* GetColumns() = 0;

        T ConvertFromArray(const bfast::byte* bytes)
        {
            T result;
            memcpy(&result, bytes, GetSize() * GetBytes());
            return result;
        }

        void ConvertFromColumns(T* dest, const ByteRangePtr* bytesArrays, int index)
        {
            for (int i = 0; i < GetSize(); i++)
            {
                memcpy(reinterpret_cast<char*>(dest) + GetBytes() * i, bytesArrays[i]->begin() + index * GetBytes(), GetBytes());
            }
        }

        bfast::byte* ConvertToArray(const T& value)
        {
            bfast::byte* result = new bfast::byte[sizeof(T)];
            memcpy(result, &value, sizeof(T));
            return result;
        }
    };

    class Vector2Converter: public Converter<Vector2>
    {
    private:
        const std::string* const COLUMNS = new std::string[2] { ".X", ".Y" };
    public:
        int GetSize() { return 2; }
        int GetBytes() { return sizeof(float); }
        const std::string* GetColumns() { return COLUMNS; }

        ~Vector2Converter()
        {
            delete[] COLUMNS;
        }
    };

    class DVector2Converter: public Converter<DVector2>
    {
    private:
        const std::string* const COLUMNS = new std::string[2] { ".X", ".Y" };
    public:
        int GetSize() { return 2; }
        int GetBytes() { return sizeof(double); }
        const std::string* GetColumns() { return COLUMNS; }

        ~DVector2Converter()
        {
            delete[] COLUMNS;
        }
    };

    class Vector3Converter: public Converter<Vector3>
    {
    private:
        const std::string* const COLUMNS = new std::string[3] { ".X", ".Y", ".Z" };
    public:
        int GetSize() { return 3; }
        int GetBytes() { return sizeof(float); }
        const std::string* GetColumns() { return COLUMNS; }

        ~Vector3Converter()
        {
            delete[] COLUMNS;
        }
    };

    class DVector3Converter: public Converter<DVector3>
    {
    private:
        const std::string* const COLUMNS = new std::string[3] { ".X", ".Y", ".Z" };
    public:
        int GetSize() { return 3; }
        int GetBytes() { return sizeof(double); }
        const std::string* GetColumns() { return COLUMNS; }

        ~DVector3Converter()
        {
            delete[] COLUMNS;
        }
    };

    class DAABox2DConverter: public Converter<DAABox2D>
    {
    private:
        const std::string* const COLUMNS = new std::string[4] { ".Min.X", ".Min.Y", ".Max.X", ".Max.Y" };
    public:
        int GetSize() { return 4; }
        int GetBytes() { return sizeof(double); }
        const std::string* GetColumns() { return COLUMNS; }

        ~DAABox2DConverter()
        {
            delete[] COLUMNS;
        }
    };

    class DAABoxConverter: public Converter<DAABox>
    {
    private:
        const std::string* const COLUMNS = new std::string[6] { ".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z" };
    public:
        int GetSize() { return 6; }
        int GetBytes() { return sizeof(double); }
        const std::string* GetColumns() { return COLUMNS; }

        ~DAABoxConverter()
        {
            delete[] COLUMNS;
        }
    };

    class AABoxConverter: public Converter<AABox>
    {
    private:
        const std::string* const COLUMNS = new std::string[6] { ".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z" };
    public:
        int GetSize() { return 6; }
        int GetBytes() { return sizeof(float); }
        const std::string* GetColumns() { return COLUMNS; }

        ~AABoxConverter()
        {
            delete[] COLUMNS;
        }
    };

    class Asset;
    class AssetTable;
    class DisplayUnit;
    class DisplayUnitTable;
    class ParameterDescriptor;
    class ParameterDescriptorTable;
    class Parameter;
    class ParameterTable;
    class Element;
    class ElementTable;
    class Workset;
    class WorksetTable;
    class AssemblyInstance;
    class AssemblyInstanceTable;
    class Group;
    class GroupTable;
    class DesignOption;
    class DesignOptionTable;
    class Level;
    class LevelTable;
    class Phase;
    class PhaseTable;
    class Room;
    class RoomTable;
    class BimDocument;
    class BimDocumentTable;
    class DisplayUnitInBimDocument;
    class DisplayUnitInBimDocumentTable;
    class PhaseOrderInBimDocument;
    class PhaseOrderInBimDocumentTable;
    class Category;
    class CategoryTable;
    class Family;
    class FamilyTable;
    class FamilyType;
    class FamilyTypeTable;
    class FamilyInstance;
    class FamilyInstanceTable;
    class View;
    class ViewTable;
    class ElementInView;
    class ElementInViewTable;
    class ShapeInView;
    class ShapeInViewTable;
    class AssetInView;
    class AssetInViewTable;
    class LevelInView;
    class LevelInViewTable;
    class Camera;
    class CameraTable;
    class Material;
    class MaterialTable;
    class MaterialInElement;
    class MaterialInElementTable;
    class CompoundStructureLayer;
    class CompoundStructureLayerTable;
    class CompoundStructure;
    class CompoundStructureTable;
    class Node;
    class NodeTable;
    class Geometry;
    class GeometryTable;
    class Shape;
    class ShapeTable;
    class ShapeCollection;
    class ShapeCollectionTable;
    class ShapeInShapeCollection;
    class ShapeInShapeCollectionTable;
    class System;
    class SystemTable;
    class ElementInSystem;
    class ElementInSystemTable;
    class Warning;
    class WarningTable;
    class ElementInWarning;
    class ElementInWarningTable;
    class BasePoint;
    class BasePointTable;
    class PhaseFilter;
    class PhaseFilterTable;
    class Grid;
    class GridTable;
    class Area;
    class AreaTable;
    class AreaScheme;
    class AreaSchemeTable;

    class DocumentModel
    {
    public:
        AssetTable* mAsset;
        DisplayUnitTable* mDisplayUnit;
        ParameterDescriptorTable* mParameterDescriptor;
        ParameterTable* mParameter;
        ElementTable* mElement;
        WorksetTable* mWorkset;
        AssemblyInstanceTable* mAssemblyInstance;
        GroupTable* mGroup;
        DesignOptionTable* mDesignOption;
        LevelTable* mLevel;
        PhaseTable* mPhase;
        RoomTable* mRoom;
        BimDocumentTable* mBimDocument;
        DisplayUnitInBimDocumentTable* mDisplayUnitInBimDocument;
        PhaseOrderInBimDocumentTable* mPhaseOrderInBimDocument;
        CategoryTable* mCategory;
        FamilyTable* mFamily;
        FamilyTypeTable* mFamilyType;
        FamilyInstanceTable* mFamilyInstance;
        ViewTable* mView;
        ElementInViewTable* mElementInView;
        ShapeInViewTable* mShapeInView;
        AssetInViewTable* mAssetInView;
        LevelInViewTable* mLevelInView;
        CameraTable* mCamera;
        MaterialTable* mMaterial;
        MaterialInElementTable* mMaterialInElement;
        CompoundStructureLayerTable* mCompoundStructureLayer;
        CompoundStructureTable* mCompoundStructure;
        NodeTable* mNode;
        GeometryTable* mGeometry;
        ShapeTable* mShape;
        ShapeCollectionTable* mShapeCollection;
        ShapeInShapeCollectionTable* mShapeInShapeCollection;
        SystemTable* mSystem;
        ElementInSystemTable* mElementInSystem;
        WarningTable* mWarning;
        ElementInWarningTable* mElementInWarning;
        BasePointTable* mBasePoint;
        PhaseFilterTable* mPhaseFilter;
        GridTable* mGrid;
        AreaTable* mArea;
        AreaSchemeTable* mAreaScheme;

        DocumentModel(VimScene& scene);
        ~DocumentModel();
    };

    class Asset
    {
    public:
        int mIndex;
        const std::string* mBufferName;

        Asset() {}
    };

    class AssetTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        AssetTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:BufferName") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:BufferName"].size() / sizeof(int);
        }

        Asset* Get(int assetIndex)
        {
            Asset* asset = new Asset();
            asset->mIndex = assetIndex;
            asset->mBufferName = GetBufferName(assetIndex);
            return asset;
        }

        std::vector<Asset>* GetAll()
        {
            bool existsBufferName = mEntityTable.mStringColumns.find("string:BufferName") == mEntityTable.mStringColumns.end();

            const int count = GetCount();

            std::vector<Asset>* asset = new std::vector<Asset>();
            asset->reserve(count);

            const std::vector<int>& bufferNameData = existsBufferName ? mEntityTable.mStringColumns["string:BufferName"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Asset entity;
                entity.mIndex = i;
                if (existsBufferName)
                    entity.mBufferName = &mStrings[bufferNameData[i]];
                asset->push_back(entity);
            }

            return asset;
        }

        const std::string* GetBufferName(int assetIndex)
        {
            if (assetIndex < 0 || assetIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:BufferName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:BufferName"][assetIndex]];
        }

        const std::vector<const std::string*>* GetAllBufferName()
        {
            if (mEntityTable.mStringColumns.find("string:BufferName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& bufferNameData = mEntityTable.mStringColumns["string:BufferName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[bufferNameData[i]]);
            }

            return result;
        }

    };

    static AssetTable* GetAssetTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Asset") == scene.mEntityTables.end())
            return {};

        return new AssetTable(scene.mEntityTables["Vim.Asset"], scene.mStrings);
    }

    class DisplayUnit
    {
    public:
        int mIndex;
        const std::string* mSpec;
        const std::string* mType;
        const std::string* mLabel;

        DisplayUnit() {}
    };

    class DisplayUnitTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        DisplayUnitTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Spec") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Spec"].size() / sizeof(int);
        }

        DisplayUnit* Get(int displayUnitIndex)
        {
            DisplayUnit* displayUnit = new DisplayUnit();
            displayUnit->mIndex = displayUnitIndex;
            displayUnit->mSpec = GetSpec(displayUnitIndex);
            displayUnit->mType = GetType(displayUnitIndex);
            displayUnit->mLabel = GetLabel(displayUnitIndex);
            return displayUnit;
        }

        std::vector<DisplayUnit>* GetAll()
        {
            bool existsSpec = mEntityTable.mStringColumns.find("string:Spec") == mEntityTable.mStringColumns.end();
            bool existsType = mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end();
            bool existsLabel = mEntityTable.mStringColumns.find("string:Label") == mEntityTable.mStringColumns.end();

            const int count = GetCount();

            std::vector<DisplayUnit>* displayUnit = new std::vector<DisplayUnit>();
            displayUnit->reserve(count);

            const std::vector<int>& specData = existsSpec ? mEntityTable.mStringColumns["string:Spec"] : std::vector<int>();

            const std::vector<int>& typeData = existsType ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();

            const std::vector<int>& labelData = existsLabel ? mEntityTable.mStringColumns["string:Label"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                DisplayUnit entity;
                entity.mIndex = i;
                if (existsSpec)
                    entity.mSpec = &mStrings[specData[i]];
                if (existsType)
                    entity.mType = &mStrings[typeData[i]];
                if (existsLabel)
                    entity.mLabel = &mStrings[labelData[i]];
                displayUnit->push_back(entity);
            }

            return displayUnit;
        }

        const std::string* GetSpec(int displayUnitIndex)
        {
            if (displayUnitIndex < 0 || displayUnitIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Spec") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Spec"][displayUnitIndex]];
        }

        const std::vector<const std::string*>* GetAllSpec()
        {
            if (mEntityTable.mStringColumns.find("string:Spec") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& specData = mEntityTable.mStringColumns["string:Spec"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[specData[i]]);
            }

            return result;
        }

        const std::string* GetType(int displayUnitIndex)
        {
            if (displayUnitIndex < 0 || displayUnitIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Type"][displayUnitIndex]];
        }

        const std::vector<const std::string*>* GetAllType()
        {
            if (mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& typeData = mEntityTable.mStringColumns["string:Type"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[typeData[i]]);
            }

            return result;
        }

        const std::string* GetLabel(int displayUnitIndex)
        {
            if (displayUnitIndex < 0 || displayUnitIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Label") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Label"][displayUnitIndex]];
        }

        const std::vector<const std::string*>* GetAllLabel()
        {
            if (mEntityTable.mStringColumns.find("string:Label") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& labelData = mEntityTable.mStringColumns["string:Label"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[labelData[i]]);
            }

            return result;
        }

    };

    static DisplayUnitTable* GetDisplayUnitTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.DisplayUnit") == scene.mEntityTables.end())
            return {};

        return new DisplayUnitTable(scene.mEntityTables["Vim.DisplayUnit"], scene.mStrings);
    }

    class ParameterDescriptor
    {
    public:
        int mIndex;
        const std::string* mName;
        const std::string* mGroup;
        const std::string* mParameterType;
        bool mIsInstance;
        bool mIsShared;
        bool mIsReadOnly;
        int mFlags;
        const std::string* mGuid;

        int mDisplayUnitIndex;
        DisplayUnit* mDisplayUnit;

        ParameterDescriptor() {}
    };

    class ParameterDescriptorTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ParameterDescriptorTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Name"].size() / sizeof(int);
        }

        ParameterDescriptor* Get(int parameterDescriptorIndex)
        {
            ParameterDescriptor* parameterDescriptor = new ParameterDescriptor();
            parameterDescriptor->mIndex = parameterDescriptorIndex;
            parameterDescriptor->mName = GetName(parameterDescriptorIndex);
            parameterDescriptor->mGroup = GetGroup(parameterDescriptorIndex);
            parameterDescriptor->mParameterType = GetParameterType(parameterDescriptorIndex);
            parameterDescriptor->mIsInstance = GetIsInstance(parameterDescriptorIndex);
            parameterDescriptor->mIsShared = GetIsShared(parameterDescriptorIndex);
            parameterDescriptor->mIsReadOnly = GetIsReadOnly(parameterDescriptorIndex);
            parameterDescriptor->mFlags = GetFlags(parameterDescriptorIndex);
            parameterDescriptor->mGuid = GetGuid(parameterDescriptorIndex);
            parameterDescriptor->mDisplayUnitIndex = GetDisplayUnitIndex(parameterDescriptorIndex);
            return parameterDescriptor;
        }

        std::vector<ParameterDescriptor>* GetAll()
        {
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsGroup = mEntityTable.mStringColumns.find("string:Group") == mEntityTable.mStringColumns.end();
            bool existsParameterType = mEntityTable.mStringColumns.find("string:ParameterType") == mEntityTable.mStringColumns.end();
            bool existsIsInstance = mEntityTable.mDataColumns.find("byte:IsInstance") == mEntityTable.mDataColumns.end();
            bool existsIsShared = mEntityTable.mDataColumns.find("byte:IsShared") == mEntityTable.mDataColumns.end();
            bool existsIsReadOnly = mEntityTable.mDataColumns.find("byte:IsReadOnly") == mEntityTable.mDataColumns.end();
            bool existsFlags = mEntityTable.mDataColumns.find("int:Flags") == mEntityTable.mDataColumns.end();
            bool existsGuid = mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end();
            bool existsDisplayUnit = mEntityTable.mIndexColumns.find("index:Vim.DisplayUnit:DisplayUnit") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ParameterDescriptor>* parameterDescriptor = new std::vector<ParameterDescriptor>();
            parameterDescriptor->reserve(count);

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            const std::vector<int>& groupData = existsGroup ? mEntityTable.mStringColumns["string:Group"] : std::vector<int>();

            const std::vector<int>& parameterTypeData = existsParameterType ? mEntityTable.mStringColumns["string:ParameterType"] : std::vector<int>();

            bfast::byte* isInstanceData = new bfast::byte[count];
            if (existsIsInstance) memcpy(isInstanceData, mEntityTable.mDataColumns["byte:IsInstance"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isSharedData = new bfast::byte[count];
            if (existsIsShared) memcpy(isSharedData, mEntityTable.mDataColumns["byte:IsShared"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isReadOnlyData = new bfast::byte[count];
            if (existsIsReadOnly) memcpy(isReadOnlyData, mEntityTable.mDataColumns["byte:IsReadOnly"].begin(), count * sizeof(bfast::byte));

            int* flagsData = new int[count];
            if (existsFlags) memcpy(flagsData, mEntityTable.mDataColumns["int:Flags"].begin(), count * sizeof(int));

            const std::vector<int>& guidData = existsGuid ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();

            const std::vector<int>& displayUnitData = existsDisplayUnit ? mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ParameterDescriptor entity;
                entity.mIndex = i;
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsGroup)
                    entity.mGroup = &mStrings[groupData[i]];
                if (existsParameterType)
                    entity.mParameterType = &mStrings[parameterTypeData[i]];
                if (existsIsInstance)
                    entity.mIsInstance = isInstanceData[i];
                if (existsIsShared)
                    entity.mIsShared = isSharedData[i];
                if (existsIsReadOnly)
                    entity.mIsReadOnly = isReadOnlyData[i];
                if (existsFlags)
                    entity.mFlags = flagsData[i];
                if (existsGuid)
                    entity.mGuid = &mStrings[guidData[i]];
                entity.mDisplayUnitIndex = existsDisplayUnit ? displayUnitData[i] : -1;
                parameterDescriptor->push_back(entity);
            }

            delete[] isInstanceData;
            delete[] isSharedData;
            delete[] isReadOnlyData;
            delete[] flagsData;

            return parameterDescriptor;
        }

        const std::string* GetName(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][parameterDescriptorIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        const std::string* GetGroup(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Group") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Group"][parameterDescriptorIndex]];
        }

        const std::vector<const std::string*>* GetAllGroup()
        {
            if (mEntityTable.mStringColumns.find("string:Group") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& groupData = mEntityTable.mStringColumns["string:Group"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[groupData[i]]);
            }

            return result;
        }

        const std::string* GetParameterType(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:ParameterType") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:ParameterType"][parameterDescriptorIndex]];
        }

        const std::vector<const std::string*>* GetAllParameterType()
        {
            if (mEntityTable.mStringColumns.find("string:ParameterType") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& parameterTypeData = mEntityTable.mStringColumns["string:ParameterType"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[parameterTypeData[i]]);
            }

            return result;
        }

        bool GetIsInstance(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsInstance") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsInstance"].begin() + parameterDescriptorIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsInstance()
        {
            if (mEntityTable.mDataColumns.find("byte:IsInstance") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isInstanceData = new bfast::byte[count];
            memcpy(isInstanceData, mEntityTable.mDataColumns["byte:IsInstance"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isInstanceData, isInstanceData + count);

            delete[] isInstanceData;

            return result;
        }

        bool GetIsShared(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsShared") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsShared"].begin() + parameterDescriptorIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsShared()
        {
            if (mEntityTable.mDataColumns.find("byte:IsShared") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isSharedData = new bfast::byte[count];
            memcpy(isSharedData, mEntityTable.mDataColumns["byte:IsShared"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isSharedData, isSharedData + count);

            delete[] isSharedData;

            return result;
        }

        bool GetIsReadOnly(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsReadOnly") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsReadOnly"].begin() + parameterDescriptorIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsReadOnly()
        {
            if (mEntityTable.mDataColumns.find("byte:IsReadOnly") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isReadOnlyData = new bfast::byte[count];
            memcpy(isReadOnlyData, mEntityTable.mDataColumns["byte:IsReadOnly"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isReadOnlyData, isReadOnlyData + count);

            delete[] isReadOnlyData;

            return result;
        }

        int GetFlags(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Flags") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Flags"].begin() + parameterDescriptorIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllFlags()
        {
            if (mEntityTable.mDataColumns.find("int:Flags") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* flagsData = new int[count];
            memcpy(flagsData, mEntityTable.mDataColumns["int:Flags"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(flagsData, flagsData + count);

            delete[] flagsData;

            return result;
        }

        const std::string* GetGuid(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Guid"][parameterDescriptorIndex]];
        }

        const std::vector<const std::string*>* GetAllGuid()
        {
            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& guidData = mEntityTable.mStringColumns["string:Guid"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[guidData[i]]);
            }

            return result;
        }

        int GetDisplayUnitIndex(int parameterDescriptorIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.DisplayUnit:DisplayUnit") == mEntityTable.mIndexColumns.end())
                return -1;

            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"][parameterDescriptorIndex];
        }

    };

    static ParameterDescriptorTable* GetParameterDescriptorTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ParameterDescriptor") == scene.mEntityTables.end())
            return {};

        return new ParameterDescriptorTable(scene.mEntityTables["Vim.ParameterDescriptor"], scene.mStrings);
    }

    class Parameter
    {
    public:
        int mIndex;
        const std::string* mValue;

        int mParameterDescriptorIndex;
        ParameterDescriptor* mParameterDescriptor;
        int mElementIndex;
        Element* mElement;

        Parameter() {}
    };

    class ParameterTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ParameterTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Value") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Value"].size() / sizeof(int);
        }

        Parameter* Get(int parameterIndex)
        {
            Parameter* parameter = new Parameter();
            parameter->mIndex = parameterIndex;
            parameter->mValue = GetValue(parameterIndex);
            parameter->mParameterDescriptorIndex = GetParameterDescriptorIndex(parameterIndex);
            parameter->mElementIndex = GetElementIndex(parameterIndex);
            return parameter;
        }

        std::vector<Parameter>* GetAll()
        {
            bool existsValue = mEntityTable.mStringColumns.find("string:Value") == mEntityTable.mStringColumns.end();
            bool existsParameterDescriptor = mEntityTable.mIndexColumns.find("index:Vim.ParameterDescriptor:ParameterDescriptor") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Parameter>* parameter = new std::vector<Parameter>();
            parameter->reserve(count);

            const std::vector<int>& valueData = existsValue ? mEntityTable.mStringColumns["string:Value"] : std::vector<int>();

            const std::vector<int>& parameterDescriptorData = existsParameterDescriptor ? mEntityTable.mIndexColumns["index:Vim.ParameterDescriptor:ParameterDescriptor"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Parameter entity;
                entity.mIndex = i;
                if (existsValue)
                    entity.mValue = &mStrings[valueData[i]];
                entity.mParameterDescriptorIndex = existsParameterDescriptor ? parameterDescriptorData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                parameter->push_back(entity);
            }

            return parameter;
        }

        const std::string* GetValue(int parameterIndex)
        {
            if (parameterIndex < 0 || parameterIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Value") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Value"][parameterIndex]];
        }

        const std::vector<const std::string*>* GetAllValue()
        {
            if (mEntityTable.mStringColumns.find("string:Value") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& valueData = mEntityTable.mStringColumns["string:Value"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[valueData[i]]);
            }

            return result;
        }

        int GetParameterDescriptorIndex(int parameterIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.ParameterDescriptor:ParameterDescriptor") == mEntityTable.mIndexColumns.end())
                return -1;

            if (parameterIndex < 0 || parameterIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.ParameterDescriptor:ParameterDescriptor"][parameterIndex];
        }

        int GetElementIndex(int parameterIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (parameterIndex < 0 || parameterIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][parameterIndex];
        }

    };

    static ParameterTable* GetParameterTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Parameter") == scene.mEntityTables.end())
            return {};

        return new ParameterTable(scene.mEntityTables["Vim.Parameter"], scene.mStrings);
    }

    class Element
    {
    public:
        int mIndex;
        int mId;
        const std::string* mType;
        const std::string* mName;
        const std::string* mUniqueId;
        Vector3 mLocation;
        const std::string* mFamilyName;
        bool mIsPinned;

        int mLevelIndex;
        Level* mLevel;
        int mPhaseCreatedIndex;
        Phase* mPhaseCreated;
        int mPhaseDemolishedIndex;
        Phase* mPhaseDemolished;
        int mCategoryIndex;
        Category* mCategory;
        int mWorksetIndex;
        Workset* mWorkset;
        int mDesignOptionIndex;
        DesignOption* mDesignOption;
        int mOwnerViewIndex;
        View* mOwnerView;
        int mGroupIndex;
        Group* mGroup;
        int mAssemblyInstanceIndex;
        AssemblyInstance* mAssemblyInstance;
        int mBimDocumentIndex;
        BimDocument* mBimDocument;
        int mRoomIndex;
        Room* mRoom;

        Element() {}
    };

    class ElementTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ElementTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:Id"].size() / sizeof(int);
        }

        Element* Get(int elementIndex)
        {
            Element* element = new Element();
            element->mIndex = elementIndex;
            element->mId = GetId(elementIndex);
            element->mType = GetType(elementIndex);
            element->mName = GetName(elementIndex);
            element->mUniqueId = GetUniqueId(elementIndex);
            element->mLocation = GetLocation(elementIndex);
            element->mFamilyName = GetFamilyName(elementIndex);
            element->mIsPinned = GetIsPinned(elementIndex);
            element->mLevelIndex = GetLevelIndex(elementIndex);
            element->mPhaseCreatedIndex = GetPhaseCreatedIndex(elementIndex);
            element->mPhaseDemolishedIndex = GetPhaseDemolishedIndex(elementIndex);
            element->mCategoryIndex = GetCategoryIndex(elementIndex);
            element->mWorksetIndex = GetWorksetIndex(elementIndex);
            element->mDesignOptionIndex = GetDesignOptionIndex(elementIndex);
            element->mOwnerViewIndex = GetOwnerViewIndex(elementIndex);
            element->mGroupIndex = GetGroupIndex(elementIndex);
            element->mAssemblyInstanceIndex = GetAssemblyInstanceIndex(elementIndex);
            element->mBimDocumentIndex = GetBimDocumentIndex(elementIndex);
            element->mRoomIndex = GetRoomIndex(elementIndex);
            return element;
        }

        std::vector<Element>* GetAll()
        {
            bool existsId = mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end();
            bool existsType = mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end();
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsUniqueId = mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end();
            bool existsLocationX = mEntityTable.mDataColumns.find("float:Location.X") == mEntityTable.mDataColumns.end();
            bool existsLocationY = mEntityTable.mDataColumns.find("float:Location.Y") == mEntityTable.mDataColumns.end();
            bool existsLocationZ = mEntityTable.mDataColumns.find("float:Location.Z") == mEntityTable.mDataColumns.end();
            bool existsFamilyName = mEntityTable.mStringColumns.find("string:FamilyName") == mEntityTable.mStringColumns.end();
            bool existsIsPinned = mEntityTable.mDataColumns.find("byte:IsPinned") == mEntityTable.mDataColumns.end();
            bool existsLevel = mEntityTable.mIndexColumns.find("index:Vim.Level:Level") == mEntityTable.mIndexColumns.end();
            bool existsPhaseCreated = mEntityTable.mIndexColumns.find("index:Vim.Phase:PhaseCreated") == mEntityTable.mIndexColumns.end();
            bool existsPhaseDemolished = mEntityTable.mIndexColumns.find("index:Vim.Phase:PhaseDemolished") == mEntityTable.mIndexColumns.end();
            bool existsCategory = mEntityTable.mIndexColumns.find("index:Vim.Category:Category") == mEntityTable.mIndexColumns.end();
            bool existsWorkset = mEntityTable.mIndexColumns.find("index:Vim.Workset:Workset") == mEntityTable.mIndexColumns.end();
            bool existsDesignOption = mEntityTable.mIndexColumns.find("index:Vim.DesignOption:DesignOption") == mEntityTable.mIndexColumns.end();
            bool existsOwnerView = mEntityTable.mIndexColumns.find("index:Vim.View:OwnerView") == mEntityTable.mIndexColumns.end();
            bool existsGroup = mEntityTable.mIndexColumns.find("index:Vim.Group:Group") == mEntityTable.mIndexColumns.end();
            bool existsAssemblyInstance = mEntityTable.mIndexColumns.find("index:Vim.AssemblyInstance:AssemblyInstance") == mEntityTable.mIndexColumns.end();
            bool existsBimDocument = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end();
            bool existsRoom = mEntityTable.mIndexColumns.find("index:Vim.Room:Room") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Element>* element = new std::vector<Element>();
            element->reserve(count);

            int* idData = new int[count];
            if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            const std::vector<int>& typeData = existsType ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            const std::vector<int>& uniqueIdData = existsUniqueId ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();

            Vector3Converter locationConverter;
            ByteRangePtr* locationData = new ByteRangePtr[locationConverter.GetSize()];
            if (existsLocationX && existsLocationY && existsLocationZ) for (int i = 0; i < locationConverter.GetSize(); i++)
                    locationData[i] = &mEntityTable.mDataColumns["float:Location" + locationConverter.GetColumns()[i]];

            const std::vector<int>& familyNameData = existsFamilyName ? mEntityTable.mStringColumns["string:FamilyName"] : std::vector<int>();

            bfast::byte* isPinnedData = new bfast::byte[count];
            if (existsIsPinned) memcpy(isPinnedData, mEntityTable.mDataColumns["byte:IsPinned"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& levelData = existsLevel ? mEntityTable.mIndexColumns["index:Vim.Level:Level"] : std::vector<int>();
            const std::vector<int>& phaseCreatedData = existsPhaseCreated ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseCreated"] : std::vector<int>();
            const std::vector<int>& phaseDemolishedData = existsPhaseDemolished ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseDemolished"] : std::vector<int>();
            const std::vector<int>& categoryData = existsCategory ? mEntityTable.mIndexColumns["index:Vim.Category:Category"] : std::vector<int>();
            const std::vector<int>& worksetData = existsWorkset ? mEntityTable.mIndexColumns["index:Vim.Workset:Workset"] : std::vector<int>();
            const std::vector<int>& designOptionData = existsDesignOption ? mEntityTable.mIndexColumns["index:Vim.DesignOption:DesignOption"] : std::vector<int>();
            const std::vector<int>& ownerViewData = existsOwnerView ? mEntityTable.mIndexColumns["index:Vim.View:OwnerView"] : std::vector<int>();
            const std::vector<int>& groupData = existsGroup ? mEntityTable.mIndexColumns["index:Vim.Group:Group"] : std::vector<int>();
            const std::vector<int>& assemblyInstanceData = existsAssemblyInstance ? mEntityTable.mIndexColumns["index:Vim.AssemblyInstance:AssemblyInstance"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            const std::vector<int>& roomData = existsRoom ? mEntityTable.mIndexColumns["index:Vim.Room:Room"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Element entity;
                entity.mIndex = i;
                if (existsId)
                    entity.mId = idData[i];
                if (existsType)
                    entity.mType = &mStrings[typeData[i]];
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsUniqueId)
                    entity.mUniqueId = &mStrings[uniqueIdData[i]];
                if (existsLocationX && existsLocationY && existsLocationZ)
                    locationConverter.ConvertFromColumns(&entity.mLocation, locationData, i);
                if (existsFamilyName)
                    entity.mFamilyName = &mStrings[familyNameData[i]];
                if (existsIsPinned)
                    entity.mIsPinned = isPinnedData[i];
                entity.mLevelIndex = existsLevel ? levelData[i] : -1;
                entity.mPhaseCreatedIndex = existsPhaseCreated ? phaseCreatedData[i] : -1;
                entity.mPhaseDemolishedIndex = existsPhaseDemolished ? phaseDemolishedData[i] : -1;
                entity.mCategoryIndex = existsCategory ? categoryData[i] : -1;
                entity.mWorksetIndex = existsWorkset ? worksetData[i] : -1;
                entity.mDesignOptionIndex = existsDesignOption ? designOptionData[i] : -1;
                entity.mOwnerViewIndex = existsOwnerView ? ownerViewData[i] : -1;
                entity.mGroupIndex = existsGroup ? groupData[i] : -1;
                entity.mAssemblyInstanceIndex = existsAssemblyInstance ? assemblyInstanceData[i] : -1;
                entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
                entity.mRoomIndex = existsRoom ? roomData[i] : -1;
                element->push_back(entity);
            }

            delete[] idData;
            delete[] locationData;
            delete[] isPinnedData;

            return element;
        }

        int GetId(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + elementIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllId()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* idData = new int[count];
            memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(idData, idData + count);

            delete[] idData;

            return result;
        }

        const std::string* GetType(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Type"][elementIndex]];
        }

        const std::vector<const std::string*>* GetAllType()
        {
            if (mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& typeData = mEntityTable.mStringColumns["string:Type"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[typeData[i]]);
            }

            return result;
        }

        const std::string* GetName(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][elementIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        const std::string* GetUniqueId(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:UniqueId"][elementIndex]];
        }

        const std::vector<const std::string*>* GetAllUniqueId()
        {
            if (mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& uniqueIdData = mEntityTable.mStringColumns["string:UniqueId"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[uniqueIdData[i]]);
            }

            return result;
        }

        Vector3 GetLocation(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Location.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Location.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Location.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter locationConverter;
            bfast::byte* bytes = new bfast::byte[locationConverter.GetSize() * locationConverter.GetBytes()];
            for (int i = 0; i < locationConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * locationConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:Location" + locationConverter.GetColumns()[i]].begin()
                       + elementIndex * locationConverter.GetBytes(),
                       locationConverter.GetBytes());
            }
            Vector3 location = locationConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return location;
        }

        const std::vector<Vector3>* GetAllLocation()
        {
            if (mEntityTable.mDataColumns.find("float:Location.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Location.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Location.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter locationConverter;
            ByteRangePtr* locationData = new ByteRangePtr[locationConverter.GetSize()];
            for (int i = 0; i < locationConverter.GetSize(); i++)
                locationData[i] = &mEntityTable.mDataColumns["float:Location" + locationConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                locationConverter.ConvertFromColumns(&value, locationData, i);
                result->push_back(value);
            }

            delete[] locationData;

            return result;
        }

        const std::string* GetFamilyName(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:FamilyName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:FamilyName"][elementIndex]];
        }

        const std::vector<const std::string*>* GetAllFamilyName()
        {
            if (mEntityTable.mStringColumns.find("string:FamilyName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& familyNameData = mEntityTable.mStringColumns["string:FamilyName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[familyNameData[i]]);
            }

            return result;
        }

        bool GetIsPinned(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsPinned") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPinned"].begin() + elementIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsPinned()
        {
            if (mEntityTable.mDataColumns.find("byte:IsPinned") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isPinnedData = new bfast::byte[count];
            memcpy(isPinnedData, mEntityTable.mDataColumns["byte:IsPinned"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isPinnedData, isPinnedData + count);

            delete[] isPinnedData;

            return result;
        }

        int GetLevelIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Level:Level") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Level:Level"][elementIndex];
        }

        int GetPhaseCreatedIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Phase:PhaseCreated") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Phase:PhaseCreated"][elementIndex];
        }

        int GetPhaseDemolishedIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Phase:PhaseDemolished") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Phase:PhaseDemolished"][elementIndex];
        }

        int GetCategoryIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Category:Category") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Category:Category"][elementIndex];
        }

        int GetWorksetIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Workset:Workset") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Workset:Workset"][elementIndex];
        }

        int GetDesignOptionIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.DesignOption:DesignOption") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.DesignOption:DesignOption"][elementIndex];
        }

        int GetOwnerViewIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:OwnerView") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:OwnerView"][elementIndex];
        }

        int GetGroupIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Group:Group") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Group:Group"][elementIndex];
        }

        int GetAssemblyInstanceIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.AssemblyInstance:AssemblyInstance") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.AssemblyInstance:AssemblyInstance"][elementIndex];
        }

        int GetBimDocumentIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][elementIndex];
        }

        int GetRoomIndex(int elementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Room:Room") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Room:Room"][elementIndex];
        }

    };

    static ElementTable* GetElementTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Element") == scene.mEntityTables.end())
            return {};

        return new ElementTable(scene.mEntityTables["Vim.Element"], scene.mStrings);
    }

    class Workset
    {
    public:
        int mIndex;
        int mId;
        const std::string* mName;
        const std::string* mKind;
        bool mIsOpen;
        bool mIsEditable;
        const std::string* mOwner;
        const std::string* mUniqueId;

        int mBimDocumentIndex;
        BimDocument* mBimDocument;

        Workset() {}
    };

    class WorksetTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        WorksetTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:Id"].size() / sizeof(int);
        }

        Workset* Get(int worksetIndex)
        {
            Workset* workset = new Workset();
            workset->mIndex = worksetIndex;
            workset->mId = GetId(worksetIndex);
            workset->mName = GetName(worksetIndex);
            workset->mKind = GetKind(worksetIndex);
            workset->mIsOpen = GetIsOpen(worksetIndex);
            workset->mIsEditable = GetIsEditable(worksetIndex);
            workset->mOwner = GetOwner(worksetIndex);
            workset->mUniqueId = GetUniqueId(worksetIndex);
            workset->mBimDocumentIndex = GetBimDocumentIndex(worksetIndex);
            return workset;
        }

        std::vector<Workset>* GetAll()
        {
            bool existsId = mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end();
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsKind = mEntityTable.mStringColumns.find("string:Kind") == mEntityTable.mStringColumns.end();
            bool existsIsOpen = mEntityTable.mDataColumns.find("byte:IsOpen") == mEntityTable.mDataColumns.end();
            bool existsIsEditable = mEntityTable.mDataColumns.find("byte:IsEditable") == mEntityTable.mDataColumns.end();
            bool existsOwner = mEntityTable.mStringColumns.find("string:Owner") == mEntityTable.mStringColumns.end();
            bool existsUniqueId = mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end();
            bool existsBimDocument = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Workset>* workset = new std::vector<Workset>();
            workset->reserve(count);

            int* idData = new int[count];
            if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            const std::vector<int>& kindData = existsKind ? mEntityTable.mStringColumns["string:Kind"] : std::vector<int>();

            bfast::byte* isOpenData = new bfast::byte[count];
            if (existsIsOpen) memcpy(isOpenData, mEntityTable.mDataColumns["byte:IsOpen"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isEditableData = new bfast::byte[count];
            if (existsIsEditable) memcpy(isEditableData, mEntityTable.mDataColumns["byte:IsEditable"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& ownerData = existsOwner ? mEntityTable.mStringColumns["string:Owner"] : std::vector<int>();

            const std::vector<int>& uniqueIdData = existsUniqueId ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();

            const std::vector<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Workset entity;
                entity.mIndex = i;
                if (existsId)
                    entity.mId = idData[i];
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsKind)
                    entity.mKind = &mStrings[kindData[i]];
                if (existsIsOpen)
                    entity.mIsOpen = isOpenData[i];
                if (existsIsEditable)
                    entity.mIsEditable = isEditableData[i];
                if (existsOwner)
                    entity.mOwner = &mStrings[ownerData[i]];
                if (existsUniqueId)
                    entity.mUniqueId = &mStrings[uniqueIdData[i]];
                entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
                workset->push_back(entity);
            }

            delete[] idData;
            delete[] isOpenData;
            delete[] isEditableData;

            return workset;
        }

        int GetId(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + worksetIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllId()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* idData = new int[count];
            memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(idData, idData + count);

            delete[] idData;

            return result;
        }

        const std::string* GetName(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][worksetIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        const std::string* GetKind(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Kind") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Kind"][worksetIndex]];
        }

        const std::vector<const std::string*>* GetAllKind()
        {
            if (mEntityTable.mStringColumns.find("string:Kind") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& kindData = mEntityTable.mStringColumns["string:Kind"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[kindData[i]]);
            }

            return result;
        }

        bool GetIsOpen(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsOpen") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsOpen"].begin() + worksetIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsOpen()
        {
            if (mEntityTable.mDataColumns.find("byte:IsOpen") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isOpenData = new bfast::byte[count];
            memcpy(isOpenData, mEntityTable.mDataColumns["byte:IsOpen"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isOpenData, isOpenData + count);

            delete[] isOpenData;

            return result;
        }

        bool GetIsEditable(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsEditable") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsEditable"].begin() + worksetIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsEditable()
        {
            if (mEntityTable.mDataColumns.find("byte:IsEditable") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isEditableData = new bfast::byte[count];
            memcpy(isEditableData, mEntityTable.mDataColumns["byte:IsEditable"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isEditableData, isEditableData + count);

            delete[] isEditableData;

            return result;
        }

        const std::string* GetOwner(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Owner") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Owner"][worksetIndex]];
        }

        const std::vector<const std::string*>* GetAllOwner()
        {
            if (mEntityTable.mStringColumns.find("string:Owner") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& ownerData = mEntityTable.mStringColumns["string:Owner"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[ownerData[i]]);
            }

            return result;
        }

        const std::string* GetUniqueId(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:UniqueId"][worksetIndex]];
        }

        const std::vector<const std::string*>* GetAllUniqueId()
        {
            if (mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& uniqueIdData = mEntityTable.mStringColumns["string:UniqueId"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[uniqueIdData[i]]);
            }

            return result;
        }

        int GetBimDocumentIndex(int worksetIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end())
                return -1;

            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][worksetIndex];
        }

    };

    static WorksetTable* GetWorksetTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Workset") == scene.mEntityTables.end())
            return {};

        return new WorksetTable(scene.mEntityTables["Vim.Workset"], scene.mStrings);
    }

    class AssemblyInstance
    {
    public:
        int mIndex;
        const std::string* mAssemblyTypeName;
        Vector3 mPosition;

        int mElementIndex;
        Element* mElement;

        AssemblyInstance() {}
    };

    class AssemblyInstanceTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        AssemblyInstanceTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:AssemblyTypeName") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:AssemblyTypeName"].size() / sizeof(int);
        }

        AssemblyInstance* Get(int assemblyInstanceIndex)
        {
            AssemblyInstance* assemblyInstance = new AssemblyInstance();
            assemblyInstance->mIndex = assemblyInstanceIndex;
            assemblyInstance->mAssemblyTypeName = GetAssemblyTypeName(assemblyInstanceIndex);
            assemblyInstance->mPosition = GetPosition(assemblyInstanceIndex);
            assemblyInstance->mElementIndex = GetElementIndex(assemblyInstanceIndex);
            return assemblyInstance;
        }

        std::vector<AssemblyInstance>* GetAll()
        {
            bool existsAssemblyTypeName = mEntityTable.mStringColumns.find("string:AssemblyTypeName") == mEntityTable.mStringColumns.end();
            bool existsPositionX = mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end();
            bool existsPositionY = mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end();
            bool existsPositionZ = mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<AssemblyInstance>* assemblyInstance = new std::vector<AssemblyInstance>();
            assemblyInstance->reserve(count);

            const std::vector<int>& assemblyTypeNameData = existsAssemblyTypeName ? mEntityTable.mStringColumns["string:AssemblyTypeName"] : std::vector<int>();

            Vector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            if (existsPositionX && existsPositionY && existsPositionZ) for (int i = 0; i < positionConverter.GetSize(); i++)
                    positionData[i] = &mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]];

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                AssemblyInstance entity;
                entity.mIndex = i;
                if (existsAssemblyTypeName)
                    entity.mAssemblyTypeName = &mStrings[assemblyTypeNameData[i]];
                if (existsPositionX && existsPositionY && existsPositionZ)
                    positionConverter.ConvertFromColumns(&entity.mPosition, positionData, i);
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                assemblyInstance->push_back(entity);
            }

            delete[] positionData;

            return assemblyInstance;
        }

        const std::string* GetAssemblyTypeName(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:AssemblyTypeName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:AssemblyTypeName"][assemblyInstanceIndex]];
        }

        const std::vector<const std::string*>* GetAllAssemblyTypeName()
        {
            if (mEntityTable.mStringColumns.find("string:AssemblyTypeName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& assemblyTypeNameData = mEntityTable.mStringColumns["string:AssemblyTypeName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[assemblyTypeNameData[i]]);
            }

            return result;
        }

        Vector3 GetPosition(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter positionConverter;
            bfast::byte* bytes = new bfast::byte[positionConverter.GetSize() * positionConverter.GetBytes()];
            for (int i = 0; i < positionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * positionConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]].begin()
                       + assemblyInstanceIndex * positionConverter.GetBytes(),
                       positionConverter.GetBytes());
            }
            Vector3 position = positionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return position;
        }

        const std::vector<Vector3>* GetAllPosition()
        {
            if (mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            for (int i = 0; i < positionConverter.GetSize(); i++)
                positionData[i] = &mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                positionConverter.ConvertFromColumns(&value, positionData, i);
                result->push_back(value);
            }

            delete[] positionData;

            return result;
        }

        int GetElementIndex(int assemblyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][assemblyInstanceIndex];
        }

    };

    static AssemblyInstanceTable* GetAssemblyInstanceTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.AssemblyInstance") == scene.mEntityTables.end())
            return {};

        return new AssemblyInstanceTable(scene.mEntityTables["Vim.AssemblyInstance"], scene.mStrings);
    }

    class Group
    {
    public:
        int mIndex;
        const std::string* mGroupType;
        Vector3 mPosition;

        int mElementIndex;
        Element* mElement;

        Group() {}
    };

    class GroupTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        GroupTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:GroupType") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:GroupType"].size() / sizeof(int);
        }

        Group* Get(int groupIndex)
        {
            Group* group = new Group();
            group->mIndex = groupIndex;
            group->mGroupType = GetGroupType(groupIndex);
            group->mPosition = GetPosition(groupIndex);
            group->mElementIndex = GetElementIndex(groupIndex);
            return group;
        }

        std::vector<Group>* GetAll()
        {
            bool existsGroupType = mEntityTable.mStringColumns.find("string:GroupType") == mEntityTable.mStringColumns.end();
            bool existsPositionX = mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end();
            bool existsPositionY = mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end();
            bool existsPositionZ = mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Group>* group = new std::vector<Group>();
            group->reserve(count);

            const std::vector<int>& groupTypeData = existsGroupType ? mEntityTable.mStringColumns["string:GroupType"] : std::vector<int>();

            Vector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            if (existsPositionX && existsPositionY && existsPositionZ) for (int i = 0; i < positionConverter.GetSize(); i++)
                    positionData[i] = &mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]];

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Group entity;
                entity.mIndex = i;
                if (existsGroupType)
                    entity.mGroupType = &mStrings[groupTypeData[i]];
                if (existsPositionX && existsPositionY && existsPositionZ)
                    positionConverter.ConvertFromColumns(&entity.mPosition, positionData, i);
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                group->push_back(entity);
            }

            delete[] positionData;

            return group;
        }

        const std::string* GetGroupType(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:GroupType") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:GroupType"][groupIndex]];
        }

        const std::vector<const std::string*>* GetAllGroupType()
        {
            if (mEntityTable.mStringColumns.find("string:GroupType") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& groupTypeData = mEntityTable.mStringColumns["string:GroupType"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[groupTypeData[i]]);
            }

            return result;
        }

        Vector3 GetPosition(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter positionConverter;
            bfast::byte* bytes = new bfast::byte[positionConverter.GetSize() * positionConverter.GetBytes()];
            for (int i = 0; i < positionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * positionConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]].begin()
                       + groupIndex * positionConverter.GetBytes(),
                       positionConverter.GetBytes());
            }
            Vector3 position = positionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return position;
        }

        const std::vector<Vector3>* GetAllPosition()
        {
            if (mEntityTable.mDataColumns.find("float:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            for (int i = 0; i < positionConverter.GetSize(); i++)
                positionData[i] = &mEntityTable.mDataColumns["float:Position" + positionConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                positionConverter.ConvertFromColumns(&value, positionData, i);
                result->push_back(value);
            }

            delete[] positionData;

            return result;
        }

        int GetElementIndex(int groupIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (groupIndex < 0 || groupIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][groupIndex];
        }

    };

    static GroupTable* GetGroupTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Group") == scene.mEntityTables.end())
            return {};

        return new GroupTable(scene.mEntityTables["Vim.Group"], scene.mStrings);
    }

    class DesignOption
    {
    public:
        int mIndex;
        bool mIsPrimary;

        int mElementIndex;
        Element* mElement;

        DesignOption() {}
    };

    class DesignOptionTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        DesignOptionTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("byte:IsPrimary") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["byte:IsPrimary"].size() / sizeof(bfast::byte);
        }

        DesignOption* Get(int designOptionIndex)
        {
            DesignOption* designOption = new DesignOption();
            designOption->mIndex = designOptionIndex;
            designOption->mIsPrimary = GetIsPrimary(designOptionIndex);
            designOption->mElementIndex = GetElementIndex(designOptionIndex);
            return designOption;
        }

        std::vector<DesignOption>* GetAll()
        {
            bool existsIsPrimary = mEntityTable.mDataColumns.find("byte:IsPrimary") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<DesignOption>* designOption = new std::vector<DesignOption>();
            designOption->reserve(count);

            bfast::byte* isPrimaryData = new bfast::byte[count];
            if (existsIsPrimary) memcpy(isPrimaryData, mEntityTable.mDataColumns["byte:IsPrimary"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                DesignOption entity;
                entity.mIndex = i;
                if (existsIsPrimary)
                    entity.mIsPrimary = isPrimaryData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                designOption->push_back(entity);
            }

            delete[] isPrimaryData;

            return designOption;
        }

        bool GetIsPrimary(int designOptionIndex)
        {
            if (designOptionIndex < 0 || designOptionIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsPrimary") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPrimary"].begin() + designOptionIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsPrimary()
        {
            if (mEntityTable.mDataColumns.find("byte:IsPrimary") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isPrimaryData = new bfast::byte[count];
            memcpy(isPrimaryData, mEntityTable.mDataColumns["byte:IsPrimary"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isPrimaryData, isPrimaryData + count);

            delete[] isPrimaryData;

            return result;
        }

        int GetElementIndex(int designOptionIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (designOptionIndex < 0 || designOptionIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][designOptionIndex];
        }

    };

    static DesignOptionTable* GetDesignOptionTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.DesignOption") == scene.mEntityTables.end())
            return {};

        return new DesignOptionTable(scene.mEntityTables["Vim.DesignOption"], scene.mStrings);
    }

    class Level
    {
    public:
        int mIndex;
        double mElevation;

        int mElementIndex;
        Element* mElement;

        Level() {}
    };

    class LevelTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        LevelTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:Elevation"].size() / sizeof(double);
        }

        Level* Get(int levelIndex)
        {
            Level* level = new Level();
            level->mIndex = levelIndex;
            level->mElevation = GetElevation(levelIndex);
            level->mElementIndex = GetElementIndex(levelIndex);
            return level;
        }

        std::vector<Level>* GetAll()
        {
            bool existsElevation = mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Level>* level = new std::vector<Level>();
            level->reserve(count);

            double* elevationData = new double[count];
            if (existsElevation) memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Level entity;
                entity.mIndex = i;
                if (existsElevation)
                    entity.mElevation = elevationData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                level->push_back(entity);
            }

            delete[] elevationData;

            return level;
        }

        double GetElevation(int levelIndex)
        {
            if (levelIndex < 0 || levelIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Elevation"].begin() + levelIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllElevation()
        {
            if (mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* elevationData = new double[count];
            memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(elevationData, elevationData + count);

            delete[] elevationData;

            return result;
        }

        int GetElementIndex(int levelIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (levelIndex < 0 || levelIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][levelIndex];
        }

    };

    static LevelTable* GetLevelTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Level") == scene.mEntityTables.end())
            return {};

        return new LevelTable(scene.mEntityTables["Vim.Level"], scene.mStrings);
    }

    class Phase
    {
    public:
        int mIndex;

        int mElementIndex;
        Element* mElement;

        Phase() {}
    };

    class PhaseTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        PhaseTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"].size() / sizeof(int);
        }

        Phase* Get(int phaseIndex)
        {
            Phase* phase = new Phase();
            phase->mIndex = phaseIndex;
            phase->mElementIndex = GetElementIndex(phaseIndex);
            return phase;
        }

        std::vector<Phase>* GetAll()
        {
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Phase>* phase = new std::vector<Phase>();
            phase->reserve(count);

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Phase entity;
                entity.mIndex = i;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                phase->push_back(entity);
            }

            return phase;
        }

        int GetElementIndex(int phaseIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (phaseIndex < 0 || phaseIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][phaseIndex];
        }

    };

    static PhaseTable* GetPhaseTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Phase") == scene.mEntityTables.end())
            return {};

        return new PhaseTable(scene.mEntityTables["Vim.Phase"], scene.mStrings);
    }

    class Room
    {
    public:
        int mIndex;
        double mBaseOffset;
        double mLimitOffset;
        double mUnboundedHeight;
        double mVolume;
        double mPerimeter;
        double mArea;
        const std::string* mNumber;

        int mUpperLimitIndex;
        Level* mUpperLimit;
        int mElementIndex;
        Element* mElement;

        Room() {}
    };

    class RoomTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        RoomTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("double:BaseOffset") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:BaseOffset"].size() / sizeof(double);
        }

        Room* Get(int roomIndex)
        {
            Room* room = new Room();
            room->mIndex = roomIndex;
            room->mBaseOffset = GetBaseOffset(roomIndex);
            room->mLimitOffset = GetLimitOffset(roomIndex);
            room->mUnboundedHeight = GetUnboundedHeight(roomIndex);
            room->mVolume = GetVolume(roomIndex);
            room->mPerimeter = GetPerimeter(roomIndex);
            room->mArea = GetArea(roomIndex);
            room->mNumber = GetNumber(roomIndex);
            room->mUpperLimitIndex = GetUpperLimitIndex(roomIndex);
            room->mElementIndex = GetElementIndex(roomIndex);
            return room;
        }

        std::vector<Room>* GetAll()
        {
            bool existsBaseOffset = mEntityTable.mDataColumns.find("double:BaseOffset") == mEntityTable.mDataColumns.end();
            bool existsLimitOffset = mEntityTable.mDataColumns.find("double:LimitOffset") == mEntityTable.mDataColumns.end();
            bool existsUnboundedHeight = mEntityTable.mDataColumns.find("double:UnboundedHeight") == mEntityTable.mDataColumns.end();
            bool existsVolume = mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end();
            bool existsPerimeter = mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end();
            bool existsArea = mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end();
            bool existsNumber = mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end();
            bool existsUpperLimit = mEntityTable.mIndexColumns.find("index:Vim.Level:UpperLimit") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Room>* room = new std::vector<Room>();
            room->reserve(count);

            double* baseOffsetData = new double[count];
            if (existsBaseOffset) memcpy(baseOffsetData, mEntityTable.mDataColumns["double:BaseOffset"].begin(), count * sizeof(double));

            double* limitOffsetData = new double[count];
            if (existsLimitOffset) memcpy(limitOffsetData, mEntityTable.mDataColumns["double:LimitOffset"].begin(), count * sizeof(double));

            double* unboundedHeightData = new double[count];
            if (existsUnboundedHeight) memcpy(unboundedHeightData, mEntityTable.mDataColumns["double:UnboundedHeight"].begin(), count * sizeof(double));

            double* volumeData = new double[count];
            if (existsVolume) memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));

            double* perimeterData = new double[count];
            if (existsPerimeter) memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));

            double* areaData = new double[count];
            if (existsArea) memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));

            const std::vector<int>& numberData = existsNumber ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();

            const std::vector<int>& upperLimitData = existsUpperLimit ? mEntityTable.mIndexColumns["index:Vim.Level:UpperLimit"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Room entity;
                entity.mIndex = i;
                if (existsBaseOffset)
                    entity.mBaseOffset = baseOffsetData[i];
                if (existsLimitOffset)
                    entity.mLimitOffset = limitOffsetData[i];
                if (existsUnboundedHeight)
                    entity.mUnboundedHeight = unboundedHeightData[i];
                if (existsVolume)
                    entity.mVolume = volumeData[i];
                if (existsPerimeter)
                    entity.mPerimeter = perimeterData[i];
                if (existsArea)
                    entity.mArea = areaData[i];
                if (existsNumber)
                    entity.mNumber = &mStrings[numberData[i]];
                entity.mUpperLimitIndex = existsUpperLimit ? upperLimitData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                room->push_back(entity);
            }

            delete[] baseOffsetData;
            delete[] limitOffsetData;
            delete[] unboundedHeightData;
            delete[] volumeData;
            delete[] perimeterData;
            delete[] areaData;

            return room;
        }

        double GetBaseOffset(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:BaseOffset") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:BaseOffset"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllBaseOffset()
        {
            if (mEntityTable.mDataColumns.find("double:BaseOffset") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* baseOffsetData = new double[count];
            memcpy(baseOffsetData, mEntityTable.mDataColumns["double:BaseOffset"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(baseOffsetData, baseOffsetData + count);

            delete[] baseOffsetData;

            return result;
        }

        double GetLimitOffset(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:LimitOffset") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:LimitOffset"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllLimitOffset()
        {
            if (mEntityTable.mDataColumns.find("double:LimitOffset") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* limitOffsetData = new double[count];
            memcpy(limitOffsetData, mEntityTable.mDataColumns["double:LimitOffset"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(limitOffsetData, limitOffsetData + count);

            delete[] limitOffsetData;

            return result;
        }

        double GetUnboundedHeight(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:UnboundedHeight") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:UnboundedHeight"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllUnboundedHeight()
        {
            if (mEntityTable.mDataColumns.find("double:UnboundedHeight") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* unboundedHeightData = new double[count];
            memcpy(unboundedHeightData, mEntityTable.mDataColumns["double:UnboundedHeight"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(unboundedHeightData, unboundedHeightData + count);

            delete[] unboundedHeightData;

            return result;
        }

        double GetVolume(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Volume"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllVolume()
        {
            if (mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* volumeData = new double[count];
            memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(volumeData, volumeData + count);

            delete[] volumeData;

            return result;
        }

        double GetPerimeter(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Perimeter"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllPerimeter()
        {
            if (mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* perimeterData = new double[count];
            memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(perimeterData, perimeterData + count);

            delete[] perimeterData;

            return result;
        }

        double GetArea(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Area"].begin() + roomIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllArea()
        {
            if (mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* areaData = new double[count];
            memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(areaData, areaData + count);

            delete[] areaData;

            return result;
        }

        const std::string* GetNumber(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Number"][roomIndex]];
        }

        const std::vector<const std::string*>* GetAllNumber()
        {
            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& numberData = mEntityTable.mStringColumns["string:Number"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[numberData[i]]);
            }

            return result;
        }

        int GetUpperLimitIndex(int roomIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Level:UpperLimit") == mEntityTable.mIndexColumns.end())
                return -1;

            if (roomIndex < 0 || roomIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Level:UpperLimit"][roomIndex];
        }

        int GetElementIndex(int roomIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (roomIndex < 0 || roomIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][roomIndex];
        }

    };

    static RoomTable* GetRoomTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Room") == scene.mEntityTables.end())
            return {};

        return new RoomTable(scene.mEntityTables["Vim.Room"], scene.mStrings);
    }

    class BimDocument
    {
    public:
        int mIndex;
        const std::string* mTitle;
        bool mIsMetric;
        const std::string* mGuid;
        int mNumSaves;
        bool mIsLinked;
        bool mIsDetached;
        bool mIsWorkshared;
        const std::string* mPathName;
        double mLatitude;
        double mLongitude;
        double mTimeZone;
        const std::string* mPlaceName;
        const std::string* mWeatherStationName;
        double mElevation;
        const std::string* mProjectLocation;
        const std::string* mIssueDate;
        const std::string* mStatus;
        const std::string* mClientName;
        const std::string* mAddress;
        const std::string* mName;
        const std::string* mNumber;
        const std::string* mAuthor;
        const std::string* mBuildingName;
        const std::string* mOrganizationName;
        const std::string* mOrganizationDescription;
        const std::string* mProduct;
        const std::string* mVersion;
        const std::string* mUser;

        int mActiveViewIndex;
        View* mActiveView;
        int mOwnerFamilyIndex;
        Family* mOwnerFamily;
        int mParentIndex;
        BimDocument* mParent;
        int mElementIndex;
        Element* mElement;

        BimDocument() {}
    };

    class BimDocumentTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        BimDocumentTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Title"].size() / sizeof(int);
        }

        BimDocument* Get(int bimDocumentIndex)
        {
            BimDocument* bimDocument = new BimDocument();
            bimDocument->mIndex = bimDocumentIndex;
            bimDocument->mTitle = GetTitle(bimDocumentIndex);
            bimDocument->mIsMetric = GetIsMetric(bimDocumentIndex);
            bimDocument->mGuid = GetGuid(bimDocumentIndex);
            bimDocument->mNumSaves = GetNumSaves(bimDocumentIndex);
            bimDocument->mIsLinked = GetIsLinked(bimDocumentIndex);
            bimDocument->mIsDetached = GetIsDetached(bimDocumentIndex);
            bimDocument->mIsWorkshared = GetIsWorkshared(bimDocumentIndex);
            bimDocument->mPathName = GetPathName(bimDocumentIndex);
            bimDocument->mLatitude = GetLatitude(bimDocumentIndex);
            bimDocument->mLongitude = GetLongitude(bimDocumentIndex);
            bimDocument->mTimeZone = GetTimeZone(bimDocumentIndex);
            bimDocument->mPlaceName = GetPlaceName(bimDocumentIndex);
            bimDocument->mWeatherStationName = GetWeatherStationName(bimDocumentIndex);
            bimDocument->mElevation = GetElevation(bimDocumentIndex);
            bimDocument->mProjectLocation = GetProjectLocation(bimDocumentIndex);
            bimDocument->mIssueDate = GetIssueDate(bimDocumentIndex);
            bimDocument->mStatus = GetStatus(bimDocumentIndex);
            bimDocument->mClientName = GetClientName(bimDocumentIndex);
            bimDocument->mAddress = GetAddress(bimDocumentIndex);
            bimDocument->mName = GetName(bimDocumentIndex);
            bimDocument->mNumber = GetNumber(bimDocumentIndex);
            bimDocument->mAuthor = GetAuthor(bimDocumentIndex);
            bimDocument->mBuildingName = GetBuildingName(bimDocumentIndex);
            bimDocument->mOrganizationName = GetOrganizationName(bimDocumentIndex);
            bimDocument->mOrganizationDescription = GetOrganizationDescription(bimDocumentIndex);
            bimDocument->mProduct = GetProduct(bimDocumentIndex);
            bimDocument->mVersion = GetVersion(bimDocumentIndex);
            bimDocument->mUser = GetUser(bimDocumentIndex);
            bimDocument->mActiveViewIndex = GetActiveViewIndex(bimDocumentIndex);
            bimDocument->mOwnerFamilyIndex = GetOwnerFamilyIndex(bimDocumentIndex);
            bimDocument->mParentIndex = GetParentIndex(bimDocumentIndex);
            bimDocument->mElementIndex = GetElementIndex(bimDocumentIndex);
            return bimDocument;
        }

        std::vector<BimDocument>* GetAll()
        {
            bool existsTitle = mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end();
            bool existsIsMetric = mEntityTable.mDataColumns.find("byte:IsMetric") == mEntityTable.mDataColumns.end();
            bool existsGuid = mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end();
            bool existsNumSaves = mEntityTable.mDataColumns.find("int:NumSaves") == mEntityTable.mDataColumns.end();
            bool existsIsLinked = mEntityTable.mDataColumns.find("byte:IsLinked") == mEntityTable.mDataColumns.end();
            bool existsIsDetached = mEntityTable.mDataColumns.find("byte:IsDetached") == mEntityTable.mDataColumns.end();
            bool existsIsWorkshared = mEntityTable.mDataColumns.find("byte:IsWorkshared") == mEntityTable.mDataColumns.end();
            bool existsPathName = mEntityTable.mStringColumns.find("string:PathName") == mEntityTable.mStringColumns.end();
            bool existsLatitude = mEntityTable.mDataColumns.find("double:Latitude") == mEntityTable.mDataColumns.end();
            bool existsLongitude = mEntityTable.mDataColumns.find("double:Longitude") == mEntityTable.mDataColumns.end();
            bool existsTimeZone = mEntityTable.mDataColumns.find("double:TimeZone") == mEntityTable.mDataColumns.end();
            bool existsPlaceName = mEntityTable.mStringColumns.find("string:PlaceName") == mEntityTable.mStringColumns.end();
            bool existsWeatherStationName = mEntityTable.mStringColumns.find("string:WeatherStationName") == mEntityTable.mStringColumns.end();
            bool existsElevation = mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end();
            bool existsProjectLocation = mEntityTable.mStringColumns.find("string:ProjectLocation") == mEntityTable.mStringColumns.end();
            bool existsIssueDate = mEntityTable.mStringColumns.find("string:IssueDate") == mEntityTable.mStringColumns.end();
            bool existsStatus = mEntityTable.mStringColumns.find("string:Status") == mEntityTable.mStringColumns.end();
            bool existsClientName = mEntityTable.mStringColumns.find("string:ClientName") == mEntityTable.mStringColumns.end();
            bool existsAddress = mEntityTable.mStringColumns.find("string:Address") == mEntityTable.mStringColumns.end();
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsNumber = mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end();
            bool existsAuthor = mEntityTable.mStringColumns.find("string:Author") == mEntityTable.mStringColumns.end();
            bool existsBuildingName = mEntityTable.mStringColumns.find("string:BuildingName") == mEntityTable.mStringColumns.end();
            bool existsOrganizationName = mEntityTable.mStringColumns.find("string:OrganizationName") == mEntityTable.mStringColumns.end();
            bool existsOrganizationDescription = mEntityTable.mStringColumns.find("string:OrganizationDescription") == mEntityTable.mStringColumns.end();
            bool existsProduct = mEntityTable.mStringColumns.find("string:Product") == mEntityTable.mStringColumns.end();
            bool existsVersion = mEntityTable.mStringColumns.find("string:Version") == mEntityTable.mStringColumns.end();
            bool existsUser = mEntityTable.mStringColumns.find("string:User") == mEntityTable.mStringColumns.end();
            bool existsActiveView = mEntityTable.mIndexColumns.find("index:Vim.View:ActiveView") == mEntityTable.mIndexColumns.end();
            bool existsOwnerFamily = mEntityTable.mIndexColumns.find("index:Vim.Family:OwnerFamily") == mEntityTable.mIndexColumns.end();
            bool existsParent = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:Parent") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<BimDocument>* bimDocument = new std::vector<BimDocument>();
            bimDocument->reserve(count);

            const std::vector<int>& titleData = existsTitle ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();

            bfast::byte* isMetricData = new bfast::byte[count];
            if (existsIsMetric) memcpy(isMetricData, mEntityTable.mDataColumns["byte:IsMetric"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& guidData = existsGuid ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();

            int* numSavesData = new int[count];
            if (existsNumSaves) memcpy(numSavesData, mEntityTable.mDataColumns["int:NumSaves"].begin(), count * sizeof(int));

            bfast::byte* isLinkedData = new bfast::byte[count];
            if (existsIsLinked) memcpy(isLinkedData, mEntityTable.mDataColumns["byte:IsLinked"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isDetachedData = new bfast::byte[count];
            if (existsIsDetached) memcpy(isDetachedData, mEntityTable.mDataColumns["byte:IsDetached"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isWorksharedData = new bfast::byte[count];
            if (existsIsWorkshared) memcpy(isWorksharedData, mEntityTable.mDataColumns["byte:IsWorkshared"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& pathNameData = existsPathName ? mEntityTable.mStringColumns["string:PathName"] : std::vector<int>();

            double* latitudeData = new double[count];
            if (existsLatitude) memcpy(latitudeData, mEntityTable.mDataColumns["double:Latitude"].begin(), count * sizeof(double));

            double* longitudeData = new double[count];
            if (existsLongitude) memcpy(longitudeData, mEntityTable.mDataColumns["double:Longitude"].begin(), count * sizeof(double));

            double* timeZoneData = new double[count];
            if (existsTimeZone) memcpy(timeZoneData, mEntityTable.mDataColumns["double:TimeZone"].begin(), count * sizeof(double));

            const std::vector<int>& placeNameData = existsPlaceName ? mEntityTable.mStringColumns["string:PlaceName"] : std::vector<int>();

            const std::vector<int>& weatherStationNameData = existsWeatherStationName ? mEntityTable.mStringColumns["string:WeatherStationName"] : std::vector<int>();

            double* elevationData = new double[count];
            if (existsElevation) memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));

            const std::vector<int>& projectLocationData = existsProjectLocation ? mEntityTable.mStringColumns["string:ProjectLocation"] : std::vector<int>();

            const std::vector<int>& issueDateData = existsIssueDate ? mEntityTable.mStringColumns["string:IssueDate"] : std::vector<int>();

            const std::vector<int>& statusData = existsStatus ? mEntityTable.mStringColumns["string:Status"] : std::vector<int>();

            const std::vector<int>& clientNameData = existsClientName ? mEntityTable.mStringColumns["string:ClientName"] : std::vector<int>();

            const std::vector<int>& addressData = existsAddress ? mEntityTable.mStringColumns["string:Address"] : std::vector<int>();

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            const std::vector<int>& numberData = existsNumber ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();

            const std::vector<int>& authorData = existsAuthor ? mEntityTable.mStringColumns["string:Author"] : std::vector<int>();

            const std::vector<int>& buildingNameData = existsBuildingName ? mEntityTable.mStringColumns["string:BuildingName"] : std::vector<int>();

            const std::vector<int>& organizationNameData = existsOrganizationName ? mEntityTable.mStringColumns["string:OrganizationName"] : std::vector<int>();

            const std::vector<int>& organizationDescriptionData = existsOrganizationDescription ? mEntityTable.mStringColumns["string:OrganizationDescription"] : std::vector<int>();

            const std::vector<int>& productData = existsProduct ? mEntityTable.mStringColumns["string:Product"] : std::vector<int>();

            const std::vector<int>& versionData = existsVersion ? mEntityTable.mStringColumns["string:Version"] : std::vector<int>();

            const std::vector<int>& userData = existsUser ? mEntityTable.mStringColumns["string:User"] : std::vector<int>();

            const std::vector<int>& activeViewData = existsActiveView ? mEntityTable.mIndexColumns["index:Vim.View:ActiveView"] : std::vector<int>();
            const std::vector<int>& ownerFamilyData = existsOwnerFamily ? mEntityTable.mIndexColumns["index:Vim.Family:OwnerFamily"] : std::vector<int>();
            const std::vector<int>& parentData = existsParent ? mEntityTable.mIndexColumns["index:Vim.BimDocument:Parent"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                BimDocument entity;
                entity.mIndex = i;
                if (existsTitle)
                    entity.mTitle = &mStrings[titleData[i]];
                if (existsIsMetric)
                    entity.mIsMetric = isMetricData[i];
                if (existsGuid)
                    entity.mGuid = &mStrings[guidData[i]];
                if (existsNumSaves)
                    entity.mNumSaves = numSavesData[i];
                if (existsIsLinked)
                    entity.mIsLinked = isLinkedData[i];
                if (existsIsDetached)
                    entity.mIsDetached = isDetachedData[i];
                if (existsIsWorkshared)
                    entity.mIsWorkshared = isWorksharedData[i];
                if (existsPathName)
                    entity.mPathName = &mStrings[pathNameData[i]];
                if (existsLatitude)
                    entity.mLatitude = latitudeData[i];
                if (existsLongitude)
                    entity.mLongitude = longitudeData[i];
                if (existsTimeZone)
                    entity.mTimeZone = timeZoneData[i];
                if (existsPlaceName)
                    entity.mPlaceName = &mStrings[placeNameData[i]];
                if (existsWeatherStationName)
                    entity.mWeatherStationName = &mStrings[weatherStationNameData[i]];
                if (existsElevation)
                    entity.mElevation = elevationData[i];
                if (existsProjectLocation)
                    entity.mProjectLocation = &mStrings[projectLocationData[i]];
                if (existsIssueDate)
                    entity.mIssueDate = &mStrings[issueDateData[i]];
                if (existsStatus)
                    entity.mStatus = &mStrings[statusData[i]];
                if (existsClientName)
                    entity.mClientName = &mStrings[clientNameData[i]];
                if (existsAddress)
                    entity.mAddress = &mStrings[addressData[i]];
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsNumber)
                    entity.mNumber = &mStrings[numberData[i]];
                if (existsAuthor)
                    entity.mAuthor = &mStrings[authorData[i]];
                if (existsBuildingName)
                    entity.mBuildingName = &mStrings[buildingNameData[i]];
                if (existsOrganizationName)
                    entity.mOrganizationName = &mStrings[organizationNameData[i]];
                if (existsOrganizationDescription)
                    entity.mOrganizationDescription = &mStrings[organizationDescriptionData[i]];
                if (existsProduct)
                    entity.mProduct = &mStrings[productData[i]];
                if (existsVersion)
                    entity.mVersion = &mStrings[versionData[i]];
                if (existsUser)
                    entity.mUser = &mStrings[userData[i]];
                entity.mActiveViewIndex = existsActiveView ? activeViewData[i] : -1;
                entity.mOwnerFamilyIndex = existsOwnerFamily ? ownerFamilyData[i] : -1;
                entity.mParentIndex = existsParent ? parentData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                bimDocument->push_back(entity);
            }

            delete[] isMetricData;
            delete[] numSavesData;
            delete[] isLinkedData;
            delete[] isDetachedData;
            delete[] isWorksharedData;
            delete[] latitudeData;
            delete[] longitudeData;
            delete[] timeZoneData;
            delete[] elevationData;

            return bimDocument;
        }

        const std::string* GetTitle(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Title"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllTitle()
        {
            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& titleData = mEntityTable.mStringColumns["string:Title"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[titleData[i]]);
            }

            return result;
        }

        bool GetIsMetric(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsMetric") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsMetric"].begin() + bimDocumentIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsMetric()
        {
            if (mEntityTable.mDataColumns.find("byte:IsMetric") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isMetricData = new bfast::byte[count];
            memcpy(isMetricData, mEntityTable.mDataColumns["byte:IsMetric"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isMetricData, isMetricData + count);

            delete[] isMetricData;

            return result;
        }

        const std::string* GetGuid(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Guid"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllGuid()
        {
            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& guidData = mEntityTable.mStringColumns["string:Guid"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[guidData[i]]);
            }

            return result;
        }

        int GetNumSaves(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:NumSaves") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:NumSaves"].begin() + bimDocumentIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllNumSaves()
        {
            if (mEntityTable.mDataColumns.find("int:NumSaves") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* numSavesData = new int[count];
            memcpy(numSavesData, mEntityTable.mDataColumns["int:NumSaves"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(numSavesData, numSavesData + count);

            delete[] numSavesData;

            return result;
        }

        bool GetIsLinked(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsLinked") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsLinked"].begin() + bimDocumentIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsLinked()
        {
            if (mEntityTable.mDataColumns.find("byte:IsLinked") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isLinkedData = new bfast::byte[count];
            memcpy(isLinkedData, mEntityTable.mDataColumns["byte:IsLinked"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isLinkedData, isLinkedData + count);

            delete[] isLinkedData;

            return result;
        }

        bool GetIsDetached(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsDetached") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsDetached"].begin() + bimDocumentIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsDetached()
        {
            if (mEntityTable.mDataColumns.find("byte:IsDetached") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isDetachedData = new bfast::byte[count];
            memcpy(isDetachedData, mEntityTable.mDataColumns["byte:IsDetached"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isDetachedData, isDetachedData + count);

            delete[] isDetachedData;

            return result;
        }

        bool GetIsWorkshared(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsWorkshared") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsWorkshared"].begin() + bimDocumentIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsWorkshared()
        {
            if (mEntityTable.mDataColumns.find("byte:IsWorkshared") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isWorksharedData = new bfast::byte[count];
            memcpy(isWorksharedData, mEntityTable.mDataColumns["byte:IsWorkshared"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isWorksharedData, isWorksharedData + count);

            delete[] isWorksharedData;

            return result;
        }

        const std::string* GetPathName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:PathName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:PathName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllPathName()
        {
            if (mEntityTable.mStringColumns.find("string:PathName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& pathNameData = mEntityTable.mStringColumns["string:PathName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[pathNameData[i]]);
            }

            return result;
        }

        double GetLatitude(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Latitude") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Latitude"].begin() + bimDocumentIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllLatitude()
        {
            if (mEntityTable.mDataColumns.find("double:Latitude") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* latitudeData = new double[count];
            memcpy(latitudeData, mEntityTable.mDataColumns["double:Latitude"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(latitudeData, latitudeData + count);

            delete[] latitudeData;

            return result;
        }

        double GetLongitude(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Longitude") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Longitude"].begin() + bimDocumentIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllLongitude()
        {
            if (mEntityTable.mDataColumns.find("double:Longitude") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* longitudeData = new double[count];
            memcpy(longitudeData, mEntityTable.mDataColumns["double:Longitude"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(longitudeData, longitudeData + count);

            delete[] longitudeData;

            return result;
        }

        double GetTimeZone(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:TimeZone") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:TimeZone"].begin() + bimDocumentIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllTimeZone()
        {
            if (mEntityTable.mDataColumns.find("double:TimeZone") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* timeZoneData = new double[count];
            memcpy(timeZoneData, mEntityTable.mDataColumns["double:TimeZone"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(timeZoneData, timeZoneData + count);

            delete[] timeZoneData;

            return result;
        }

        const std::string* GetPlaceName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:PlaceName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:PlaceName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllPlaceName()
        {
            if (mEntityTable.mStringColumns.find("string:PlaceName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& placeNameData = mEntityTable.mStringColumns["string:PlaceName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[placeNameData[i]]);
            }

            return result;
        }

        const std::string* GetWeatherStationName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:WeatherStationName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:WeatherStationName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllWeatherStationName()
        {
            if (mEntityTable.mStringColumns.find("string:WeatherStationName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& weatherStationNameData = mEntityTable.mStringColumns["string:WeatherStationName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[weatherStationNameData[i]]);
            }

            return result;
        }

        double GetElevation(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Elevation"].begin() + bimDocumentIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllElevation()
        {
            if (mEntityTable.mDataColumns.find("double:Elevation") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* elevationData = new double[count];
            memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(elevationData, elevationData + count);

            delete[] elevationData;

            return result;
        }

        const std::string* GetProjectLocation(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:ProjectLocation") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:ProjectLocation"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllProjectLocation()
        {
            if (mEntityTable.mStringColumns.find("string:ProjectLocation") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& projectLocationData = mEntityTable.mStringColumns["string:ProjectLocation"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[projectLocationData[i]]);
            }

            return result;
        }

        const std::string* GetIssueDate(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:IssueDate") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:IssueDate"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllIssueDate()
        {
            if (mEntityTable.mStringColumns.find("string:IssueDate") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& issueDateData = mEntityTable.mStringColumns["string:IssueDate"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[issueDateData[i]]);
            }

            return result;
        }

        const std::string* GetStatus(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Status") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Status"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllStatus()
        {
            if (mEntityTable.mStringColumns.find("string:Status") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& statusData = mEntityTable.mStringColumns["string:Status"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[statusData[i]]);
            }

            return result;
        }

        const std::string* GetClientName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:ClientName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:ClientName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllClientName()
        {
            if (mEntityTable.mStringColumns.find("string:ClientName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& clientNameData = mEntityTable.mStringColumns["string:ClientName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[clientNameData[i]]);
            }

            return result;
        }

        const std::string* GetAddress(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Address") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Address"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllAddress()
        {
            if (mEntityTable.mStringColumns.find("string:Address") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& addressData = mEntityTable.mStringColumns["string:Address"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[addressData[i]]);
            }

            return result;
        }

        const std::string* GetName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        const std::string* GetNumber(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Number"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllNumber()
        {
            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& numberData = mEntityTable.mStringColumns["string:Number"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[numberData[i]]);
            }

            return result;
        }

        const std::string* GetAuthor(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Author") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Author"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllAuthor()
        {
            if (mEntityTable.mStringColumns.find("string:Author") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& authorData = mEntityTable.mStringColumns["string:Author"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[authorData[i]]);
            }

            return result;
        }

        const std::string* GetBuildingName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:BuildingName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:BuildingName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllBuildingName()
        {
            if (mEntityTable.mStringColumns.find("string:BuildingName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& buildingNameData = mEntityTable.mStringColumns["string:BuildingName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[buildingNameData[i]]);
            }

            return result;
        }

        const std::string* GetOrganizationName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:OrganizationName") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:OrganizationName"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllOrganizationName()
        {
            if (mEntityTable.mStringColumns.find("string:OrganizationName") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& organizationNameData = mEntityTable.mStringColumns["string:OrganizationName"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[organizationNameData[i]]);
            }

            return result;
        }

        const std::string* GetOrganizationDescription(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:OrganizationDescription") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:OrganizationDescription"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllOrganizationDescription()
        {
            if (mEntityTable.mStringColumns.find("string:OrganizationDescription") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& organizationDescriptionData = mEntityTable.mStringColumns["string:OrganizationDescription"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[organizationDescriptionData[i]]);
            }

            return result;
        }

        const std::string* GetProduct(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Product") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Product"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllProduct()
        {
            if (mEntityTable.mStringColumns.find("string:Product") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& productData = mEntityTable.mStringColumns["string:Product"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[productData[i]]);
            }

            return result;
        }

        const std::string* GetVersion(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Version") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Version"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllVersion()
        {
            if (mEntityTable.mStringColumns.find("string:Version") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& versionData = mEntityTable.mStringColumns["string:Version"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[versionData[i]]);
            }

            return result;
        }

        const std::string* GetUser(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:User") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:User"][bimDocumentIndex]];
        }

        const std::vector<const std::string*>* GetAllUser()
        {
            if (mEntityTable.mStringColumns.find("string:User") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& userData = mEntityTable.mStringColumns["string:User"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[userData[i]]);
            }

            return result;
        }

        int GetActiveViewIndex(int bimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:ActiveView") == mEntityTable.mIndexColumns.end())
                return -1;

            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:ActiveView"][bimDocumentIndex];
        }

        int GetOwnerFamilyIndex(int bimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Family:OwnerFamily") == mEntityTable.mIndexColumns.end())
                return -1;

            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Family:OwnerFamily"][bimDocumentIndex];
        }

        int GetParentIndex(int bimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:Parent") == mEntityTable.mIndexColumns.end())
                return -1;

            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:Parent"][bimDocumentIndex];
        }

        int GetElementIndex(int bimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][bimDocumentIndex];
        }

    };

    static BimDocumentTable* GetBimDocumentTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.BimDocument") == scene.mEntityTables.end())
            return {};

        return new BimDocumentTable(scene.mEntityTables["Vim.BimDocument"], scene.mStrings);
    }

    class DisplayUnitInBimDocument
    {
    public:
        int mIndex;

        int mDisplayUnitIndex;
        DisplayUnit* mDisplayUnit;
        int mBimDocumentIndex;
        BimDocument* mBimDocument;

        DisplayUnitInBimDocument() {}
    };

    class DisplayUnitInBimDocumentTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        DisplayUnitInBimDocumentTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.DisplayUnit:DisplayUnit") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"].size() / sizeof(int);
        }

        DisplayUnitInBimDocument* Get(int displayUnitInBimDocumentIndex)
        {
            DisplayUnitInBimDocument* displayUnitInBimDocument = new DisplayUnitInBimDocument();
            displayUnitInBimDocument->mIndex = displayUnitInBimDocumentIndex;
            displayUnitInBimDocument->mDisplayUnitIndex = GetDisplayUnitIndex(displayUnitInBimDocumentIndex);
            displayUnitInBimDocument->mBimDocumentIndex = GetBimDocumentIndex(displayUnitInBimDocumentIndex);
            return displayUnitInBimDocument;
        }

        std::vector<DisplayUnitInBimDocument>* GetAll()
        {
            bool existsDisplayUnit = mEntityTable.mIndexColumns.find("index:Vim.DisplayUnit:DisplayUnit") == mEntityTable.mIndexColumns.end();
            bool existsBimDocument = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<DisplayUnitInBimDocument>* displayUnitInBimDocument = new std::vector<DisplayUnitInBimDocument>();
            displayUnitInBimDocument->reserve(count);

            const std::vector<int>& displayUnitData = existsDisplayUnit ? mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                DisplayUnitInBimDocument entity;
                entity.mIndex = i;
                entity.mDisplayUnitIndex = existsDisplayUnit ? displayUnitData[i] : -1;
                entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
                displayUnitInBimDocument->push_back(entity);
            }

            return displayUnitInBimDocument;
        }

        int GetDisplayUnitIndex(int displayUnitInBimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.DisplayUnit:DisplayUnit") == mEntityTable.mIndexColumns.end())
                return -1;

            if (displayUnitInBimDocumentIndex < 0 || displayUnitInBimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"][displayUnitInBimDocumentIndex];
        }

        int GetBimDocumentIndex(int displayUnitInBimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end())
                return -1;

            if (displayUnitInBimDocumentIndex < 0 || displayUnitInBimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][displayUnitInBimDocumentIndex];
        }

    };

    static DisplayUnitInBimDocumentTable* GetDisplayUnitInBimDocumentTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.DisplayUnitInBimDocument") == scene.mEntityTables.end())
            return {};

        return new DisplayUnitInBimDocumentTable(scene.mEntityTables["Vim.DisplayUnitInBimDocument"], scene.mStrings);
    }

    class PhaseOrderInBimDocument
    {
    public:
        int mIndex;
        int mOrderIndex;

        int mPhaseIndex;
        Phase* mPhase;
        int mBimDocumentIndex;
        BimDocument* mBimDocument;

        PhaseOrderInBimDocument() {}
    };

    class PhaseOrderInBimDocumentTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        PhaseOrderInBimDocumentTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:OrderIndex"].size() / sizeof(int);
        }

        PhaseOrderInBimDocument* Get(int phaseOrderInBimDocumentIndex)
        {
            PhaseOrderInBimDocument* phaseOrderInBimDocument = new PhaseOrderInBimDocument();
            phaseOrderInBimDocument->mIndex = phaseOrderInBimDocumentIndex;
            phaseOrderInBimDocument->mOrderIndex = GetOrderIndex(phaseOrderInBimDocumentIndex);
            phaseOrderInBimDocument->mPhaseIndex = GetPhaseIndex(phaseOrderInBimDocumentIndex);
            phaseOrderInBimDocument->mBimDocumentIndex = GetBimDocumentIndex(phaseOrderInBimDocumentIndex);
            return phaseOrderInBimDocument;
        }

        std::vector<PhaseOrderInBimDocument>* GetAll()
        {
            bool existsOrderIndex = mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end();
            bool existsPhase = mEntityTable.mIndexColumns.find("index:Vim.Phase:Phase") == mEntityTable.mIndexColumns.end();
            bool existsBimDocument = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<PhaseOrderInBimDocument>* phaseOrderInBimDocument = new std::vector<PhaseOrderInBimDocument>();
            phaseOrderInBimDocument->reserve(count);

            int* orderIndexData = new int[count];
            if (existsOrderIndex) memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));

            const std::vector<int>& phaseData = existsPhase ? mEntityTable.mIndexColumns["index:Vim.Phase:Phase"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                PhaseOrderInBimDocument entity;
                entity.mIndex = i;
                if (existsOrderIndex)
                    entity.mOrderIndex = orderIndexData[i];
                entity.mPhaseIndex = existsPhase ? phaseData[i] : -1;
                entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
                phaseOrderInBimDocument->push_back(entity);
            }

            delete[] orderIndexData;

            return phaseOrderInBimDocument;
        }

        int GetOrderIndex(int phaseOrderInBimDocumentIndex)
        {
            if (phaseOrderInBimDocumentIndex < 0 || phaseOrderInBimDocumentIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:OrderIndex"].begin() + phaseOrderInBimDocumentIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllOrderIndex()
        {
            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* orderIndexData = new int[count];
            memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(orderIndexData, orderIndexData + count);

            delete[] orderIndexData;

            return result;
        }

        int GetPhaseIndex(int phaseOrderInBimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Phase:Phase") == mEntityTable.mIndexColumns.end())
                return -1;

            if (phaseOrderInBimDocumentIndex < 0 || phaseOrderInBimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Phase:Phase"][phaseOrderInBimDocumentIndex];
        }

        int GetBimDocumentIndex(int phaseOrderInBimDocumentIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end())
                return -1;

            if (phaseOrderInBimDocumentIndex < 0 || phaseOrderInBimDocumentIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][phaseOrderInBimDocumentIndex];
        }

    };

    static PhaseOrderInBimDocumentTable* GetPhaseOrderInBimDocumentTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.PhaseOrderInBimDocument") == scene.mEntityTables.end())
            return {};

        return new PhaseOrderInBimDocumentTable(scene.mEntityTables["Vim.PhaseOrderInBimDocument"], scene.mStrings);
    }

    class Category
    {
    public:
        int mIndex;
        const std::string* mName;
        int mId;
        const std::string* mCategoryType;
        DVector3 mLineColor;
        const std::string* mBuiltInCategory;

        int mParentIndex;
        Category* mParent;
        int mMaterialIndex;
        Material* mMaterial;

        Category() {}
    };

    class CategoryTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        CategoryTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Name"].size() / sizeof(int);
        }

        Category* Get(int categoryIndex)
        {
            Category* category = new Category();
            category->mIndex = categoryIndex;
            category->mName = GetName(categoryIndex);
            category->mId = GetId(categoryIndex);
            category->mCategoryType = GetCategoryType(categoryIndex);
            category->mLineColor = GetLineColor(categoryIndex);
            category->mBuiltInCategory = GetBuiltInCategory(categoryIndex);
            category->mParentIndex = GetParentIndex(categoryIndex);
            category->mMaterialIndex = GetMaterialIndex(categoryIndex);
            return category;
        }

        std::vector<Category>* GetAll()
        {
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsId = mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end();
            bool existsCategoryType = mEntityTable.mStringColumns.find("string:CategoryType") == mEntityTable.mStringColumns.end();
            bool existsLineColorX = mEntityTable.mDataColumns.find("double:LineColor.X") == mEntityTable.mDataColumns.end();
            bool existsLineColorY = mEntityTable.mDataColumns.find("double:LineColor.Y") == mEntityTable.mDataColumns.end();
            bool existsLineColorZ = mEntityTable.mDataColumns.find("double:LineColor.Z") == mEntityTable.mDataColumns.end();
            bool existsBuiltInCategory = mEntityTable.mStringColumns.find("string:BuiltInCategory") == mEntityTable.mStringColumns.end();
            bool existsParent = mEntityTable.mIndexColumns.find("index:Vim.Category:Parent") == mEntityTable.mIndexColumns.end();
            bool existsMaterial = mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Category>* category = new std::vector<Category>();
            category->reserve(count);

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            int* idData = new int[count];
            if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            const std::vector<int>& categoryTypeData = existsCategoryType ? mEntityTable.mStringColumns["string:CategoryType"] : std::vector<int>();

            DVector3Converter lineColorConverter;
            ByteRangePtr* lineColorData = new ByteRangePtr[lineColorConverter.GetSize()];
            if (existsLineColorX && existsLineColorY && existsLineColorZ) for (int i = 0; i < lineColorConverter.GetSize(); i++)
                    lineColorData[i] = &mEntityTable.mDataColumns["double:LineColor" + lineColorConverter.GetColumns()[i]];

            const std::vector<int>& builtInCategoryData = existsBuiltInCategory ? mEntityTable.mStringColumns["string:BuiltInCategory"] : std::vector<int>();

            const std::vector<int>& parentData = existsParent ? mEntityTable.mIndexColumns["index:Vim.Category:Parent"] : std::vector<int>();
            const std::vector<int>& materialData = existsMaterial ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Category entity;
                entity.mIndex = i;
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsId)
                    entity.mId = idData[i];
                if (existsCategoryType)
                    entity.mCategoryType = &mStrings[categoryTypeData[i]];
                if (existsLineColorX && existsLineColorY && existsLineColorZ)
                    lineColorConverter.ConvertFromColumns(&entity.mLineColor, lineColorData, i);
                if (existsBuiltInCategory)
                    entity.mBuiltInCategory = &mStrings[builtInCategoryData[i]];
                entity.mParentIndex = existsParent ? parentData[i] : -1;
                entity.mMaterialIndex = existsMaterial ? materialData[i] : -1;
                category->push_back(entity);
            }

            delete[] idData;
            delete[] lineColorData;

            return category;
        }

        const std::string* GetName(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][categoryIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        int GetId(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + categoryIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllId()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* idData = new int[count];
            memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(idData, idData + count);

            delete[] idData;

            return result;
        }

        const std::string* GetCategoryType(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:CategoryType") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:CategoryType"][categoryIndex]];
        }

        const std::vector<const std::string*>* GetAllCategoryType()
        {
            if (mEntityTable.mStringColumns.find("string:CategoryType") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& categoryTypeData = mEntityTable.mStringColumns["string:CategoryType"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[categoryTypeData[i]]);
            }

            return result;
        }

        DVector3 GetLineColor(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:LineColor.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:LineColor.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:LineColor.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter lineColorConverter;
            bfast::byte* bytes = new bfast::byte[lineColorConverter.GetSize() * lineColorConverter.GetBytes()];
            for (int i = 0; i < lineColorConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * lineColorConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:LineColor" + lineColorConverter.GetColumns()[i]].begin()
                       + categoryIndex * lineColorConverter.GetBytes(),
                       lineColorConverter.GetBytes());
            }
            DVector3 lineColor = lineColorConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return lineColor;
        }

        const std::vector<DVector3>* GetAllLineColor()
        {
            if (mEntityTable.mDataColumns.find("double:LineColor.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:LineColor.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:LineColor.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter lineColorConverter;
            ByteRangePtr* lineColorData = new ByteRangePtr[lineColorConverter.GetSize()];
            for (int i = 0; i < lineColorConverter.GetSize(); i++)
                lineColorData[i] = &mEntityTable.mDataColumns["double:LineColor" + lineColorConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                lineColorConverter.ConvertFromColumns(&value, lineColorData, i);
                result->push_back(value);
            }

            delete[] lineColorData;

            return result;
        }

        const std::string* GetBuiltInCategory(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:BuiltInCategory") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:BuiltInCategory"][categoryIndex]];
        }

        const std::vector<const std::string*>* GetAllBuiltInCategory()
        {
            if (mEntityTable.mStringColumns.find("string:BuiltInCategory") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& builtInCategoryData = mEntityTable.mStringColumns["string:BuiltInCategory"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[builtInCategoryData[i]]);
            }

            return result;
        }

        int GetParentIndex(int categoryIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Category:Parent") == mEntityTable.mIndexColumns.end())
                return -1;

            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Category:Parent"][categoryIndex];
        }

        int GetMaterialIndex(int categoryIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end())
                return -1;

            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Material:Material"][categoryIndex];
        }

    };

    static CategoryTable* GetCategoryTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Category") == scene.mEntityTables.end())
            return {};

        return new CategoryTable(scene.mEntityTables["Vim.Category"], scene.mStrings);
    }

    class Family
    {
    public:
        int mIndex;
        const std::string* mStructuralMaterialType;
        const std::string* mStructuralSectionShape;
        bool mIsSystemFamily;
        bool mIsInPlace;

        int mFamilyCategoryIndex;
        Category* mFamilyCategory;
        int mElementIndex;
        Element* mElement;

        Family() {}
    };

    class FamilyTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        FamilyTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:StructuralMaterialType") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:StructuralMaterialType"].size() / sizeof(int);
        }

        Family* Get(int familyIndex)
        {
            Family* family = new Family();
            family->mIndex = familyIndex;
            family->mStructuralMaterialType = GetStructuralMaterialType(familyIndex);
            family->mStructuralSectionShape = GetStructuralSectionShape(familyIndex);
            family->mIsSystemFamily = GetIsSystemFamily(familyIndex);
            family->mIsInPlace = GetIsInPlace(familyIndex);
            family->mFamilyCategoryIndex = GetFamilyCategoryIndex(familyIndex);
            family->mElementIndex = GetElementIndex(familyIndex);
            return family;
        }

        std::vector<Family>* GetAll()
        {
            bool existsStructuralMaterialType = mEntityTable.mStringColumns.find("string:StructuralMaterialType") == mEntityTable.mStringColumns.end();
            bool existsStructuralSectionShape = mEntityTable.mStringColumns.find("string:StructuralSectionShape") == mEntityTable.mStringColumns.end();
            bool existsIsSystemFamily = mEntityTable.mDataColumns.find("byte:IsSystemFamily") == mEntityTable.mDataColumns.end();
            bool existsIsInPlace = mEntityTable.mDataColumns.find("byte:IsInPlace") == mEntityTable.mDataColumns.end();
            bool existsFamilyCategory = mEntityTable.mIndexColumns.find("index:Vim.Category:FamilyCategory") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Family>* family = new std::vector<Family>();
            family->reserve(count);

            const std::vector<int>& structuralMaterialTypeData = existsStructuralMaterialType ? mEntityTable.mStringColumns["string:StructuralMaterialType"] : std::vector<int>();

            const std::vector<int>& structuralSectionShapeData = existsStructuralSectionShape ? mEntityTable.mStringColumns["string:StructuralSectionShape"] : std::vector<int>();

            bfast::byte* isSystemFamilyData = new bfast::byte[count];
            if (existsIsSystemFamily) memcpy(isSystemFamilyData, mEntityTable.mDataColumns["byte:IsSystemFamily"].begin(), count * sizeof(bfast::byte));

            bfast::byte* isInPlaceData = new bfast::byte[count];
            if (existsIsInPlace) memcpy(isInPlaceData, mEntityTable.mDataColumns["byte:IsInPlace"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& familyCategoryData = existsFamilyCategory ? mEntityTable.mIndexColumns["index:Vim.Category:FamilyCategory"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Family entity;
                entity.mIndex = i;
                if (existsStructuralMaterialType)
                    entity.mStructuralMaterialType = &mStrings[structuralMaterialTypeData[i]];
                if (existsStructuralSectionShape)
                    entity.mStructuralSectionShape = &mStrings[structuralSectionShapeData[i]];
                if (existsIsSystemFamily)
                    entity.mIsSystemFamily = isSystemFamilyData[i];
                if (existsIsInPlace)
                    entity.mIsInPlace = isInPlaceData[i];
                entity.mFamilyCategoryIndex = existsFamilyCategory ? familyCategoryData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                family->push_back(entity);
            }

            delete[] isSystemFamilyData;
            delete[] isInPlaceData;

            return family;
        }

        const std::string* GetStructuralMaterialType(int familyIndex)
        {
            if (familyIndex < 0 || familyIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:StructuralMaterialType") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:StructuralMaterialType"][familyIndex]];
        }

        const std::vector<const std::string*>* GetAllStructuralMaterialType()
        {
            if (mEntityTable.mStringColumns.find("string:StructuralMaterialType") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& structuralMaterialTypeData = mEntityTable.mStringColumns["string:StructuralMaterialType"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[structuralMaterialTypeData[i]]);
            }

            return result;
        }

        const std::string* GetStructuralSectionShape(int familyIndex)
        {
            if (familyIndex < 0 || familyIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:StructuralSectionShape") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:StructuralSectionShape"][familyIndex]];
        }

        const std::vector<const std::string*>* GetAllStructuralSectionShape()
        {
            if (mEntityTable.mStringColumns.find("string:StructuralSectionShape") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& structuralSectionShapeData = mEntityTable.mStringColumns["string:StructuralSectionShape"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[structuralSectionShapeData[i]]);
            }

            return result;
        }

        bool GetIsSystemFamily(int familyIndex)
        {
            if (familyIndex < 0 || familyIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsSystemFamily") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSystemFamily"].begin() + familyIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsSystemFamily()
        {
            if (mEntityTable.mDataColumns.find("byte:IsSystemFamily") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isSystemFamilyData = new bfast::byte[count];
            memcpy(isSystemFamilyData, mEntityTable.mDataColumns["byte:IsSystemFamily"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isSystemFamilyData, isSystemFamilyData + count);

            delete[] isSystemFamilyData;

            return result;
        }

        bool GetIsInPlace(int familyIndex)
        {
            if (familyIndex < 0 || familyIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsInPlace") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsInPlace"].begin() + familyIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsInPlace()
        {
            if (mEntityTable.mDataColumns.find("byte:IsInPlace") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isInPlaceData = new bfast::byte[count];
            memcpy(isInPlaceData, mEntityTable.mDataColumns["byte:IsInPlace"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isInPlaceData, isInPlaceData + count);

            delete[] isInPlaceData;

            return result;
        }

        int GetFamilyCategoryIndex(int familyIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Category:FamilyCategory") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyIndex < 0 || familyIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Category:FamilyCategory"][familyIndex];
        }

        int GetElementIndex(int familyIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyIndex < 0 || familyIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][familyIndex];
        }

    };

    static FamilyTable* GetFamilyTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Family") == scene.mEntityTables.end())
            return {};

        return new FamilyTable(scene.mEntityTables["Vim.Family"], scene.mStrings);
    }

    class FamilyType
    {
    public:
        int mIndex;
        bool mIsSystemFamilyType;

        int mFamilyIndex;
        Family* mFamily;
        int mCompoundStructureIndex;
        CompoundStructure* mCompoundStructure;
        int mElementIndex;
        Element* mElement;

        FamilyType() {}
    };

    class FamilyTypeTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        FamilyTypeTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("byte:IsSystemFamilyType") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["byte:IsSystemFamilyType"].size() / sizeof(bfast::byte);
        }

        FamilyType* Get(int familyTypeIndex)
        {
            FamilyType* familyType = new FamilyType();
            familyType->mIndex = familyTypeIndex;
            familyType->mIsSystemFamilyType = GetIsSystemFamilyType(familyTypeIndex);
            familyType->mFamilyIndex = GetFamilyIndex(familyTypeIndex);
            familyType->mCompoundStructureIndex = GetCompoundStructureIndex(familyTypeIndex);
            familyType->mElementIndex = GetElementIndex(familyTypeIndex);
            return familyType;
        }

        std::vector<FamilyType>* GetAll()
        {
            bool existsIsSystemFamilyType = mEntityTable.mDataColumns.find("byte:IsSystemFamilyType") == mEntityTable.mDataColumns.end();
            bool existsFamily = mEntityTable.mIndexColumns.find("index:Vim.Family:Family") == mEntityTable.mIndexColumns.end();
            bool existsCompoundStructure = mEntityTable.mIndexColumns.find("index:Vim.CompoundStructure:CompoundStructure") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<FamilyType>* familyType = new std::vector<FamilyType>();
            familyType->reserve(count);

            bfast::byte* isSystemFamilyTypeData = new bfast::byte[count];
            if (existsIsSystemFamilyType) memcpy(isSystemFamilyTypeData, mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& familyData = existsFamily ? mEntityTable.mIndexColumns["index:Vim.Family:Family"] : std::vector<int>();
            const std::vector<int>& compoundStructureData = existsCompoundStructure ? mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                FamilyType entity;
                entity.mIndex = i;
                if (existsIsSystemFamilyType)
                    entity.mIsSystemFamilyType = isSystemFamilyTypeData[i];
                entity.mFamilyIndex = existsFamily ? familyData[i] : -1;
                entity.mCompoundStructureIndex = existsCompoundStructure ? compoundStructureData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                familyType->push_back(entity);
            }

            delete[] isSystemFamilyTypeData;

            return familyType;
        }

        bool GetIsSystemFamilyType(int familyTypeIndex)
        {
            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsSystemFamilyType") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin() + familyTypeIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsSystemFamilyType()
        {
            if (mEntityTable.mDataColumns.find("byte:IsSystemFamilyType") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isSystemFamilyTypeData = new bfast::byte[count];
            memcpy(isSystemFamilyTypeData, mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isSystemFamilyTypeData, isSystemFamilyTypeData + count);

            delete[] isSystemFamilyTypeData;

            return result;
        }

        int GetFamilyIndex(int familyTypeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Family:Family") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Family:Family"][familyTypeIndex];
        }

        int GetCompoundStructureIndex(int familyTypeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.CompoundStructure:CompoundStructure") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"][familyTypeIndex];
        }

        int GetElementIndex(int familyTypeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][familyTypeIndex];
        }

    };

    static FamilyTypeTable* GetFamilyTypeTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.FamilyType") == scene.mEntityTables.end())
            return {};

        return new FamilyTypeTable(scene.mEntityTables["Vim.FamilyType"], scene.mStrings);
    }

    class FamilyInstance
    {
    public:
        int mIndex;
        bool mFacingFlipped;
        Vector3 mFacingOrientation;
        bool mHandFlipped;
        bool mMirrored;
        bool mHasModifiedGeometry;
        float mScale;
        Vector3 mBasisX;
        Vector3 mBasisY;
        Vector3 mBasisZ;
        Vector3 mTranslation;
        Vector3 mHandOrientation;

        int mFamilyTypeIndex;
        FamilyType* mFamilyType;
        int mHostIndex;
        Element* mHost;
        int mFromRoomIndex;
        Room* mFromRoom;
        int mToRoomIndex;
        Room* mToRoom;
        int mElementIndex;
        Element* mElement;

        FamilyInstance() {}
    };

    class FamilyInstanceTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        FamilyInstanceTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("byte:FacingFlipped") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["byte:FacingFlipped"].size() / sizeof(bfast::byte);
        }

        FamilyInstance* Get(int familyInstanceIndex)
        {
            FamilyInstance* familyInstance = new FamilyInstance();
            familyInstance->mIndex = familyInstanceIndex;
            familyInstance->mFacingFlipped = GetFacingFlipped(familyInstanceIndex);
            familyInstance->mFacingOrientation = GetFacingOrientation(familyInstanceIndex);
            familyInstance->mHandFlipped = GetHandFlipped(familyInstanceIndex);
            familyInstance->mMirrored = GetMirrored(familyInstanceIndex);
            familyInstance->mHasModifiedGeometry = GetHasModifiedGeometry(familyInstanceIndex);
            familyInstance->mScale = GetScale(familyInstanceIndex);
            familyInstance->mBasisX = GetBasisX(familyInstanceIndex);
            familyInstance->mBasisY = GetBasisY(familyInstanceIndex);
            familyInstance->mBasisZ = GetBasisZ(familyInstanceIndex);
            familyInstance->mTranslation = GetTranslation(familyInstanceIndex);
            familyInstance->mHandOrientation = GetHandOrientation(familyInstanceIndex);
            familyInstance->mFamilyTypeIndex = GetFamilyTypeIndex(familyInstanceIndex);
            familyInstance->mHostIndex = GetHostIndex(familyInstanceIndex);
            familyInstance->mFromRoomIndex = GetFromRoomIndex(familyInstanceIndex);
            familyInstance->mToRoomIndex = GetToRoomIndex(familyInstanceIndex);
            familyInstance->mElementIndex = GetElementIndex(familyInstanceIndex);
            return familyInstance;
        }

        std::vector<FamilyInstance>* GetAll()
        {
            bool existsFacingFlipped = mEntityTable.mDataColumns.find("byte:FacingFlipped") == mEntityTable.mDataColumns.end();
            bool existsFacingOrientationX = mEntityTable.mDataColumns.find("float:FacingOrientation.X") == mEntityTable.mDataColumns.end();
            bool existsFacingOrientationY = mEntityTable.mDataColumns.find("float:FacingOrientation.Y") == mEntityTable.mDataColumns.end();
            bool existsFacingOrientationZ = mEntityTable.mDataColumns.find("float:FacingOrientation.Z") == mEntityTable.mDataColumns.end();
            bool existsHandFlipped = mEntityTable.mDataColumns.find("byte:HandFlipped") == mEntityTable.mDataColumns.end();
            bool existsMirrored = mEntityTable.mDataColumns.find("byte:Mirrored") == mEntityTable.mDataColumns.end();
            bool existsHasModifiedGeometry = mEntityTable.mDataColumns.find("byte:HasModifiedGeometry") == mEntityTable.mDataColumns.end();
            bool existsScale = mEntityTable.mDataColumns.find("float:Scale") == mEntityTable.mDataColumns.end();
            bool existsBasisXX = mEntityTable.mDataColumns.find("float:BasisX.X") == mEntityTable.mDataColumns.end();
            bool existsBasisXY = mEntityTable.mDataColumns.find("float:BasisX.Y") == mEntityTable.mDataColumns.end();
            bool existsBasisXZ = mEntityTable.mDataColumns.find("float:BasisX.Z") == mEntityTable.mDataColumns.end();
            bool existsBasisYX = mEntityTable.mDataColumns.find("float:BasisY.X") == mEntityTable.mDataColumns.end();
            bool existsBasisYY = mEntityTable.mDataColumns.find("float:BasisY.Y") == mEntityTable.mDataColumns.end();
            bool existsBasisYZ = mEntityTable.mDataColumns.find("float:BasisY.Z") == mEntityTable.mDataColumns.end();
            bool existsBasisZX = mEntityTable.mDataColumns.find("float:BasisZ.X") == mEntityTable.mDataColumns.end();
            bool existsBasisZY = mEntityTable.mDataColumns.find("float:BasisZ.Y") == mEntityTable.mDataColumns.end();
            bool existsBasisZZ = mEntityTable.mDataColumns.find("float:BasisZ.Z") == mEntityTable.mDataColumns.end();
            bool existsTranslationX = mEntityTable.mDataColumns.find("float:Translation.X") == mEntityTable.mDataColumns.end();
            bool existsTranslationY = mEntityTable.mDataColumns.find("float:Translation.Y") == mEntityTable.mDataColumns.end();
            bool existsTranslationZ = mEntityTable.mDataColumns.find("float:Translation.Z") == mEntityTable.mDataColumns.end();
            bool existsHandOrientationX = mEntityTable.mDataColumns.find("float:HandOrientation.X") == mEntityTable.mDataColumns.end();
            bool existsHandOrientationY = mEntityTable.mDataColumns.find("float:HandOrientation.Y") == mEntityTable.mDataColumns.end();
            bool existsHandOrientationZ = mEntityTable.mDataColumns.find("float:HandOrientation.Z") == mEntityTable.mDataColumns.end();
            bool existsFamilyType = mEntityTable.mIndexColumns.find("index:Vim.FamilyType:FamilyType") == mEntityTable.mIndexColumns.end();
            bool existsHost = mEntityTable.mIndexColumns.find("index:Vim.Element:Host") == mEntityTable.mIndexColumns.end();
            bool existsFromRoom = mEntityTable.mIndexColumns.find("index:Vim.Room:FromRoom") == mEntityTable.mIndexColumns.end();
            bool existsToRoom = mEntityTable.mIndexColumns.find("index:Vim.Room:ToRoom") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<FamilyInstance>* familyInstance = new std::vector<FamilyInstance>();
            familyInstance->reserve(count);

            bfast::byte* facingFlippedData = new bfast::byte[count];
            if (existsFacingFlipped) memcpy(facingFlippedData, mEntityTable.mDataColumns["byte:FacingFlipped"].begin(), count * sizeof(bfast::byte));

            Vector3Converter facingOrientationConverter;
            ByteRangePtr* facingOrientationData = new ByteRangePtr[facingOrientationConverter.GetSize()];
            if (existsFacingOrientationX && existsFacingOrientationY && existsFacingOrientationZ) for (int i = 0; i < facingOrientationConverter.GetSize(); i++)
                    facingOrientationData[i] = &mEntityTable.mDataColumns["float:FacingOrientation" + facingOrientationConverter.GetColumns()[i]];

            bfast::byte* handFlippedData = new bfast::byte[count];
            if (existsHandFlipped) memcpy(handFlippedData, mEntityTable.mDataColumns["byte:HandFlipped"].begin(), count * sizeof(bfast::byte));

            bfast::byte* mirroredData = new bfast::byte[count];
            if (existsMirrored) memcpy(mirroredData, mEntityTable.mDataColumns["byte:Mirrored"].begin(), count * sizeof(bfast::byte));

            bfast::byte* hasModifiedGeometryData = new bfast::byte[count];
            if (existsHasModifiedGeometry) memcpy(hasModifiedGeometryData, mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin(), count * sizeof(bfast::byte));

            float* scaleData = new float[count];
            if (existsScale) memcpy(scaleData, mEntityTable.mDataColumns["float:Scale"].begin(), count * sizeof(float));

            Vector3Converter basisXConverter;
            ByteRangePtr* basisXData = new ByteRangePtr[basisXConverter.GetSize()];
            if (existsBasisXX && existsBasisXY && existsBasisXZ) for (int i = 0; i < basisXConverter.GetSize(); i++)
                    basisXData[i] = &mEntityTable.mDataColumns["float:BasisX" + basisXConverter.GetColumns()[i]];

            Vector3Converter basisYConverter;
            ByteRangePtr* basisYData = new ByteRangePtr[basisYConverter.GetSize()];
            if (existsBasisYX && existsBasisYY && existsBasisYZ) for (int i = 0; i < basisYConverter.GetSize(); i++)
                    basisYData[i] = &mEntityTable.mDataColumns["float:BasisY" + basisYConverter.GetColumns()[i]];

            Vector3Converter basisZConverter;
            ByteRangePtr* basisZData = new ByteRangePtr[basisZConverter.GetSize()];
            if (existsBasisZX && existsBasisZY && existsBasisZZ) for (int i = 0; i < basisZConverter.GetSize(); i++)
                    basisZData[i] = &mEntityTable.mDataColumns["float:BasisZ" + basisZConverter.GetColumns()[i]];

            Vector3Converter translationConverter;
            ByteRangePtr* translationData = new ByteRangePtr[translationConverter.GetSize()];
            if (existsTranslationX && existsTranslationY && existsTranslationZ) for (int i = 0; i < translationConverter.GetSize(); i++)
                    translationData[i] = &mEntityTable.mDataColumns["float:Translation" + translationConverter.GetColumns()[i]];

            Vector3Converter handOrientationConverter;
            ByteRangePtr* handOrientationData = new ByteRangePtr[handOrientationConverter.GetSize()];
            if (existsHandOrientationX && existsHandOrientationY && existsHandOrientationZ) for (int i = 0; i < handOrientationConverter.GetSize(); i++)
                    handOrientationData[i] = &mEntityTable.mDataColumns["float:HandOrientation" + handOrientationConverter.GetColumns()[i]];

            const std::vector<int>& familyTypeData = existsFamilyType ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& hostData = existsHost ? mEntityTable.mIndexColumns["index:Vim.Element:Host"] : std::vector<int>();
            const std::vector<int>& fromRoomData = existsFromRoom ? mEntityTable.mIndexColumns["index:Vim.Room:FromRoom"] : std::vector<int>();
            const std::vector<int>& toRoomData = existsToRoom ? mEntityTable.mIndexColumns["index:Vim.Room:ToRoom"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                FamilyInstance entity;
                entity.mIndex = i;
                if (existsFacingFlipped)
                    entity.mFacingFlipped = facingFlippedData[i];
                if (existsFacingOrientationX && existsFacingOrientationY && existsFacingOrientationZ)
                    facingOrientationConverter.ConvertFromColumns(&entity.mFacingOrientation, facingOrientationData, i);
                if (existsHandFlipped)
                    entity.mHandFlipped = handFlippedData[i];
                if (existsMirrored)
                    entity.mMirrored = mirroredData[i];
                if (existsHasModifiedGeometry)
                    entity.mHasModifiedGeometry = hasModifiedGeometryData[i];
                if (existsScale)
                    entity.mScale = scaleData[i];
                if (existsBasisXX && existsBasisXY && existsBasisXZ)
                    basisXConverter.ConvertFromColumns(&entity.mBasisX, basisXData, i);
                if (existsBasisYX && existsBasisYY && existsBasisYZ)
                    basisYConverter.ConvertFromColumns(&entity.mBasisY, basisYData, i);
                if (existsBasisZX && existsBasisZY && existsBasisZZ)
                    basisZConverter.ConvertFromColumns(&entity.mBasisZ, basisZData, i);
                if (existsTranslationX && existsTranslationY && existsTranslationZ)
                    translationConverter.ConvertFromColumns(&entity.mTranslation, translationData, i);
                if (existsHandOrientationX && existsHandOrientationY && existsHandOrientationZ)
                    handOrientationConverter.ConvertFromColumns(&entity.mHandOrientation, handOrientationData, i);
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
                entity.mHostIndex = existsHost ? hostData[i] : -1;
                entity.mFromRoomIndex = existsFromRoom ? fromRoomData[i] : -1;
                entity.mToRoomIndex = existsToRoom ? toRoomData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                familyInstance->push_back(entity);
            }

            delete[] facingFlippedData;
            delete[] facingOrientationData;
            delete[] handFlippedData;
            delete[] mirroredData;
            delete[] hasModifiedGeometryData;
            delete[] scaleData;
            delete[] basisXData;
            delete[] basisYData;
            delete[] basisZData;
            delete[] translationData;
            delete[] handOrientationData;

            return familyInstance;
        }

        bool GetFacingFlipped(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:FacingFlipped") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:FacingFlipped"].begin() + familyInstanceIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllFacingFlipped()
        {
            if (mEntityTable.mDataColumns.find("byte:FacingFlipped") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* facingFlippedData = new bfast::byte[count];
            memcpy(facingFlippedData, mEntityTable.mDataColumns["byte:FacingFlipped"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(facingFlippedData, facingFlippedData + count);

            delete[] facingFlippedData;

            return result;
        }

        Vector3 GetFacingOrientation(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:FacingOrientation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:FacingOrientation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:FacingOrientation.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter facingOrientationConverter;
            bfast::byte* bytes = new bfast::byte[facingOrientationConverter.GetSize() * facingOrientationConverter.GetBytes()];
            for (int i = 0; i < facingOrientationConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * facingOrientationConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:FacingOrientation" + facingOrientationConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * facingOrientationConverter.GetBytes(),
                       facingOrientationConverter.GetBytes());
            }
            Vector3 facingOrientation = facingOrientationConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return facingOrientation;
        }

        const std::vector<Vector3>* GetAllFacingOrientation()
        {
            if (mEntityTable.mDataColumns.find("float:FacingOrientation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:FacingOrientation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:FacingOrientation.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter facingOrientationConverter;
            ByteRangePtr* facingOrientationData = new ByteRangePtr[facingOrientationConverter.GetSize()];
            for (int i = 0; i < facingOrientationConverter.GetSize(); i++)
                facingOrientationData[i] = &mEntityTable.mDataColumns["float:FacingOrientation" + facingOrientationConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                facingOrientationConverter.ConvertFromColumns(&value, facingOrientationData, i);
                result->push_back(value);
            }

            delete[] facingOrientationData;

            return result;
        }

        bool GetHandFlipped(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:HandFlipped") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:HandFlipped"].begin() + familyInstanceIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllHandFlipped()
        {
            if (mEntityTable.mDataColumns.find("byte:HandFlipped") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* handFlippedData = new bfast::byte[count];
            memcpy(handFlippedData, mEntityTable.mDataColumns["byte:HandFlipped"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(handFlippedData, handFlippedData + count);

            delete[] handFlippedData;

            return result;
        }

        bool GetMirrored(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:Mirrored") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:Mirrored"].begin() + familyInstanceIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllMirrored()
        {
            if (mEntityTable.mDataColumns.find("byte:Mirrored") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* mirroredData = new bfast::byte[count];
            memcpy(mirroredData, mEntityTable.mDataColumns["byte:Mirrored"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(mirroredData, mirroredData + count);

            delete[] mirroredData;

            return result;
        }

        bool GetHasModifiedGeometry(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:HasModifiedGeometry") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin() + familyInstanceIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllHasModifiedGeometry()
        {
            if (mEntityTable.mDataColumns.find("byte:HasModifiedGeometry") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* hasModifiedGeometryData = new bfast::byte[count];
            memcpy(hasModifiedGeometryData, mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(hasModifiedGeometryData, hasModifiedGeometryData + count);

            delete[] hasModifiedGeometryData;

            return result;
        }

        float GetScale(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Scale") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Scale"].begin() + familyInstanceIndex * sizeof(float)));
        }

        const std::vector<float>* GetAllScale()
        {
            if (mEntityTable.mDataColumns.find("float:Scale") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            float* scaleData = new float[count];
            memcpy(scaleData, mEntityTable.mDataColumns["float:Scale"].begin(), count * sizeof(float));

            std::vector<float>* result = new std::vector<float>(scaleData, scaleData + count);

            delete[] scaleData;

            return result;
        }

        Vector3 GetBasisX(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:BasisX.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisX.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisX.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter basisXConverter;
            bfast::byte* bytes = new bfast::byte[basisXConverter.GetSize() * basisXConverter.GetBytes()];
            for (int i = 0; i < basisXConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * basisXConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:BasisX" + basisXConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * basisXConverter.GetBytes(),
                       basisXConverter.GetBytes());
            }
            Vector3 basisX = basisXConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return basisX;
        }

        const std::vector<Vector3>* GetAllBasisX()
        {
            if (mEntityTable.mDataColumns.find("float:BasisX.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisX.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisX.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter basisXConverter;
            ByteRangePtr* basisXData = new ByteRangePtr[basisXConverter.GetSize()];
            for (int i = 0; i < basisXConverter.GetSize(); i++)
                basisXData[i] = &mEntityTable.mDataColumns["float:BasisX" + basisXConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                basisXConverter.ConvertFromColumns(&value, basisXData, i);
                result->push_back(value);
            }

            delete[] basisXData;

            return result;
        }

        Vector3 GetBasisY(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:BasisY.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisY.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisY.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter basisYConverter;
            bfast::byte* bytes = new bfast::byte[basisYConverter.GetSize() * basisYConverter.GetBytes()];
            for (int i = 0; i < basisYConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * basisYConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:BasisY" + basisYConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * basisYConverter.GetBytes(),
                       basisYConverter.GetBytes());
            }
            Vector3 basisY = basisYConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return basisY;
        }

        const std::vector<Vector3>* GetAllBasisY()
        {
            if (mEntityTable.mDataColumns.find("float:BasisY.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisY.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisY.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter basisYConverter;
            ByteRangePtr* basisYData = new ByteRangePtr[basisYConverter.GetSize()];
            for (int i = 0; i < basisYConverter.GetSize(); i++)
                basisYData[i] = &mEntityTable.mDataColumns["float:BasisY" + basisYConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                basisYConverter.ConvertFromColumns(&value, basisYData, i);
                result->push_back(value);
            }

            delete[] basisYData;

            return result;
        }

        Vector3 GetBasisZ(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:BasisZ.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisZ.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisZ.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter basisZConverter;
            bfast::byte* bytes = new bfast::byte[basisZConverter.GetSize() * basisZConverter.GetBytes()];
            for (int i = 0; i < basisZConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * basisZConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:BasisZ" + basisZConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * basisZConverter.GetBytes(),
                       basisZConverter.GetBytes());
            }
            Vector3 basisZ = basisZConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return basisZ;
        }

        const std::vector<Vector3>* GetAllBasisZ()
        {
            if (mEntityTable.mDataColumns.find("float:BasisZ.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisZ.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:BasisZ.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter basisZConverter;
            ByteRangePtr* basisZData = new ByteRangePtr[basisZConverter.GetSize()];
            for (int i = 0; i < basisZConverter.GetSize(); i++)
                basisZData[i] = &mEntityTable.mDataColumns["float:BasisZ" + basisZConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                basisZConverter.ConvertFromColumns(&value, basisZData, i);
                result->push_back(value);
            }

            delete[] basisZData;

            return result;
        }

        Vector3 GetTranslation(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Translation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Translation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Translation.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter translationConverter;
            bfast::byte* bytes = new bfast::byte[translationConverter.GetSize() * translationConverter.GetBytes()];
            for (int i = 0; i < translationConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * translationConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:Translation" + translationConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * translationConverter.GetBytes(),
                       translationConverter.GetBytes());
            }
            Vector3 translation = translationConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return translation;
        }

        const std::vector<Vector3>* GetAllTranslation()
        {
            if (mEntityTable.mDataColumns.find("float:Translation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Translation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Translation.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter translationConverter;
            ByteRangePtr* translationData = new ByteRangePtr[translationConverter.GetSize()];
            for (int i = 0; i < translationConverter.GetSize(); i++)
                translationData[i] = &mEntityTable.mDataColumns["float:Translation" + translationConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                translationConverter.ConvertFromColumns(&value, translationData, i);
                result->push_back(value);
            }

            delete[] translationData;

            return result;
        }

        Vector3 GetHandOrientation(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:HandOrientation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:HandOrientation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:HandOrientation.Z") == mEntityTable.mDataColumns.end())
                return {};

            Vector3Converter handOrientationConverter;
            bfast::byte* bytes = new bfast::byte[handOrientationConverter.GetSize() * handOrientationConverter.GetBytes()];
            for (int i = 0; i < handOrientationConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * handOrientationConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:HandOrientation" + handOrientationConverter.GetColumns()[i]].begin()
                       + familyInstanceIndex * handOrientationConverter.GetBytes(),
                       handOrientationConverter.GetBytes());
            }
            Vector3 handOrientation = handOrientationConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return handOrientation;
        }

        const std::vector<Vector3>* GetAllHandOrientation()
        {
            if (mEntityTable.mDataColumns.find("float:HandOrientation.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:HandOrientation.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:HandOrientation.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            Vector3Converter handOrientationConverter;
            ByteRangePtr* handOrientationData = new ByteRangePtr[handOrientationConverter.GetSize()];
            for (int i = 0; i < handOrientationConverter.GetSize(); i++)
                handOrientationData[i] = &mEntityTable.mDataColumns["float:HandOrientation" + handOrientationConverter.GetColumns()[i]];

            std::vector<Vector3>* result = new std::vector<Vector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                Vector3 value;
                handOrientationConverter.ConvertFromColumns(&value, handOrientationData, i);
                result->push_back(value);
            }

            delete[] handOrientationData;

            return result;
        }

        int GetFamilyTypeIndex(int familyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.FamilyType:FamilyType") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][familyInstanceIndex];
        }

        int GetHostIndex(int familyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Host") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Host"][familyInstanceIndex];
        }

        int GetFromRoomIndex(int familyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Room:FromRoom") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Room:FromRoom"][familyInstanceIndex];
        }

        int GetToRoomIndex(int familyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Room:ToRoom") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Room:ToRoom"][familyInstanceIndex];
        }

        int GetElementIndex(int familyInstanceIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][familyInstanceIndex];
        }

    };

    static FamilyInstanceTable* GetFamilyInstanceTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.FamilyInstance") == scene.mEntityTables.end())
            return {};

        return new FamilyInstanceTable(scene.mEntityTables["Vim.FamilyInstance"], scene.mStrings);
    }

    class View
    {
    public:
        int mIndex;
        const std::string* mTitle;
        const std::string* mViewType;
        DVector3 mUp;
        DVector3 mRight;
        DVector3 mOrigin;
        DVector3 mViewDirection;
        DVector3 mViewPosition;
        double mScale;
        DAABox2D mOutline;
        int mDetailLevel;

        int mCameraIndex;
        Camera* mCamera;
        int mElementIndex;
        Element* mElement;

        View() {}
    };

    class ViewTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ViewTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Title"].size() / sizeof(int);
        }

        View* Get(int viewIndex)
        {
            View* view = new View();
            view->mIndex = viewIndex;
            view->mTitle = GetTitle(viewIndex);
            view->mViewType = GetViewType(viewIndex);
            view->mUp = GetUp(viewIndex);
            view->mRight = GetRight(viewIndex);
            view->mOrigin = GetOrigin(viewIndex);
            view->mViewDirection = GetViewDirection(viewIndex);
            view->mViewPosition = GetViewPosition(viewIndex);
            view->mScale = GetScale(viewIndex);
            view->mOutline = GetOutline(viewIndex);
            view->mDetailLevel = GetDetailLevel(viewIndex);
            view->mCameraIndex = GetCameraIndex(viewIndex);
            view->mElementIndex = GetElementIndex(viewIndex);
            return view;
        }

        std::vector<View>* GetAll()
        {
            bool existsTitle = mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end();
            bool existsViewType = mEntityTable.mStringColumns.find("string:ViewType") == mEntityTable.mStringColumns.end();
            bool existsUpX = mEntityTable.mDataColumns.find("double:Up.X") == mEntityTable.mDataColumns.end();
            bool existsUpY = mEntityTable.mDataColumns.find("double:Up.Y") == mEntityTable.mDataColumns.end();
            bool existsUpZ = mEntityTable.mDataColumns.find("double:Up.Z") == mEntityTable.mDataColumns.end();
            bool existsRightX = mEntityTable.mDataColumns.find("double:Right.X") == mEntityTable.mDataColumns.end();
            bool existsRightY = mEntityTable.mDataColumns.find("double:Right.Y") == mEntityTable.mDataColumns.end();
            bool existsRightZ = mEntityTable.mDataColumns.find("double:Right.Z") == mEntityTable.mDataColumns.end();
            bool existsOriginX = mEntityTable.mDataColumns.find("double:Origin.X") == mEntityTable.mDataColumns.end();
            bool existsOriginY = mEntityTable.mDataColumns.find("double:Origin.Y") == mEntityTable.mDataColumns.end();
            bool existsOriginZ = mEntityTable.mDataColumns.find("double:Origin.Z") == mEntityTable.mDataColumns.end();
            bool existsViewDirectionX = mEntityTable.mDataColumns.find("double:ViewDirection.X") == mEntityTable.mDataColumns.end();
            bool existsViewDirectionY = mEntityTable.mDataColumns.find("double:ViewDirection.Y") == mEntityTable.mDataColumns.end();
            bool existsViewDirectionZ = mEntityTable.mDataColumns.find("double:ViewDirection.Z") == mEntityTable.mDataColumns.end();
            bool existsViewPositionX = mEntityTable.mDataColumns.find("double:ViewPosition.X") == mEntityTable.mDataColumns.end();
            bool existsViewPositionY = mEntityTable.mDataColumns.find("double:ViewPosition.Y") == mEntityTable.mDataColumns.end();
            bool existsViewPositionZ = mEntityTable.mDataColumns.find("double:ViewPosition.Z") == mEntityTable.mDataColumns.end();
            bool existsScale = mEntityTable.mDataColumns.find("double:Scale") == mEntityTable.mDataColumns.end();
            bool existsOutlineMinX = mEntityTable.mDataColumns.find("double:Outline.Min.X") == mEntityTable.mDataColumns.end();
            bool existsOutlineMinY = mEntityTable.mDataColumns.find("double:Outline.Min.Y") == mEntityTable.mDataColumns.end();
            bool existsOutlineMaxX = mEntityTable.mDataColumns.find("double:Outline.Max.X") == mEntityTable.mDataColumns.end();
            bool existsOutlineMaxY = mEntityTable.mDataColumns.find("double:Outline.Max.Y") == mEntityTable.mDataColumns.end();
            bool existsDetailLevel = mEntityTable.mDataColumns.find("int:DetailLevel") == mEntityTable.mDataColumns.end();
            bool existsCamera = mEntityTable.mIndexColumns.find("index:Vim.Camera:Camera") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<View>* view = new std::vector<View>();
            view->reserve(count);

            const std::vector<int>& titleData = existsTitle ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();

            const std::vector<int>& viewTypeData = existsViewType ? mEntityTable.mStringColumns["string:ViewType"] : std::vector<int>();

            DVector3Converter upConverter;
            ByteRangePtr* upData = new ByteRangePtr[upConverter.GetSize()];
            if (existsUpX && existsUpY && existsUpZ) for (int i = 0; i < upConverter.GetSize(); i++)
                    upData[i] = &mEntityTable.mDataColumns["double:Up" + upConverter.GetColumns()[i]];

            DVector3Converter rightConverter;
            ByteRangePtr* rightData = new ByteRangePtr[rightConverter.GetSize()];
            if (existsRightX && existsRightY && existsRightZ) for (int i = 0; i < rightConverter.GetSize(); i++)
                    rightData[i] = &mEntityTable.mDataColumns["double:Right" + rightConverter.GetColumns()[i]];

            DVector3Converter originConverter;
            ByteRangePtr* originData = new ByteRangePtr[originConverter.GetSize()];
            if (existsOriginX && existsOriginY && existsOriginZ) for (int i = 0; i < originConverter.GetSize(); i++)
                    originData[i] = &mEntityTable.mDataColumns["double:Origin" + originConverter.GetColumns()[i]];

            DVector3Converter viewDirectionConverter;
            ByteRangePtr* viewDirectionData = new ByteRangePtr[viewDirectionConverter.GetSize()];
            if (existsViewDirectionX && existsViewDirectionY && existsViewDirectionZ) for (int i = 0; i < viewDirectionConverter.GetSize(); i++)
                    viewDirectionData[i] = &mEntityTable.mDataColumns["double:ViewDirection" + viewDirectionConverter.GetColumns()[i]];

            DVector3Converter viewPositionConverter;
            ByteRangePtr* viewPositionData = new ByteRangePtr[viewPositionConverter.GetSize()];
            if (existsViewPositionX && existsViewPositionY && existsViewPositionZ) for (int i = 0; i < viewPositionConverter.GetSize(); i++)
                    viewPositionData[i] = &mEntityTable.mDataColumns["double:ViewPosition" + viewPositionConverter.GetColumns()[i]];

            double* scaleData = new double[count];
            if (existsScale) memcpy(scaleData, mEntityTable.mDataColumns["double:Scale"].begin(), count * sizeof(double));

            DAABox2DConverter outlineConverter;
            ByteRangePtr* outlineData = new ByteRangePtr[outlineConverter.GetSize()];
            if (existsOutlineMinX && existsOutlineMinY && existsOutlineMaxX && existsOutlineMaxY) for (int i = 0; i < outlineConverter.GetSize(); i++)
                    outlineData[i] = &mEntityTable.mDataColumns["double:Outline" + outlineConverter.GetColumns()[i]];

            int* detailLevelData = new int[count];
            if (existsDetailLevel) memcpy(detailLevelData, mEntityTable.mDataColumns["int:DetailLevel"].begin(), count * sizeof(int));

            const std::vector<int>& cameraData = existsCamera ? mEntityTable.mIndexColumns["index:Vim.Camera:Camera"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                View entity;
                entity.mIndex = i;
                if (existsTitle)
                    entity.mTitle = &mStrings[titleData[i]];
                if (existsViewType)
                    entity.mViewType = &mStrings[viewTypeData[i]];
                if (existsUpX && existsUpY && existsUpZ)
                    upConverter.ConvertFromColumns(&entity.mUp, upData, i);
                if (existsRightX && existsRightY && existsRightZ)
                    rightConverter.ConvertFromColumns(&entity.mRight, rightData, i);
                if (existsOriginX && existsOriginY && existsOriginZ)
                    originConverter.ConvertFromColumns(&entity.mOrigin, originData, i);
                if (existsViewDirectionX && existsViewDirectionY && existsViewDirectionZ)
                    viewDirectionConverter.ConvertFromColumns(&entity.mViewDirection, viewDirectionData, i);
                if (existsViewPositionX && existsViewPositionY && existsViewPositionZ)
                    viewPositionConverter.ConvertFromColumns(&entity.mViewPosition, viewPositionData, i);
                if (existsScale)
                    entity.mScale = scaleData[i];
                if (existsOutlineMinX && existsOutlineMinY && existsOutlineMaxX && existsOutlineMaxY)
                    outlineConverter.ConvertFromColumns(&entity.mOutline, outlineData, i);
                if (existsDetailLevel)
                    entity.mDetailLevel = detailLevelData[i];
                entity.mCameraIndex = existsCamera ? cameraData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                view->push_back(entity);
            }

            delete[] upData;
            delete[] rightData;
            delete[] originData;
            delete[] viewDirectionData;
            delete[] viewPositionData;
            delete[] scaleData;
            delete[] outlineData;
            delete[] detailLevelData;

            return view;
        }

        const std::string* GetTitle(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Title"][viewIndex]];
        }

        const std::vector<const std::string*>* GetAllTitle()
        {
            if (mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& titleData = mEntityTable.mStringColumns["string:Title"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[titleData[i]]);
            }

            return result;
        }

        const std::string* GetViewType(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:ViewType") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:ViewType"][viewIndex]];
        }

        const std::vector<const std::string*>* GetAllViewType()
        {
            if (mEntityTable.mStringColumns.find("string:ViewType") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& viewTypeData = mEntityTable.mStringColumns["string:ViewType"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[viewTypeData[i]]);
            }

            return result;
        }

        DVector3 GetUp(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Up.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Up.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Up.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter upConverter;
            bfast::byte* bytes = new bfast::byte[upConverter.GetSize() * upConverter.GetBytes()];
            for (int i = 0; i < upConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * upConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Up" + upConverter.GetColumns()[i]].begin()
                       + viewIndex * upConverter.GetBytes(),
                       upConverter.GetBytes());
            }
            DVector3 up = upConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return up;
        }

        const std::vector<DVector3>* GetAllUp()
        {
            if (mEntityTable.mDataColumns.find("double:Up.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Up.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Up.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter upConverter;
            ByteRangePtr* upData = new ByteRangePtr[upConverter.GetSize()];
            for (int i = 0; i < upConverter.GetSize(); i++)
                upData[i] = &mEntityTable.mDataColumns["double:Up" + upConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                upConverter.ConvertFromColumns(&value, upData, i);
                result->push_back(value);
            }

            delete[] upData;

            return result;
        }

        DVector3 GetRight(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Right.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Right.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Right.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter rightConverter;
            bfast::byte* bytes = new bfast::byte[rightConverter.GetSize() * rightConverter.GetBytes()];
            for (int i = 0; i < rightConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * rightConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Right" + rightConverter.GetColumns()[i]].begin()
                       + viewIndex * rightConverter.GetBytes(),
                       rightConverter.GetBytes());
            }
            DVector3 right = rightConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return right;
        }

        const std::vector<DVector3>* GetAllRight()
        {
            if (mEntityTable.mDataColumns.find("double:Right.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Right.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Right.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter rightConverter;
            ByteRangePtr* rightData = new ByteRangePtr[rightConverter.GetSize()];
            for (int i = 0; i < rightConverter.GetSize(); i++)
                rightData[i] = &mEntityTable.mDataColumns["double:Right" + rightConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                rightConverter.ConvertFromColumns(&value, rightData, i);
                result->push_back(value);
            }

            delete[] rightData;

            return result;
        }

        DVector3 GetOrigin(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Origin.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Origin.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Origin.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter originConverter;
            bfast::byte* bytes = new bfast::byte[originConverter.GetSize() * originConverter.GetBytes()];
            for (int i = 0; i < originConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * originConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Origin" + originConverter.GetColumns()[i]].begin()
                       + viewIndex * originConverter.GetBytes(),
                       originConverter.GetBytes());
            }
            DVector3 origin = originConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return origin;
        }

        const std::vector<DVector3>* GetAllOrigin()
        {
            if (mEntityTable.mDataColumns.find("double:Origin.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Origin.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Origin.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter originConverter;
            ByteRangePtr* originData = new ByteRangePtr[originConverter.GetSize()];
            for (int i = 0; i < originConverter.GetSize(); i++)
                originData[i] = &mEntityTable.mDataColumns["double:Origin" + originConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                originConverter.ConvertFromColumns(&value, originData, i);
                result->push_back(value);
            }

            delete[] originData;

            return result;
        }

        DVector3 GetViewDirection(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:ViewDirection.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewDirection.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewDirection.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter viewDirectionConverter;
            bfast::byte* bytes = new bfast::byte[viewDirectionConverter.GetSize() * viewDirectionConverter.GetBytes()];
            for (int i = 0; i < viewDirectionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * viewDirectionConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:ViewDirection" + viewDirectionConverter.GetColumns()[i]].begin()
                       + viewIndex * viewDirectionConverter.GetBytes(),
                       viewDirectionConverter.GetBytes());
            }
            DVector3 viewDirection = viewDirectionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return viewDirection;
        }

        const std::vector<DVector3>* GetAllViewDirection()
        {
            if (mEntityTable.mDataColumns.find("double:ViewDirection.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewDirection.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewDirection.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter viewDirectionConverter;
            ByteRangePtr* viewDirectionData = new ByteRangePtr[viewDirectionConverter.GetSize()];
            for (int i = 0; i < viewDirectionConverter.GetSize(); i++)
                viewDirectionData[i] = &mEntityTable.mDataColumns["double:ViewDirection" + viewDirectionConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                viewDirectionConverter.ConvertFromColumns(&value, viewDirectionData, i);
                result->push_back(value);
            }

            delete[] viewDirectionData;

            return result;
        }

        DVector3 GetViewPosition(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:ViewPosition.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewPosition.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewPosition.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter viewPositionConverter;
            bfast::byte* bytes = new bfast::byte[viewPositionConverter.GetSize() * viewPositionConverter.GetBytes()];
            for (int i = 0; i < viewPositionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * viewPositionConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:ViewPosition" + viewPositionConverter.GetColumns()[i]].begin()
                       + viewIndex * viewPositionConverter.GetBytes(),
                       viewPositionConverter.GetBytes());
            }
            DVector3 viewPosition = viewPositionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return viewPosition;
        }

        const std::vector<DVector3>* GetAllViewPosition()
        {
            if (mEntityTable.mDataColumns.find("double:ViewPosition.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewPosition.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ViewPosition.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter viewPositionConverter;
            ByteRangePtr* viewPositionData = new ByteRangePtr[viewPositionConverter.GetSize()];
            for (int i = 0; i < viewPositionConverter.GetSize(); i++)
                viewPositionData[i] = &mEntityTable.mDataColumns["double:ViewPosition" + viewPositionConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                viewPositionConverter.ConvertFromColumns(&value, viewPositionData, i);
                result->push_back(value);
            }

            delete[] viewPositionData;

            return result;
        }

        double GetScale(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Scale") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Scale"].begin() + viewIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllScale()
        {
            if (mEntityTable.mDataColumns.find("double:Scale") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* scaleData = new double[count];
            memcpy(scaleData, mEntityTable.mDataColumns["double:Scale"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(scaleData, scaleData + count);

            delete[] scaleData;

            return result;
        }

        DAABox2D GetOutline(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Outline.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Max.Y") == mEntityTable.mDataColumns.end())
                return {};

            DAABox2DConverter outlineConverter;
            bfast::byte* bytes = new bfast::byte[outlineConverter.GetSize() * outlineConverter.GetBytes()];
            for (int i = 0; i < outlineConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * outlineConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Outline" + outlineConverter.GetColumns()[i]].begin()
                       + viewIndex * outlineConverter.GetBytes(),
                       outlineConverter.GetBytes());
            }
            DAABox2D outline = outlineConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return outline;
        }

        const std::vector<DAABox2D>* GetAllOutline()
        {
            if (mEntityTable.mDataColumns.find("double:Outline.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Outline.Max.Y") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DAABox2DConverter outlineConverter;
            ByteRangePtr* outlineData = new ByteRangePtr[outlineConverter.GetSize()];
            for (int i = 0; i < outlineConverter.GetSize(); i++)
                outlineData[i] = &mEntityTable.mDataColumns["double:Outline" + outlineConverter.GetColumns()[i]];

            std::vector<DAABox2D>* result = new std::vector<DAABox2D>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DAABox2D value;
                outlineConverter.ConvertFromColumns(&value, outlineData, i);
                result->push_back(value);
            }

            delete[] outlineData;

            return result;
        }

        int GetDetailLevel(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:DetailLevel") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:DetailLevel"].begin() + viewIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllDetailLevel()
        {
            if (mEntityTable.mDataColumns.find("int:DetailLevel") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* detailLevelData = new int[count];
            memcpy(detailLevelData, mEntityTable.mDataColumns["int:DetailLevel"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(detailLevelData, detailLevelData + count);

            delete[] detailLevelData;

            return result;
        }

        int GetCameraIndex(int viewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Camera:Camera") == mEntityTable.mIndexColumns.end())
                return -1;

            if (viewIndex < 0 || viewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Camera:Camera"][viewIndex];
        }

        int GetElementIndex(int viewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (viewIndex < 0 || viewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][viewIndex];
        }

    };

    static ViewTable* GetViewTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.View") == scene.mEntityTables.end())
            return {};

        return new ViewTable(scene.mEntityTables["Vim.View"], scene.mStrings);
    }

    class ElementInView
    {
    public:
        int mIndex;

        int mViewIndex;
        View* mView;
        int mElementIndex;
        Element* mElement;

        ElementInView() {}
    };

    class ElementInViewTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ElementInViewTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.View:View"].size() / sizeof(int);
        }

        ElementInView* Get(int elementInViewIndex)
        {
            ElementInView* elementInView = new ElementInView();
            elementInView->mIndex = elementInViewIndex;
            elementInView->mViewIndex = GetViewIndex(elementInViewIndex);
            elementInView->mElementIndex = GetElementIndex(elementInViewIndex);
            return elementInView;
        }

        std::vector<ElementInView>* GetAll()
        {
            bool existsView = mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ElementInView>* elementInView = new std::vector<ElementInView>();
            elementInView->reserve(count);

            const std::vector<int>& viewData = existsView ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ElementInView entity;
                entity.mIndex = i;
                entity.mViewIndex = existsView ? viewData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                elementInView->push_back(entity);
            }

            return elementInView;
        }

        int GetViewIndex(int elementInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInViewIndex < 0 || elementInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:View"][elementInViewIndex];
        }

        int GetElementIndex(int elementInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInViewIndex < 0 || elementInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][elementInViewIndex];
        }

    };

    static ElementInViewTable* GetElementInViewTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ElementInView") == scene.mEntityTables.end())
            return {};

        return new ElementInViewTable(scene.mEntityTables["Vim.ElementInView"], scene.mStrings);
    }

    class ShapeInView
    {
    public:
        int mIndex;

        int mShapeIndex;
        Shape* mShape;
        int mViewIndex;
        View* mView;

        ShapeInView() {}
    };

    class ShapeInViewTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ShapeInViewTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"].size() / sizeof(int);
        }

        ShapeInView* Get(int shapeInViewIndex)
        {
            ShapeInView* shapeInView = new ShapeInView();
            shapeInView->mIndex = shapeInViewIndex;
            shapeInView->mShapeIndex = GetShapeIndex(shapeInViewIndex);
            shapeInView->mViewIndex = GetViewIndex(shapeInViewIndex);
            return shapeInView;
        }

        std::vector<ShapeInView>* GetAll()
        {
            bool existsShape = mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end();
            bool existsView = mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ShapeInView>* shapeInView = new std::vector<ShapeInView>();
            shapeInView->reserve(count);

            const std::vector<int>& shapeData = existsShape ? mEntityTable.mIndexColumns["index:Vim.Shape:Shape"] : std::vector<int>();
            const std::vector<int>& viewData = existsView ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ShapeInView entity;
                entity.mIndex = i;
                entity.mShapeIndex = existsShape ? shapeData[i] : -1;
                entity.mViewIndex = existsView ? viewData[i] : -1;
                shapeInView->push_back(entity);
            }

            return shapeInView;
        }

        int GetShapeIndex(int shapeInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeInViewIndex < 0 || shapeInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"][shapeInViewIndex];
        }

        int GetViewIndex(int shapeInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeInViewIndex < 0 || shapeInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:View"][shapeInViewIndex];
        }

    };

    static ShapeInViewTable* GetShapeInViewTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ShapeInView") == scene.mEntityTables.end())
            return {};

        return new ShapeInViewTable(scene.mEntityTables["Vim.ShapeInView"], scene.mStrings);
    }

    class AssetInView
    {
    public:
        int mIndex;

        int mAssetIndex;
        Asset* mAsset;
        int mViewIndex;
        View* mView;

        AssetInView() {}
    };

    class AssetInViewTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        AssetInViewTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Asset:Asset") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Asset:Asset"].size() / sizeof(int);
        }

        AssetInView* Get(int assetInViewIndex)
        {
            AssetInView* assetInView = new AssetInView();
            assetInView->mIndex = assetInViewIndex;
            assetInView->mAssetIndex = GetAssetIndex(assetInViewIndex);
            assetInView->mViewIndex = GetViewIndex(assetInViewIndex);
            return assetInView;
        }

        std::vector<AssetInView>* GetAll()
        {
            bool existsAsset = mEntityTable.mIndexColumns.find("index:Vim.Asset:Asset") == mEntityTable.mIndexColumns.end();
            bool existsView = mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<AssetInView>* assetInView = new std::vector<AssetInView>();
            assetInView->reserve(count);

            const std::vector<int>& assetData = existsAsset ? mEntityTable.mIndexColumns["index:Vim.Asset:Asset"] : std::vector<int>();
            const std::vector<int>& viewData = existsView ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                AssetInView entity;
                entity.mIndex = i;
                entity.mAssetIndex = existsAsset ? assetData[i] : -1;
                entity.mViewIndex = existsView ? viewData[i] : -1;
                assetInView->push_back(entity);
            }

            return assetInView;
        }

        int GetAssetIndex(int assetInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Asset:Asset") == mEntityTable.mIndexColumns.end())
                return -1;

            if (assetInViewIndex < 0 || assetInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Asset:Asset"][assetInViewIndex];
        }

        int GetViewIndex(int assetInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end())
                return -1;

            if (assetInViewIndex < 0 || assetInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:View"][assetInViewIndex];
        }

    };

    static AssetInViewTable* GetAssetInViewTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.AssetInView") == scene.mEntityTables.end())
            return {};

        return new AssetInViewTable(scene.mEntityTables["Vim.AssetInView"], scene.mStrings);
    }

    class LevelInView
    {
    public:
        int mIndex;
        DAABox mExtents;

        int mLevelIndex;
        Level* mLevel;
        int mViewIndex;
        View* mView;

        LevelInView() {}
    };

    class LevelInViewTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        LevelInViewTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            DAABoxConverter converter;
            if (mEntityTable.mDataColumns.find("double:Extents" + converter.GetColumns()[0]) == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:Extents" + converter.GetColumns()[0]].size() / sizeof(double);
        }

        LevelInView* Get(int levelInViewIndex)
        {
            LevelInView* levelInView = new LevelInView();
            levelInView->mIndex = levelInViewIndex;
            levelInView->mExtents = GetExtents(levelInViewIndex);
            levelInView->mLevelIndex = GetLevelIndex(levelInViewIndex);
            levelInView->mViewIndex = GetViewIndex(levelInViewIndex);
            return levelInView;
        }

        std::vector<LevelInView>* GetAll()
        {
            bool existsExtentsMinX = mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end();
            bool existsExtentsMinY = mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end();
            bool existsExtentsMinZ = mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxX = mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxY = mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxZ = mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end();
            bool existsLevel = mEntityTable.mIndexColumns.find("index:Vim.Level:Level") == mEntityTable.mIndexColumns.end();
            bool existsView = mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<LevelInView>* levelInView = new std::vector<LevelInView>();
            levelInView->reserve(count);

            DAABoxConverter extentsConverter;
            ByteRangePtr* extentsData = new ByteRangePtr[extentsConverter.GetSize()];
            if (existsExtentsMinX && existsExtentsMinY && existsExtentsMinZ && existsExtentsMaxX && existsExtentsMaxY && existsExtentsMaxZ) for (int i = 0; i < extentsConverter.GetSize(); i++)
                    extentsData[i] = &mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]];

            const std::vector<int>& levelData = existsLevel ? mEntityTable.mIndexColumns["index:Vim.Level:Level"] : std::vector<int>();
            const std::vector<int>& viewData = existsView ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                LevelInView entity;
                entity.mIndex = i;
                if (existsExtentsMinX && existsExtentsMinY && existsExtentsMinZ && existsExtentsMaxX && existsExtentsMaxY && existsExtentsMaxZ)
                    extentsConverter.ConvertFromColumns(&entity.mExtents, extentsData, i);
                entity.mLevelIndex = existsLevel ? levelData[i] : -1;
                entity.mViewIndex = existsView ? viewData[i] : -1;
                levelInView->push_back(entity);
            }

            delete[] extentsData;

            return levelInView;
        }

        DAABox GetExtents(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            DAABoxConverter extentsConverter;
            bfast::byte* bytes = new bfast::byte[extentsConverter.GetSize() * extentsConverter.GetBytes()];
            for (int i = 0; i < extentsConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * extentsConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]].begin()
                       + levelInViewIndex * extentsConverter.GetBytes(),
                       extentsConverter.GetBytes());
            }
            DAABox extents = extentsConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return extents;
        }

        const std::vector<DAABox>* GetAllExtents()
        {
            if (mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DAABoxConverter extentsConverter;
            ByteRangePtr* extentsData = new ByteRangePtr[extentsConverter.GetSize()];
            for (int i = 0; i < extentsConverter.GetSize(); i++)
                extentsData[i] = &mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]];

            std::vector<DAABox>* result = new std::vector<DAABox>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DAABox value;
                extentsConverter.ConvertFromColumns(&value, extentsData, i);
                result->push_back(value);
            }

            delete[] extentsData;

            return result;
        }

        int GetLevelIndex(int levelInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Level:Level") == mEntityTable.mIndexColumns.end())
                return -1;

            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Level:Level"][levelInViewIndex];
        }

        int GetViewIndex(int levelInViewIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.View:View") == mEntityTable.mIndexColumns.end())
                return -1;

            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.View:View"][levelInViewIndex];
        }

    };

    static LevelInViewTable* GetLevelInViewTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.LevelInView") == scene.mEntityTables.end())
            return {};

        return new LevelInViewTable(scene.mEntityTables["Vim.LevelInView"], scene.mStrings);
    }

    class Camera
    {
    public:
        int mIndex;
        int mId;
        int mIsPerspective;
        double mVerticalExtent;
        double mHorizontalExtent;
        double mFarDistance;
        double mNearDistance;
        double mTargetDistance;
        double mRightOffset;
        double mUpOffset;

        Camera() {}
    };

    class CameraTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        CameraTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:Id"].size() / sizeof(int);
        }

        Camera* Get(int cameraIndex)
        {
            Camera* camera = new Camera();
            camera->mIndex = cameraIndex;
            camera->mId = GetId(cameraIndex);
            camera->mIsPerspective = GetIsPerspective(cameraIndex);
            camera->mVerticalExtent = GetVerticalExtent(cameraIndex);
            camera->mHorizontalExtent = GetHorizontalExtent(cameraIndex);
            camera->mFarDistance = GetFarDistance(cameraIndex);
            camera->mNearDistance = GetNearDistance(cameraIndex);
            camera->mTargetDistance = GetTargetDistance(cameraIndex);
            camera->mRightOffset = GetRightOffset(cameraIndex);
            camera->mUpOffset = GetUpOffset(cameraIndex);
            return camera;
        }

        std::vector<Camera>* GetAll()
        {
            bool existsId = mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end();
            bool existsIsPerspective = mEntityTable.mDataColumns.find("int:IsPerspective") == mEntityTable.mDataColumns.end();
            bool existsVerticalExtent = mEntityTable.mDataColumns.find("double:VerticalExtent") == mEntityTable.mDataColumns.end();
            bool existsHorizontalExtent = mEntityTable.mDataColumns.find("double:HorizontalExtent") == mEntityTable.mDataColumns.end();
            bool existsFarDistance = mEntityTable.mDataColumns.find("double:FarDistance") == mEntityTable.mDataColumns.end();
            bool existsNearDistance = mEntityTable.mDataColumns.find("double:NearDistance") == mEntityTable.mDataColumns.end();
            bool existsTargetDistance = mEntityTable.mDataColumns.find("double:TargetDistance") == mEntityTable.mDataColumns.end();
            bool existsRightOffset = mEntityTable.mDataColumns.find("double:RightOffset") == mEntityTable.mDataColumns.end();
            bool existsUpOffset = mEntityTable.mDataColumns.find("double:UpOffset") == mEntityTable.mDataColumns.end();

            const int count = GetCount();

            std::vector<Camera>* camera = new std::vector<Camera>();
            camera->reserve(count);

            int* idData = new int[count];
            if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            int* isPerspectiveData = new int[count];
            if (existsIsPerspective) memcpy(isPerspectiveData, mEntityTable.mDataColumns["int:IsPerspective"].begin(), count * sizeof(int));

            double* verticalExtentData = new double[count];
            if (existsVerticalExtent) memcpy(verticalExtentData, mEntityTable.mDataColumns["double:VerticalExtent"].begin(), count * sizeof(double));

            double* horizontalExtentData = new double[count];
            if (existsHorizontalExtent) memcpy(horizontalExtentData, mEntityTable.mDataColumns["double:HorizontalExtent"].begin(), count * sizeof(double));

            double* farDistanceData = new double[count];
            if (existsFarDistance) memcpy(farDistanceData, mEntityTable.mDataColumns["double:FarDistance"].begin(), count * sizeof(double));

            double* nearDistanceData = new double[count];
            if (existsNearDistance) memcpy(nearDistanceData, mEntityTable.mDataColumns["double:NearDistance"].begin(), count * sizeof(double));

            double* targetDistanceData = new double[count];
            if (existsTargetDistance) memcpy(targetDistanceData, mEntityTable.mDataColumns["double:TargetDistance"].begin(), count * sizeof(double));

            double* rightOffsetData = new double[count];
            if (existsRightOffset) memcpy(rightOffsetData, mEntityTable.mDataColumns["double:RightOffset"].begin(), count * sizeof(double));

            double* upOffsetData = new double[count];
            if (existsUpOffset) memcpy(upOffsetData, mEntityTable.mDataColumns["double:UpOffset"].begin(), count * sizeof(double));

            for (int i = 0; i < count; ++i)
            {
                Camera entity;
                entity.mIndex = i;
                if (existsId)
                    entity.mId = idData[i];
                if (existsIsPerspective)
                    entity.mIsPerspective = isPerspectiveData[i];
                if (existsVerticalExtent)
                    entity.mVerticalExtent = verticalExtentData[i];
                if (existsHorizontalExtent)
                    entity.mHorizontalExtent = horizontalExtentData[i];
                if (existsFarDistance)
                    entity.mFarDistance = farDistanceData[i];
                if (existsNearDistance)
                    entity.mNearDistance = nearDistanceData[i];
                if (existsTargetDistance)
                    entity.mTargetDistance = targetDistanceData[i];
                if (existsRightOffset)
                    entity.mRightOffset = rightOffsetData[i];
                if (existsUpOffset)
                    entity.mUpOffset = upOffsetData[i];
                camera->push_back(entity);
            }

            delete[] idData;
            delete[] isPerspectiveData;
            delete[] verticalExtentData;
            delete[] horizontalExtentData;
            delete[] farDistanceData;
            delete[] nearDistanceData;
            delete[] targetDistanceData;
            delete[] rightOffsetData;
            delete[] upOffsetData;

            return camera;
        }

        int GetId(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + cameraIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllId()
        {
            if (mEntityTable.mDataColumns.find("int:Id") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* idData = new int[count];
            memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(idData, idData + count);

            delete[] idData;

            return result;
        }

        int GetIsPerspective(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:IsPerspective") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:IsPerspective"].begin() + cameraIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllIsPerspective()
        {
            if (mEntityTable.mDataColumns.find("int:IsPerspective") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* isPerspectiveData = new int[count];
            memcpy(isPerspectiveData, mEntityTable.mDataColumns["int:IsPerspective"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(isPerspectiveData, isPerspectiveData + count);

            delete[] isPerspectiveData;

            return result;
        }

        double GetVerticalExtent(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:VerticalExtent") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:VerticalExtent"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllVerticalExtent()
        {
            if (mEntityTable.mDataColumns.find("double:VerticalExtent") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* verticalExtentData = new double[count];
            memcpy(verticalExtentData, mEntityTable.mDataColumns["double:VerticalExtent"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(verticalExtentData, verticalExtentData + count);

            delete[] verticalExtentData;

            return result;
        }

        double GetHorizontalExtent(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:HorizontalExtent") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:HorizontalExtent"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllHorizontalExtent()
        {
            if (mEntityTable.mDataColumns.find("double:HorizontalExtent") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* horizontalExtentData = new double[count];
            memcpy(horizontalExtentData, mEntityTable.mDataColumns["double:HorizontalExtent"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(horizontalExtentData, horizontalExtentData + count);

            delete[] horizontalExtentData;

            return result;
        }

        double GetFarDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:FarDistance") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:FarDistance"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllFarDistance()
        {
            if (mEntityTable.mDataColumns.find("double:FarDistance") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* farDistanceData = new double[count];
            memcpy(farDistanceData, mEntityTable.mDataColumns["double:FarDistance"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(farDistanceData, farDistanceData + count);

            delete[] farDistanceData;

            return result;
        }

        double GetNearDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:NearDistance") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NearDistance"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllNearDistance()
        {
            if (mEntityTable.mDataColumns.find("double:NearDistance") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* nearDistanceData = new double[count];
            memcpy(nearDistanceData, mEntityTable.mDataColumns["double:NearDistance"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(nearDistanceData, nearDistanceData + count);

            delete[] nearDistanceData;

            return result;
        }

        double GetTargetDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:TargetDistance") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:TargetDistance"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllTargetDistance()
        {
            if (mEntityTable.mDataColumns.find("double:TargetDistance") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* targetDistanceData = new double[count];
            memcpy(targetDistanceData, mEntityTable.mDataColumns["double:TargetDistance"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(targetDistanceData, targetDistanceData + count);

            delete[] targetDistanceData;

            return result;
        }

        double GetRightOffset(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:RightOffset") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:RightOffset"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllRightOffset()
        {
            if (mEntityTable.mDataColumns.find("double:RightOffset") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* rightOffsetData = new double[count];
            memcpy(rightOffsetData, mEntityTable.mDataColumns["double:RightOffset"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(rightOffsetData, rightOffsetData + count);

            delete[] rightOffsetData;

            return result;
        }

        double GetUpOffset(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:UpOffset") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:UpOffset"].begin() + cameraIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllUpOffset()
        {
            if (mEntityTable.mDataColumns.find("double:UpOffset") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* upOffsetData = new double[count];
            memcpy(upOffsetData, mEntityTable.mDataColumns["double:UpOffset"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(upOffsetData, upOffsetData + count);

            delete[] upOffsetData;

            return result;
        }

    };

    static CameraTable* GetCameraTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Camera") == scene.mEntityTables.end())
            return {};

        return new CameraTable(scene.mEntityTables["Vim.Camera"], scene.mStrings);
    }

    class Material
    {
    public:
        int mIndex;
        const std::string* mName;
        const std::string* mMaterialCategory;
        DVector3 mColor;
        DVector2 mColorUvScaling;
        DVector2 mColorUvOffset;
        DVector2 mNormalUvScaling;
        DVector2 mNormalUvOffset;
        double mNormalAmount;
        double mGlossiness;
        double mSmoothness;
        double mTransparency;

        int mColorTextureFileIndex;
        Asset* mColorTextureFile;
        int mNormalTextureFileIndex;
        Asset* mNormalTextureFile;
        int mElementIndex;
        Element* mElement;

        Material() {}
    };

    class MaterialTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        MaterialTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Name"].size() / sizeof(int);
        }

        Material* Get(int materialIndex)
        {
            Material* material = new Material();
            material->mIndex = materialIndex;
            material->mName = GetName(materialIndex);
            material->mMaterialCategory = GetMaterialCategory(materialIndex);
            material->mColor = GetColor(materialIndex);
            material->mColorUvScaling = GetColorUvScaling(materialIndex);
            material->mColorUvOffset = GetColorUvOffset(materialIndex);
            material->mNormalUvScaling = GetNormalUvScaling(materialIndex);
            material->mNormalUvOffset = GetNormalUvOffset(materialIndex);
            material->mNormalAmount = GetNormalAmount(materialIndex);
            material->mGlossiness = GetGlossiness(materialIndex);
            material->mSmoothness = GetSmoothness(materialIndex);
            material->mTransparency = GetTransparency(materialIndex);
            material->mColorTextureFileIndex = GetColorTextureFileIndex(materialIndex);
            material->mNormalTextureFileIndex = GetNormalTextureFileIndex(materialIndex);
            material->mElementIndex = GetElementIndex(materialIndex);
            return material;
        }

        std::vector<Material>* GetAll()
        {
            bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
            bool existsMaterialCategory = mEntityTable.mStringColumns.find("string:MaterialCategory") == mEntityTable.mStringColumns.end();
            bool existsColorX = mEntityTable.mDataColumns.find("double:Color.X") == mEntityTable.mDataColumns.end();
            bool existsColorY = mEntityTable.mDataColumns.find("double:Color.Y") == mEntityTable.mDataColumns.end();
            bool existsColorZ = mEntityTable.mDataColumns.find("double:Color.Z") == mEntityTable.mDataColumns.end();
            bool existsColorUvScalingX = mEntityTable.mDataColumns.find("double:ColorUvScaling.X") == mEntityTable.mDataColumns.end();
            bool existsColorUvScalingY = mEntityTable.mDataColumns.find("double:ColorUvScaling.Y") == mEntityTable.mDataColumns.end();
            bool existsColorUvOffsetX = mEntityTable.mDataColumns.find("double:ColorUvOffset.X") == mEntityTable.mDataColumns.end();
            bool existsColorUvOffsetY = mEntityTable.mDataColumns.find("double:ColorUvOffset.Y") == mEntityTable.mDataColumns.end();
            bool existsNormalUvScalingX = mEntityTable.mDataColumns.find("double:NormalUvScaling.X") == mEntityTable.mDataColumns.end();
            bool existsNormalUvScalingY = mEntityTable.mDataColumns.find("double:NormalUvScaling.Y") == mEntityTable.mDataColumns.end();
            bool existsNormalUvOffsetX = mEntityTable.mDataColumns.find("double:NormalUvOffset.X") == mEntityTable.mDataColumns.end();
            bool existsNormalUvOffsetY = mEntityTable.mDataColumns.find("double:NormalUvOffset.Y") == mEntityTable.mDataColumns.end();
            bool existsNormalAmount = mEntityTable.mDataColumns.find("double:NormalAmount") == mEntityTable.mDataColumns.end();
            bool existsGlossiness = mEntityTable.mDataColumns.find("double:Glossiness") == mEntityTable.mDataColumns.end();
            bool existsSmoothness = mEntityTable.mDataColumns.find("double:Smoothness") == mEntityTable.mDataColumns.end();
            bool existsTransparency = mEntityTable.mDataColumns.find("double:Transparency") == mEntityTable.mDataColumns.end();
            bool existsColorTextureFile = mEntityTable.mIndexColumns.find("index:Vim.Asset:ColorTextureFile") == mEntityTable.mIndexColumns.end();
            bool existsNormalTextureFile = mEntityTable.mIndexColumns.find("index:Vim.Asset:NormalTextureFile") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Material>* material = new std::vector<Material>();
            material->reserve(count);

            const std::vector<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();

            const std::vector<int>& materialCategoryData = existsMaterialCategory ? mEntityTable.mStringColumns["string:MaterialCategory"] : std::vector<int>();

            DVector3Converter colorConverter;
            ByteRangePtr* colorData = new ByteRangePtr[colorConverter.GetSize()];
            if (existsColorX && existsColorY && existsColorZ) for (int i = 0; i < colorConverter.GetSize(); i++)
                    colorData[i] = &mEntityTable.mDataColumns["double:Color" + colorConverter.GetColumns()[i]];

            DVector2Converter colorUvScalingConverter;
            ByteRangePtr* colorUvScalingData = new ByteRangePtr[colorUvScalingConverter.GetSize()];
            if (existsColorUvScalingX && existsColorUvScalingY) for (int i = 0; i < colorUvScalingConverter.GetSize(); i++)
                    colorUvScalingData[i] = &mEntityTable.mDataColumns["double:ColorUvScaling" + colorUvScalingConverter.GetColumns()[i]];

            DVector2Converter colorUvOffsetConverter;
            ByteRangePtr* colorUvOffsetData = new ByteRangePtr[colorUvOffsetConverter.GetSize()];
            if (existsColorUvOffsetX && existsColorUvOffsetY) for (int i = 0; i < colorUvOffsetConverter.GetSize(); i++)
                    colorUvOffsetData[i] = &mEntityTable.mDataColumns["double:ColorUvOffset" + colorUvOffsetConverter.GetColumns()[i]];

            DVector2Converter normalUvScalingConverter;
            ByteRangePtr* normalUvScalingData = new ByteRangePtr[normalUvScalingConverter.GetSize()];
            if (existsNormalUvScalingX && existsNormalUvScalingY) for (int i = 0; i < normalUvScalingConverter.GetSize(); i++)
                    normalUvScalingData[i] = &mEntityTable.mDataColumns["double:NormalUvScaling" + normalUvScalingConverter.GetColumns()[i]];

            DVector2Converter normalUvOffsetConverter;
            ByteRangePtr* normalUvOffsetData = new ByteRangePtr[normalUvOffsetConverter.GetSize()];
            if (existsNormalUvOffsetX && existsNormalUvOffsetY) for (int i = 0; i < normalUvOffsetConverter.GetSize(); i++)
                    normalUvOffsetData[i] = &mEntityTable.mDataColumns["double:NormalUvOffset" + normalUvOffsetConverter.GetColumns()[i]];

            double* normalAmountData = new double[count];
            if (existsNormalAmount) memcpy(normalAmountData, mEntityTable.mDataColumns["double:NormalAmount"].begin(), count * sizeof(double));

            double* glossinessData = new double[count];
            if (existsGlossiness) memcpy(glossinessData, mEntityTable.mDataColumns["double:Glossiness"].begin(), count * sizeof(double));

            double* smoothnessData = new double[count];
            if (existsSmoothness) memcpy(smoothnessData, mEntityTable.mDataColumns["double:Smoothness"].begin(), count * sizeof(double));

            double* transparencyData = new double[count];
            if (existsTransparency) memcpy(transparencyData, mEntityTable.mDataColumns["double:Transparency"].begin(), count * sizeof(double));

            const std::vector<int>& colorTextureFileData = existsColorTextureFile ? mEntityTable.mIndexColumns["index:Vim.Asset:ColorTextureFile"] : std::vector<int>();
            const std::vector<int>& normalTextureFileData = existsNormalTextureFile ? mEntityTable.mIndexColumns["index:Vim.Asset:NormalTextureFile"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Material entity;
                entity.mIndex = i;
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsMaterialCategory)
                    entity.mMaterialCategory = &mStrings[materialCategoryData[i]];
                if (existsColorX && existsColorY && existsColorZ)
                    colorConverter.ConvertFromColumns(&entity.mColor, colorData, i);
                if (existsColorUvScalingX && existsColorUvScalingY)
                    colorUvScalingConverter.ConvertFromColumns(&entity.mColorUvScaling, colorUvScalingData, i);
                if (existsColorUvOffsetX && existsColorUvOffsetY)
                    colorUvOffsetConverter.ConvertFromColumns(&entity.mColorUvOffset, colorUvOffsetData, i);
                if (existsNormalUvScalingX && existsNormalUvScalingY)
                    normalUvScalingConverter.ConvertFromColumns(&entity.mNormalUvScaling, normalUvScalingData, i);
                if (existsNormalUvOffsetX && existsNormalUvOffsetY)
                    normalUvOffsetConverter.ConvertFromColumns(&entity.mNormalUvOffset, normalUvOffsetData, i);
                if (existsNormalAmount)
                    entity.mNormalAmount = normalAmountData[i];
                if (existsGlossiness)
                    entity.mGlossiness = glossinessData[i];
                if (existsSmoothness)
                    entity.mSmoothness = smoothnessData[i];
                if (existsTransparency)
                    entity.mTransparency = transparencyData[i];
                entity.mColorTextureFileIndex = existsColorTextureFile ? colorTextureFileData[i] : -1;
                entity.mNormalTextureFileIndex = existsNormalTextureFile ? normalTextureFileData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                material->push_back(entity);
            }

            delete[] colorData;
            delete[] colorUvScalingData;
            delete[] colorUvOffsetData;
            delete[] normalUvScalingData;
            delete[] normalUvOffsetData;
            delete[] normalAmountData;
            delete[] glossinessData;
            delete[] smoothnessData;
            delete[] transparencyData;

            return material;
        }

        const std::string* GetName(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Name"][materialIndex]];
        }

        const std::vector<const std::string*>* GetAllName()
        {
            if (mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& nameData = mEntityTable.mStringColumns["string:Name"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }

            return result;
        }

        const std::string* GetMaterialCategory(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:MaterialCategory") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:MaterialCategory"][materialIndex]];
        }

        const std::vector<const std::string*>* GetAllMaterialCategory()
        {
            if (mEntityTable.mStringColumns.find("string:MaterialCategory") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& materialCategoryData = mEntityTable.mStringColumns["string:MaterialCategory"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[materialCategoryData[i]]);
            }

            return result;
        }

        DVector3 GetColor(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Color.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Color.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Color.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter colorConverter;
            bfast::byte* bytes = new bfast::byte[colorConverter.GetSize() * colorConverter.GetBytes()];
            for (int i = 0; i < colorConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * colorConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Color" + colorConverter.GetColumns()[i]].begin()
                       + materialIndex * colorConverter.GetBytes(),
                       colorConverter.GetBytes());
            }
            DVector3 color = colorConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return color;
        }

        const std::vector<DVector3>* GetAllColor()
        {
            if (mEntityTable.mDataColumns.find("double:Color.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Color.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Color.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter colorConverter;
            ByteRangePtr* colorData = new ByteRangePtr[colorConverter.GetSize()];
            for (int i = 0; i < colorConverter.GetSize(); i++)
                colorData[i] = &mEntityTable.mDataColumns["double:Color" + colorConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                colorConverter.ConvertFromColumns(&value, colorData, i);
                result->push_back(value);
            }

            delete[] colorData;

            return result;
        }

        DVector2 GetColorUvScaling(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:ColorUvScaling.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ColorUvScaling.Y") == mEntityTable.mDataColumns.end())
                return {};

            DVector2Converter colorUvScalingConverter;
            bfast::byte* bytes = new bfast::byte[colorUvScalingConverter.GetSize() * colorUvScalingConverter.GetBytes()];
            for (int i = 0; i < colorUvScalingConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * colorUvScalingConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:ColorUvScaling" + colorUvScalingConverter.GetColumns()[i]].begin()
                       + materialIndex * colorUvScalingConverter.GetBytes(),
                       colorUvScalingConverter.GetBytes());
            }
            DVector2 colorUvScaling = colorUvScalingConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return colorUvScaling;
        }

        const std::vector<DVector2>* GetAllColorUvScaling()
        {
            if (mEntityTable.mDataColumns.find("double:ColorUvScaling.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ColorUvScaling.Y") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector2Converter colorUvScalingConverter;
            ByteRangePtr* colorUvScalingData = new ByteRangePtr[colorUvScalingConverter.GetSize()];
            for (int i = 0; i < colorUvScalingConverter.GetSize(); i++)
                colorUvScalingData[i] = &mEntityTable.mDataColumns["double:ColorUvScaling" + colorUvScalingConverter.GetColumns()[i]];

            std::vector<DVector2>* result = new std::vector<DVector2>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector2 value;
                colorUvScalingConverter.ConvertFromColumns(&value, colorUvScalingData, i);
                result->push_back(value);
            }

            delete[] colorUvScalingData;

            return result;
        }

        DVector2 GetColorUvOffset(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:ColorUvOffset.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ColorUvOffset.Y") == mEntityTable.mDataColumns.end())
                return {};

            DVector2Converter colorUvOffsetConverter;
            bfast::byte* bytes = new bfast::byte[colorUvOffsetConverter.GetSize() * colorUvOffsetConverter.GetBytes()];
            for (int i = 0; i < colorUvOffsetConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * colorUvOffsetConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:ColorUvOffset" + colorUvOffsetConverter.GetColumns()[i]].begin()
                       + materialIndex * colorUvOffsetConverter.GetBytes(),
                       colorUvOffsetConverter.GetBytes());
            }
            DVector2 colorUvOffset = colorUvOffsetConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return colorUvOffset;
        }

        const std::vector<DVector2>* GetAllColorUvOffset()
        {
            if (mEntityTable.mDataColumns.find("double:ColorUvOffset.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:ColorUvOffset.Y") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector2Converter colorUvOffsetConverter;
            ByteRangePtr* colorUvOffsetData = new ByteRangePtr[colorUvOffsetConverter.GetSize()];
            for (int i = 0; i < colorUvOffsetConverter.GetSize(); i++)
                colorUvOffsetData[i] = &mEntityTable.mDataColumns["double:ColorUvOffset" + colorUvOffsetConverter.GetColumns()[i]];

            std::vector<DVector2>* result = new std::vector<DVector2>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector2 value;
                colorUvOffsetConverter.ConvertFromColumns(&value, colorUvOffsetData, i);
                result->push_back(value);
            }

            delete[] colorUvOffsetData;

            return result;
        }

        DVector2 GetNormalUvScaling(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:NormalUvScaling.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:NormalUvScaling.Y") == mEntityTable.mDataColumns.end())
                return {};

            DVector2Converter normalUvScalingConverter;
            bfast::byte* bytes = new bfast::byte[normalUvScalingConverter.GetSize() * normalUvScalingConverter.GetBytes()];
            for (int i = 0; i < normalUvScalingConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * normalUvScalingConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:NormalUvScaling" + normalUvScalingConverter.GetColumns()[i]].begin()
                       + materialIndex * normalUvScalingConverter.GetBytes(),
                       normalUvScalingConverter.GetBytes());
            }
            DVector2 normalUvScaling = normalUvScalingConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return normalUvScaling;
        }

        const std::vector<DVector2>* GetAllNormalUvScaling()
        {
            if (mEntityTable.mDataColumns.find("double:NormalUvScaling.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:NormalUvScaling.Y") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector2Converter normalUvScalingConverter;
            ByteRangePtr* normalUvScalingData = new ByteRangePtr[normalUvScalingConverter.GetSize()];
            for (int i = 0; i < normalUvScalingConverter.GetSize(); i++)
                normalUvScalingData[i] = &mEntityTable.mDataColumns["double:NormalUvScaling" + normalUvScalingConverter.GetColumns()[i]];

            std::vector<DVector2>* result = new std::vector<DVector2>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector2 value;
                normalUvScalingConverter.ConvertFromColumns(&value, normalUvScalingData, i);
                result->push_back(value);
            }

            delete[] normalUvScalingData;

            return result;
        }

        DVector2 GetNormalUvOffset(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:NormalUvOffset.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:NormalUvOffset.Y") == mEntityTable.mDataColumns.end())
                return {};

            DVector2Converter normalUvOffsetConverter;
            bfast::byte* bytes = new bfast::byte[normalUvOffsetConverter.GetSize() * normalUvOffsetConverter.GetBytes()];
            for (int i = 0; i < normalUvOffsetConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * normalUvOffsetConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:NormalUvOffset" + normalUvOffsetConverter.GetColumns()[i]].begin()
                       + materialIndex * normalUvOffsetConverter.GetBytes(),
                       normalUvOffsetConverter.GetBytes());
            }
            DVector2 normalUvOffset = normalUvOffsetConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return normalUvOffset;
        }

        const std::vector<DVector2>* GetAllNormalUvOffset()
        {
            if (mEntityTable.mDataColumns.find("double:NormalUvOffset.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:NormalUvOffset.Y") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector2Converter normalUvOffsetConverter;
            ByteRangePtr* normalUvOffsetData = new ByteRangePtr[normalUvOffsetConverter.GetSize()];
            for (int i = 0; i < normalUvOffsetConverter.GetSize(); i++)
                normalUvOffsetData[i] = &mEntityTable.mDataColumns["double:NormalUvOffset" + normalUvOffsetConverter.GetColumns()[i]];

            std::vector<DVector2>* result = new std::vector<DVector2>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector2 value;
                normalUvOffsetConverter.ConvertFromColumns(&value, normalUvOffsetData, i);
                result->push_back(value);
            }

            delete[] normalUvOffsetData;

            return result;
        }

        double GetNormalAmount(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:NormalAmount") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalAmount"].begin() + materialIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllNormalAmount()
        {
            if (mEntityTable.mDataColumns.find("double:NormalAmount") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* normalAmountData = new double[count];
            memcpy(normalAmountData, mEntityTable.mDataColumns["double:NormalAmount"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(normalAmountData, normalAmountData + count);

            delete[] normalAmountData;

            return result;
        }

        double GetGlossiness(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Glossiness") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Glossiness"].begin() + materialIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllGlossiness()
        {
            if (mEntityTable.mDataColumns.find("double:Glossiness") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* glossinessData = new double[count];
            memcpy(glossinessData, mEntityTable.mDataColumns["double:Glossiness"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(glossinessData, glossinessData + count);

            delete[] glossinessData;

            return result;
        }

        double GetSmoothness(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Smoothness") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Smoothness"].begin() + materialIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllSmoothness()
        {
            if (mEntityTable.mDataColumns.find("double:Smoothness") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* smoothnessData = new double[count];
            memcpy(smoothnessData, mEntityTable.mDataColumns["double:Smoothness"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(smoothnessData, smoothnessData + count);

            delete[] smoothnessData;

            return result;
        }

        double GetTransparency(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Transparency") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Transparency"].begin() + materialIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllTransparency()
        {
            if (mEntityTable.mDataColumns.find("double:Transparency") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* transparencyData = new double[count];
            memcpy(transparencyData, mEntityTable.mDataColumns["double:Transparency"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(transparencyData, transparencyData + count);

            delete[] transparencyData;

            return result;
        }

        int GetColorTextureFileIndex(int materialIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Asset:ColorTextureFile") == mEntityTable.mIndexColumns.end())
                return -1;

            if (materialIndex < 0 || materialIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Asset:ColorTextureFile"][materialIndex];
        }

        int GetNormalTextureFileIndex(int materialIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Asset:NormalTextureFile") == mEntityTable.mIndexColumns.end())
                return -1;

            if (materialIndex < 0 || materialIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Asset:NormalTextureFile"][materialIndex];
        }

        int GetElementIndex(int materialIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (materialIndex < 0 || materialIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][materialIndex];
        }

    };

    static MaterialTable* GetMaterialTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Material") == scene.mEntityTables.end())
            return {};

        return new MaterialTable(scene.mEntityTables["Vim.Material"], scene.mStrings);
    }

    class MaterialInElement
    {
    public:
        int mIndex;
        double mArea;
        double mVolume;
        bool mIsPaint;

        int mMaterialIndex;
        Material* mMaterial;
        int mElementIndex;
        Element* mElement;

        MaterialInElement() {}
    };

    class MaterialInElementTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        MaterialInElementTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:Area"].size() / sizeof(double);
        }

        MaterialInElement* Get(int materialInElementIndex)
        {
            MaterialInElement* materialInElement = new MaterialInElement();
            materialInElement->mIndex = materialInElementIndex;
            materialInElement->mArea = GetArea(materialInElementIndex);
            materialInElement->mVolume = GetVolume(materialInElementIndex);
            materialInElement->mIsPaint = GetIsPaint(materialInElementIndex);
            materialInElement->mMaterialIndex = GetMaterialIndex(materialInElementIndex);
            materialInElement->mElementIndex = GetElementIndex(materialInElementIndex);
            return materialInElement;
        }

        std::vector<MaterialInElement>* GetAll()
        {
            bool existsArea = mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end();
            bool existsVolume = mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end();
            bool existsIsPaint = mEntityTable.mDataColumns.find("byte:IsPaint") == mEntityTable.mDataColumns.end();
            bool existsMaterial = mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<MaterialInElement>* materialInElement = new std::vector<MaterialInElement>();
            materialInElement->reserve(count);

            double* areaData = new double[count];
            if (existsArea) memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));

            double* volumeData = new double[count];
            if (existsVolume) memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));

            bfast::byte* isPaintData = new bfast::byte[count];
            if (existsIsPaint) memcpy(isPaintData, mEntityTable.mDataColumns["byte:IsPaint"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& materialData = existsMaterial ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                MaterialInElement entity;
                entity.mIndex = i;
                if (existsArea)
                    entity.mArea = areaData[i];
                if (existsVolume)
                    entity.mVolume = volumeData[i];
                if (existsIsPaint)
                    entity.mIsPaint = isPaintData[i];
                entity.mMaterialIndex = existsMaterial ? materialData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                materialInElement->push_back(entity);
            }

            delete[] areaData;
            delete[] volumeData;
            delete[] isPaintData;

            return materialInElement;
        }

        double GetArea(int materialInElementIndex)
        {
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Area"].begin() + materialInElementIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllArea()
        {
            if (mEntityTable.mDataColumns.find("double:Area") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* areaData = new double[count];
            memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(areaData, areaData + count);

            delete[] areaData;

            return result;
        }

        double GetVolume(int materialInElementIndex)
        {
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Volume"].begin() + materialInElementIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllVolume()
        {
            if (mEntityTable.mDataColumns.find("double:Volume") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* volumeData = new double[count];
            memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(volumeData, volumeData + count);

            delete[] volumeData;

            return result;
        }

        bool GetIsPaint(int materialInElementIndex)
        {
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsPaint") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPaint"].begin() + materialInElementIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsPaint()
        {
            if (mEntityTable.mDataColumns.find("byte:IsPaint") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isPaintData = new bfast::byte[count];
            memcpy(isPaintData, mEntityTable.mDataColumns["byte:IsPaint"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isPaintData, isPaintData + count);

            delete[] isPaintData;

            return result;
        }

        int GetMaterialIndex(int materialInElementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end())
                return -1;

            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Material:Material"][materialInElementIndex];
        }

        int GetElementIndex(int materialInElementIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][materialInElementIndex];
        }

    };

    static MaterialInElementTable* GetMaterialInElementTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.MaterialInElement") == scene.mEntityTables.end())
            return {};

        return new MaterialInElementTable(scene.mEntityTables["Vim.MaterialInElement"], scene.mStrings);
    }

    class CompoundStructureLayer
    {
    public:
        int mIndex;
        int mOrderIndex;
        double mWidth;
        const std::string* mMaterialFunctionAssignment;

        int mMaterialIndex;
        Material* mMaterial;
        int mCompoundStructureIndex;
        CompoundStructure* mCompoundStructure;

        CompoundStructureLayer() {}
    };

    class CompoundStructureLayerTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        CompoundStructureLayerTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:OrderIndex"].size() / sizeof(int);
        }

        CompoundStructureLayer* Get(int compoundStructureLayerIndex)
        {
            CompoundStructureLayer* compoundStructureLayer = new CompoundStructureLayer();
            compoundStructureLayer->mIndex = compoundStructureLayerIndex;
            compoundStructureLayer->mOrderIndex = GetOrderIndex(compoundStructureLayerIndex);
            compoundStructureLayer->mWidth = GetWidth(compoundStructureLayerIndex);
            compoundStructureLayer->mMaterialFunctionAssignment = GetMaterialFunctionAssignment(compoundStructureLayerIndex);
            compoundStructureLayer->mMaterialIndex = GetMaterialIndex(compoundStructureLayerIndex);
            compoundStructureLayer->mCompoundStructureIndex = GetCompoundStructureIndex(compoundStructureLayerIndex);
            return compoundStructureLayer;
        }

        std::vector<CompoundStructureLayer>* GetAll()
        {
            bool existsOrderIndex = mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end();
            bool existsWidth = mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end();
            bool existsMaterialFunctionAssignment = mEntityTable.mStringColumns.find("string:MaterialFunctionAssignment") == mEntityTable.mStringColumns.end();
            bool existsMaterial = mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end();
            bool existsCompoundStructure = mEntityTable.mIndexColumns.find("index:Vim.CompoundStructure:CompoundStructure") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<CompoundStructureLayer>* compoundStructureLayer = new std::vector<CompoundStructureLayer>();
            compoundStructureLayer->reserve(count);

            int* orderIndexData = new int[count];
            if (existsOrderIndex) memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));

            double* widthData = new double[count];
            if (existsWidth) memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));

            const std::vector<int>& materialFunctionAssignmentData = existsMaterialFunctionAssignment ? mEntityTable.mStringColumns["string:MaterialFunctionAssignment"] : std::vector<int>();

            const std::vector<int>& materialData = existsMaterial ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();
            const std::vector<int>& compoundStructureData = existsCompoundStructure ? mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                CompoundStructureLayer entity;
                entity.mIndex = i;
                if (existsOrderIndex)
                    entity.mOrderIndex = orderIndexData[i];
                if (existsWidth)
                    entity.mWidth = widthData[i];
                if (existsMaterialFunctionAssignment)
                    entity.mMaterialFunctionAssignment = &mStrings[materialFunctionAssignmentData[i]];
                entity.mMaterialIndex = existsMaterial ? materialData[i] : -1;
                entity.mCompoundStructureIndex = existsCompoundStructure ? compoundStructureData[i] : -1;
                compoundStructureLayer->push_back(entity);
            }

            delete[] orderIndexData;
            delete[] widthData;

            return compoundStructureLayer;
        }

        int GetOrderIndex(int compoundStructureLayerIndex)
        {
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:OrderIndex"].begin() + compoundStructureLayerIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllOrderIndex()
        {
            if (mEntityTable.mDataColumns.find("int:OrderIndex") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* orderIndexData = new int[count];
            memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(orderIndexData, orderIndexData + count);

            delete[] orderIndexData;

            return result;
        }

        double GetWidth(int compoundStructureLayerIndex)
        {
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Width"].begin() + compoundStructureLayerIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllWidth()
        {
            if (mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* widthData = new double[count];
            memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(widthData, widthData + count);

            delete[] widthData;

            return result;
        }

        const std::string* GetMaterialFunctionAssignment(int compoundStructureLayerIndex)
        {
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:MaterialFunctionAssignment") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:MaterialFunctionAssignment"][compoundStructureLayerIndex]];
        }

        const std::vector<const std::string*>* GetAllMaterialFunctionAssignment()
        {
            if (mEntityTable.mStringColumns.find("string:MaterialFunctionAssignment") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& materialFunctionAssignmentData = mEntityTable.mStringColumns["string:MaterialFunctionAssignment"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[materialFunctionAssignmentData[i]]);
            }

            return result;
        }

        int GetMaterialIndex(int compoundStructureLayerIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Material:Material") == mEntityTable.mIndexColumns.end())
                return -1;

            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Material:Material"][compoundStructureLayerIndex];
        }

        int GetCompoundStructureIndex(int compoundStructureLayerIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.CompoundStructure:CompoundStructure") == mEntityTable.mIndexColumns.end())
                return -1;

            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"][compoundStructureLayerIndex];
        }

    };

    static CompoundStructureLayerTable* GetCompoundStructureLayerTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.CompoundStructureLayer") == scene.mEntityTables.end())
            return {};

        return new CompoundStructureLayerTable(scene.mEntityTables["Vim.CompoundStructureLayer"], scene.mStrings);
    }

    class CompoundStructure
    {
    public:
        int mIndex;
        double mWidth;

        int mStructuralLayerIndex;
        CompoundStructureLayer* mStructuralLayer;

        CompoundStructure() {}
    };

    class CompoundStructureTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        CompoundStructureTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:Width"].size() / sizeof(double);
        }

        CompoundStructure* Get(int compoundStructureIndex)
        {
            CompoundStructure* compoundStructure = new CompoundStructure();
            compoundStructure->mIndex = compoundStructureIndex;
            compoundStructure->mWidth = GetWidth(compoundStructureIndex);
            compoundStructure->mStructuralLayerIndex = GetStructuralLayerIndex(compoundStructureIndex);
            return compoundStructure;
        }

        std::vector<CompoundStructure>* GetAll()
        {
            bool existsWidth = mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end();
            bool existsStructuralLayer = mEntityTable.mIndexColumns.find("index:Vim.CompoundStructureLayer:StructuralLayer") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<CompoundStructure>* compoundStructure = new std::vector<CompoundStructure>();
            compoundStructure->reserve(count);

            double* widthData = new double[count];
            if (existsWidth) memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));

            const std::vector<int>& structuralLayerData = existsStructuralLayer ? mEntityTable.mIndexColumns["index:Vim.CompoundStructureLayer:StructuralLayer"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                CompoundStructure entity;
                entity.mIndex = i;
                if (existsWidth)
                    entity.mWidth = widthData[i];
                entity.mStructuralLayerIndex = existsStructuralLayer ? structuralLayerData[i] : -1;
                compoundStructure->push_back(entity);
            }

            delete[] widthData;

            return compoundStructure;
        }

        double GetWidth(int compoundStructureIndex)
        {
            if (compoundStructureIndex < 0 || compoundStructureIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Width"].begin() + compoundStructureIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllWidth()
        {
            if (mEntityTable.mDataColumns.find("double:Width") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* widthData = new double[count];
            memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(widthData, widthData + count);

            delete[] widthData;

            return result;
        }

        int GetStructuralLayerIndex(int compoundStructureIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.CompoundStructureLayer:StructuralLayer") == mEntityTable.mIndexColumns.end())
                return -1;

            if (compoundStructureIndex < 0 || compoundStructureIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.CompoundStructureLayer:StructuralLayer"][compoundStructureIndex];
        }

    };

    static CompoundStructureTable* GetCompoundStructureTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.CompoundStructure") == scene.mEntityTables.end())
            return {};

        return new CompoundStructureTable(scene.mEntityTables["Vim.CompoundStructure"], scene.mStrings);
    }

    class Node
    {
    public:
        int mIndex;

        int mElementIndex;
        Element* mElement;

        Node() {}
    };

    class NodeTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        NodeTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"].size() / sizeof(int);
        }

        Node* Get(int nodeIndex)
        {
            Node* node = new Node();
            node->mIndex = nodeIndex;
            node->mElementIndex = GetElementIndex(nodeIndex);
            return node;
        }

        std::vector<Node>* GetAll()
        {
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Node>* node = new std::vector<Node>();
            node->reserve(count);

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Node entity;
                entity.mIndex = i;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                node->push_back(entity);
            }

            return node;
        }

        int GetElementIndex(int nodeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (nodeIndex < 0 || nodeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][nodeIndex];
        }

    };

    static NodeTable* GetNodeTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Node") == scene.mEntityTables.end())
            return {};

        return new NodeTable(scene.mEntityTables["Vim.Node"], scene.mStrings);
    }

    class Geometry
    {
    public:
        int mIndex;
        AABox mBox;
        int mVertexCount;
        int mFaceCount;

        Geometry() {}
    };

    class GeometryTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        GeometryTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            AABoxConverter converter;
            if (mEntityTable.mDataColumns.find("float:Box" + converter.GetColumns()[0]) == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["float:Box" + converter.GetColumns()[0]].size() / sizeof(float);
        }

        Geometry* Get(int geometryIndex)
        {
            Geometry* geometry = new Geometry();
            geometry->mIndex = geometryIndex;
            geometry->mBox = GetBox(geometryIndex);
            geometry->mVertexCount = GetVertexCount(geometryIndex);
            geometry->mFaceCount = GetFaceCount(geometryIndex);
            return geometry;
        }

        std::vector<Geometry>* GetAll()
        {
            bool existsBoxMinX = mEntityTable.mDataColumns.find("float:Box.Min.X") == mEntityTable.mDataColumns.end();
            bool existsBoxMinY = mEntityTable.mDataColumns.find("float:Box.Min.Y") == mEntityTable.mDataColumns.end();
            bool existsBoxMinZ = mEntityTable.mDataColumns.find("float:Box.Min.Z") == mEntityTable.mDataColumns.end();
            bool existsBoxMaxX = mEntityTable.mDataColumns.find("float:Box.Max.X") == mEntityTable.mDataColumns.end();
            bool existsBoxMaxY = mEntityTable.mDataColumns.find("float:Box.Max.Y") == mEntityTable.mDataColumns.end();
            bool existsBoxMaxZ = mEntityTable.mDataColumns.find("float:Box.Max.Z") == mEntityTable.mDataColumns.end();
            bool existsVertexCount = mEntityTable.mDataColumns.find("int:VertexCount") == mEntityTable.mDataColumns.end();
            bool existsFaceCount = mEntityTable.mDataColumns.find("int:FaceCount") == mEntityTable.mDataColumns.end();

            const int count = GetCount();

            std::vector<Geometry>* geometry = new std::vector<Geometry>();
            geometry->reserve(count);

            AABoxConverter boxConverter;
            ByteRangePtr* boxData = new ByteRangePtr[boxConverter.GetSize()];
            if (existsBoxMinX && existsBoxMinY && existsBoxMinZ && existsBoxMaxX && existsBoxMaxY && existsBoxMaxZ) for (int i = 0; i < boxConverter.GetSize(); i++)
                    boxData[i] = &mEntityTable.mDataColumns["float:Box" + boxConverter.GetColumns()[i]];

            int* vertexCountData = new int[count];
            if (existsVertexCount) memcpy(vertexCountData, mEntityTable.mDataColumns["int:VertexCount"].begin(), count * sizeof(int));

            int* faceCountData = new int[count];
            if (existsFaceCount) memcpy(faceCountData, mEntityTable.mDataColumns["int:FaceCount"].begin(), count * sizeof(int));

            for (int i = 0; i < count; ++i)
            {
                Geometry entity;
                entity.mIndex = i;
                if (existsBoxMinX && existsBoxMinY && existsBoxMinZ && existsBoxMaxX && existsBoxMaxY && existsBoxMaxZ)
                    boxConverter.ConvertFromColumns(&entity.mBox, boxData, i);
                if (existsVertexCount)
                    entity.mVertexCount = vertexCountData[i];
                if (existsFaceCount)
                    entity.mFaceCount = faceCountData[i];
                geometry->push_back(entity);
            }

            delete[] boxData;
            delete[] vertexCountData;
            delete[] faceCountData;

            return geometry;
        }

        AABox GetBox(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("float:Box.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            AABoxConverter boxConverter;
            bfast::byte* bytes = new bfast::byte[boxConverter.GetSize() * boxConverter.GetBytes()];
            for (int i = 0; i < boxConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * boxConverter.GetBytes(),
                       mEntityTable.mDataColumns["float:Box" + boxConverter.GetColumns()[i]].begin()
                       + geometryIndex * boxConverter.GetBytes(),
                       boxConverter.GetBytes());
            }
            AABox box = boxConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return box;
        }

        const std::vector<AABox>* GetAllBox()
        {
            if (mEntityTable.mDataColumns.find("float:Box.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("float:Box.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            AABoxConverter boxConverter;
            ByteRangePtr* boxData = new ByteRangePtr[boxConverter.GetSize()];
            for (int i = 0; i < boxConverter.GetSize(); i++)
                boxData[i] = &mEntityTable.mDataColumns["float:Box" + boxConverter.GetColumns()[i]];

            std::vector<AABox>* result = new std::vector<AABox>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                AABox value;
                boxConverter.ConvertFromColumns(&value, boxData, i);
                result->push_back(value);
            }

            delete[] boxData;

            return result;
        }

        int GetVertexCount(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:VertexCount") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:VertexCount"].begin() + geometryIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllVertexCount()
        {
            if (mEntityTable.mDataColumns.find("int:VertexCount") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* vertexCountData = new int[count];
            memcpy(vertexCountData, mEntityTable.mDataColumns["int:VertexCount"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(vertexCountData, vertexCountData + count);

            delete[] vertexCountData;

            return result;
        }

        int GetFaceCount(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:FaceCount") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:FaceCount"].begin() + geometryIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllFaceCount()
        {
            if (mEntityTable.mDataColumns.find("int:FaceCount") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* faceCountData = new int[count];
            memcpy(faceCountData, mEntityTable.mDataColumns["int:FaceCount"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(faceCountData, faceCountData + count);

            delete[] faceCountData;

            return result;
        }

    };

    static GeometryTable* GetGeometryTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Geometry") == scene.mEntityTables.end())
            return {};

        return new GeometryTable(scene.mEntityTables["Vim.Geometry"], scene.mStrings);
    }

    class Shape
    {
    public:
        int mIndex;

        int mElementIndex;
        Element* mElement;

        Shape() {}
    };

    class ShapeTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ShapeTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"].size() / sizeof(int);
        }

        Shape* Get(int shapeIndex)
        {
            Shape* shape = new Shape();
            shape->mIndex = shapeIndex;
            shape->mElementIndex = GetElementIndex(shapeIndex);
            return shape;
        }

        std::vector<Shape>* GetAll()
        {
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Shape>* shape = new std::vector<Shape>();
            shape->reserve(count);

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Shape entity;
                entity.mIndex = i;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                shape->push_back(entity);
            }

            return shape;
        }

        int GetElementIndex(int shapeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeIndex < 0 || shapeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][shapeIndex];
        }

    };

    static ShapeTable* GetShapeTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Shape") == scene.mEntityTables.end())
            return {};

        return new ShapeTable(scene.mEntityTables["Vim.Shape"], scene.mStrings);
    }

    class ShapeCollection
    {
    public:
        int mIndex;

        int mElementIndex;
        Element* mElement;

        ShapeCollection() {}
    };

    class ShapeCollectionTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ShapeCollectionTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"].size() / sizeof(int);
        }

        ShapeCollection* Get(int shapeCollectionIndex)
        {
            ShapeCollection* shapeCollection = new ShapeCollection();
            shapeCollection->mIndex = shapeCollectionIndex;
            shapeCollection->mElementIndex = GetElementIndex(shapeCollectionIndex);
            return shapeCollection;
        }

        std::vector<ShapeCollection>* GetAll()
        {
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ShapeCollection>* shapeCollection = new std::vector<ShapeCollection>();
            shapeCollection->reserve(count);

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ShapeCollection entity;
                entity.mIndex = i;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                shapeCollection->push_back(entity);
            }

            return shapeCollection;
        }

        int GetElementIndex(int shapeCollectionIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeCollectionIndex < 0 || shapeCollectionIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][shapeCollectionIndex];
        }

    };

    static ShapeCollectionTable* GetShapeCollectionTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ShapeCollection") == scene.mEntityTables.end())
            return {};

        return new ShapeCollectionTable(scene.mEntityTables["Vim.ShapeCollection"], scene.mStrings);
    }

    class ShapeInShapeCollection
    {
    public:
        int mIndex;

        int mShapeIndex;
        Shape* mShape;
        int mShapeCollectionIndex;
        ShapeCollection* mShapeCollection;

        ShapeInShapeCollection() {}
    };

    class ShapeInShapeCollectionTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ShapeInShapeCollectionTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"].size() / sizeof(int);
        }

        ShapeInShapeCollection* Get(int shapeInShapeCollectionIndex)
        {
            ShapeInShapeCollection* shapeInShapeCollection = new ShapeInShapeCollection();
            shapeInShapeCollection->mIndex = shapeInShapeCollectionIndex;
            shapeInShapeCollection->mShapeIndex = GetShapeIndex(shapeInShapeCollectionIndex);
            shapeInShapeCollection->mShapeCollectionIndex = GetShapeCollectionIndex(shapeInShapeCollectionIndex);
            return shapeInShapeCollection;
        }

        std::vector<ShapeInShapeCollection>* GetAll()
        {
            bool existsShape = mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end();
            bool existsShapeCollection = mEntityTable.mIndexColumns.find("index:Vim.ShapeCollection:ShapeCollection") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ShapeInShapeCollection>* shapeInShapeCollection = new std::vector<ShapeInShapeCollection>();
            shapeInShapeCollection->reserve(count);

            const std::vector<int>& shapeData = existsShape ? mEntityTable.mIndexColumns["index:Vim.Shape:Shape"] : std::vector<int>();
            const std::vector<int>& shapeCollectionData = existsShapeCollection ? mEntityTable.mIndexColumns["index:Vim.ShapeCollection:ShapeCollection"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ShapeInShapeCollection entity;
                entity.mIndex = i;
                entity.mShapeIndex = existsShape ? shapeData[i] : -1;
                entity.mShapeCollectionIndex = existsShapeCollection ? shapeCollectionData[i] : -1;
                shapeInShapeCollection->push_back(entity);
            }

            return shapeInShapeCollection;
        }

        int GetShapeIndex(int shapeInShapeCollectionIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Shape:Shape") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeInShapeCollectionIndex < 0 || shapeInShapeCollectionIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"][shapeInShapeCollectionIndex];
        }

        int GetShapeCollectionIndex(int shapeInShapeCollectionIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.ShapeCollection:ShapeCollection") == mEntityTable.mIndexColumns.end())
                return -1;

            if (shapeInShapeCollectionIndex < 0 || shapeInShapeCollectionIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.ShapeCollection:ShapeCollection"][shapeInShapeCollectionIndex];
        }

    };

    static ShapeInShapeCollectionTable* GetShapeInShapeCollectionTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ShapeInShapeCollection") == scene.mEntityTables.end())
            return {};

        return new ShapeInShapeCollectionTable(scene.mEntityTables["Vim.ShapeInShapeCollection"], scene.mStrings);
    }

    class System
    {
    public:
        int mIndex;
        int mSystemType;

        int mElementIndex;
        Element* mElement;

        System() {}
    };

    class SystemTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        SystemTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:SystemType") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:SystemType"].size() / sizeof(int);
        }

        System* Get(int systemIndex)
        {
            System* system = new System();
            system->mIndex = systemIndex;
            system->mSystemType = GetSystemType(systemIndex);
            system->mElementIndex = GetElementIndex(systemIndex);
            return system;
        }

        std::vector<System>* GetAll()
        {
            bool existsSystemType = mEntityTable.mDataColumns.find("int:SystemType") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<System>* system = new std::vector<System>();
            system->reserve(count);

            int* systemTypeData = new int[count];
            if (existsSystemType) memcpy(systemTypeData, mEntityTable.mDataColumns["int:SystemType"].begin(), count * sizeof(int));

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                System entity;
                entity.mIndex = i;
                if (existsSystemType)
                    entity.mSystemType = systemTypeData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                system->push_back(entity);
            }

            delete[] systemTypeData;

            return system;
        }

        int GetSystemType(int systemIndex)
        {
            if (systemIndex < 0 || systemIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:SystemType") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:SystemType"].begin() + systemIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllSystemType()
        {
            if (mEntityTable.mDataColumns.find("int:SystemType") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* systemTypeData = new int[count];
            memcpy(systemTypeData, mEntityTable.mDataColumns["int:SystemType"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(systemTypeData, systemTypeData + count);

            delete[] systemTypeData;

            return result;
        }

        int GetElementIndex(int systemIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (systemIndex < 0 || systemIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][systemIndex];
        }

    };

    static SystemTable* GetSystemTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.System") == scene.mEntityTables.end())
            return {};

        return new SystemTable(scene.mEntityTables["Vim.System"], scene.mStrings);
    }

    class ElementInSystem
    {
    public:
        int mIndex;
        int mRoles;

        int mSystemIndex;
        System* mSystem;
        int mElementIndex;
        Element* mElement;

        ElementInSystem() {}
    };

    class ElementInSystemTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ElementInSystemTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:Roles") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:Roles"].size() / sizeof(int);
        }

        ElementInSystem* Get(int elementInSystemIndex)
        {
            ElementInSystem* elementInSystem = new ElementInSystem();
            elementInSystem->mIndex = elementInSystemIndex;
            elementInSystem->mRoles = GetRoles(elementInSystemIndex);
            elementInSystem->mSystemIndex = GetSystemIndex(elementInSystemIndex);
            elementInSystem->mElementIndex = GetElementIndex(elementInSystemIndex);
            return elementInSystem;
        }

        std::vector<ElementInSystem>* GetAll()
        {
            bool existsRoles = mEntityTable.mDataColumns.find("int:Roles") == mEntityTable.mDataColumns.end();
            bool existsSystem = mEntityTable.mIndexColumns.find("index:Vim.System:System") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ElementInSystem>* elementInSystem = new std::vector<ElementInSystem>();
            elementInSystem->reserve(count);

            int* rolesData = new int[count];
            if (existsRoles) memcpy(rolesData, mEntityTable.mDataColumns["int:Roles"].begin(), count * sizeof(int));

            const std::vector<int>& systemData = existsSystem ? mEntityTable.mIndexColumns["index:Vim.System:System"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ElementInSystem entity;
                entity.mIndex = i;
                if (existsRoles)
                    entity.mRoles = rolesData[i];
                entity.mSystemIndex = existsSystem ? systemData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                elementInSystem->push_back(entity);
            }

            delete[] rolesData;

            return elementInSystem;
        }

        int GetRoles(int elementInSystemIndex)
        {
            if (elementInSystemIndex < 0 || elementInSystemIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Roles") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Roles"].begin() + elementInSystemIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllRoles()
        {
            if (mEntityTable.mDataColumns.find("int:Roles") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* rolesData = new int[count];
            memcpy(rolesData, mEntityTable.mDataColumns["int:Roles"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(rolesData, rolesData + count);

            delete[] rolesData;

            return result;
        }

        int GetSystemIndex(int elementInSystemIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.System:System") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInSystemIndex < 0 || elementInSystemIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.System:System"][elementInSystemIndex];
        }

        int GetElementIndex(int elementInSystemIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInSystemIndex < 0 || elementInSystemIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][elementInSystemIndex];
        }

    };

    static ElementInSystemTable* GetElementInSystemTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ElementInSystem") == scene.mEntityTables.end())
            return {};

        return new ElementInSystemTable(scene.mEntityTables["Vim.ElementInSystem"], scene.mStrings);
    }

    class Warning
    {
    public:
        int mIndex;
        const std::string* mGuid;
        const std::string* mSeverity;
        const std::string* mDescription;

        int mBimDocumentIndex;
        BimDocument* mBimDocument;

        Warning() {}
    };

    class WarningTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        WarningTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return 0;

            return mEntityTable.mStringColumns["string:Guid"].size() / sizeof(int);
        }

        Warning* Get(int warningIndex)
        {
            Warning* warning = new Warning();
            warning->mIndex = warningIndex;
            warning->mGuid = GetGuid(warningIndex);
            warning->mSeverity = GetSeverity(warningIndex);
            warning->mDescription = GetDescription(warningIndex);
            warning->mBimDocumentIndex = GetBimDocumentIndex(warningIndex);
            return warning;
        }

        std::vector<Warning>* GetAll()
        {
            bool existsGuid = mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end();
            bool existsSeverity = mEntityTable.mStringColumns.find("string:Severity") == mEntityTable.mStringColumns.end();
            bool existsDescription = mEntityTable.mStringColumns.find("string:Description") == mEntityTable.mStringColumns.end();
            bool existsBimDocument = mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Warning>* warning = new std::vector<Warning>();
            warning->reserve(count);

            const std::vector<int>& guidData = existsGuid ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();

            const std::vector<int>& severityData = existsSeverity ? mEntityTable.mStringColumns["string:Severity"] : std::vector<int>();

            const std::vector<int>& descriptionData = existsDescription ? mEntityTable.mStringColumns["string:Description"] : std::vector<int>();

            const std::vector<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Warning entity;
                entity.mIndex = i;
                if (existsGuid)
                    entity.mGuid = &mStrings[guidData[i]];
                if (existsSeverity)
                    entity.mSeverity = &mStrings[severityData[i]];
                if (existsDescription)
                    entity.mDescription = &mStrings[descriptionData[i]];
                entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
                warning->push_back(entity);
            }

            return warning;
        }

        const std::string* GetGuid(int warningIndex)
        {
            if (warningIndex < 0 || warningIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Guid"][warningIndex]];
        }

        const std::vector<const std::string*>* GetAllGuid()
        {
            if (mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& guidData = mEntityTable.mStringColumns["string:Guid"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[guidData[i]]);
            }

            return result;
        }

        const std::string* GetSeverity(int warningIndex)
        {
            if (warningIndex < 0 || warningIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Severity") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Severity"][warningIndex]];
        }

        const std::vector<const std::string*>* GetAllSeverity()
        {
            if (mEntityTable.mStringColumns.find("string:Severity") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& severityData = mEntityTable.mStringColumns["string:Severity"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[severityData[i]]);
            }

            return result;
        }

        const std::string* GetDescription(int warningIndex)
        {
            if (warningIndex < 0 || warningIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Description") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Description"][warningIndex]];
        }

        const std::vector<const std::string*>* GetAllDescription()
        {
            if (mEntityTable.mStringColumns.find("string:Description") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& descriptionData = mEntityTable.mStringColumns["string:Description"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[descriptionData[i]]);
            }

            return result;
        }

        int GetBimDocumentIndex(int warningIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.BimDocument:BimDocument") == mEntityTable.mIndexColumns.end())
                return -1;

            if (warningIndex < 0 || warningIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][warningIndex];
        }

    };

    static WarningTable* GetWarningTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Warning") == scene.mEntityTables.end())
            return {};

        return new WarningTable(scene.mEntityTables["Vim.Warning"], scene.mStrings);
    }

    class ElementInWarning
    {
    public:
        int mIndex;

        int mWarningIndex;
        Warning* mWarning;
        int mElementIndex;
        Element* mElement;

        ElementInWarning() {}
    };

    class ElementInWarningTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ElementInWarningTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Warning:Warning") == mEntityTable.mIndexColumns.end())
                return 0;

            return mEntityTable.mIndexColumns["index:Vim.Warning:Warning"].size() / sizeof(int);
        }

        ElementInWarning* Get(int elementInWarningIndex)
        {
            ElementInWarning* elementInWarning = new ElementInWarning();
            elementInWarning->mIndex = elementInWarningIndex;
            elementInWarning->mWarningIndex = GetWarningIndex(elementInWarningIndex);
            elementInWarning->mElementIndex = GetElementIndex(elementInWarningIndex);
            return elementInWarning;
        }

        std::vector<ElementInWarning>* GetAll()
        {
            bool existsWarning = mEntityTable.mIndexColumns.find("index:Vim.Warning:Warning") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<ElementInWarning>* elementInWarning = new std::vector<ElementInWarning>();
            elementInWarning->reserve(count);

            const std::vector<int>& warningData = existsWarning ? mEntityTable.mIndexColumns["index:Vim.Warning:Warning"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                ElementInWarning entity;
                entity.mIndex = i;
                entity.mWarningIndex = existsWarning ? warningData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                elementInWarning->push_back(entity);
            }

            return elementInWarning;
        }

        int GetWarningIndex(int elementInWarningIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Warning:Warning") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInWarningIndex < 0 || elementInWarningIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Warning:Warning"][elementInWarningIndex];
        }

        int GetElementIndex(int elementInWarningIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (elementInWarningIndex < 0 || elementInWarningIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][elementInWarningIndex];
        }

    };

    static ElementInWarningTable* GetElementInWarningTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ElementInWarning") == scene.mEntityTables.end())
            return {};

        return new ElementInWarningTable(scene.mEntityTables["Vim.ElementInWarning"], scene.mStrings);
    }

    class BasePoint
    {
    public:
        int mIndex;
        bool mIsSurveyPoint;
        DVector3 mPosition;
        DVector3 mSharedPosition;

        int mElementIndex;
        Element* mElement;

        BasePoint() {}
    };

    class BasePointTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        BasePointTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("byte:IsSurveyPoint") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["byte:IsSurveyPoint"].size() / sizeof(bfast::byte);
        }

        BasePoint* Get(int basePointIndex)
        {
            BasePoint* basePoint = new BasePoint();
            basePoint->mIndex = basePointIndex;
            basePoint->mIsSurveyPoint = GetIsSurveyPoint(basePointIndex);
            basePoint->mPosition = GetPosition(basePointIndex);
            basePoint->mSharedPosition = GetSharedPosition(basePointIndex);
            basePoint->mElementIndex = GetElementIndex(basePointIndex);
            return basePoint;
        }

        std::vector<BasePoint>* GetAll()
        {
            bool existsIsSurveyPoint = mEntityTable.mDataColumns.find("byte:IsSurveyPoint") == mEntityTable.mDataColumns.end();
            bool existsPositionX = mEntityTable.mDataColumns.find("double:Position.X") == mEntityTable.mDataColumns.end();
            bool existsPositionY = mEntityTable.mDataColumns.find("double:Position.Y") == mEntityTable.mDataColumns.end();
            bool existsPositionZ = mEntityTable.mDataColumns.find("double:Position.Z") == mEntityTable.mDataColumns.end();
            bool existsSharedPositionX = mEntityTable.mDataColumns.find("double:SharedPosition.X") == mEntityTable.mDataColumns.end();
            bool existsSharedPositionY = mEntityTable.mDataColumns.find("double:SharedPosition.Y") == mEntityTable.mDataColumns.end();
            bool existsSharedPositionZ = mEntityTable.mDataColumns.find("double:SharedPosition.Z") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<BasePoint>* basePoint = new std::vector<BasePoint>();
            basePoint->reserve(count);

            bfast::byte* isSurveyPointData = new bfast::byte[count];
            if (existsIsSurveyPoint) memcpy(isSurveyPointData, mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin(), count * sizeof(bfast::byte));

            DVector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            if (existsPositionX && existsPositionY && existsPositionZ) for (int i = 0; i < positionConverter.GetSize(); i++)
                    positionData[i] = &mEntityTable.mDataColumns["double:Position" + positionConverter.GetColumns()[i]];

            DVector3Converter sharedPositionConverter;
            ByteRangePtr* sharedPositionData = new ByteRangePtr[sharedPositionConverter.GetSize()];
            if (existsSharedPositionX && existsSharedPositionY && existsSharedPositionZ) for (int i = 0; i < sharedPositionConverter.GetSize(); i++)
                    sharedPositionData[i] = &mEntityTable.mDataColumns["double:SharedPosition" + sharedPositionConverter.GetColumns()[i]];

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                BasePoint entity;
                entity.mIndex = i;
                if (existsIsSurveyPoint)
                    entity.mIsSurveyPoint = isSurveyPointData[i];
                if (existsPositionX && existsPositionY && existsPositionZ)
                    positionConverter.ConvertFromColumns(&entity.mPosition, positionData, i);
                if (existsSharedPositionX && existsSharedPositionY && existsSharedPositionZ)
                    sharedPositionConverter.ConvertFromColumns(&entity.mSharedPosition, sharedPositionData, i);
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                basePoint->push_back(entity);
            }

            delete[] isSurveyPointData;
            delete[] positionData;
            delete[] sharedPositionData;

            return basePoint;
        }

        bool GetIsSurveyPoint(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsSurveyPoint") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin() + basePointIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsSurveyPoint()
        {
            if (mEntityTable.mDataColumns.find("byte:IsSurveyPoint") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isSurveyPointData = new bfast::byte[count];
            memcpy(isSurveyPointData, mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isSurveyPointData, isSurveyPointData + count);

            delete[] isSurveyPointData;

            return result;
        }

        DVector3 GetPosition(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter positionConverter;
            bfast::byte* bytes = new bfast::byte[positionConverter.GetSize() * positionConverter.GetBytes()];
            for (int i = 0; i < positionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * positionConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Position" + positionConverter.GetColumns()[i]].begin()
                       + basePointIndex * positionConverter.GetBytes(),
                       positionConverter.GetBytes());
            }
            DVector3 position = positionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return position;
        }

        const std::vector<DVector3>* GetAllPosition()
        {
            if (mEntityTable.mDataColumns.find("double:Position.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Position.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Position.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter positionConverter;
            ByteRangePtr* positionData = new ByteRangePtr[positionConverter.GetSize()];
            for (int i = 0; i < positionConverter.GetSize(); i++)
                positionData[i] = &mEntityTable.mDataColumns["double:Position" + positionConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                positionConverter.ConvertFromColumns(&value, positionData, i);
                result->push_back(value);
            }

            delete[] positionData;

            return result;
        }

        DVector3 GetSharedPosition(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:SharedPosition.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:SharedPosition.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:SharedPosition.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter sharedPositionConverter;
            bfast::byte* bytes = new bfast::byte[sharedPositionConverter.GetSize() * sharedPositionConverter.GetBytes()];
            for (int i = 0; i < sharedPositionConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * sharedPositionConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:SharedPosition" + sharedPositionConverter.GetColumns()[i]].begin()
                       + basePointIndex * sharedPositionConverter.GetBytes(),
                       sharedPositionConverter.GetBytes());
            }
            DVector3 sharedPosition = sharedPositionConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return sharedPosition;
        }

        const std::vector<DVector3>* GetAllSharedPosition()
        {
            if (mEntityTable.mDataColumns.find("double:SharedPosition.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:SharedPosition.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:SharedPosition.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter sharedPositionConverter;
            ByteRangePtr* sharedPositionData = new ByteRangePtr[sharedPositionConverter.GetSize()];
            for (int i = 0; i < sharedPositionConverter.GetSize(); i++)
                sharedPositionData[i] = &mEntityTable.mDataColumns["double:SharedPosition" + sharedPositionConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                sharedPositionConverter.ConvertFromColumns(&value, sharedPositionData, i);
                result->push_back(value);
            }

            delete[] sharedPositionData;

            return result;
        }

        int GetElementIndex(int basePointIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][basePointIndex];
        }

    };

    static BasePointTable* GetBasePointTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.BasePoint") == scene.mEntityTables.end())
            return {};

        return new BasePointTable(scene.mEntityTables["Vim.BasePoint"], scene.mStrings);
    }

    class PhaseFilter
    {
    public:
        int mIndex;
        int mNew;
        int mExisting;
        int mDemolished;
        int mTemporary;

        int mElementIndex;
        Element* mElement;

        PhaseFilter() {}
    };

    class PhaseFilterTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        PhaseFilterTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("int:New") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["int:New"].size() / sizeof(int);
        }

        PhaseFilter* Get(int phaseFilterIndex)
        {
            PhaseFilter* phaseFilter = new PhaseFilter();
            phaseFilter->mIndex = phaseFilterIndex;
            phaseFilter->mNew = GetNew(phaseFilterIndex);
            phaseFilter->mExisting = GetExisting(phaseFilterIndex);
            phaseFilter->mDemolished = GetDemolished(phaseFilterIndex);
            phaseFilter->mTemporary = GetTemporary(phaseFilterIndex);
            phaseFilter->mElementIndex = GetElementIndex(phaseFilterIndex);
            return phaseFilter;
        }

        std::vector<PhaseFilter>* GetAll()
        {
            bool existsNew = mEntityTable.mDataColumns.find("int:New") == mEntityTable.mDataColumns.end();
            bool existsExisting = mEntityTable.mDataColumns.find("int:Existing") == mEntityTable.mDataColumns.end();
            bool existsDemolished = mEntityTable.mDataColumns.find("int:Demolished") == mEntityTable.mDataColumns.end();
            bool existsTemporary = mEntityTable.mDataColumns.find("int:Temporary") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<PhaseFilter>* phaseFilter = new std::vector<PhaseFilter>();
            phaseFilter->reserve(count);

            int* newData = new int[count];
            if (existsNew) memcpy(newData, mEntityTable.mDataColumns["int:New"].begin(), count * sizeof(int));

            int* existingData = new int[count];
            if (existsExisting) memcpy(existingData, mEntityTable.mDataColumns["int:Existing"].begin(), count * sizeof(int));

            int* demolishedData = new int[count];
            if (existsDemolished) memcpy(demolishedData, mEntityTable.mDataColumns["int:Demolished"].begin(), count * sizeof(int));

            int* temporaryData = new int[count];
            if (existsTemporary) memcpy(temporaryData, mEntityTable.mDataColumns["int:Temporary"].begin(), count * sizeof(int));

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                PhaseFilter entity;
                entity.mIndex = i;
                if (existsNew)
                    entity.mNew = newData[i];
                if (existsExisting)
                    entity.mExisting = existingData[i];
                if (existsDemolished)
                    entity.mDemolished = demolishedData[i];
                if (existsTemporary)
                    entity.mTemporary = temporaryData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                phaseFilter->push_back(entity);
            }

            delete[] newData;
            delete[] existingData;
            delete[] demolishedData;
            delete[] temporaryData;

            return phaseFilter;
        }

        int GetNew(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:New") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:New"].begin() + phaseFilterIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllNew()
        {
            if (mEntityTable.mDataColumns.find("int:New") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* newData = new int[count];
            memcpy(newData, mEntityTable.mDataColumns["int:New"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(newData, newData + count);

            delete[] newData;

            return result;
        }

        int GetExisting(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Existing") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Existing"].begin() + phaseFilterIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllExisting()
        {
            if (mEntityTable.mDataColumns.find("int:Existing") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* existingData = new int[count];
            memcpy(existingData, mEntityTable.mDataColumns["int:Existing"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(existingData, existingData + count);

            delete[] existingData;

            return result;
        }

        int GetDemolished(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Demolished") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Demolished"].begin() + phaseFilterIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllDemolished()
        {
            if (mEntityTable.mDataColumns.find("int:Demolished") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* demolishedData = new int[count];
            memcpy(demolishedData, mEntityTable.mDataColumns["int:Demolished"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(demolishedData, demolishedData + count);

            delete[] demolishedData;

            return result;
        }

        int GetTemporary(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("int:Temporary") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Temporary"].begin() + phaseFilterIndex * sizeof(int)));
        }

        const std::vector<int>* GetAllTemporary()
        {
            if (mEntityTable.mDataColumns.find("int:Temporary") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            int* temporaryData = new int[count];
            memcpy(temporaryData, mEntityTable.mDataColumns["int:Temporary"].begin(), count * sizeof(int));

            std::vector<int>* result = new std::vector<int>(temporaryData, temporaryData + count);

            delete[] temporaryData;

            return result;
        }

        int GetElementIndex(int phaseFilterIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][phaseFilterIndex];
        }

    };

    static PhaseFilterTable* GetPhaseFilterTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.PhaseFilter") == scene.mEntityTables.end())
            return {};

        return new PhaseFilterTable(scene.mEntityTables["Vim.PhaseFilter"], scene.mStrings);
    }

    class Grid
    {
    public:
        int mIndex;
        DVector3 mStartPoint;
        DVector3 mEndPoint;
        bool mIsCurved;
        DAABox mExtents;

        int mElementIndex;
        Element* mElement;

        Grid() {}
    };

    class GridTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        GridTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            DVector3Converter converter;
            if (mEntityTable.mDataColumns.find("double:StartPoint" + converter.GetColumns()[0]) == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:StartPoint" + converter.GetColumns()[0]].size() / sizeof(double);
        }

        Grid* Get(int gridIndex)
        {
            Grid* grid = new Grid();
            grid->mIndex = gridIndex;
            grid->mStartPoint = GetStartPoint(gridIndex);
            grid->mEndPoint = GetEndPoint(gridIndex);
            grid->mIsCurved = GetIsCurved(gridIndex);
            grid->mExtents = GetExtents(gridIndex);
            grid->mElementIndex = GetElementIndex(gridIndex);
            return grid;
        }

        std::vector<Grid>* GetAll()
        {
            bool existsStartPointX = mEntityTable.mDataColumns.find("double:StartPoint.X") == mEntityTable.mDataColumns.end();
            bool existsStartPointY = mEntityTable.mDataColumns.find("double:StartPoint.Y") == mEntityTable.mDataColumns.end();
            bool existsStartPointZ = mEntityTable.mDataColumns.find("double:StartPoint.Z") == mEntityTable.mDataColumns.end();
            bool existsEndPointX = mEntityTable.mDataColumns.find("double:EndPoint.X") == mEntityTable.mDataColumns.end();
            bool existsEndPointY = mEntityTable.mDataColumns.find("double:EndPoint.Y") == mEntityTable.mDataColumns.end();
            bool existsEndPointZ = mEntityTable.mDataColumns.find("double:EndPoint.Z") == mEntityTable.mDataColumns.end();
            bool existsIsCurved = mEntityTable.mDataColumns.find("byte:IsCurved") == mEntityTable.mDataColumns.end();
            bool existsExtentsMinX = mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end();
            bool existsExtentsMinY = mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end();
            bool existsExtentsMinZ = mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxX = mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxY = mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end();
            bool existsExtentsMaxZ = mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Grid>* grid = new std::vector<Grid>();
            grid->reserve(count);

            DVector3Converter startPointConverter;
            ByteRangePtr* startPointData = new ByteRangePtr[startPointConverter.GetSize()];
            if (existsStartPointX && existsStartPointY && existsStartPointZ) for (int i = 0; i < startPointConverter.GetSize(); i++)
                    startPointData[i] = &mEntityTable.mDataColumns["double:StartPoint" + startPointConverter.GetColumns()[i]];

            DVector3Converter endPointConverter;
            ByteRangePtr* endPointData = new ByteRangePtr[endPointConverter.GetSize()];
            if (existsEndPointX && existsEndPointY && existsEndPointZ) for (int i = 0; i < endPointConverter.GetSize(); i++)
                    endPointData[i] = &mEntityTable.mDataColumns["double:EndPoint" + endPointConverter.GetColumns()[i]];

            bfast::byte* isCurvedData = new bfast::byte[count];
            if (existsIsCurved) memcpy(isCurvedData, mEntityTable.mDataColumns["byte:IsCurved"].begin(), count * sizeof(bfast::byte));

            DAABoxConverter extentsConverter;
            ByteRangePtr* extentsData = new ByteRangePtr[extentsConverter.GetSize()];
            if (existsExtentsMinX && existsExtentsMinY && existsExtentsMinZ && existsExtentsMaxX && existsExtentsMaxY && existsExtentsMaxZ) for (int i = 0; i < extentsConverter.GetSize(); i++)
                    extentsData[i] = &mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]];

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Grid entity;
                entity.mIndex = i;
                if (existsStartPointX && existsStartPointY && existsStartPointZ)
                    startPointConverter.ConvertFromColumns(&entity.mStartPoint, startPointData, i);
                if (existsEndPointX && existsEndPointY && existsEndPointZ)
                    endPointConverter.ConvertFromColumns(&entity.mEndPoint, endPointData, i);
                if (existsIsCurved)
                    entity.mIsCurved = isCurvedData[i];
                if (existsExtentsMinX && existsExtentsMinY && existsExtentsMinZ && existsExtentsMaxX && existsExtentsMaxY && existsExtentsMaxZ)
                    extentsConverter.ConvertFromColumns(&entity.mExtents, extentsData, i);
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                grid->push_back(entity);
            }

            delete[] startPointData;
            delete[] endPointData;
            delete[] isCurvedData;
            delete[] extentsData;

            return grid;
        }

        DVector3 GetStartPoint(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:StartPoint.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:StartPoint.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:StartPoint.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter startPointConverter;
            bfast::byte* bytes = new bfast::byte[startPointConverter.GetSize() * startPointConverter.GetBytes()];
            for (int i = 0; i < startPointConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * startPointConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:StartPoint" + startPointConverter.GetColumns()[i]].begin()
                       + gridIndex * startPointConverter.GetBytes(),
                       startPointConverter.GetBytes());
            }
            DVector3 startPoint = startPointConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return startPoint;
        }

        const std::vector<DVector3>* GetAllStartPoint()
        {
            if (mEntityTable.mDataColumns.find("double:StartPoint.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:StartPoint.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:StartPoint.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter startPointConverter;
            ByteRangePtr* startPointData = new ByteRangePtr[startPointConverter.GetSize()];
            for (int i = 0; i < startPointConverter.GetSize(); i++)
                startPointData[i] = &mEntityTable.mDataColumns["double:StartPoint" + startPointConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                startPointConverter.ConvertFromColumns(&value, startPointData, i);
                result->push_back(value);
            }

            delete[] startPointData;

            return result;
        }

        DVector3 GetEndPoint(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:EndPoint.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:EndPoint.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:EndPoint.Z") == mEntityTable.mDataColumns.end())
                return {};

            DVector3Converter endPointConverter;
            bfast::byte* bytes = new bfast::byte[endPointConverter.GetSize() * endPointConverter.GetBytes()];
            for (int i = 0; i < endPointConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * endPointConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:EndPoint" + endPointConverter.GetColumns()[i]].begin()
                       + gridIndex * endPointConverter.GetBytes(),
                       endPointConverter.GetBytes());
            }
            DVector3 endPoint = endPointConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return endPoint;
        }

        const std::vector<DVector3>* GetAllEndPoint()
        {
            if (mEntityTable.mDataColumns.find("double:EndPoint.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:EndPoint.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:EndPoint.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DVector3Converter endPointConverter;
            ByteRangePtr* endPointData = new ByteRangePtr[endPointConverter.GetSize()];
            for (int i = 0; i < endPointConverter.GetSize(); i++)
                endPointData[i] = &mEntityTable.mDataColumns["double:EndPoint" + endPointConverter.GetColumns()[i]];

            std::vector<DVector3>* result = new std::vector<DVector3>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DVector3 value;
                endPointConverter.ConvertFromColumns(&value, endPointData, i);
                result->push_back(value);
            }

            delete[] endPointData;

            return result;
        }

        bool GetIsCurved(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsCurved") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsCurved"].begin() + gridIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsCurved()
        {
            if (mEntityTable.mDataColumns.find("byte:IsCurved") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isCurvedData = new bfast::byte[count];
            memcpy(isCurvedData, mEntityTable.mDataColumns["byte:IsCurved"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isCurvedData, isCurvedData + count);

            delete[] isCurvedData;

            return result;
        }

        DAABox GetExtents(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            DAABoxConverter extentsConverter;
            bfast::byte* bytes = new bfast::byte[extentsConverter.GetSize() * extentsConverter.GetBytes()];
            for (int i = 0; i < extentsConverter.GetSize(); ++i)
            {
                memcpy(bytes + i * extentsConverter.GetBytes(),
                       mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]].begin()
                       + gridIndex * extentsConverter.GetBytes(),
                       extentsConverter.GetBytes());
            }
            DAABox extents = extentsConverter.ConvertFromArray(bytes);
            delete[] bytes;
            return extents;
        }

        const std::vector<DAABox>* GetAllExtents()
        {
            if (mEntityTable.mDataColumns.find("double:Extents.Min.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Min.Z") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.X") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Y") == mEntityTable.mDataColumns.end() ||
                mEntityTable.mDataColumns.find("double:Extents.Max.Z") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            DAABoxConverter extentsConverter;
            ByteRangePtr* extentsData = new ByteRangePtr[extentsConverter.GetSize()];
            for (int i = 0; i < extentsConverter.GetSize(); i++)
                extentsData[i] = &mEntityTable.mDataColumns["double:Extents" + extentsConverter.GetColumns()[i]];

            std::vector<DAABox>* result = new std::vector<DAABox>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                DAABox value;
                extentsConverter.ConvertFromColumns(&value, extentsData, i);
                result->push_back(value);
            }

            delete[] extentsData;

            return result;
        }

        int GetElementIndex(int gridIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (gridIndex < 0 || gridIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][gridIndex];
        }

    };

    static GridTable* GetGridTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Grid") == scene.mEntityTables.end())
            return {};

        return new GridTable(scene.mEntityTables["Vim.Grid"], scene.mStrings);
    }

    class Area
    {
    public:
        int mIndex;
        double mValue;
        double mPerimeter;
        const std::string* mNumber;
        bool mIsGrossInterior;

        int mAreaSchemeIndex;
        AreaScheme* mAreaScheme;
        int mElementIndex;
        Element* mElement;

        Area() {}
    };

    class AreaTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        AreaTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("double:Value") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["double:Value"].size() / sizeof(double);
        }

        Area* Get(int areaIndex)
        {
            Area* area = new Area();
            area->mIndex = areaIndex;
            area->mValue = GetValue(areaIndex);
            area->mPerimeter = GetPerimeter(areaIndex);
            area->mNumber = GetNumber(areaIndex);
            area->mIsGrossInterior = GetIsGrossInterior(areaIndex);
            area->mAreaSchemeIndex = GetAreaSchemeIndex(areaIndex);
            area->mElementIndex = GetElementIndex(areaIndex);
            return area;
        }

        std::vector<Area>* GetAll()
        {
            bool existsValue = mEntityTable.mDataColumns.find("double:Value") == mEntityTable.mDataColumns.end();
            bool existsPerimeter = mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end();
            bool existsNumber = mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end();
            bool existsIsGrossInterior = mEntityTable.mDataColumns.find("byte:IsGrossInterior") == mEntityTable.mDataColumns.end();
            bool existsAreaScheme = mEntityTable.mIndexColumns.find("index:Vim.AreaScheme:AreaScheme") == mEntityTable.mIndexColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<Area>* area = new std::vector<Area>();
            area->reserve(count);

            double* valueData = new double[count];
            if (existsValue) memcpy(valueData, mEntityTable.mDataColumns["double:Value"].begin(), count * sizeof(double));

            double* perimeterData = new double[count];
            if (existsPerimeter) memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));

            const std::vector<int>& numberData = existsNumber ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();

            bfast::byte* isGrossInteriorData = new bfast::byte[count];
            if (existsIsGrossInterior) memcpy(isGrossInteriorData, mEntityTable.mDataColumns["byte:IsGrossInterior"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& areaSchemeData = existsAreaScheme ? mEntityTable.mIndexColumns["index:Vim.AreaScheme:AreaScheme"] : std::vector<int>();
            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                Area entity;
                entity.mIndex = i;
                if (existsValue)
                    entity.mValue = valueData[i];
                if (existsPerimeter)
                    entity.mPerimeter = perimeterData[i];
                if (existsNumber)
                    entity.mNumber = &mStrings[numberData[i]];
                if (existsIsGrossInterior)
                    entity.mIsGrossInterior = isGrossInteriorData[i];
                entity.mAreaSchemeIndex = existsAreaScheme ? areaSchemeData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                area->push_back(entity);
            }

            delete[] valueData;
            delete[] perimeterData;
            delete[] isGrossInteriorData;

            return area;
        }

        double GetValue(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Value") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Value"].begin() + areaIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllValue()
        {
            if (mEntityTable.mDataColumns.find("double:Value") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* valueData = new double[count];
            memcpy(valueData, mEntityTable.mDataColumns["double:Value"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(valueData, valueData + count);

            delete[] valueData;

            return result;
        }

        double GetPerimeter(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Perimeter"].begin() + areaIndex * sizeof(double)));
        }

        const std::vector<double>* GetAllPerimeter()
        {
            if (mEntityTable.mDataColumns.find("double:Perimeter") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            double* perimeterData = new double[count];
            memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));

            std::vector<double>* result = new std::vector<double>(perimeterData, perimeterData + count);

            delete[] perimeterData;

            return result;
        }

        const std::string* GetNumber(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};

            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            return &mStrings[mEntityTable.mStringColumns["string:Number"][areaIndex]];
        }

        const std::vector<const std::string*>* GetAllNumber()
        {
            if (mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end())
                return {};

            const int count = GetCount();
            const std::vector<int>& numberData = mEntityTable.mStringColumns["string:Number"];

            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);

            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[numberData[i]]);
            }

            return result;
        }

        bool GetIsGrossInterior(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsGrossInterior") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsGrossInterior"].begin() + areaIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsGrossInterior()
        {
            if (mEntityTable.mDataColumns.find("byte:IsGrossInterior") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isGrossInteriorData = new bfast::byte[count];
            memcpy(isGrossInteriorData, mEntityTable.mDataColumns["byte:IsGrossInterior"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isGrossInteriorData, isGrossInteriorData + count);

            delete[] isGrossInteriorData;

            return result;
        }

        int GetAreaSchemeIndex(int areaIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.AreaScheme:AreaScheme") == mEntityTable.mIndexColumns.end())
                return -1;

            if (areaIndex < 0 || areaIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.AreaScheme:AreaScheme"][areaIndex];
        }

        int GetElementIndex(int areaIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (areaIndex < 0 || areaIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][areaIndex];
        }

    };

    static AreaTable* GetAreaTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Area") == scene.mEntityTables.end())
            return {};

        return new AreaTable(scene.mEntityTables["Vim.Area"], scene.mStrings);
    }

    class AreaScheme
    {
    public:
        int mIndex;
        bool mIsGrossBuildingArea;

        int mElementIndex;
        Element* mElement;

        AreaScheme() {}
    };

    class AreaSchemeTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        AreaSchemeTable(EntityTable& entityTable, std::vector<std::string>& strings):
                mEntityTable(entityTable), mStrings(strings) {}

        int GetCount()
        {
            if (mEntityTable.mDataColumns.find("byte:IsGrossBuildingArea") == mEntityTable.mDataColumns.end())
                return 0;

            return mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].size() / sizeof(bfast::byte);
        }

        AreaScheme* Get(int areaSchemeIndex)
        {
            AreaScheme* areaScheme = new AreaScheme();
            areaScheme->mIndex = areaSchemeIndex;
            areaScheme->mIsGrossBuildingArea = GetIsGrossBuildingArea(areaSchemeIndex);
            areaScheme->mElementIndex = GetElementIndex(areaSchemeIndex);
            return areaScheme;
        }

        std::vector<AreaScheme>* GetAll()
        {
            bool existsIsGrossBuildingArea = mEntityTable.mDataColumns.find("byte:IsGrossBuildingArea") == mEntityTable.mDataColumns.end();
            bool existsElement = mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end();

            const int count = GetCount();

            std::vector<AreaScheme>* areaScheme = new std::vector<AreaScheme>();
            areaScheme->reserve(count);

            bfast::byte* isGrossBuildingAreaData = new bfast::byte[count];
            if (existsIsGrossBuildingArea) memcpy(isGrossBuildingAreaData, mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin(), count * sizeof(bfast::byte));

            const std::vector<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();

            for (int i = 0; i < count; ++i)
            {
                AreaScheme entity;
                entity.mIndex = i;
                if (existsIsGrossBuildingArea)
                    entity.mIsGrossBuildingArea = isGrossBuildingAreaData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                areaScheme->push_back(entity);
            }

            delete[] isGrossBuildingAreaData;

            return areaScheme;
        }

        bool GetIsGrossBuildingArea(int areaSchemeIndex)
        {
            if (areaSchemeIndex < 0 || areaSchemeIndex >= GetCount())
                return {};

            if (mEntityTable.mDataColumns.find("byte:IsGrossBuildingArea") == mEntityTable.mDataColumns.end())
                return {};

            return *reinterpret_cast<bool*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin() + areaSchemeIndex * sizeof(bool)));
        }

        const std::vector<bool>* GetAllIsGrossBuildingArea()
        {
            if (mEntityTable.mDataColumns.find("byte:IsGrossBuildingArea") == mEntityTable.mDataColumns.end())
                return {};

            const int count = GetCount();
            bfast::byte* isGrossBuildingAreaData = new bfast::byte[count];
            memcpy(isGrossBuildingAreaData, mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin(), count * sizeof(bfast::byte));

            std::vector<bool>* result = new std::vector<bool>(isGrossBuildingAreaData, isGrossBuildingAreaData + count);

            delete[] isGrossBuildingAreaData;

            return result;
        }

        int GetElementIndex(int areaSchemeIndex)
        {
            if (mEntityTable.mIndexColumns.find("index:Vim.Element:Element") == mEntityTable.mIndexColumns.end())
                return -1;

            if (areaSchemeIndex < 0 || areaSchemeIndex >= GetCount())
                return -1;

            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][areaSchemeIndex];
        }

    };

    static AreaSchemeTable* GetAreaSchemeTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.AreaScheme") == scene.mEntityTables.end())
            return {};

        return new AreaSchemeTable(scene.mEntityTables["Vim.AreaScheme"], scene.mStrings);
    }

    DocumentModel::DocumentModel(VimScene& scene)
    {
        mAsset = GetAssetTable(scene);
        mDisplayUnit = GetDisplayUnitTable(scene);
        mParameterDescriptor = GetParameterDescriptorTable(scene);
        mParameter = GetParameterTable(scene);
        mElement = GetElementTable(scene);
        mWorkset = GetWorksetTable(scene);
        mAssemblyInstance = GetAssemblyInstanceTable(scene);
        mGroup = GetGroupTable(scene);
        mDesignOption = GetDesignOptionTable(scene);
        mLevel = GetLevelTable(scene);
        mPhase = GetPhaseTable(scene);
        mRoom = GetRoomTable(scene);
        mBimDocument = GetBimDocumentTable(scene);
        mDisplayUnitInBimDocument = GetDisplayUnitInBimDocumentTable(scene);
        mPhaseOrderInBimDocument = GetPhaseOrderInBimDocumentTable(scene);
        mCategory = GetCategoryTable(scene);
        mFamily = GetFamilyTable(scene);
        mFamilyType = GetFamilyTypeTable(scene);
        mFamilyInstance = GetFamilyInstanceTable(scene);
        mView = GetViewTable(scene);
        mElementInView = GetElementInViewTable(scene);
        mShapeInView = GetShapeInViewTable(scene);
        mAssetInView = GetAssetInViewTable(scene);
        mLevelInView = GetLevelInViewTable(scene);
        mCamera = GetCameraTable(scene);
        mMaterial = GetMaterialTable(scene);
        mMaterialInElement = GetMaterialInElementTable(scene);
        mCompoundStructureLayer = GetCompoundStructureLayerTable(scene);
        mCompoundStructure = GetCompoundStructureTable(scene);
        mNode = GetNodeTable(scene);
        mGeometry = GetGeometryTable(scene);
        mShape = GetShapeTable(scene);
        mShapeCollection = GetShapeCollectionTable(scene);
        mShapeInShapeCollection = GetShapeInShapeCollectionTable(scene);
        mSystem = GetSystemTable(scene);
        mElementInSystem = GetElementInSystemTable(scene);
        mWarning = GetWarningTable(scene);
        mElementInWarning = GetElementInWarningTable(scene);
        mBasePoint = GetBasePointTable(scene);
        mPhaseFilter = GetPhaseFilterTable(scene);
        mGrid = GetGridTable(scene);
        mArea = GetAreaTable(scene);
        mAreaScheme = GetAreaSchemeTable(scene);
    }

    DocumentModel::~DocumentModel()
    {
        delete mAsset;
        delete mDisplayUnit;
        delete mParameterDescriptor;
        delete mParameter;
        delete mElement;
        delete mWorkset;
        delete mAssemblyInstance;
        delete mGroup;
        delete mDesignOption;
        delete mLevel;
        delete mPhase;
        delete mRoom;
        delete mBimDocument;
        delete mDisplayUnitInBimDocument;
        delete mPhaseOrderInBimDocument;
        delete mCategory;
        delete mFamily;
        delete mFamilyType;
        delete mFamilyInstance;
        delete mView;
        delete mElementInView;
        delete mShapeInView;
        delete mAssetInView;
        delete mLevelInView;
        delete mCamera;
        delete mMaterial;
        delete mMaterialInElement;
        delete mCompoundStructureLayer;
        delete mCompoundStructure;
        delete mNode;
        delete mGeometry;
        delete mShape;
        delete mShapeCollection;
        delete mShapeInShapeCollection;
        delete mSystem;
        delete mElementInSystem;
        delete mWarning;
        delete mElementInWarning;
        delete mBasePoint;
        delete mPhaseFilter;
        delete mGrid;
        delete mArea;
        delete mAreaScheme;
    }
}

#endif
