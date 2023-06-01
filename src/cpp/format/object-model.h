// AUTO-GENERATED FILE, DO NOT MODIFY.

#ifndef __OBJECT_MODEL_H__
#define __OBJECT_MODEL_H__

#include <string>
#include <vector>
#include "bfast.h"
#include "vim.h"

namespace Vim
{
    typedef bfast::ByteRange* ByteRangePtr;
    
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
    class Schedule;
    class ScheduleTable;
    class ScheduleColumn;
    class ScheduleColumnTable;
    class ScheduleCell;
    class ScheduleCellTable;
    
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
        ScheduleTable* mSchedule;
        ScheduleColumnTable* mScheduleColumn;
        ScheduleCellTable* mScheduleCell;
        
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
            return mEntityTable.get_count();
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
            bool existsBufferName = mEntityTable.column_exists("string:BufferName");
            
            const int count = GetCount();
            
            std::vector<Asset>* asset = new std::vector<Asset>();
            asset->reserve(count);
            
            const std::vector<int>& bufferNameData = mEntityTable.column_exists("string:BufferName") ? mEntityTable.mStringColumns["string:BufferName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:BufferName")) {
                return &mStrings[mEntityTable.mStringColumns["string:BufferName"][assetIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllBufferName()
        {
            const int count = GetCount();
            
            const std::vector<int>& bufferNameData = mEntityTable.column_exists("string:BufferName") ? mEntityTable.mStringColumns["string:BufferName"] : std::vector<int>();
            
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
            return mEntityTable.get_count();
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
            bool existsSpec = mEntityTable.column_exists("string:Spec");
            bool existsType = mEntityTable.column_exists("string:Type");
            bool existsLabel = mEntityTable.column_exists("string:Label");
            
            const int count = GetCount();
            
            std::vector<DisplayUnit>* displayUnit = new std::vector<DisplayUnit>();
            displayUnit->reserve(count);
            
            const std::vector<int>& specData = mEntityTable.column_exists("string:Spec") ? mEntityTable.mStringColumns["string:Spec"] : std::vector<int>();
            
            const std::vector<int>& typeData = mEntityTable.column_exists("string:Type") ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();
            
            const std::vector<int>& labelData = mEntityTable.column_exists("string:Label") ? mEntityTable.mStringColumns["string:Label"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Spec")) {
                return &mStrings[mEntityTable.mStringColumns["string:Spec"][displayUnitIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllSpec()
        {
            const int count = GetCount();
            
            const std::vector<int>& specData = mEntityTable.column_exists("string:Spec") ? mEntityTable.mStringColumns["string:Spec"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Type")) {
                return &mStrings[mEntityTable.mStringColumns["string:Type"][displayUnitIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllType()
        {
            const int count = GetCount();
            
            const std::vector<int>& typeData = mEntityTable.column_exists("string:Type") ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Label")) {
                return &mStrings[mEntityTable.mStringColumns["string:Label"][displayUnitIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllLabel()
        {
            const int count = GetCount();
            
            const std::vector<int>& labelData = mEntityTable.column_exists("string:Label") ? mEntityTable.mStringColumns["string:Label"] : std::vector<int>();
            
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
            return mEntityTable.get_count();
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
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsGroup = mEntityTable.column_exists("string:Group");
            bool existsParameterType = mEntityTable.column_exists("string:ParameterType");
            bool existsIsInstance = mEntityTable.column_exists("byte:IsInstance");
            bool existsIsShared = mEntityTable.column_exists("byte:IsShared");
            bool existsIsReadOnly = mEntityTable.column_exists("byte:IsReadOnly");
            bool existsFlags = mEntityTable.column_exists("int:Flags");
            bool existsGuid = mEntityTable.column_exists("string:Guid");
            bool existsDisplayUnit = mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit");
            
            const int count = GetCount();
            
            std::vector<ParameterDescriptor>* parameterDescriptor = new std::vector<ParameterDescriptor>();
            parameterDescriptor->reserve(count);
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            const std::vector<int>& groupData = mEntityTable.column_exists("string:Group") ? mEntityTable.mStringColumns["string:Group"] : std::vector<int>();
            
            const std::vector<int>& parameterTypeData = mEntityTable.column_exists("string:ParameterType") ? mEntityTable.mStringColumns["string:ParameterType"] : std::vector<int>();
            
            bfast::byte* isInstanceData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsInstance")) {
                memcpy(isInstanceData, mEntityTable.mDataColumns["byte:IsInstance"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isSharedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsShared")) {
                memcpy(isSharedData, mEntityTable.mDataColumns["byte:IsShared"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isReadOnlyData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsReadOnly")) {
                memcpy(isReadOnlyData, mEntityTable.mDataColumns["byte:IsReadOnly"].begin(), count * sizeof(bfast::byte));
            }
            
            int* flagsData = new int[count];
            if (mEntityTable.column_exists("int:Flags")) {
                memcpy(flagsData, mEntityTable.mDataColumns["int:Flags"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
            const std::vector<int>& displayUnitData = mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit") ? mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][parameterDescriptorIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Group")) {
                return &mStrings[mEntityTable.mStringColumns["string:Group"][parameterDescriptorIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllGroup()
        {
            const int count = GetCount();
            
            const std::vector<int>& groupData = mEntityTable.column_exists("string:Group") ? mEntityTable.mStringColumns["string:Group"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:ParameterType")) {
                return &mStrings[mEntityTable.mStringColumns["string:ParameterType"][parameterDescriptorIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllParameterType()
        {
            const int count = GetCount();
            
            const std::vector<int>& parameterTypeData = mEntityTable.column_exists("string:ParameterType") ? mEntityTable.mStringColumns["string:ParameterType"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsInstance")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsInstance"].begin() + parameterDescriptorIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsInstance()
        {
            const int count = GetCount();
            
            bfast::byte* isInstanceData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsInstance")) {
                memcpy(isInstanceData, mEntityTable.mDataColumns["byte:IsInstance"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isInstanceData, isInstanceData + count);
            
            delete[] isInstanceData;
            
            return result;
        }
        
        bool GetIsShared(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsShared")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsShared"].begin() + parameterDescriptorIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsShared()
        {
            const int count = GetCount();
            
            bfast::byte* isSharedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsShared")) {
                memcpy(isSharedData, mEntityTable.mDataColumns["byte:IsShared"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isSharedData, isSharedData + count);
            
            delete[] isSharedData;
            
            return result;
        }
        
        bool GetIsReadOnly(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsReadOnly")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsReadOnly"].begin() + parameterDescriptorIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsReadOnly()
        {
            const int count = GetCount();
            
            bfast::byte* isReadOnlyData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsReadOnly")) {
                memcpy(isReadOnlyData, mEntityTable.mDataColumns["byte:IsReadOnly"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isReadOnlyData, isReadOnlyData + count);
            
            delete[] isReadOnlyData;
            
            return result;
        }
        
        int GetFlags(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:Flags")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Flags"].begin() + parameterDescriptorIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllFlags()
        {
            const int count = GetCount();
            
            int* flagsData = new int[count];
            if (mEntityTable.column_exists("int:Flags")) {
                memcpy(flagsData, mEntityTable.mDataColumns["int:Flags"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(flagsData, flagsData + count);
            
            delete[] flagsData;
            
            return result;
        }
        
        const std::string* GetGuid(int parameterDescriptorIndex)
        {
            if (parameterDescriptorIndex < 0 || parameterDescriptorIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Guid")) {
                return &mStrings[mEntityTable.mStringColumns["string:Guid"][parameterDescriptorIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllGuid()
        {
            const int count = GetCount();
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsValue = mEntityTable.column_exists("string:Value");
            bool existsParameterDescriptor = mEntityTable.column_exists("index:Vim.ParameterDescriptor:ParameterDescriptor");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Parameter>* parameter = new std::vector<Parameter>();
            parameter->reserve(count);
            
            const std::vector<int>& valueData = mEntityTable.column_exists("string:Value") ? mEntityTable.mStringColumns["string:Value"] : std::vector<int>();
            
            const std::vector<int>& parameterDescriptorData = mEntityTable.column_exists("index:Vim.ParameterDescriptor:ParameterDescriptor") ? mEntityTable.mIndexColumns["index:Vim.ParameterDescriptor:ParameterDescriptor"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Value")) {
                return &mStrings[mEntityTable.mStringColumns["string:Value"][parameterIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllValue()
        {
            const int count = GetCount();
            
            const std::vector<int>& valueData = mEntityTable.column_exists("string:Value") ? mEntityTable.mStringColumns["string:Value"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.ParameterDescriptor:ParameterDescriptor")) {
                return -1;
            }
            
            if (parameterIndex < 0 || parameterIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.ParameterDescriptor:ParameterDescriptor"][parameterIndex];
        }
        
        int GetElementIndex(int parameterIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        long long mId;
        const std::string* mType;
        const std::string* mName;
        const std::string* mUniqueId;
        float mLocation_X;
        float mLocation_Y;
        float mLocation_Z;
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
            return mEntityTable.get_count();
        }
        
        Element* Get(int elementIndex)
        {
            Element* element = new Element();
            element->mIndex = elementIndex;
            element->mId = GetId(elementIndex);
            element->mType = GetType(elementIndex);
            element->mName = GetName(elementIndex);
            element->mUniqueId = GetUniqueId(elementIndex);
            element->mLocation_X = GetLocation_X(elementIndex);
            element->mLocation_Y = GetLocation_Y(elementIndex);
            element->mLocation_Z = GetLocation_Z(elementIndex);
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
            bool existsId = mEntityTable.column_exists("long:Id") || mEntityTable.column_exists("int:Id");
            bool existsType = mEntityTable.column_exists("string:Type");
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsUniqueId = mEntityTable.column_exists("string:UniqueId");
            bool existsLocation_X = mEntityTable.column_exists("float:Location.X");
            bool existsLocation_Y = mEntityTable.column_exists("float:Location.Y");
            bool existsLocation_Z = mEntityTable.column_exists("float:Location.Z");
            bool existsFamilyName = mEntityTable.column_exists("string:FamilyName");
            bool existsIsPinned = mEntityTable.column_exists("byte:IsPinned");
            bool existsLevel = mEntityTable.column_exists("index:Vim.Level:Level");
            bool existsPhaseCreated = mEntityTable.column_exists("index:Vim.Phase:PhaseCreated");
            bool existsPhaseDemolished = mEntityTable.column_exists("index:Vim.Phase:PhaseDemolished");
            bool existsCategory = mEntityTable.column_exists("index:Vim.Category:Category");
            bool existsWorkset = mEntityTable.column_exists("index:Vim.Workset:Workset");
            bool existsDesignOption = mEntityTable.column_exists("index:Vim.DesignOption:DesignOption");
            bool existsOwnerView = mEntityTable.column_exists("index:Vim.View:OwnerView");
            bool existsGroup = mEntityTable.column_exists("index:Vim.Group:Group");
            bool existsAssemblyInstance = mEntityTable.column_exists("index:Vim.AssemblyInstance:AssemblyInstance");
            bool existsBimDocument = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument");
            bool existsRoom = mEntityTable.column_exists("index:Vim.Room:Room");
            
            const int count = GetCount();
            
            std::vector<Element>* element = new std::vector<Element>();
            element->reserve(count);
            
            long long* idData = new long long[count];
            if (mEntityTable.column_exists("long:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["long:Id"].begin(), count * sizeof(long long));
            }
            else if (mEntityTable.column_exists("int:Id")) {
                for (int i = 0; i < count; ++i) {
                    idData[i] = static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + i * sizeof(int))));
                }
            }
            
            const std::vector<int>& typeData = mEntityTable.column_exists("string:Type") ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            const std::vector<int>& uniqueIdData = mEntityTable.column_exists("string:UniqueId") ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();
            
            float* location_XData = new float[count];
            if (mEntityTable.column_exists("float:Location.X")) {
                memcpy(location_XData, mEntityTable.mDataColumns["float:Location.X"].begin(), count * sizeof(float));
            }
            
            float* location_YData = new float[count];
            if (mEntityTable.column_exists("float:Location.Y")) {
                memcpy(location_YData, mEntityTable.mDataColumns["float:Location.Y"].begin(), count * sizeof(float));
            }
            
            float* location_ZData = new float[count];
            if (mEntityTable.column_exists("float:Location.Z")) {
                memcpy(location_ZData, mEntityTable.mDataColumns["float:Location.Z"].begin(), count * sizeof(float));
            }
            
            const std::vector<int>& familyNameData = mEntityTable.column_exists("string:FamilyName") ? mEntityTable.mStringColumns["string:FamilyName"] : std::vector<int>();
            
            bfast::byte* isPinnedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPinned")) {
                memcpy(isPinnedData, mEntityTable.mDataColumns["byte:IsPinned"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& levelData = mEntityTable.column_exists("index:Vim.Level:Level") ? mEntityTable.mIndexColumns["index:Vim.Level:Level"] : std::vector<int>();
            const std::vector<int>& phaseCreatedData = mEntityTable.column_exists("index:Vim.Phase:PhaseCreated") ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseCreated"] : std::vector<int>();
            const std::vector<int>& phaseDemolishedData = mEntityTable.column_exists("index:Vim.Phase:PhaseDemolished") ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseDemolished"] : std::vector<int>();
            const std::vector<int>& categoryData = mEntityTable.column_exists("index:Vim.Category:Category") ? mEntityTable.mIndexColumns["index:Vim.Category:Category"] : std::vector<int>();
            const std::vector<int>& worksetData = mEntityTable.column_exists("index:Vim.Workset:Workset") ? mEntityTable.mIndexColumns["index:Vim.Workset:Workset"] : std::vector<int>();
            const std::vector<int>& designOptionData = mEntityTable.column_exists("index:Vim.DesignOption:DesignOption") ? mEntityTable.mIndexColumns["index:Vim.DesignOption:DesignOption"] : std::vector<int>();
            const std::vector<int>& ownerViewData = mEntityTable.column_exists("index:Vim.View:OwnerView") ? mEntityTable.mIndexColumns["index:Vim.View:OwnerView"] : std::vector<int>();
            const std::vector<int>& groupData = mEntityTable.column_exists("index:Vim.Group:Group") ? mEntityTable.mIndexColumns["index:Vim.Group:Group"] : std::vector<int>();
            const std::vector<int>& assemblyInstanceData = mEntityTable.column_exists("index:Vim.AssemblyInstance:AssemblyInstance") ? mEntityTable.mIndexColumns["index:Vim.AssemblyInstance:AssemblyInstance"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            const std::vector<int>& roomData = mEntityTable.column_exists("index:Vim.Room:Room") ? mEntityTable.mIndexColumns["index:Vim.Room:Room"] : std::vector<int>();
            
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
                if (existsLocation_X)
                    entity.mLocation_X = location_XData[i];
                if (existsLocation_Y)
                    entity.mLocation_Y = location_YData[i];
                if (existsLocation_Z)
                    entity.mLocation_Z = location_ZData[i];
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
            delete[] location_XData;
            delete[] location_YData;
            delete[] location_ZData;
            delete[] isPinnedData;
            
            return element;
        }
        
        long long GetId(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("long:Id")) {
                return *reinterpret_cast<long long*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["long:Id"].begin() + elementIndex * sizeof(long long)));
            }
            
            if (mEntityTable.column_exists("int:Id")) {
                return static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + elementIndex * sizeof(int))));
            }
            
            return {};
        }
        
        const std::vector<long long>* GetAllId()
        {
            const int count = GetCount();
            
            long long* idData = new long long[count];
            if (mEntityTable.column_exists("long:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["long:Id"].begin(), count * sizeof(long long));
            }
            else if (mEntityTable.column_exists("int:Id")) {
                for (int i = 0; i < count; ++i) {
                    idData[i] = static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + i * sizeof(int))));
                }
            }
            
            std::vector<long long>* result = new std::vector<long long>(idData, idData + count);
            
            delete[] idData;
            
            return result;
        }
        
        const std::string* GetType(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Type")) {
                return &mStrings[mEntityTable.mStringColumns["string:Type"][elementIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllType()
        {
            const int count = GetCount();
            
            const std::vector<int>& typeData = mEntityTable.column_exists("string:Type") ? mEntityTable.mStringColumns["string:Type"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][elementIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:UniqueId")) {
                return &mStrings[mEntityTable.mStringColumns["string:UniqueId"][elementIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllUniqueId()
        {
            const int count = GetCount();
            
            const std::vector<int>& uniqueIdData = mEntityTable.column_exists("string:UniqueId") ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[uniqueIdData[i]]);
            }
            
            return result;
        }
        
        float GetLocation_X(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Location.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Location.X"].begin() + elementIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllLocation_X()
        {
            const int count = GetCount();
            
            float* location_XData = new float[count];
            if (mEntityTable.column_exists("float:Location.X")) {
                memcpy(location_XData, mEntityTable.mDataColumns["float:Location.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(location_XData, location_XData + count);
            
            delete[] location_XData;
            
            return result;
        }
        
        float GetLocation_Y(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Location.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Location.Y"].begin() + elementIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllLocation_Y()
        {
            const int count = GetCount();
            
            float* location_YData = new float[count];
            if (mEntityTable.column_exists("float:Location.Y")) {
                memcpy(location_YData, mEntityTable.mDataColumns["float:Location.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(location_YData, location_YData + count);
            
            delete[] location_YData;
            
            return result;
        }
        
        float GetLocation_Z(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Location.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Location.Z"].begin() + elementIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllLocation_Z()
        {
            const int count = GetCount();
            
            float* location_ZData = new float[count];
            if (mEntityTable.column_exists("float:Location.Z")) {
                memcpy(location_ZData, mEntityTable.mDataColumns["float:Location.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(location_ZData, location_ZData + count);
            
            delete[] location_ZData;
            
            return result;
        }
        
        const std::string* GetFamilyName(int elementIndex)
        {
            if (elementIndex < 0 || elementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:FamilyName")) {
                return &mStrings[mEntityTable.mStringColumns["string:FamilyName"][elementIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllFamilyName()
        {
            const int count = GetCount();
            
            const std::vector<int>& familyNameData = mEntityTable.column_exists("string:FamilyName") ? mEntityTable.mStringColumns["string:FamilyName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsPinned")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPinned"].begin() + elementIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsPinned()
        {
            const int count = GetCount();
            
            bfast::byte* isPinnedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPinned")) {
                memcpy(isPinnedData, mEntityTable.mDataColumns["byte:IsPinned"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isPinnedData, isPinnedData + count);
            
            delete[] isPinnedData;
            
            return result;
        }
        
        int GetLevelIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Level:Level")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Level:Level"][elementIndex];
        }
        
        int GetPhaseCreatedIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Phase:PhaseCreated")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Phase:PhaseCreated"][elementIndex];
        }
        
        int GetPhaseDemolishedIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Phase:PhaseDemolished")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Phase:PhaseDemolished"][elementIndex];
        }
        
        int GetCategoryIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Category:Category")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Category:Category"][elementIndex];
        }
        
        int GetWorksetIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Workset:Workset")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Workset:Workset"][elementIndex];
        }
        
        int GetDesignOptionIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.DesignOption:DesignOption")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.DesignOption:DesignOption"][elementIndex];
        }
        
        int GetOwnerViewIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.View:OwnerView")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.View:OwnerView"][elementIndex];
        }
        
        int GetGroupIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Group:Group")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Group:Group"][elementIndex];
        }
        
        int GetAssemblyInstanceIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.AssemblyInstance:AssemblyInstance")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.AssemblyInstance:AssemblyInstance"][elementIndex];
        }
        
        int GetBimDocumentIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.BimDocument:BimDocument")) {
                return -1;
            }
            
            if (elementIndex < 0 || elementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"][elementIndex];
        }
        
        int GetRoomIndex(int elementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Room:Room")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsId = mEntityTable.column_exists("int:Id");
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsKind = mEntityTable.column_exists("string:Kind");
            bool existsIsOpen = mEntityTable.column_exists("byte:IsOpen");
            bool existsIsEditable = mEntityTable.column_exists("byte:IsEditable");
            bool existsOwner = mEntityTable.column_exists("string:Owner");
            bool existsUniqueId = mEntityTable.column_exists("string:UniqueId");
            bool existsBimDocument = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument");
            
            const int count = GetCount();
            
            std::vector<Workset>* workset = new std::vector<Workset>();
            workset->reserve(count);
            
            int* idData = new int[count];
            if (mEntityTable.column_exists("int:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            const std::vector<int>& kindData = mEntityTable.column_exists("string:Kind") ? mEntityTable.mStringColumns["string:Kind"] : std::vector<int>();
            
            bfast::byte* isOpenData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsOpen")) {
                memcpy(isOpenData, mEntityTable.mDataColumns["byte:IsOpen"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isEditableData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsEditable")) {
                memcpy(isEditableData, mEntityTable.mDataColumns["byte:IsEditable"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& ownerData = mEntityTable.column_exists("string:Owner") ? mEntityTable.mStringColumns["string:Owner"] : std::vector<int>();
            
            const std::vector<int>& uniqueIdData = mEntityTable.column_exists("string:UniqueId") ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();
            
            const std::vector<int>& bimDocumentData = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:Id")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + worksetIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllId()
        {
            const int count = GetCount();
            
            int* idData = new int[count];
            if (mEntityTable.column_exists("int:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(idData, idData + count);
            
            delete[] idData;
            
            return result;
        }
        
        const std::string* GetName(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][worksetIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Kind")) {
                return &mStrings[mEntityTable.mStringColumns["string:Kind"][worksetIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllKind()
        {
            const int count = GetCount();
            
            const std::vector<int>& kindData = mEntityTable.column_exists("string:Kind") ? mEntityTable.mStringColumns["string:Kind"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsOpen")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsOpen"].begin() + worksetIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsOpen()
        {
            const int count = GetCount();
            
            bfast::byte* isOpenData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsOpen")) {
                memcpy(isOpenData, mEntityTable.mDataColumns["byte:IsOpen"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isOpenData, isOpenData + count);
            
            delete[] isOpenData;
            
            return result;
        }
        
        bool GetIsEditable(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsEditable")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsEditable"].begin() + worksetIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsEditable()
        {
            const int count = GetCount();
            
            bfast::byte* isEditableData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsEditable")) {
                memcpy(isEditableData, mEntityTable.mDataColumns["byte:IsEditable"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isEditableData, isEditableData + count);
            
            delete[] isEditableData;
            
            return result;
        }
        
        const std::string* GetOwner(int worksetIndex)
        {
            if (worksetIndex < 0 || worksetIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Owner")) {
                return &mStrings[mEntityTable.mStringColumns["string:Owner"][worksetIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllOwner()
        {
            const int count = GetCount();
            
            const std::vector<int>& ownerData = mEntityTable.column_exists("string:Owner") ? mEntityTable.mStringColumns["string:Owner"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:UniqueId")) {
                return &mStrings[mEntityTable.mStringColumns["string:UniqueId"][worksetIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllUniqueId()
        {
            const int count = GetCount();
            
            const std::vector<int>& uniqueIdData = mEntityTable.column_exists("string:UniqueId") ? mEntityTable.mStringColumns["string:UniqueId"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.BimDocument:BimDocument")) {
                return -1;
            }
            
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
        float mPosition_X;
        float mPosition_Y;
        float mPosition_Z;
        
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
            return mEntityTable.get_count();
        }
        
        AssemblyInstance* Get(int assemblyInstanceIndex)
        {
            AssemblyInstance* assemblyInstance = new AssemblyInstance();
            assemblyInstance->mIndex = assemblyInstanceIndex;
            assemblyInstance->mAssemblyTypeName = GetAssemblyTypeName(assemblyInstanceIndex);
            assemblyInstance->mPosition_X = GetPosition_X(assemblyInstanceIndex);
            assemblyInstance->mPosition_Y = GetPosition_Y(assemblyInstanceIndex);
            assemblyInstance->mPosition_Z = GetPosition_Z(assemblyInstanceIndex);
            assemblyInstance->mElementIndex = GetElementIndex(assemblyInstanceIndex);
            return assemblyInstance;
        }
        
        std::vector<AssemblyInstance>* GetAll()
        {
            bool existsAssemblyTypeName = mEntityTable.column_exists("string:AssemblyTypeName");
            bool existsPosition_X = mEntityTable.column_exists("float:Position.X");
            bool existsPosition_Y = mEntityTable.column_exists("float:Position.Y");
            bool existsPosition_Z = mEntityTable.column_exists("float:Position.Z");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<AssemblyInstance>* assemblyInstance = new std::vector<AssemblyInstance>();
            assemblyInstance->reserve(count);
            
            const std::vector<int>& assemblyTypeNameData = mEntityTable.column_exists("string:AssemblyTypeName") ? mEntityTable.mStringColumns["string:AssemblyTypeName"] : std::vector<int>();
            
            float* position_XData = new float[count];
            if (mEntityTable.column_exists("float:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["float:Position.X"].begin(), count * sizeof(float));
            }
            
            float* position_YData = new float[count];
            if (mEntityTable.column_exists("float:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["float:Position.Y"].begin(), count * sizeof(float));
            }
            
            float* position_ZData = new float[count];
            if (mEntityTable.column_exists("float:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["float:Position.Z"].begin(), count * sizeof(float));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                AssemblyInstance entity;
                entity.mIndex = i;
                if (existsAssemblyTypeName)
                    entity.mAssemblyTypeName = &mStrings[assemblyTypeNameData[i]];
                if (existsPosition_X)
                    entity.mPosition_X = position_XData[i];
                if (existsPosition_Y)
                    entity.mPosition_Y = position_YData[i];
                if (existsPosition_Z)
                    entity.mPosition_Z = position_ZData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                assemblyInstance->push_back(entity);
            }
            
            delete[] position_XData;
            delete[] position_YData;
            delete[] position_ZData;
            
            return assemblyInstance;
        }
        
        const std::string* GetAssemblyTypeName(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:AssemblyTypeName")) {
                return &mStrings[mEntityTable.mStringColumns["string:AssemblyTypeName"][assemblyInstanceIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllAssemblyTypeName()
        {
            const int count = GetCount();
            
            const std::vector<int>& assemblyTypeNameData = mEntityTable.column_exists("string:AssemblyTypeName") ? mEntityTable.mStringColumns["string:AssemblyTypeName"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[assemblyTypeNameData[i]]);
            }
            
            return result;
        }
        
        float GetPosition_X(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.X"].begin() + assemblyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_X()
        {
            const int count = GetCount();
            
            float* position_XData = new float[count];
            if (mEntityTable.column_exists("float:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["float:Position.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_XData, position_XData + count);
            
            delete[] position_XData;
            
            return result;
        }
        
        float GetPosition_Y(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.Y"].begin() + assemblyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_Y()
        {
            const int count = GetCount();
            
            float* position_YData = new float[count];
            if (mEntityTable.column_exists("float:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["float:Position.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_YData, position_YData + count);
            
            delete[] position_YData;
            
            return result;
        }
        
        float GetPosition_Z(int assemblyInstanceIndex)
        {
            if (assemblyInstanceIndex < 0 || assemblyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.Z"].begin() + assemblyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_Z()
        {
            const int count = GetCount();
            
            float* position_ZData = new float[count];
            if (mEntityTable.column_exists("float:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["float:Position.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_ZData, position_ZData + count);
            
            delete[] position_ZData;
            
            return result;
        }
        
        int GetElementIndex(int assemblyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        float mPosition_X;
        float mPosition_Y;
        float mPosition_Z;
        
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
            return mEntityTable.get_count();
        }
        
        Group* Get(int groupIndex)
        {
            Group* group = new Group();
            group->mIndex = groupIndex;
            group->mGroupType = GetGroupType(groupIndex);
            group->mPosition_X = GetPosition_X(groupIndex);
            group->mPosition_Y = GetPosition_Y(groupIndex);
            group->mPosition_Z = GetPosition_Z(groupIndex);
            group->mElementIndex = GetElementIndex(groupIndex);
            return group;
        }
        
        std::vector<Group>* GetAll()
        {
            bool existsGroupType = mEntityTable.column_exists("string:GroupType");
            bool existsPosition_X = mEntityTable.column_exists("float:Position.X");
            bool existsPosition_Y = mEntityTable.column_exists("float:Position.Y");
            bool existsPosition_Z = mEntityTable.column_exists("float:Position.Z");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Group>* group = new std::vector<Group>();
            group->reserve(count);
            
            const std::vector<int>& groupTypeData = mEntityTable.column_exists("string:GroupType") ? mEntityTable.mStringColumns["string:GroupType"] : std::vector<int>();
            
            float* position_XData = new float[count];
            if (mEntityTable.column_exists("float:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["float:Position.X"].begin(), count * sizeof(float));
            }
            
            float* position_YData = new float[count];
            if (mEntityTable.column_exists("float:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["float:Position.Y"].begin(), count * sizeof(float));
            }
            
            float* position_ZData = new float[count];
            if (mEntityTable.column_exists("float:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["float:Position.Z"].begin(), count * sizeof(float));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                Group entity;
                entity.mIndex = i;
                if (existsGroupType)
                    entity.mGroupType = &mStrings[groupTypeData[i]];
                if (existsPosition_X)
                    entity.mPosition_X = position_XData[i];
                if (existsPosition_Y)
                    entity.mPosition_Y = position_YData[i];
                if (existsPosition_Z)
                    entity.mPosition_Z = position_ZData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                group->push_back(entity);
            }
            
            delete[] position_XData;
            delete[] position_YData;
            delete[] position_ZData;
            
            return group;
        }
        
        const std::string* GetGroupType(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:GroupType")) {
                return &mStrings[mEntityTable.mStringColumns["string:GroupType"][groupIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllGroupType()
        {
            const int count = GetCount();
            
            const std::vector<int>& groupTypeData = mEntityTable.column_exists("string:GroupType") ? mEntityTable.mStringColumns["string:GroupType"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[groupTypeData[i]]);
            }
            
            return result;
        }
        
        float GetPosition_X(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.X"].begin() + groupIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_X()
        {
            const int count = GetCount();
            
            float* position_XData = new float[count];
            if (mEntityTable.column_exists("float:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["float:Position.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_XData, position_XData + count);
            
            delete[] position_XData;
            
            return result;
        }
        
        float GetPosition_Y(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.Y"].begin() + groupIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_Y()
        {
            const int count = GetCount();
            
            float* position_YData = new float[count];
            if (mEntityTable.column_exists("float:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["float:Position.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_YData, position_YData + count);
            
            delete[] position_YData;
            
            return result;
        }
        
        float GetPosition_Z(int groupIndex)
        {
            if (groupIndex < 0 || groupIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Position.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Position.Z"].begin() + groupIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllPosition_Z()
        {
            const int count = GetCount();
            
            float* position_ZData = new float[count];
            if (mEntityTable.column_exists("float:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["float:Position.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(position_ZData, position_ZData + count);
            
            delete[] position_ZData;
            
            return result;
        }
        
        int GetElementIndex(int groupIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsIsPrimary = mEntityTable.column_exists("byte:IsPrimary");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<DesignOption>* designOption = new std::vector<DesignOption>();
            designOption->reserve(count);
            
            bfast::byte* isPrimaryData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPrimary")) {
                memcpy(isPrimaryData, mEntityTable.mDataColumns["byte:IsPrimary"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsPrimary")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPrimary"].begin() + designOptionIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsPrimary()
        {
            const int count = GetCount();
            
            bfast::byte* isPrimaryData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPrimary")) {
                memcpy(isPrimaryData, mEntityTable.mDataColumns["byte:IsPrimary"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isPrimaryData, isPrimaryData + count);
            
            delete[] isPrimaryData;
            
            return result;
        }
        
        int GetElementIndex(int designOptionIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        
        int mFamilyTypeIndex;
        FamilyType* mFamilyType;
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
            return mEntityTable.get_count();
        }
        
        Level* Get(int levelIndex)
        {
            Level* level = new Level();
            level->mIndex = levelIndex;
            level->mElevation = GetElevation(levelIndex);
            level->mFamilyTypeIndex = GetFamilyTypeIndex(levelIndex);
            level->mElementIndex = GetElementIndex(levelIndex);
            return level;
        }
        
        std::vector<Level>* GetAll()
        {
            bool existsElevation = mEntityTable.column_exists("double:Elevation");
            bool existsFamilyType = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Level>* level = new std::vector<Level>();
            level->reserve(count);
            
            double* elevationData = new double[count];
            if (mEntityTable.column_exists("double:Elevation")) {
                memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& familyTypeData = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType") ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                Level entity;
                entity.mIndex = i;
                if (existsElevation)
                    entity.mElevation = elevationData[i];
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
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
            
            if (mEntityTable.column_exists("double:Elevation")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Elevation"].begin() + levelIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllElevation()
        {
            const int count = GetCount();
            
            double* elevationData = new double[count];
            if (mEntityTable.column_exists("double:Elevation")) {
                memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(elevationData, elevationData + count);
            
            delete[] elevationData;
            
            return result;
        }
        
        int GetFamilyTypeIndex(int levelIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.FamilyType:FamilyType")) {
                return -1;
            }
            
            if (levelIndex < 0 || levelIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][levelIndex];
        }
        
        int GetElementIndex(int levelIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Phase>* phase = new std::vector<Phase>();
            phase->reserve(count);
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsBaseOffset = mEntityTable.column_exists("double:BaseOffset");
            bool existsLimitOffset = mEntityTable.column_exists("double:LimitOffset");
            bool existsUnboundedHeight = mEntityTable.column_exists("double:UnboundedHeight");
            bool existsVolume = mEntityTable.column_exists("double:Volume");
            bool existsPerimeter = mEntityTable.column_exists("double:Perimeter");
            bool existsArea = mEntityTable.column_exists("double:Area");
            bool existsNumber = mEntityTable.column_exists("string:Number");
            bool existsUpperLimit = mEntityTable.column_exists("index:Vim.Level:UpperLimit");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Room>* room = new std::vector<Room>();
            room->reserve(count);
            
            double* baseOffsetData = new double[count];
            if (mEntityTable.column_exists("double:BaseOffset")) {
                memcpy(baseOffsetData, mEntityTable.mDataColumns["double:BaseOffset"].begin(), count * sizeof(double));
            }
            
            double* limitOffsetData = new double[count];
            if (mEntityTable.column_exists("double:LimitOffset")) {
                memcpy(limitOffsetData, mEntityTable.mDataColumns["double:LimitOffset"].begin(), count * sizeof(double));
            }
            
            double* unboundedHeightData = new double[count];
            if (mEntityTable.column_exists("double:UnboundedHeight")) {
                memcpy(unboundedHeightData, mEntityTable.mDataColumns["double:UnboundedHeight"].begin(), count * sizeof(double));
            }
            
            double* volumeData = new double[count];
            if (mEntityTable.column_exists("double:Volume")) {
                memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));
            }
            
            double* perimeterData = new double[count];
            if (mEntityTable.column_exists("double:Perimeter")) {
                memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));
            }
            
            double* areaData = new double[count];
            if (mEntityTable.column_exists("double:Area")) {
                memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
            const std::vector<int>& upperLimitData = mEntityTable.column_exists("index:Vim.Level:UpperLimit") ? mEntityTable.mIndexColumns["index:Vim.Level:UpperLimit"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:BaseOffset")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:BaseOffset"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllBaseOffset()
        {
            const int count = GetCount();
            
            double* baseOffsetData = new double[count];
            if (mEntityTable.column_exists("double:BaseOffset")) {
                memcpy(baseOffsetData, mEntityTable.mDataColumns["double:BaseOffset"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(baseOffsetData, baseOffsetData + count);
            
            delete[] baseOffsetData;
            
            return result;
        }
        
        double GetLimitOffset(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:LimitOffset")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:LimitOffset"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLimitOffset()
        {
            const int count = GetCount();
            
            double* limitOffsetData = new double[count];
            if (mEntityTable.column_exists("double:LimitOffset")) {
                memcpy(limitOffsetData, mEntityTable.mDataColumns["double:LimitOffset"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(limitOffsetData, limitOffsetData + count);
            
            delete[] limitOffsetData;
            
            return result;
        }
        
        double GetUnboundedHeight(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:UnboundedHeight")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:UnboundedHeight"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllUnboundedHeight()
        {
            const int count = GetCount();
            
            double* unboundedHeightData = new double[count];
            if (mEntityTable.column_exists("double:UnboundedHeight")) {
                memcpy(unboundedHeightData, mEntityTable.mDataColumns["double:UnboundedHeight"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(unboundedHeightData, unboundedHeightData + count);
            
            delete[] unboundedHeightData;
            
            return result;
        }
        
        double GetVolume(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Volume")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Volume"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllVolume()
        {
            const int count = GetCount();
            
            double* volumeData = new double[count];
            if (mEntityTable.column_exists("double:Volume")) {
                memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(volumeData, volumeData + count);
            
            delete[] volumeData;
            
            return result;
        }
        
        double GetPerimeter(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Perimeter")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Perimeter"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllPerimeter()
        {
            const int count = GetCount();
            
            double* perimeterData = new double[count];
            if (mEntityTable.column_exists("double:Perimeter")) {
                memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(perimeterData, perimeterData + count);
            
            delete[] perimeterData;
            
            return result;
        }
        
        double GetArea(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Area")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Area"].begin() + roomIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllArea()
        {
            const int count = GetCount();
            
            double* areaData = new double[count];
            if (mEntityTable.column_exists("double:Area")) {
                memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(areaData, areaData + count);
            
            delete[] areaData;
            
            return result;
        }
        
        const std::string* GetNumber(int roomIndex)
        {
            if (roomIndex < 0 || roomIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Number")) {
                return &mStrings[mEntityTable.mStringColumns["string:Number"][roomIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllNumber()
        {
            const int count = GetCount();
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Level:UpperLimit")) {
                return -1;
            }
            
            if (roomIndex < 0 || roomIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Level:UpperLimit"][roomIndex];
        }
        
        int GetElementIndex(int roomIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsTitle = mEntityTable.column_exists("string:Title");
            bool existsIsMetric = mEntityTable.column_exists("byte:IsMetric");
            bool existsGuid = mEntityTable.column_exists("string:Guid");
            bool existsNumSaves = mEntityTable.column_exists("int:NumSaves");
            bool existsIsLinked = mEntityTable.column_exists("byte:IsLinked");
            bool existsIsDetached = mEntityTable.column_exists("byte:IsDetached");
            bool existsIsWorkshared = mEntityTable.column_exists("byte:IsWorkshared");
            bool existsPathName = mEntityTable.column_exists("string:PathName");
            bool existsLatitude = mEntityTable.column_exists("double:Latitude");
            bool existsLongitude = mEntityTable.column_exists("double:Longitude");
            bool existsTimeZone = mEntityTable.column_exists("double:TimeZone");
            bool existsPlaceName = mEntityTable.column_exists("string:PlaceName");
            bool existsWeatherStationName = mEntityTable.column_exists("string:WeatherStationName");
            bool existsElevation = mEntityTable.column_exists("double:Elevation");
            bool existsProjectLocation = mEntityTable.column_exists("string:ProjectLocation");
            bool existsIssueDate = mEntityTable.column_exists("string:IssueDate");
            bool existsStatus = mEntityTable.column_exists("string:Status");
            bool existsClientName = mEntityTable.column_exists("string:ClientName");
            bool existsAddress = mEntityTable.column_exists("string:Address");
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsNumber = mEntityTable.column_exists("string:Number");
            bool existsAuthor = mEntityTable.column_exists("string:Author");
            bool existsBuildingName = mEntityTable.column_exists("string:BuildingName");
            bool existsOrganizationName = mEntityTable.column_exists("string:OrganizationName");
            bool existsOrganizationDescription = mEntityTable.column_exists("string:OrganizationDescription");
            bool existsProduct = mEntityTable.column_exists("string:Product");
            bool existsVersion = mEntityTable.column_exists("string:Version");
            bool existsUser = mEntityTable.column_exists("string:User");
            bool existsActiveView = mEntityTable.column_exists("index:Vim.View:ActiveView");
            bool existsOwnerFamily = mEntityTable.column_exists("index:Vim.Family:OwnerFamily");
            bool existsParent = mEntityTable.column_exists("index:Vim.BimDocument:Parent");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<BimDocument>* bimDocument = new std::vector<BimDocument>();
            bimDocument->reserve(count);
            
            const std::vector<int>& titleData = mEntityTable.column_exists("string:Title") ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();
            
            bfast::byte* isMetricData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsMetric")) {
                memcpy(isMetricData, mEntityTable.mDataColumns["byte:IsMetric"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
            int* numSavesData = new int[count];
            if (mEntityTable.column_exists("int:NumSaves")) {
                memcpy(numSavesData, mEntityTable.mDataColumns["int:NumSaves"].begin(), count * sizeof(int));
            }
            
            bfast::byte* isLinkedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsLinked")) {
                memcpy(isLinkedData, mEntityTable.mDataColumns["byte:IsLinked"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isDetachedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsDetached")) {
                memcpy(isDetachedData, mEntityTable.mDataColumns["byte:IsDetached"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isWorksharedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsWorkshared")) {
                memcpy(isWorksharedData, mEntityTable.mDataColumns["byte:IsWorkshared"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& pathNameData = mEntityTable.column_exists("string:PathName") ? mEntityTable.mStringColumns["string:PathName"] : std::vector<int>();
            
            double* latitudeData = new double[count];
            if (mEntityTable.column_exists("double:Latitude")) {
                memcpy(latitudeData, mEntityTable.mDataColumns["double:Latitude"].begin(), count * sizeof(double));
            }
            
            double* longitudeData = new double[count];
            if (mEntityTable.column_exists("double:Longitude")) {
                memcpy(longitudeData, mEntityTable.mDataColumns["double:Longitude"].begin(), count * sizeof(double));
            }
            
            double* timeZoneData = new double[count];
            if (mEntityTable.column_exists("double:TimeZone")) {
                memcpy(timeZoneData, mEntityTable.mDataColumns["double:TimeZone"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& placeNameData = mEntityTable.column_exists("string:PlaceName") ? mEntityTable.mStringColumns["string:PlaceName"] : std::vector<int>();
            
            const std::vector<int>& weatherStationNameData = mEntityTable.column_exists("string:WeatherStationName") ? mEntityTable.mStringColumns["string:WeatherStationName"] : std::vector<int>();
            
            double* elevationData = new double[count];
            if (mEntityTable.column_exists("double:Elevation")) {
                memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& projectLocationData = mEntityTable.column_exists("string:ProjectLocation") ? mEntityTable.mStringColumns["string:ProjectLocation"] : std::vector<int>();
            
            const std::vector<int>& issueDateData = mEntityTable.column_exists("string:IssueDate") ? mEntityTable.mStringColumns["string:IssueDate"] : std::vector<int>();
            
            const std::vector<int>& statusData = mEntityTable.column_exists("string:Status") ? mEntityTable.mStringColumns["string:Status"] : std::vector<int>();
            
            const std::vector<int>& clientNameData = mEntityTable.column_exists("string:ClientName") ? mEntityTable.mStringColumns["string:ClientName"] : std::vector<int>();
            
            const std::vector<int>& addressData = mEntityTable.column_exists("string:Address") ? mEntityTable.mStringColumns["string:Address"] : std::vector<int>();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
            const std::vector<int>& authorData = mEntityTable.column_exists("string:Author") ? mEntityTable.mStringColumns["string:Author"] : std::vector<int>();
            
            const std::vector<int>& buildingNameData = mEntityTable.column_exists("string:BuildingName") ? mEntityTable.mStringColumns["string:BuildingName"] : std::vector<int>();
            
            const std::vector<int>& organizationNameData = mEntityTable.column_exists("string:OrganizationName") ? mEntityTable.mStringColumns["string:OrganizationName"] : std::vector<int>();
            
            const std::vector<int>& organizationDescriptionData = mEntityTable.column_exists("string:OrganizationDescription") ? mEntityTable.mStringColumns["string:OrganizationDescription"] : std::vector<int>();
            
            const std::vector<int>& productData = mEntityTable.column_exists("string:Product") ? mEntityTable.mStringColumns["string:Product"] : std::vector<int>();
            
            const std::vector<int>& versionData = mEntityTable.column_exists("string:Version") ? mEntityTable.mStringColumns["string:Version"] : std::vector<int>();
            
            const std::vector<int>& userData = mEntityTable.column_exists("string:User") ? mEntityTable.mStringColumns["string:User"] : std::vector<int>();
            
            const std::vector<int>& activeViewData = mEntityTable.column_exists("index:Vim.View:ActiveView") ? mEntityTable.mIndexColumns["index:Vim.View:ActiveView"] : std::vector<int>();
            const std::vector<int>& ownerFamilyData = mEntityTable.column_exists("index:Vim.Family:OwnerFamily") ? mEntityTable.mIndexColumns["index:Vim.Family:OwnerFamily"] : std::vector<int>();
            const std::vector<int>& parentData = mEntityTable.column_exists("index:Vim.BimDocument:Parent") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:Parent"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Title")) {
                return &mStrings[mEntityTable.mStringColumns["string:Title"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllTitle()
        {
            const int count = GetCount();
            
            const std::vector<int>& titleData = mEntityTable.column_exists("string:Title") ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsMetric")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsMetric"].begin() + bimDocumentIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsMetric()
        {
            const int count = GetCount();
            
            bfast::byte* isMetricData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsMetric")) {
                memcpy(isMetricData, mEntityTable.mDataColumns["byte:IsMetric"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isMetricData, isMetricData + count);
            
            delete[] isMetricData;
            
            return result;
        }
        
        const std::string* GetGuid(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Guid")) {
                return &mStrings[mEntityTable.mStringColumns["string:Guid"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllGuid()
        {
            const int count = GetCount();
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:NumSaves")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:NumSaves"].begin() + bimDocumentIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllNumSaves()
        {
            const int count = GetCount();
            
            int* numSavesData = new int[count];
            if (mEntityTable.column_exists("int:NumSaves")) {
                memcpy(numSavesData, mEntityTable.mDataColumns["int:NumSaves"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(numSavesData, numSavesData + count);
            
            delete[] numSavesData;
            
            return result;
        }
        
        bool GetIsLinked(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsLinked")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsLinked"].begin() + bimDocumentIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsLinked()
        {
            const int count = GetCount();
            
            bfast::byte* isLinkedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsLinked")) {
                memcpy(isLinkedData, mEntityTable.mDataColumns["byte:IsLinked"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isLinkedData, isLinkedData + count);
            
            delete[] isLinkedData;
            
            return result;
        }
        
        bool GetIsDetached(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsDetached")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsDetached"].begin() + bimDocumentIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsDetached()
        {
            const int count = GetCount();
            
            bfast::byte* isDetachedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsDetached")) {
                memcpy(isDetachedData, mEntityTable.mDataColumns["byte:IsDetached"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isDetachedData, isDetachedData + count);
            
            delete[] isDetachedData;
            
            return result;
        }
        
        bool GetIsWorkshared(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsWorkshared")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsWorkshared"].begin() + bimDocumentIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsWorkshared()
        {
            const int count = GetCount();
            
            bfast::byte* isWorksharedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsWorkshared")) {
                memcpy(isWorksharedData, mEntityTable.mDataColumns["byte:IsWorkshared"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isWorksharedData, isWorksharedData + count);
            
            delete[] isWorksharedData;
            
            return result;
        }
        
        const std::string* GetPathName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:PathName")) {
                return &mStrings[mEntityTable.mStringColumns["string:PathName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllPathName()
        {
            const int count = GetCount();
            
            const std::vector<int>& pathNameData = mEntityTable.column_exists("string:PathName") ? mEntityTable.mStringColumns["string:PathName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:Latitude")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Latitude"].begin() + bimDocumentIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLatitude()
        {
            const int count = GetCount();
            
            double* latitudeData = new double[count];
            if (mEntityTable.column_exists("double:Latitude")) {
                memcpy(latitudeData, mEntityTable.mDataColumns["double:Latitude"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(latitudeData, latitudeData + count);
            
            delete[] latitudeData;
            
            return result;
        }
        
        double GetLongitude(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Longitude")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Longitude"].begin() + bimDocumentIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLongitude()
        {
            const int count = GetCount();
            
            double* longitudeData = new double[count];
            if (mEntityTable.column_exists("double:Longitude")) {
                memcpy(longitudeData, mEntityTable.mDataColumns["double:Longitude"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(longitudeData, longitudeData + count);
            
            delete[] longitudeData;
            
            return result;
        }
        
        double GetTimeZone(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:TimeZone")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:TimeZone"].begin() + bimDocumentIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllTimeZone()
        {
            const int count = GetCount();
            
            double* timeZoneData = new double[count];
            if (mEntityTable.column_exists("double:TimeZone")) {
                memcpy(timeZoneData, mEntityTable.mDataColumns["double:TimeZone"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(timeZoneData, timeZoneData + count);
            
            delete[] timeZoneData;
            
            return result;
        }
        
        const std::string* GetPlaceName(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:PlaceName")) {
                return &mStrings[mEntityTable.mStringColumns["string:PlaceName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllPlaceName()
        {
            const int count = GetCount();
            
            const std::vector<int>& placeNameData = mEntityTable.column_exists("string:PlaceName") ? mEntityTable.mStringColumns["string:PlaceName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:WeatherStationName")) {
                return &mStrings[mEntityTable.mStringColumns["string:WeatherStationName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllWeatherStationName()
        {
            const int count = GetCount();
            
            const std::vector<int>& weatherStationNameData = mEntityTable.column_exists("string:WeatherStationName") ? mEntityTable.mStringColumns["string:WeatherStationName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:Elevation")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Elevation"].begin() + bimDocumentIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllElevation()
        {
            const int count = GetCount();
            
            double* elevationData = new double[count];
            if (mEntityTable.column_exists("double:Elevation")) {
                memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(elevationData, elevationData + count);
            
            delete[] elevationData;
            
            return result;
        }
        
        const std::string* GetProjectLocation(int bimDocumentIndex)
        {
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:ProjectLocation")) {
                return &mStrings[mEntityTable.mStringColumns["string:ProjectLocation"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllProjectLocation()
        {
            const int count = GetCount();
            
            const std::vector<int>& projectLocationData = mEntityTable.column_exists("string:ProjectLocation") ? mEntityTable.mStringColumns["string:ProjectLocation"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:IssueDate")) {
                return &mStrings[mEntityTable.mStringColumns["string:IssueDate"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllIssueDate()
        {
            const int count = GetCount();
            
            const std::vector<int>& issueDateData = mEntityTable.column_exists("string:IssueDate") ? mEntityTable.mStringColumns["string:IssueDate"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Status")) {
                return &mStrings[mEntityTable.mStringColumns["string:Status"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllStatus()
        {
            const int count = GetCount();
            
            const std::vector<int>& statusData = mEntityTable.column_exists("string:Status") ? mEntityTable.mStringColumns["string:Status"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:ClientName")) {
                return &mStrings[mEntityTable.mStringColumns["string:ClientName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllClientName()
        {
            const int count = GetCount();
            
            const std::vector<int>& clientNameData = mEntityTable.column_exists("string:ClientName") ? mEntityTable.mStringColumns["string:ClientName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Address")) {
                return &mStrings[mEntityTable.mStringColumns["string:Address"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllAddress()
        {
            const int count = GetCount();
            
            const std::vector<int>& addressData = mEntityTable.column_exists("string:Address") ? mEntityTable.mStringColumns["string:Address"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Number")) {
                return &mStrings[mEntityTable.mStringColumns["string:Number"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllNumber()
        {
            const int count = GetCount();
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Author")) {
                return &mStrings[mEntityTable.mStringColumns["string:Author"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllAuthor()
        {
            const int count = GetCount();
            
            const std::vector<int>& authorData = mEntityTable.column_exists("string:Author") ? mEntityTable.mStringColumns["string:Author"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:BuildingName")) {
                return &mStrings[mEntityTable.mStringColumns["string:BuildingName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllBuildingName()
        {
            const int count = GetCount();
            
            const std::vector<int>& buildingNameData = mEntityTable.column_exists("string:BuildingName") ? mEntityTable.mStringColumns["string:BuildingName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:OrganizationName")) {
                return &mStrings[mEntityTable.mStringColumns["string:OrganizationName"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllOrganizationName()
        {
            const int count = GetCount();
            
            const std::vector<int>& organizationNameData = mEntityTable.column_exists("string:OrganizationName") ? mEntityTable.mStringColumns["string:OrganizationName"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:OrganizationDescription")) {
                return &mStrings[mEntityTable.mStringColumns["string:OrganizationDescription"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllOrganizationDescription()
        {
            const int count = GetCount();
            
            const std::vector<int>& organizationDescriptionData = mEntityTable.column_exists("string:OrganizationDescription") ? mEntityTable.mStringColumns["string:OrganizationDescription"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Product")) {
                return &mStrings[mEntityTable.mStringColumns["string:Product"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllProduct()
        {
            const int count = GetCount();
            
            const std::vector<int>& productData = mEntityTable.column_exists("string:Product") ? mEntityTable.mStringColumns["string:Product"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Version")) {
                return &mStrings[mEntityTable.mStringColumns["string:Version"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllVersion()
        {
            const int count = GetCount();
            
            const std::vector<int>& versionData = mEntityTable.column_exists("string:Version") ? mEntityTable.mStringColumns["string:Version"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:User")) {
                return &mStrings[mEntityTable.mStringColumns["string:User"][bimDocumentIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllUser()
        {
            const int count = GetCount();
            
            const std::vector<int>& userData = mEntityTable.column_exists("string:User") ? mEntityTable.mStringColumns["string:User"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.View:ActiveView")) {
                return -1;
            }
            
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.View:ActiveView"][bimDocumentIndex];
        }
        
        int GetOwnerFamilyIndex(int bimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Family:OwnerFamily")) {
                return -1;
            }
            
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Family:OwnerFamily"][bimDocumentIndex];
        }
        
        int GetParentIndex(int bimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.BimDocument:Parent")) {
                return -1;
            }
            
            if (bimDocumentIndex < 0 || bimDocumentIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.BimDocument:Parent"][bimDocumentIndex];
        }
        
        int GetElementIndex(int bimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsDisplayUnit = mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit");
            bool existsBimDocument = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument");
            
            const int count = GetCount();
            
            std::vector<DisplayUnitInBimDocument>* displayUnitInBimDocument = new std::vector<DisplayUnitInBimDocument>();
            displayUnitInBimDocument->reserve(count);
            
            const std::vector<int>& displayUnitData = mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit") ? mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.DisplayUnit:DisplayUnit")) {
                return -1;
            }
            
            if (displayUnitInBimDocumentIndex < 0 || displayUnitInBimDocumentIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"][displayUnitInBimDocumentIndex];
        }
        
        int GetBimDocumentIndex(int displayUnitInBimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.BimDocument:BimDocument")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsOrderIndex = mEntityTable.column_exists("int:OrderIndex");
            bool existsPhase = mEntityTable.column_exists("index:Vim.Phase:Phase");
            bool existsBimDocument = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument");
            
            const int count = GetCount();
            
            std::vector<PhaseOrderInBimDocument>* phaseOrderInBimDocument = new std::vector<PhaseOrderInBimDocument>();
            phaseOrderInBimDocument->reserve(count);
            
            int* orderIndexData = new int[count];
            if (mEntityTable.column_exists("int:OrderIndex")) {
                memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& phaseData = mEntityTable.column_exists("index:Vim.Phase:Phase") ? mEntityTable.mIndexColumns["index:Vim.Phase:Phase"] : std::vector<int>();
            const std::vector<int>& bimDocumentData = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:OrderIndex")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:OrderIndex"].begin() + phaseOrderInBimDocumentIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllOrderIndex()
        {
            const int count = GetCount();
            
            int* orderIndexData = new int[count];
            if (mEntityTable.column_exists("int:OrderIndex")) {
                memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(orderIndexData, orderIndexData + count);
            
            delete[] orderIndexData;
            
            return result;
        }
        
        int GetPhaseIndex(int phaseOrderInBimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Phase:Phase")) {
                return -1;
            }
            
            if (phaseOrderInBimDocumentIndex < 0 || phaseOrderInBimDocumentIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Phase:Phase"][phaseOrderInBimDocumentIndex];
        }
        
        int GetBimDocumentIndex(int phaseOrderInBimDocumentIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.BimDocument:BimDocument")) {
                return -1;
            }
            
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
        long long mId;
        const std::string* mCategoryType;
        double mLineColor_X;
        double mLineColor_Y;
        double mLineColor_Z;
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
            return mEntityTable.get_count();
        }
        
        Category* Get(int categoryIndex)
        {
            Category* category = new Category();
            category->mIndex = categoryIndex;
            category->mName = GetName(categoryIndex);
            category->mId = GetId(categoryIndex);
            category->mCategoryType = GetCategoryType(categoryIndex);
            category->mLineColor_X = GetLineColor_X(categoryIndex);
            category->mLineColor_Y = GetLineColor_Y(categoryIndex);
            category->mLineColor_Z = GetLineColor_Z(categoryIndex);
            category->mBuiltInCategory = GetBuiltInCategory(categoryIndex);
            category->mParentIndex = GetParentIndex(categoryIndex);
            category->mMaterialIndex = GetMaterialIndex(categoryIndex);
            return category;
        }
        
        std::vector<Category>* GetAll()
        {
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsId = mEntityTable.column_exists("long:Id") || mEntityTable.column_exists("int:Id");
            bool existsCategoryType = mEntityTable.column_exists("string:CategoryType");
            bool existsLineColor_X = mEntityTable.column_exists("double:LineColor.X");
            bool existsLineColor_Y = mEntityTable.column_exists("double:LineColor.Y");
            bool existsLineColor_Z = mEntityTable.column_exists("double:LineColor.Z");
            bool existsBuiltInCategory = mEntityTable.column_exists("string:BuiltInCategory");
            bool existsParent = mEntityTable.column_exists("index:Vim.Category:Parent");
            bool existsMaterial = mEntityTable.column_exists("index:Vim.Material:Material");
            
            const int count = GetCount();
            
            std::vector<Category>* category = new std::vector<Category>();
            category->reserve(count);
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            long long* idData = new long long[count];
            if (mEntityTable.column_exists("long:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["long:Id"].begin(), count * sizeof(long long));
            }
            else if (mEntityTable.column_exists("int:Id")) {
                for (int i = 0; i < count; ++i) {
                    idData[i] = static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + i * sizeof(int))));
                }
            }
            
            const std::vector<int>& categoryTypeData = mEntityTable.column_exists("string:CategoryType") ? mEntityTable.mStringColumns["string:CategoryType"] : std::vector<int>();
            
            double* lineColor_XData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.X")) {
                memcpy(lineColor_XData, mEntityTable.mDataColumns["double:LineColor.X"].begin(), count * sizeof(double));
            }
            
            double* lineColor_YData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.Y")) {
                memcpy(lineColor_YData, mEntityTable.mDataColumns["double:LineColor.Y"].begin(), count * sizeof(double));
            }
            
            double* lineColor_ZData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.Z")) {
                memcpy(lineColor_ZData, mEntityTable.mDataColumns["double:LineColor.Z"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& builtInCategoryData = mEntityTable.column_exists("string:BuiltInCategory") ? mEntityTable.mStringColumns["string:BuiltInCategory"] : std::vector<int>();
            
            const std::vector<int>& parentData = mEntityTable.column_exists("index:Vim.Category:Parent") ? mEntityTable.mIndexColumns["index:Vim.Category:Parent"] : std::vector<int>();
            const std::vector<int>& materialData = mEntityTable.column_exists("index:Vim.Material:Material") ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();
            
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
                if (existsLineColor_X)
                    entity.mLineColor_X = lineColor_XData[i];
                if (existsLineColor_Y)
                    entity.mLineColor_Y = lineColor_YData[i];
                if (existsLineColor_Z)
                    entity.mLineColor_Z = lineColor_ZData[i];
                if (existsBuiltInCategory)
                    entity.mBuiltInCategory = &mStrings[builtInCategoryData[i]];
                entity.mParentIndex = existsParent ? parentData[i] : -1;
                entity.mMaterialIndex = existsMaterial ? materialData[i] : -1;
                category->push_back(entity);
            }
            
            delete[] idData;
            delete[] lineColor_XData;
            delete[] lineColor_YData;
            delete[] lineColor_ZData;
            
            return category;
        }
        
        const std::string* GetName(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][categoryIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }
            
            return result;
        }
        
        long long GetId(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("long:Id")) {
                return *reinterpret_cast<long long*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["long:Id"].begin() + categoryIndex * sizeof(long long)));
            }
            
            if (mEntityTable.column_exists("int:Id")) {
                return static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + categoryIndex * sizeof(int))));
            }
            
            return {};
        }
        
        const std::vector<long long>* GetAllId()
        {
            const int count = GetCount();
            
            long long* idData = new long long[count];
            if (mEntityTable.column_exists("long:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["long:Id"].begin(), count * sizeof(long long));
            }
            else if (mEntityTable.column_exists("int:Id")) {
                for (int i = 0; i < count; ++i) {
                    idData[i] = static_cast<long long>(*reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + i * sizeof(int))));
                }
            }
            
            std::vector<long long>* result = new std::vector<long long>(idData, idData + count);
            
            delete[] idData;
            
            return result;
        }
        
        const std::string* GetCategoryType(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:CategoryType")) {
                return &mStrings[mEntityTable.mStringColumns["string:CategoryType"][categoryIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllCategoryType()
        {
            const int count = GetCount();
            
            const std::vector<int>& categoryTypeData = mEntityTable.column_exists("string:CategoryType") ? mEntityTable.mStringColumns["string:CategoryType"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[categoryTypeData[i]]);
            }
            
            return result;
        }
        
        double GetLineColor_X(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:LineColor.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:LineColor.X"].begin() + categoryIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLineColor_X()
        {
            const int count = GetCount();
            
            double* lineColor_XData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.X")) {
                memcpy(lineColor_XData, mEntityTable.mDataColumns["double:LineColor.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(lineColor_XData, lineColor_XData + count);
            
            delete[] lineColor_XData;
            
            return result;
        }
        
        double GetLineColor_Y(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:LineColor.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:LineColor.Y"].begin() + categoryIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLineColor_Y()
        {
            const int count = GetCount();
            
            double* lineColor_YData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.Y")) {
                memcpy(lineColor_YData, mEntityTable.mDataColumns["double:LineColor.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(lineColor_YData, lineColor_YData + count);
            
            delete[] lineColor_YData;
            
            return result;
        }
        
        double GetLineColor_Z(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:LineColor.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:LineColor.Z"].begin() + categoryIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllLineColor_Z()
        {
            const int count = GetCount();
            
            double* lineColor_ZData = new double[count];
            if (mEntityTable.column_exists("double:LineColor.Z")) {
                memcpy(lineColor_ZData, mEntityTable.mDataColumns["double:LineColor.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(lineColor_ZData, lineColor_ZData + count);
            
            delete[] lineColor_ZData;
            
            return result;
        }
        
        const std::string* GetBuiltInCategory(int categoryIndex)
        {
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:BuiltInCategory")) {
                return &mStrings[mEntityTable.mStringColumns["string:BuiltInCategory"][categoryIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllBuiltInCategory()
        {
            const int count = GetCount();
            
            const std::vector<int>& builtInCategoryData = mEntityTable.column_exists("string:BuiltInCategory") ? mEntityTable.mStringColumns["string:BuiltInCategory"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Category:Parent")) {
                return -1;
            }
            
            if (categoryIndex < 0 || categoryIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Category:Parent"][categoryIndex];
        }
        
        int GetMaterialIndex(int categoryIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Material:Material")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsStructuralMaterialType = mEntityTable.column_exists("string:StructuralMaterialType");
            bool existsStructuralSectionShape = mEntityTable.column_exists("string:StructuralSectionShape");
            bool existsIsSystemFamily = mEntityTable.column_exists("byte:IsSystemFamily");
            bool existsIsInPlace = mEntityTable.column_exists("byte:IsInPlace");
            bool existsFamilyCategory = mEntityTable.column_exists("index:Vim.Category:FamilyCategory");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Family>* family = new std::vector<Family>();
            family->reserve(count);
            
            const std::vector<int>& structuralMaterialTypeData = mEntityTable.column_exists("string:StructuralMaterialType") ? mEntityTable.mStringColumns["string:StructuralMaterialType"] : std::vector<int>();
            
            const std::vector<int>& structuralSectionShapeData = mEntityTable.column_exists("string:StructuralSectionShape") ? mEntityTable.mStringColumns["string:StructuralSectionShape"] : std::vector<int>();
            
            bfast::byte* isSystemFamilyData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSystemFamily")) {
                memcpy(isSystemFamilyData, mEntityTable.mDataColumns["byte:IsSystemFamily"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* isInPlaceData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsInPlace")) {
                memcpy(isInPlaceData, mEntityTable.mDataColumns["byte:IsInPlace"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& familyCategoryData = mEntityTable.column_exists("index:Vim.Category:FamilyCategory") ? mEntityTable.mIndexColumns["index:Vim.Category:FamilyCategory"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:StructuralMaterialType")) {
                return &mStrings[mEntityTable.mStringColumns["string:StructuralMaterialType"][familyIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllStructuralMaterialType()
        {
            const int count = GetCount();
            
            const std::vector<int>& structuralMaterialTypeData = mEntityTable.column_exists("string:StructuralMaterialType") ? mEntityTable.mStringColumns["string:StructuralMaterialType"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:StructuralSectionShape")) {
                return &mStrings[mEntityTable.mStringColumns["string:StructuralSectionShape"][familyIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllStructuralSectionShape()
        {
            const int count = GetCount();
            
            const std::vector<int>& structuralSectionShapeData = mEntityTable.column_exists("string:StructuralSectionShape") ? mEntityTable.mStringColumns["string:StructuralSectionShape"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsSystemFamily")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSystemFamily"].begin() + familyIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsSystemFamily()
        {
            const int count = GetCount();
            
            bfast::byte* isSystemFamilyData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSystemFamily")) {
                memcpy(isSystemFamilyData, mEntityTable.mDataColumns["byte:IsSystemFamily"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isSystemFamilyData, isSystemFamilyData + count);
            
            delete[] isSystemFamilyData;
            
            return result;
        }
        
        bool GetIsInPlace(int familyIndex)
        {
            if (familyIndex < 0 || familyIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsInPlace")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsInPlace"].begin() + familyIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsInPlace()
        {
            const int count = GetCount();
            
            bfast::byte* isInPlaceData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsInPlace")) {
                memcpy(isInPlaceData, mEntityTable.mDataColumns["byte:IsInPlace"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isInPlaceData, isInPlaceData + count);
            
            delete[] isInPlaceData;
            
            return result;
        }
        
        int GetFamilyCategoryIndex(int familyIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Category:FamilyCategory")) {
                return -1;
            }
            
            if (familyIndex < 0 || familyIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Category:FamilyCategory"][familyIndex];
        }
        
        int GetElementIndex(int familyIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsIsSystemFamilyType = mEntityTable.column_exists("byte:IsSystemFamilyType");
            bool existsFamily = mEntityTable.column_exists("index:Vim.Family:Family");
            bool existsCompoundStructure = mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<FamilyType>* familyType = new std::vector<FamilyType>();
            familyType->reserve(count);
            
            bfast::byte* isSystemFamilyTypeData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSystemFamilyType")) {
                memcpy(isSystemFamilyTypeData, mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& familyData = mEntityTable.column_exists("index:Vim.Family:Family") ? mEntityTable.mIndexColumns["index:Vim.Family:Family"] : std::vector<int>();
            const std::vector<int>& compoundStructureData = mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure") ? mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsSystemFamilyType")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin() + familyTypeIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsSystemFamilyType()
        {
            const int count = GetCount();
            
            bfast::byte* isSystemFamilyTypeData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSystemFamilyType")) {
                memcpy(isSystemFamilyTypeData, mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isSystemFamilyTypeData, isSystemFamilyTypeData + count);
            
            delete[] isSystemFamilyTypeData;
            
            return result;
        }
        
        int GetFamilyIndex(int familyTypeIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Family:Family")) {
                return -1;
            }
            
            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Family:Family"][familyTypeIndex];
        }
        
        int GetCompoundStructureIndex(int familyTypeIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure")) {
                return -1;
            }
            
            if (familyTypeIndex < 0 || familyTypeIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"][familyTypeIndex];
        }
        
        int GetElementIndex(int familyTypeIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        float mFacingOrientation_X;
        float mFacingOrientation_Y;
        float mFacingOrientation_Z;
        bool mHandFlipped;
        bool mMirrored;
        bool mHasModifiedGeometry;
        float mScale;
        float mBasisX_X;
        float mBasisX_Y;
        float mBasisX_Z;
        float mBasisY_X;
        float mBasisY_Y;
        float mBasisY_Z;
        float mBasisZ_X;
        float mBasisZ_Y;
        float mBasisZ_Z;
        float mTranslation_X;
        float mTranslation_Y;
        float mTranslation_Z;
        float mHandOrientation_X;
        float mHandOrientation_Y;
        float mHandOrientation_Z;
        
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
            return mEntityTable.get_count();
        }
        
        FamilyInstance* Get(int familyInstanceIndex)
        {
            FamilyInstance* familyInstance = new FamilyInstance();
            familyInstance->mIndex = familyInstanceIndex;
            familyInstance->mFacingFlipped = GetFacingFlipped(familyInstanceIndex);
            familyInstance->mFacingOrientation_X = GetFacingOrientation_X(familyInstanceIndex);
            familyInstance->mFacingOrientation_Y = GetFacingOrientation_Y(familyInstanceIndex);
            familyInstance->mFacingOrientation_Z = GetFacingOrientation_Z(familyInstanceIndex);
            familyInstance->mHandFlipped = GetHandFlipped(familyInstanceIndex);
            familyInstance->mMirrored = GetMirrored(familyInstanceIndex);
            familyInstance->mHasModifiedGeometry = GetHasModifiedGeometry(familyInstanceIndex);
            familyInstance->mScale = GetScale(familyInstanceIndex);
            familyInstance->mBasisX_X = GetBasisX_X(familyInstanceIndex);
            familyInstance->mBasisX_Y = GetBasisX_Y(familyInstanceIndex);
            familyInstance->mBasisX_Z = GetBasisX_Z(familyInstanceIndex);
            familyInstance->mBasisY_X = GetBasisY_X(familyInstanceIndex);
            familyInstance->mBasisY_Y = GetBasisY_Y(familyInstanceIndex);
            familyInstance->mBasisY_Z = GetBasisY_Z(familyInstanceIndex);
            familyInstance->mBasisZ_X = GetBasisZ_X(familyInstanceIndex);
            familyInstance->mBasisZ_Y = GetBasisZ_Y(familyInstanceIndex);
            familyInstance->mBasisZ_Z = GetBasisZ_Z(familyInstanceIndex);
            familyInstance->mTranslation_X = GetTranslation_X(familyInstanceIndex);
            familyInstance->mTranslation_Y = GetTranslation_Y(familyInstanceIndex);
            familyInstance->mTranslation_Z = GetTranslation_Z(familyInstanceIndex);
            familyInstance->mHandOrientation_X = GetHandOrientation_X(familyInstanceIndex);
            familyInstance->mHandOrientation_Y = GetHandOrientation_Y(familyInstanceIndex);
            familyInstance->mHandOrientation_Z = GetHandOrientation_Z(familyInstanceIndex);
            familyInstance->mFamilyTypeIndex = GetFamilyTypeIndex(familyInstanceIndex);
            familyInstance->mHostIndex = GetHostIndex(familyInstanceIndex);
            familyInstance->mFromRoomIndex = GetFromRoomIndex(familyInstanceIndex);
            familyInstance->mToRoomIndex = GetToRoomIndex(familyInstanceIndex);
            familyInstance->mElementIndex = GetElementIndex(familyInstanceIndex);
            return familyInstance;
        }
        
        std::vector<FamilyInstance>* GetAll()
        {
            bool existsFacingFlipped = mEntityTable.column_exists("byte:FacingFlipped");
            bool existsFacingOrientation_X = mEntityTable.column_exists("float:FacingOrientation.X");
            bool existsFacingOrientation_Y = mEntityTable.column_exists("float:FacingOrientation.Y");
            bool existsFacingOrientation_Z = mEntityTable.column_exists("float:FacingOrientation.Z");
            bool existsHandFlipped = mEntityTable.column_exists("byte:HandFlipped");
            bool existsMirrored = mEntityTable.column_exists("byte:Mirrored");
            bool existsHasModifiedGeometry = mEntityTable.column_exists("byte:HasModifiedGeometry");
            bool existsScale = mEntityTable.column_exists("float:Scale");
            bool existsBasisX_X = mEntityTable.column_exists("float:BasisX.X");
            bool existsBasisX_Y = mEntityTable.column_exists("float:BasisX.Y");
            bool existsBasisX_Z = mEntityTable.column_exists("float:BasisX.Z");
            bool existsBasisY_X = mEntityTable.column_exists("float:BasisY.X");
            bool existsBasisY_Y = mEntityTable.column_exists("float:BasisY.Y");
            bool existsBasisY_Z = mEntityTable.column_exists("float:BasisY.Z");
            bool existsBasisZ_X = mEntityTable.column_exists("float:BasisZ.X");
            bool existsBasisZ_Y = mEntityTable.column_exists("float:BasisZ.Y");
            bool existsBasisZ_Z = mEntityTable.column_exists("float:BasisZ.Z");
            bool existsTranslation_X = mEntityTable.column_exists("float:Translation.X");
            bool existsTranslation_Y = mEntityTable.column_exists("float:Translation.Y");
            bool existsTranslation_Z = mEntityTable.column_exists("float:Translation.Z");
            bool existsHandOrientation_X = mEntityTable.column_exists("float:HandOrientation.X");
            bool existsHandOrientation_Y = mEntityTable.column_exists("float:HandOrientation.Y");
            bool existsHandOrientation_Z = mEntityTable.column_exists("float:HandOrientation.Z");
            bool existsFamilyType = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType");
            bool existsHost = mEntityTable.column_exists("index:Vim.Element:Host");
            bool existsFromRoom = mEntityTable.column_exists("index:Vim.Room:FromRoom");
            bool existsToRoom = mEntityTable.column_exists("index:Vim.Room:ToRoom");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<FamilyInstance>* familyInstance = new std::vector<FamilyInstance>();
            familyInstance->reserve(count);
            
            bfast::byte* facingFlippedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:FacingFlipped")) {
                memcpy(facingFlippedData, mEntityTable.mDataColumns["byte:FacingFlipped"].begin(), count * sizeof(bfast::byte));
            }
            
            float* facingOrientation_XData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.X")) {
                memcpy(facingOrientation_XData, mEntityTable.mDataColumns["float:FacingOrientation.X"].begin(), count * sizeof(float));
            }
            
            float* facingOrientation_YData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.Y")) {
                memcpy(facingOrientation_YData, mEntityTable.mDataColumns["float:FacingOrientation.Y"].begin(), count * sizeof(float));
            }
            
            float* facingOrientation_ZData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.Z")) {
                memcpy(facingOrientation_ZData, mEntityTable.mDataColumns["float:FacingOrientation.Z"].begin(), count * sizeof(float));
            }
            
            bfast::byte* handFlippedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:HandFlipped")) {
                memcpy(handFlippedData, mEntityTable.mDataColumns["byte:HandFlipped"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* mirroredData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:Mirrored")) {
                memcpy(mirroredData, mEntityTable.mDataColumns["byte:Mirrored"].begin(), count * sizeof(bfast::byte));
            }
            
            bfast::byte* hasModifiedGeometryData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:HasModifiedGeometry")) {
                memcpy(hasModifiedGeometryData, mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin(), count * sizeof(bfast::byte));
            }
            
            float* scaleData = new float[count];
            if (mEntityTable.column_exists("float:Scale")) {
                memcpy(scaleData, mEntityTable.mDataColumns["float:Scale"].begin(), count * sizeof(float));
            }
            
            float* basisX_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.X")) {
                memcpy(basisX_XData, mEntityTable.mDataColumns["float:BasisX.X"].begin(), count * sizeof(float));
            }
            
            float* basisX_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.Y")) {
                memcpy(basisX_YData, mEntityTable.mDataColumns["float:BasisX.Y"].begin(), count * sizeof(float));
            }
            
            float* basisX_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.Z")) {
                memcpy(basisX_ZData, mEntityTable.mDataColumns["float:BasisX.Z"].begin(), count * sizeof(float));
            }
            
            float* basisY_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.X")) {
                memcpy(basisY_XData, mEntityTable.mDataColumns["float:BasisY.X"].begin(), count * sizeof(float));
            }
            
            float* basisY_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.Y")) {
                memcpy(basisY_YData, mEntityTable.mDataColumns["float:BasisY.Y"].begin(), count * sizeof(float));
            }
            
            float* basisY_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.Z")) {
                memcpy(basisY_ZData, mEntityTable.mDataColumns["float:BasisY.Z"].begin(), count * sizeof(float));
            }
            
            float* basisZ_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.X")) {
                memcpy(basisZ_XData, mEntityTable.mDataColumns["float:BasisZ.X"].begin(), count * sizeof(float));
            }
            
            float* basisZ_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.Y")) {
                memcpy(basisZ_YData, mEntityTable.mDataColumns["float:BasisZ.Y"].begin(), count * sizeof(float));
            }
            
            float* basisZ_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.Z")) {
                memcpy(basisZ_ZData, mEntityTable.mDataColumns["float:BasisZ.Z"].begin(), count * sizeof(float));
            }
            
            float* translation_XData = new float[count];
            if (mEntityTable.column_exists("float:Translation.X")) {
                memcpy(translation_XData, mEntityTable.mDataColumns["float:Translation.X"].begin(), count * sizeof(float));
            }
            
            float* translation_YData = new float[count];
            if (mEntityTable.column_exists("float:Translation.Y")) {
                memcpy(translation_YData, mEntityTable.mDataColumns["float:Translation.Y"].begin(), count * sizeof(float));
            }
            
            float* translation_ZData = new float[count];
            if (mEntityTable.column_exists("float:Translation.Z")) {
                memcpy(translation_ZData, mEntityTable.mDataColumns["float:Translation.Z"].begin(), count * sizeof(float));
            }
            
            float* handOrientation_XData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.X")) {
                memcpy(handOrientation_XData, mEntityTable.mDataColumns["float:HandOrientation.X"].begin(), count * sizeof(float));
            }
            
            float* handOrientation_YData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.Y")) {
                memcpy(handOrientation_YData, mEntityTable.mDataColumns["float:HandOrientation.Y"].begin(), count * sizeof(float));
            }
            
            float* handOrientation_ZData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.Z")) {
                memcpy(handOrientation_ZData, mEntityTable.mDataColumns["float:HandOrientation.Z"].begin(), count * sizeof(float));
            }
            
            const std::vector<int>& familyTypeData = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType") ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& hostData = mEntityTable.column_exists("index:Vim.Element:Host") ? mEntityTable.mIndexColumns["index:Vim.Element:Host"] : std::vector<int>();
            const std::vector<int>& fromRoomData = mEntityTable.column_exists("index:Vim.Room:FromRoom") ? mEntityTable.mIndexColumns["index:Vim.Room:FromRoom"] : std::vector<int>();
            const std::vector<int>& toRoomData = mEntityTable.column_exists("index:Vim.Room:ToRoom") ? mEntityTable.mIndexColumns["index:Vim.Room:ToRoom"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                FamilyInstance entity;
                entity.mIndex = i;
                if (existsFacingFlipped)
                    entity.mFacingFlipped = facingFlippedData[i];
                if (existsFacingOrientation_X)
                    entity.mFacingOrientation_X = facingOrientation_XData[i];
                if (existsFacingOrientation_Y)
                    entity.mFacingOrientation_Y = facingOrientation_YData[i];
                if (existsFacingOrientation_Z)
                    entity.mFacingOrientation_Z = facingOrientation_ZData[i];
                if (existsHandFlipped)
                    entity.mHandFlipped = handFlippedData[i];
                if (existsMirrored)
                    entity.mMirrored = mirroredData[i];
                if (existsHasModifiedGeometry)
                    entity.mHasModifiedGeometry = hasModifiedGeometryData[i];
                if (existsScale)
                    entity.mScale = scaleData[i];
                if (existsBasisX_X)
                    entity.mBasisX_X = basisX_XData[i];
                if (existsBasisX_Y)
                    entity.mBasisX_Y = basisX_YData[i];
                if (existsBasisX_Z)
                    entity.mBasisX_Z = basisX_ZData[i];
                if (existsBasisY_X)
                    entity.mBasisY_X = basisY_XData[i];
                if (existsBasisY_Y)
                    entity.mBasisY_Y = basisY_YData[i];
                if (existsBasisY_Z)
                    entity.mBasisY_Z = basisY_ZData[i];
                if (existsBasisZ_X)
                    entity.mBasisZ_X = basisZ_XData[i];
                if (existsBasisZ_Y)
                    entity.mBasisZ_Y = basisZ_YData[i];
                if (existsBasisZ_Z)
                    entity.mBasisZ_Z = basisZ_ZData[i];
                if (existsTranslation_X)
                    entity.mTranslation_X = translation_XData[i];
                if (existsTranslation_Y)
                    entity.mTranslation_Y = translation_YData[i];
                if (existsTranslation_Z)
                    entity.mTranslation_Z = translation_ZData[i];
                if (existsHandOrientation_X)
                    entity.mHandOrientation_X = handOrientation_XData[i];
                if (existsHandOrientation_Y)
                    entity.mHandOrientation_Y = handOrientation_YData[i];
                if (existsHandOrientation_Z)
                    entity.mHandOrientation_Z = handOrientation_ZData[i];
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
                entity.mHostIndex = existsHost ? hostData[i] : -1;
                entity.mFromRoomIndex = existsFromRoom ? fromRoomData[i] : -1;
                entity.mToRoomIndex = existsToRoom ? toRoomData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                familyInstance->push_back(entity);
            }
            
            delete[] facingFlippedData;
            delete[] facingOrientation_XData;
            delete[] facingOrientation_YData;
            delete[] facingOrientation_ZData;
            delete[] handFlippedData;
            delete[] mirroredData;
            delete[] hasModifiedGeometryData;
            delete[] scaleData;
            delete[] basisX_XData;
            delete[] basisX_YData;
            delete[] basisX_ZData;
            delete[] basisY_XData;
            delete[] basisY_YData;
            delete[] basisY_ZData;
            delete[] basisZ_XData;
            delete[] basisZ_YData;
            delete[] basisZ_ZData;
            delete[] translation_XData;
            delete[] translation_YData;
            delete[] translation_ZData;
            delete[] handOrientation_XData;
            delete[] handOrientation_YData;
            delete[] handOrientation_ZData;
            
            return familyInstance;
        }
        
        bool GetFacingFlipped(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:FacingFlipped")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:FacingFlipped"].begin() + familyInstanceIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllFacingFlipped()
        {
            const int count = GetCount();
            
            bfast::byte* facingFlippedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:FacingFlipped")) {
                memcpy(facingFlippedData, mEntityTable.mDataColumns["byte:FacingFlipped"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(facingFlippedData, facingFlippedData + count);
            
            delete[] facingFlippedData;
            
            return result;
        }
        
        float GetFacingOrientation_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:FacingOrientation.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:FacingOrientation.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllFacingOrientation_X()
        {
            const int count = GetCount();
            
            float* facingOrientation_XData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.X")) {
                memcpy(facingOrientation_XData, mEntityTable.mDataColumns["float:FacingOrientation.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(facingOrientation_XData, facingOrientation_XData + count);
            
            delete[] facingOrientation_XData;
            
            return result;
        }
        
        float GetFacingOrientation_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:FacingOrientation.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:FacingOrientation.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllFacingOrientation_Y()
        {
            const int count = GetCount();
            
            float* facingOrientation_YData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.Y")) {
                memcpy(facingOrientation_YData, mEntityTable.mDataColumns["float:FacingOrientation.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(facingOrientation_YData, facingOrientation_YData + count);
            
            delete[] facingOrientation_YData;
            
            return result;
        }
        
        float GetFacingOrientation_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:FacingOrientation.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:FacingOrientation.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllFacingOrientation_Z()
        {
            const int count = GetCount();
            
            float* facingOrientation_ZData = new float[count];
            if (mEntityTable.column_exists("float:FacingOrientation.Z")) {
                memcpy(facingOrientation_ZData, mEntityTable.mDataColumns["float:FacingOrientation.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(facingOrientation_ZData, facingOrientation_ZData + count);
            
            delete[] facingOrientation_ZData;
            
            return result;
        }
        
        bool GetHandFlipped(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:HandFlipped")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:HandFlipped"].begin() + familyInstanceIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllHandFlipped()
        {
            const int count = GetCount();
            
            bfast::byte* handFlippedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:HandFlipped")) {
                memcpy(handFlippedData, mEntityTable.mDataColumns["byte:HandFlipped"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(handFlippedData, handFlippedData + count);
            
            delete[] handFlippedData;
            
            return result;
        }
        
        bool GetMirrored(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:Mirrored")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:Mirrored"].begin() + familyInstanceIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllMirrored()
        {
            const int count = GetCount();
            
            bfast::byte* mirroredData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:Mirrored")) {
                memcpy(mirroredData, mEntityTable.mDataColumns["byte:Mirrored"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(mirroredData, mirroredData + count);
            
            delete[] mirroredData;
            
            return result;
        }
        
        bool GetHasModifiedGeometry(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:HasModifiedGeometry")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin() + familyInstanceIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllHasModifiedGeometry()
        {
            const int count = GetCount();
            
            bfast::byte* hasModifiedGeometryData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:HasModifiedGeometry")) {
                memcpy(hasModifiedGeometryData, mEntityTable.mDataColumns["byte:HasModifiedGeometry"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(hasModifiedGeometryData, hasModifiedGeometryData + count);
            
            delete[] hasModifiedGeometryData;
            
            return result;
        }
        
        float GetScale(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Scale")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Scale"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllScale()
        {
            const int count = GetCount();
            
            float* scaleData = new float[count];
            if (mEntityTable.column_exists("float:Scale")) {
                memcpy(scaleData, mEntityTable.mDataColumns["float:Scale"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(scaleData, scaleData + count);
            
            delete[] scaleData;
            
            return result;
        }
        
        float GetBasisX_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisX.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisX.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisX_X()
        {
            const int count = GetCount();
            
            float* basisX_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.X")) {
                memcpy(basisX_XData, mEntityTable.mDataColumns["float:BasisX.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisX_XData, basisX_XData + count);
            
            delete[] basisX_XData;
            
            return result;
        }
        
        float GetBasisX_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisX.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisX.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisX_Y()
        {
            const int count = GetCount();
            
            float* basisX_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.Y")) {
                memcpy(basisX_YData, mEntityTable.mDataColumns["float:BasisX.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisX_YData, basisX_YData + count);
            
            delete[] basisX_YData;
            
            return result;
        }
        
        float GetBasisX_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisX.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisX.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisX_Z()
        {
            const int count = GetCount();
            
            float* basisX_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisX.Z")) {
                memcpy(basisX_ZData, mEntityTable.mDataColumns["float:BasisX.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisX_ZData, basisX_ZData + count);
            
            delete[] basisX_ZData;
            
            return result;
        }
        
        float GetBasisY_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisY.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisY.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisY_X()
        {
            const int count = GetCount();
            
            float* basisY_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.X")) {
                memcpy(basisY_XData, mEntityTable.mDataColumns["float:BasisY.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisY_XData, basisY_XData + count);
            
            delete[] basisY_XData;
            
            return result;
        }
        
        float GetBasisY_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisY.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisY.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisY_Y()
        {
            const int count = GetCount();
            
            float* basisY_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.Y")) {
                memcpy(basisY_YData, mEntityTable.mDataColumns["float:BasisY.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisY_YData, basisY_YData + count);
            
            delete[] basisY_YData;
            
            return result;
        }
        
        float GetBasisY_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisY.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisY.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisY_Z()
        {
            const int count = GetCount();
            
            float* basisY_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisY.Z")) {
                memcpy(basisY_ZData, mEntityTable.mDataColumns["float:BasisY.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisY_ZData, basisY_ZData + count);
            
            delete[] basisY_ZData;
            
            return result;
        }
        
        float GetBasisZ_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisZ.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisZ.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisZ_X()
        {
            const int count = GetCount();
            
            float* basisZ_XData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.X")) {
                memcpy(basisZ_XData, mEntityTable.mDataColumns["float:BasisZ.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisZ_XData, basisZ_XData + count);
            
            delete[] basisZ_XData;
            
            return result;
        }
        
        float GetBasisZ_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisZ.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisZ.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisZ_Y()
        {
            const int count = GetCount();
            
            float* basisZ_YData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.Y")) {
                memcpy(basisZ_YData, mEntityTable.mDataColumns["float:BasisZ.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisZ_YData, basisZ_YData + count);
            
            delete[] basisZ_YData;
            
            return result;
        }
        
        float GetBasisZ_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:BasisZ.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:BasisZ.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBasisZ_Z()
        {
            const int count = GetCount();
            
            float* basisZ_ZData = new float[count];
            if (mEntityTable.column_exists("float:BasisZ.Z")) {
                memcpy(basisZ_ZData, mEntityTable.mDataColumns["float:BasisZ.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(basisZ_ZData, basisZ_ZData + count);
            
            delete[] basisZ_ZData;
            
            return result;
        }
        
        float GetTranslation_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Translation.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Translation.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllTranslation_X()
        {
            const int count = GetCount();
            
            float* translation_XData = new float[count];
            if (mEntityTable.column_exists("float:Translation.X")) {
                memcpy(translation_XData, mEntityTable.mDataColumns["float:Translation.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(translation_XData, translation_XData + count);
            
            delete[] translation_XData;
            
            return result;
        }
        
        float GetTranslation_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Translation.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Translation.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllTranslation_Y()
        {
            const int count = GetCount();
            
            float* translation_YData = new float[count];
            if (mEntityTable.column_exists("float:Translation.Y")) {
                memcpy(translation_YData, mEntityTable.mDataColumns["float:Translation.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(translation_YData, translation_YData + count);
            
            delete[] translation_YData;
            
            return result;
        }
        
        float GetTranslation_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Translation.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Translation.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllTranslation_Z()
        {
            const int count = GetCount();
            
            float* translation_ZData = new float[count];
            if (mEntityTable.column_exists("float:Translation.Z")) {
                memcpy(translation_ZData, mEntityTable.mDataColumns["float:Translation.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(translation_ZData, translation_ZData + count);
            
            delete[] translation_ZData;
            
            return result;
        }
        
        float GetHandOrientation_X(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:HandOrientation.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:HandOrientation.X"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllHandOrientation_X()
        {
            const int count = GetCount();
            
            float* handOrientation_XData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.X")) {
                memcpy(handOrientation_XData, mEntityTable.mDataColumns["float:HandOrientation.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(handOrientation_XData, handOrientation_XData + count);
            
            delete[] handOrientation_XData;
            
            return result;
        }
        
        float GetHandOrientation_Y(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:HandOrientation.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:HandOrientation.Y"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllHandOrientation_Y()
        {
            const int count = GetCount();
            
            float* handOrientation_YData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.Y")) {
                memcpy(handOrientation_YData, mEntityTable.mDataColumns["float:HandOrientation.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(handOrientation_YData, handOrientation_YData + count);
            
            delete[] handOrientation_YData;
            
            return result;
        }
        
        float GetHandOrientation_Z(int familyInstanceIndex)
        {
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:HandOrientation.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:HandOrientation.Z"].begin() + familyInstanceIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllHandOrientation_Z()
        {
            const int count = GetCount();
            
            float* handOrientation_ZData = new float[count];
            if (mEntityTable.column_exists("float:HandOrientation.Z")) {
                memcpy(handOrientation_ZData, mEntityTable.mDataColumns["float:HandOrientation.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(handOrientation_ZData, handOrientation_ZData + count);
            
            delete[] handOrientation_ZData;
            
            return result;
        }
        
        int GetFamilyTypeIndex(int familyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.FamilyType:FamilyType")) {
                return -1;
            }
            
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][familyInstanceIndex];
        }
        
        int GetHostIndex(int familyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Host")) {
                return -1;
            }
            
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Element:Host"][familyInstanceIndex];
        }
        
        int GetFromRoomIndex(int familyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Room:FromRoom")) {
                return -1;
            }
            
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Room:FromRoom"][familyInstanceIndex];
        }
        
        int GetToRoomIndex(int familyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Room:ToRoom")) {
                return -1;
            }
            
            if (familyInstanceIndex < 0 || familyInstanceIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Room:ToRoom"][familyInstanceIndex];
        }
        
        int GetElementIndex(int familyInstanceIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        double mUp_X;
        double mUp_Y;
        double mUp_Z;
        double mRight_X;
        double mRight_Y;
        double mRight_Z;
        double mOrigin_X;
        double mOrigin_Y;
        double mOrigin_Z;
        double mViewDirection_X;
        double mViewDirection_Y;
        double mViewDirection_Z;
        double mViewPosition_X;
        double mViewPosition_Y;
        double mViewPosition_Z;
        double mScale;
        double mOutline_Min_X;
        double mOutline_Min_Y;
        double mOutline_Max_X;
        double mOutline_Max_Y;
        int mDetailLevel;
        
        int mCameraIndex;
        Camera* mCamera;
        int mFamilyTypeIndex;
        FamilyType* mFamilyType;
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
            return mEntityTable.get_count();
        }
        
        View* Get(int viewIndex)
        {
            View* view = new View();
            view->mIndex = viewIndex;
            view->mTitle = GetTitle(viewIndex);
            view->mViewType = GetViewType(viewIndex);
            view->mUp_X = GetUp_X(viewIndex);
            view->mUp_Y = GetUp_Y(viewIndex);
            view->mUp_Z = GetUp_Z(viewIndex);
            view->mRight_X = GetRight_X(viewIndex);
            view->mRight_Y = GetRight_Y(viewIndex);
            view->mRight_Z = GetRight_Z(viewIndex);
            view->mOrigin_X = GetOrigin_X(viewIndex);
            view->mOrigin_Y = GetOrigin_Y(viewIndex);
            view->mOrigin_Z = GetOrigin_Z(viewIndex);
            view->mViewDirection_X = GetViewDirection_X(viewIndex);
            view->mViewDirection_Y = GetViewDirection_Y(viewIndex);
            view->mViewDirection_Z = GetViewDirection_Z(viewIndex);
            view->mViewPosition_X = GetViewPosition_X(viewIndex);
            view->mViewPosition_Y = GetViewPosition_Y(viewIndex);
            view->mViewPosition_Z = GetViewPosition_Z(viewIndex);
            view->mScale = GetScale(viewIndex);
            view->mOutline_Min_X = GetOutline_Min_X(viewIndex);
            view->mOutline_Min_Y = GetOutline_Min_Y(viewIndex);
            view->mOutline_Max_X = GetOutline_Max_X(viewIndex);
            view->mOutline_Max_Y = GetOutline_Max_Y(viewIndex);
            view->mDetailLevel = GetDetailLevel(viewIndex);
            view->mCameraIndex = GetCameraIndex(viewIndex);
            view->mFamilyTypeIndex = GetFamilyTypeIndex(viewIndex);
            view->mElementIndex = GetElementIndex(viewIndex);
            return view;
        }
        
        std::vector<View>* GetAll()
        {
            bool existsTitle = mEntityTable.column_exists("string:Title");
            bool existsViewType = mEntityTable.column_exists("string:ViewType");
            bool existsUp_X = mEntityTable.column_exists("double:Up.X");
            bool existsUp_Y = mEntityTable.column_exists("double:Up.Y");
            bool existsUp_Z = mEntityTable.column_exists("double:Up.Z");
            bool existsRight_X = mEntityTable.column_exists("double:Right.X");
            bool existsRight_Y = mEntityTable.column_exists("double:Right.Y");
            bool existsRight_Z = mEntityTable.column_exists("double:Right.Z");
            bool existsOrigin_X = mEntityTable.column_exists("double:Origin.X");
            bool existsOrigin_Y = mEntityTable.column_exists("double:Origin.Y");
            bool existsOrigin_Z = mEntityTable.column_exists("double:Origin.Z");
            bool existsViewDirection_X = mEntityTable.column_exists("double:ViewDirection.X");
            bool existsViewDirection_Y = mEntityTable.column_exists("double:ViewDirection.Y");
            bool existsViewDirection_Z = mEntityTable.column_exists("double:ViewDirection.Z");
            bool existsViewPosition_X = mEntityTable.column_exists("double:ViewPosition.X");
            bool existsViewPosition_Y = mEntityTable.column_exists("double:ViewPosition.Y");
            bool existsViewPosition_Z = mEntityTable.column_exists("double:ViewPosition.Z");
            bool existsScale = mEntityTable.column_exists("double:Scale");
            bool existsOutline_Min_X = mEntityTable.column_exists("double:Outline.Min.X");
            bool existsOutline_Min_Y = mEntityTable.column_exists("double:Outline.Min.Y");
            bool existsOutline_Max_X = mEntityTable.column_exists("double:Outline.Max.X");
            bool existsOutline_Max_Y = mEntityTable.column_exists("double:Outline.Max.Y");
            bool existsDetailLevel = mEntityTable.column_exists("int:DetailLevel");
            bool existsCamera = mEntityTable.column_exists("index:Vim.Camera:Camera");
            bool existsFamilyType = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<View>* view = new std::vector<View>();
            view->reserve(count);
            
            const std::vector<int>& titleData = mEntityTable.column_exists("string:Title") ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();
            
            const std::vector<int>& viewTypeData = mEntityTable.column_exists("string:ViewType") ? mEntityTable.mStringColumns["string:ViewType"] : std::vector<int>();
            
            double* up_XData = new double[count];
            if (mEntityTable.column_exists("double:Up.X")) {
                memcpy(up_XData, mEntityTable.mDataColumns["double:Up.X"].begin(), count * sizeof(double));
            }
            
            double* up_YData = new double[count];
            if (mEntityTable.column_exists("double:Up.Y")) {
                memcpy(up_YData, mEntityTable.mDataColumns["double:Up.Y"].begin(), count * sizeof(double));
            }
            
            double* up_ZData = new double[count];
            if (mEntityTable.column_exists("double:Up.Z")) {
                memcpy(up_ZData, mEntityTable.mDataColumns["double:Up.Z"].begin(), count * sizeof(double));
            }
            
            double* right_XData = new double[count];
            if (mEntityTable.column_exists("double:Right.X")) {
                memcpy(right_XData, mEntityTable.mDataColumns["double:Right.X"].begin(), count * sizeof(double));
            }
            
            double* right_YData = new double[count];
            if (mEntityTable.column_exists("double:Right.Y")) {
                memcpy(right_YData, mEntityTable.mDataColumns["double:Right.Y"].begin(), count * sizeof(double));
            }
            
            double* right_ZData = new double[count];
            if (mEntityTable.column_exists("double:Right.Z")) {
                memcpy(right_ZData, mEntityTable.mDataColumns["double:Right.Z"].begin(), count * sizeof(double));
            }
            
            double* origin_XData = new double[count];
            if (mEntityTable.column_exists("double:Origin.X")) {
                memcpy(origin_XData, mEntityTable.mDataColumns["double:Origin.X"].begin(), count * sizeof(double));
            }
            
            double* origin_YData = new double[count];
            if (mEntityTable.column_exists("double:Origin.Y")) {
                memcpy(origin_YData, mEntityTable.mDataColumns["double:Origin.Y"].begin(), count * sizeof(double));
            }
            
            double* origin_ZData = new double[count];
            if (mEntityTable.column_exists("double:Origin.Z")) {
                memcpy(origin_ZData, mEntityTable.mDataColumns["double:Origin.Z"].begin(), count * sizeof(double));
            }
            
            double* viewDirection_XData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.X")) {
                memcpy(viewDirection_XData, mEntityTable.mDataColumns["double:ViewDirection.X"].begin(), count * sizeof(double));
            }
            
            double* viewDirection_YData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.Y")) {
                memcpy(viewDirection_YData, mEntityTable.mDataColumns["double:ViewDirection.Y"].begin(), count * sizeof(double));
            }
            
            double* viewDirection_ZData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.Z")) {
                memcpy(viewDirection_ZData, mEntityTable.mDataColumns["double:ViewDirection.Z"].begin(), count * sizeof(double));
            }
            
            double* viewPosition_XData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.X")) {
                memcpy(viewPosition_XData, mEntityTable.mDataColumns["double:ViewPosition.X"].begin(), count * sizeof(double));
            }
            
            double* viewPosition_YData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.Y")) {
                memcpy(viewPosition_YData, mEntityTable.mDataColumns["double:ViewPosition.Y"].begin(), count * sizeof(double));
            }
            
            double* viewPosition_ZData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.Z")) {
                memcpy(viewPosition_ZData, mEntityTable.mDataColumns["double:ViewPosition.Z"].begin(), count * sizeof(double));
            }
            
            double* scaleData = new double[count];
            if (mEntityTable.column_exists("double:Scale")) {
                memcpy(scaleData, mEntityTable.mDataColumns["double:Scale"].begin(), count * sizeof(double));
            }
            
            double* outline_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Min.X")) {
                memcpy(outline_Min_XData, mEntityTable.mDataColumns["double:Outline.Min.X"].begin(), count * sizeof(double));
            }
            
            double* outline_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Min.Y")) {
                memcpy(outline_Min_YData, mEntityTable.mDataColumns["double:Outline.Min.Y"].begin(), count * sizeof(double));
            }
            
            double* outline_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Max.X")) {
                memcpy(outline_Max_XData, mEntityTable.mDataColumns["double:Outline.Max.X"].begin(), count * sizeof(double));
            }
            
            double* outline_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Max.Y")) {
                memcpy(outline_Max_YData, mEntityTable.mDataColumns["double:Outline.Max.Y"].begin(), count * sizeof(double));
            }
            
            int* detailLevelData = new int[count];
            if (mEntityTable.column_exists("int:DetailLevel")) {
                memcpy(detailLevelData, mEntityTable.mDataColumns["int:DetailLevel"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& cameraData = mEntityTable.column_exists("index:Vim.Camera:Camera") ? mEntityTable.mIndexColumns["index:Vim.Camera:Camera"] : std::vector<int>();
            const std::vector<int>& familyTypeData = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType") ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                View entity;
                entity.mIndex = i;
                if (existsTitle)
                    entity.mTitle = &mStrings[titleData[i]];
                if (existsViewType)
                    entity.mViewType = &mStrings[viewTypeData[i]];
                if (existsUp_X)
                    entity.mUp_X = up_XData[i];
                if (existsUp_Y)
                    entity.mUp_Y = up_YData[i];
                if (existsUp_Z)
                    entity.mUp_Z = up_ZData[i];
                if (existsRight_X)
                    entity.mRight_X = right_XData[i];
                if (existsRight_Y)
                    entity.mRight_Y = right_YData[i];
                if (existsRight_Z)
                    entity.mRight_Z = right_ZData[i];
                if (existsOrigin_X)
                    entity.mOrigin_X = origin_XData[i];
                if (existsOrigin_Y)
                    entity.mOrigin_Y = origin_YData[i];
                if (existsOrigin_Z)
                    entity.mOrigin_Z = origin_ZData[i];
                if (existsViewDirection_X)
                    entity.mViewDirection_X = viewDirection_XData[i];
                if (existsViewDirection_Y)
                    entity.mViewDirection_Y = viewDirection_YData[i];
                if (existsViewDirection_Z)
                    entity.mViewDirection_Z = viewDirection_ZData[i];
                if (existsViewPosition_X)
                    entity.mViewPosition_X = viewPosition_XData[i];
                if (existsViewPosition_Y)
                    entity.mViewPosition_Y = viewPosition_YData[i];
                if (existsViewPosition_Z)
                    entity.mViewPosition_Z = viewPosition_ZData[i];
                if (existsScale)
                    entity.mScale = scaleData[i];
                if (existsOutline_Min_X)
                    entity.mOutline_Min_X = outline_Min_XData[i];
                if (existsOutline_Min_Y)
                    entity.mOutline_Min_Y = outline_Min_YData[i];
                if (existsOutline_Max_X)
                    entity.mOutline_Max_X = outline_Max_XData[i];
                if (existsOutline_Max_Y)
                    entity.mOutline_Max_Y = outline_Max_YData[i];
                if (existsDetailLevel)
                    entity.mDetailLevel = detailLevelData[i];
                entity.mCameraIndex = existsCamera ? cameraData[i] : -1;
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                view->push_back(entity);
            }
            
            delete[] up_XData;
            delete[] up_YData;
            delete[] up_ZData;
            delete[] right_XData;
            delete[] right_YData;
            delete[] right_ZData;
            delete[] origin_XData;
            delete[] origin_YData;
            delete[] origin_ZData;
            delete[] viewDirection_XData;
            delete[] viewDirection_YData;
            delete[] viewDirection_ZData;
            delete[] viewPosition_XData;
            delete[] viewPosition_YData;
            delete[] viewPosition_ZData;
            delete[] scaleData;
            delete[] outline_Min_XData;
            delete[] outline_Min_YData;
            delete[] outline_Max_XData;
            delete[] outline_Max_YData;
            delete[] detailLevelData;
            
            return view;
        }
        
        const std::string* GetTitle(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Title")) {
                return &mStrings[mEntityTable.mStringColumns["string:Title"][viewIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllTitle()
        {
            const int count = GetCount();
            
            const std::vector<int>& titleData = mEntityTable.column_exists("string:Title") ? mEntityTable.mStringColumns["string:Title"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:ViewType")) {
                return &mStrings[mEntityTable.mStringColumns["string:ViewType"][viewIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllViewType()
        {
            const int count = GetCount();
            
            const std::vector<int>& viewTypeData = mEntityTable.column_exists("string:ViewType") ? mEntityTable.mStringColumns["string:ViewType"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[viewTypeData[i]]);
            }
            
            return result;
        }
        
        double GetUp_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Up.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Up.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllUp_X()
        {
            const int count = GetCount();
            
            double* up_XData = new double[count];
            if (mEntityTable.column_exists("double:Up.X")) {
                memcpy(up_XData, mEntityTable.mDataColumns["double:Up.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(up_XData, up_XData + count);
            
            delete[] up_XData;
            
            return result;
        }
        
        double GetUp_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Up.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Up.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllUp_Y()
        {
            const int count = GetCount();
            
            double* up_YData = new double[count];
            if (mEntityTable.column_exists("double:Up.Y")) {
                memcpy(up_YData, mEntityTable.mDataColumns["double:Up.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(up_YData, up_YData + count);
            
            delete[] up_YData;
            
            return result;
        }
        
        double GetUp_Z(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Up.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Up.Z"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllUp_Z()
        {
            const int count = GetCount();
            
            double* up_ZData = new double[count];
            if (mEntityTable.column_exists("double:Up.Z")) {
                memcpy(up_ZData, mEntityTable.mDataColumns["double:Up.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(up_ZData, up_ZData + count);
            
            delete[] up_ZData;
            
            return result;
        }
        
        double GetRight_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Right.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Right.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllRight_X()
        {
            const int count = GetCount();
            
            double* right_XData = new double[count];
            if (mEntityTable.column_exists("double:Right.X")) {
                memcpy(right_XData, mEntityTable.mDataColumns["double:Right.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(right_XData, right_XData + count);
            
            delete[] right_XData;
            
            return result;
        }
        
        double GetRight_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Right.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Right.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllRight_Y()
        {
            const int count = GetCount();
            
            double* right_YData = new double[count];
            if (mEntityTable.column_exists("double:Right.Y")) {
                memcpy(right_YData, mEntityTable.mDataColumns["double:Right.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(right_YData, right_YData + count);
            
            delete[] right_YData;
            
            return result;
        }
        
        double GetRight_Z(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Right.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Right.Z"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllRight_Z()
        {
            const int count = GetCount();
            
            double* right_ZData = new double[count];
            if (mEntityTable.column_exists("double:Right.Z")) {
                memcpy(right_ZData, mEntityTable.mDataColumns["double:Right.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(right_ZData, right_ZData + count);
            
            delete[] right_ZData;
            
            return result;
        }
        
        double GetOrigin_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Origin.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Origin.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOrigin_X()
        {
            const int count = GetCount();
            
            double* origin_XData = new double[count];
            if (mEntityTable.column_exists("double:Origin.X")) {
                memcpy(origin_XData, mEntityTable.mDataColumns["double:Origin.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(origin_XData, origin_XData + count);
            
            delete[] origin_XData;
            
            return result;
        }
        
        double GetOrigin_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Origin.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Origin.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOrigin_Y()
        {
            const int count = GetCount();
            
            double* origin_YData = new double[count];
            if (mEntityTable.column_exists("double:Origin.Y")) {
                memcpy(origin_YData, mEntityTable.mDataColumns["double:Origin.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(origin_YData, origin_YData + count);
            
            delete[] origin_YData;
            
            return result;
        }
        
        double GetOrigin_Z(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Origin.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Origin.Z"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOrigin_Z()
        {
            const int count = GetCount();
            
            double* origin_ZData = new double[count];
            if (mEntityTable.column_exists("double:Origin.Z")) {
                memcpy(origin_ZData, mEntityTable.mDataColumns["double:Origin.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(origin_ZData, origin_ZData + count);
            
            delete[] origin_ZData;
            
            return result;
        }
        
        double GetViewDirection_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewDirection.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewDirection.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewDirection_X()
        {
            const int count = GetCount();
            
            double* viewDirection_XData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.X")) {
                memcpy(viewDirection_XData, mEntityTable.mDataColumns["double:ViewDirection.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewDirection_XData, viewDirection_XData + count);
            
            delete[] viewDirection_XData;
            
            return result;
        }
        
        double GetViewDirection_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewDirection.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewDirection.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewDirection_Y()
        {
            const int count = GetCount();
            
            double* viewDirection_YData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.Y")) {
                memcpy(viewDirection_YData, mEntityTable.mDataColumns["double:ViewDirection.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewDirection_YData, viewDirection_YData + count);
            
            delete[] viewDirection_YData;
            
            return result;
        }
        
        double GetViewDirection_Z(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewDirection.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewDirection.Z"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewDirection_Z()
        {
            const int count = GetCount();
            
            double* viewDirection_ZData = new double[count];
            if (mEntityTable.column_exists("double:ViewDirection.Z")) {
                memcpy(viewDirection_ZData, mEntityTable.mDataColumns["double:ViewDirection.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewDirection_ZData, viewDirection_ZData + count);
            
            delete[] viewDirection_ZData;
            
            return result;
        }
        
        double GetViewPosition_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewPosition.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewPosition.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewPosition_X()
        {
            const int count = GetCount();
            
            double* viewPosition_XData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.X")) {
                memcpy(viewPosition_XData, mEntityTable.mDataColumns["double:ViewPosition.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewPosition_XData, viewPosition_XData + count);
            
            delete[] viewPosition_XData;
            
            return result;
        }
        
        double GetViewPosition_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewPosition.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewPosition.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewPosition_Y()
        {
            const int count = GetCount();
            
            double* viewPosition_YData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.Y")) {
                memcpy(viewPosition_YData, mEntityTable.mDataColumns["double:ViewPosition.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewPosition_YData, viewPosition_YData + count);
            
            delete[] viewPosition_YData;
            
            return result;
        }
        
        double GetViewPosition_Z(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ViewPosition.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ViewPosition.Z"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllViewPosition_Z()
        {
            const int count = GetCount();
            
            double* viewPosition_ZData = new double[count];
            if (mEntityTable.column_exists("double:ViewPosition.Z")) {
                memcpy(viewPosition_ZData, mEntityTable.mDataColumns["double:ViewPosition.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(viewPosition_ZData, viewPosition_ZData + count);
            
            delete[] viewPosition_ZData;
            
            return result;
        }
        
        double GetScale(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Scale")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Scale"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllScale()
        {
            const int count = GetCount();
            
            double* scaleData = new double[count];
            if (mEntityTable.column_exists("double:Scale")) {
                memcpy(scaleData, mEntityTable.mDataColumns["double:Scale"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(scaleData, scaleData + count);
            
            delete[] scaleData;
            
            return result;
        }
        
        double GetOutline_Min_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Outline.Min.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Outline.Min.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOutline_Min_X()
        {
            const int count = GetCount();
            
            double* outline_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Min.X")) {
                memcpy(outline_Min_XData, mEntityTable.mDataColumns["double:Outline.Min.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(outline_Min_XData, outline_Min_XData + count);
            
            delete[] outline_Min_XData;
            
            return result;
        }
        
        double GetOutline_Min_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Outline.Min.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Outline.Min.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOutline_Min_Y()
        {
            const int count = GetCount();
            
            double* outline_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Min.Y")) {
                memcpy(outline_Min_YData, mEntityTable.mDataColumns["double:Outline.Min.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(outline_Min_YData, outline_Min_YData + count);
            
            delete[] outline_Min_YData;
            
            return result;
        }
        
        double GetOutline_Max_X(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Outline.Max.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Outline.Max.X"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOutline_Max_X()
        {
            const int count = GetCount();
            
            double* outline_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Max.X")) {
                memcpy(outline_Max_XData, mEntityTable.mDataColumns["double:Outline.Max.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(outline_Max_XData, outline_Max_XData + count);
            
            delete[] outline_Max_XData;
            
            return result;
        }
        
        double GetOutline_Max_Y(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Outline.Max.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Outline.Max.Y"].begin() + viewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllOutline_Max_Y()
        {
            const int count = GetCount();
            
            double* outline_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Outline.Max.Y")) {
                memcpy(outline_Max_YData, mEntityTable.mDataColumns["double:Outline.Max.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(outline_Max_YData, outline_Max_YData + count);
            
            delete[] outline_Max_YData;
            
            return result;
        }
        
        int GetDetailLevel(int viewIndex)
        {
            if (viewIndex < 0 || viewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:DetailLevel")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:DetailLevel"].begin() + viewIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllDetailLevel()
        {
            const int count = GetCount();
            
            int* detailLevelData = new int[count];
            if (mEntityTable.column_exists("int:DetailLevel")) {
                memcpy(detailLevelData, mEntityTable.mDataColumns["int:DetailLevel"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(detailLevelData, detailLevelData + count);
            
            delete[] detailLevelData;
            
            return result;
        }
        
        int GetCameraIndex(int viewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Camera:Camera")) {
                return -1;
            }
            
            if (viewIndex < 0 || viewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Camera:Camera"][viewIndex];
        }
        
        int GetFamilyTypeIndex(int viewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.FamilyType:FamilyType")) {
                return -1;
            }
            
            if (viewIndex < 0 || viewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][viewIndex];
        }
        
        int GetElementIndex(int viewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsView = mEntityTable.column_exists("index:Vim.View:View");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<ElementInView>* elementInView = new std::vector<ElementInView>();
            elementInView->reserve(count);
            
            const std::vector<int>& viewData = mEntityTable.column_exists("index:Vim.View:View") ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.View:View")) {
                return -1;
            }
            
            if (elementInViewIndex < 0 || elementInViewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.View:View"][elementInViewIndex];
        }
        
        int GetElementIndex(int elementInViewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsShape = mEntityTable.column_exists("index:Vim.Shape:Shape");
            bool existsView = mEntityTable.column_exists("index:Vim.View:View");
            
            const int count = GetCount();
            
            std::vector<ShapeInView>* shapeInView = new std::vector<ShapeInView>();
            shapeInView->reserve(count);
            
            const std::vector<int>& shapeData = mEntityTable.column_exists("index:Vim.Shape:Shape") ? mEntityTable.mIndexColumns["index:Vim.Shape:Shape"] : std::vector<int>();
            const std::vector<int>& viewData = mEntityTable.column_exists("index:Vim.View:View") ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Shape:Shape")) {
                return -1;
            }
            
            if (shapeInViewIndex < 0 || shapeInViewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"][shapeInViewIndex];
        }
        
        int GetViewIndex(int shapeInViewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.View:View")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsAsset = mEntityTable.column_exists("index:Vim.Asset:Asset");
            bool existsView = mEntityTable.column_exists("index:Vim.View:View");
            
            const int count = GetCount();
            
            std::vector<AssetInView>* assetInView = new std::vector<AssetInView>();
            assetInView->reserve(count);
            
            const std::vector<int>& assetData = mEntityTable.column_exists("index:Vim.Asset:Asset") ? mEntityTable.mIndexColumns["index:Vim.Asset:Asset"] : std::vector<int>();
            const std::vector<int>& viewData = mEntityTable.column_exists("index:Vim.View:View") ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Asset:Asset")) {
                return -1;
            }
            
            if (assetInViewIndex < 0 || assetInViewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Asset:Asset"][assetInViewIndex];
        }
        
        int GetViewIndex(int assetInViewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.View:View")) {
                return -1;
            }
            
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
        double mExtents_Min_X;
        double mExtents_Min_Y;
        double mExtents_Min_Z;
        double mExtents_Max_X;
        double mExtents_Max_Y;
        double mExtents_Max_Z;
        
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
            return mEntityTable.get_count();
        }
        
        LevelInView* Get(int levelInViewIndex)
        {
            LevelInView* levelInView = new LevelInView();
            levelInView->mIndex = levelInViewIndex;
            levelInView->mExtents_Min_X = GetExtents_Min_X(levelInViewIndex);
            levelInView->mExtents_Min_Y = GetExtents_Min_Y(levelInViewIndex);
            levelInView->mExtents_Min_Z = GetExtents_Min_Z(levelInViewIndex);
            levelInView->mExtents_Max_X = GetExtents_Max_X(levelInViewIndex);
            levelInView->mExtents_Max_Y = GetExtents_Max_Y(levelInViewIndex);
            levelInView->mExtents_Max_Z = GetExtents_Max_Z(levelInViewIndex);
            levelInView->mLevelIndex = GetLevelIndex(levelInViewIndex);
            levelInView->mViewIndex = GetViewIndex(levelInViewIndex);
            return levelInView;
        }
        
        std::vector<LevelInView>* GetAll()
        {
            bool existsExtents_Min_X = mEntityTable.column_exists("double:Extents.Min.X");
            bool existsExtents_Min_Y = mEntityTable.column_exists("double:Extents.Min.Y");
            bool existsExtents_Min_Z = mEntityTable.column_exists("double:Extents.Min.Z");
            bool existsExtents_Max_X = mEntityTable.column_exists("double:Extents.Max.X");
            bool existsExtents_Max_Y = mEntityTable.column_exists("double:Extents.Max.Y");
            bool existsExtents_Max_Z = mEntityTable.column_exists("double:Extents.Max.Z");
            bool existsLevel = mEntityTable.column_exists("index:Vim.Level:Level");
            bool existsView = mEntityTable.column_exists("index:Vim.View:View");
            
            const int count = GetCount();
            
            std::vector<LevelInView>* levelInView = new std::vector<LevelInView>();
            levelInView->reserve(count);
            
            double* extents_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                memcpy(extents_Min_XData, mEntityTable.mDataColumns["double:Extents.Min.X"].begin(), count * sizeof(double));
            }
            
            double* extents_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                memcpy(extents_Min_YData, mEntityTable.mDataColumns["double:Extents.Min.Y"].begin(), count * sizeof(double));
            }
            
            double* extents_Min_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                memcpy(extents_Min_ZData, mEntityTable.mDataColumns["double:Extents.Min.Z"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                memcpy(extents_Max_XData, mEntityTable.mDataColumns["double:Extents.Max.X"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                memcpy(extents_Max_YData, mEntityTable.mDataColumns["double:Extents.Max.Y"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                memcpy(extents_Max_ZData, mEntityTable.mDataColumns["double:Extents.Max.Z"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& levelData = mEntityTable.column_exists("index:Vim.Level:Level") ? mEntityTable.mIndexColumns["index:Vim.Level:Level"] : std::vector<int>();
            const std::vector<int>& viewData = mEntityTable.column_exists("index:Vim.View:View") ? mEntityTable.mIndexColumns["index:Vim.View:View"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                LevelInView entity;
                entity.mIndex = i;
                if (existsExtents_Min_X)
                    entity.mExtents_Min_X = extents_Min_XData[i];
                if (existsExtents_Min_Y)
                    entity.mExtents_Min_Y = extents_Min_YData[i];
                if (existsExtents_Min_Z)
                    entity.mExtents_Min_Z = extents_Min_ZData[i];
                if (existsExtents_Max_X)
                    entity.mExtents_Max_X = extents_Max_XData[i];
                if (existsExtents_Max_Y)
                    entity.mExtents_Max_Y = extents_Max_YData[i];
                if (existsExtents_Max_Z)
                    entity.mExtents_Max_Z = extents_Max_ZData[i];
                entity.mLevelIndex = existsLevel ? levelData[i] : -1;
                entity.mViewIndex = existsView ? viewData[i] : -1;
                levelInView->push_back(entity);
            }
            
            delete[] extents_Min_XData;
            delete[] extents_Min_YData;
            delete[] extents_Min_ZData;
            delete[] extents_Max_XData;
            delete[] extents_Max_YData;
            delete[] extents_Max_ZData;
            
            return levelInView;
        }
        
        double GetExtents_Min_X(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.X"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_X()
        {
            const int count = GetCount();
            
            double* extents_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                memcpy(extents_Min_XData, mEntityTable.mDataColumns["double:Extents.Min.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_XData, extents_Min_XData + count);
            
            delete[] extents_Min_XData;
            
            return result;
        }
        
        double GetExtents_Min_Y(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.Y"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_Y()
        {
            const int count = GetCount();
            
            double* extents_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                memcpy(extents_Min_YData, mEntityTable.mDataColumns["double:Extents.Min.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_YData, extents_Min_YData + count);
            
            delete[] extents_Min_YData;
            
            return result;
        }
        
        double GetExtents_Min_Z(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.Z"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_Z()
        {
            const int count = GetCount();
            
            double* extents_Min_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                memcpy(extents_Min_ZData, mEntityTable.mDataColumns["double:Extents.Min.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_ZData, extents_Min_ZData + count);
            
            delete[] extents_Min_ZData;
            
            return result;
        }
        
        double GetExtents_Max_X(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.X"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_X()
        {
            const int count = GetCount();
            
            double* extents_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                memcpy(extents_Max_XData, mEntityTable.mDataColumns["double:Extents.Max.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_XData, extents_Max_XData + count);
            
            delete[] extents_Max_XData;
            
            return result;
        }
        
        double GetExtents_Max_Y(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.Y"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_Y()
        {
            const int count = GetCount();
            
            double* extents_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                memcpy(extents_Max_YData, mEntityTable.mDataColumns["double:Extents.Max.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_YData, extents_Max_YData + count);
            
            delete[] extents_Max_YData;
            
            return result;
        }
        
        double GetExtents_Max_Z(int levelInViewIndex)
        {
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.Z"].begin() + levelInViewIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_Z()
        {
            const int count = GetCount();
            
            double* extents_Max_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                memcpy(extents_Max_ZData, mEntityTable.mDataColumns["double:Extents.Max.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_ZData, extents_Max_ZData + count);
            
            delete[] extents_Max_ZData;
            
            return result;
        }
        
        int GetLevelIndex(int levelInViewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Level:Level")) {
                return -1;
            }
            
            if (levelInViewIndex < 0 || levelInViewIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Level:Level"][levelInViewIndex];
        }
        
        int GetViewIndex(int levelInViewIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.View:View")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsId = mEntityTable.column_exists("int:Id");
            bool existsIsPerspective = mEntityTable.column_exists("int:IsPerspective");
            bool existsVerticalExtent = mEntityTable.column_exists("double:VerticalExtent");
            bool existsHorizontalExtent = mEntityTable.column_exists("double:HorizontalExtent");
            bool existsFarDistance = mEntityTable.column_exists("double:FarDistance");
            bool existsNearDistance = mEntityTable.column_exists("double:NearDistance");
            bool existsTargetDistance = mEntityTable.column_exists("double:TargetDistance");
            bool existsRightOffset = mEntityTable.column_exists("double:RightOffset");
            bool existsUpOffset = mEntityTable.column_exists("double:UpOffset");
            
            const int count = GetCount();
            
            std::vector<Camera>* camera = new std::vector<Camera>();
            camera->reserve(count);
            
            int* idData = new int[count];
            if (mEntityTable.column_exists("int:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));
            }
            
            int* isPerspectiveData = new int[count];
            if (mEntityTable.column_exists("int:IsPerspective")) {
                memcpy(isPerspectiveData, mEntityTable.mDataColumns["int:IsPerspective"].begin(), count * sizeof(int));
            }
            
            double* verticalExtentData = new double[count];
            if (mEntityTable.column_exists("double:VerticalExtent")) {
                memcpy(verticalExtentData, mEntityTable.mDataColumns["double:VerticalExtent"].begin(), count * sizeof(double));
            }
            
            double* horizontalExtentData = new double[count];
            if (mEntityTable.column_exists("double:HorizontalExtent")) {
                memcpy(horizontalExtentData, mEntityTable.mDataColumns["double:HorizontalExtent"].begin(), count * sizeof(double));
            }
            
            double* farDistanceData = new double[count];
            if (mEntityTable.column_exists("double:FarDistance")) {
                memcpy(farDistanceData, mEntityTable.mDataColumns["double:FarDistance"].begin(), count * sizeof(double));
            }
            
            double* nearDistanceData = new double[count];
            if (mEntityTable.column_exists("double:NearDistance")) {
                memcpy(nearDistanceData, mEntityTable.mDataColumns["double:NearDistance"].begin(), count * sizeof(double));
            }
            
            double* targetDistanceData = new double[count];
            if (mEntityTable.column_exists("double:TargetDistance")) {
                memcpy(targetDistanceData, mEntityTable.mDataColumns["double:TargetDistance"].begin(), count * sizeof(double));
            }
            
            double* rightOffsetData = new double[count];
            if (mEntityTable.column_exists("double:RightOffset")) {
                memcpy(rightOffsetData, mEntityTable.mDataColumns["double:RightOffset"].begin(), count * sizeof(double));
            }
            
            double* upOffsetData = new double[count];
            if (mEntityTable.column_exists("double:UpOffset")) {
                memcpy(upOffsetData, mEntityTable.mDataColumns["double:UpOffset"].begin(), count * sizeof(double));
            }
            
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
            
            if (mEntityTable.column_exists("int:Id")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Id"].begin() + cameraIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllId()
        {
            const int count = GetCount();
            
            int* idData = new int[count];
            if (mEntityTable.column_exists("int:Id")) {
                memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(idData, idData + count);
            
            delete[] idData;
            
            return result;
        }
        
        int GetIsPerspective(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:IsPerspective")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:IsPerspective"].begin() + cameraIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllIsPerspective()
        {
            const int count = GetCount();
            
            int* isPerspectiveData = new int[count];
            if (mEntityTable.column_exists("int:IsPerspective")) {
                memcpy(isPerspectiveData, mEntityTable.mDataColumns["int:IsPerspective"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(isPerspectiveData, isPerspectiveData + count);
            
            delete[] isPerspectiveData;
            
            return result;
        }
        
        double GetVerticalExtent(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:VerticalExtent")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:VerticalExtent"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllVerticalExtent()
        {
            const int count = GetCount();
            
            double* verticalExtentData = new double[count];
            if (mEntityTable.column_exists("double:VerticalExtent")) {
                memcpy(verticalExtentData, mEntityTable.mDataColumns["double:VerticalExtent"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(verticalExtentData, verticalExtentData + count);
            
            delete[] verticalExtentData;
            
            return result;
        }
        
        double GetHorizontalExtent(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:HorizontalExtent")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:HorizontalExtent"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllHorizontalExtent()
        {
            const int count = GetCount();
            
            double* horizontalExtentData = new double[count];
            if (mEntityTable.column_exists("double:HorizontalExtent")) {
                memcpy(horizontalExtentData, mEntityTable.mDataColumns["double:HorizontalExtent"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(horizontalExtentData, horizontalExtentData + count);
            
            delete[] horizontalExtentData;
            
            return result;
        }
        
        double GetFarDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:FarDistance")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:FarDistance"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllFarDistance()
        {
            const int count = GetCount();
            
            double* farDistanceData = new double[count];
            if (mEntityTable.column_exists("double:FarDistance")) {
                memcpy(farDistanceData, mEntityTable.mDataColumns["double:FarDistance"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(farDistanceData, farDistanceData + count);
            
            delete[] farDistanceData;
            
            return result;
        }
        
        double GetNearDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NearDistance")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NearDistance"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNearDistance()
        {
            const int count = GetCount();
            
            double* nearDistanceData = new double[count];
            if (mEntityTable.column_exists("double:NearDistance")) {
                memcpy(nearDistanceData, mEntityTable.mDataColumns["double:NearDistance"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(nearDistanceData, nearDistanceData + count);
            
            delete[] nearDistanceData;
            
            return result;
        }
        
        double GetTargetDistance(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:TargetDistance")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:TargetDistance"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllTargetDistance()
        {
            const int count = GetCount();
            
            double* targetDistanceData = new double[count];
            if (mEntityTable.column_exists("double:TargetDistance")) {
                memcpy(targetDistanceData, mEntityTable.mDataColumns["double:TargetDistance"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(targetDistanceData, targetDistanceData + count);
            
            delete[] targetDistanceData;
            
            return result;
        }
        
        double GetRightOffset(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:RightOffset")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:RightOffset"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllRightOffset()
        {
            const int count = GetCount();
            
            double* rightOffsetData = new double[count];
            if (mEntityTable.column_exists("double:RightOffset")) {
                memcpy(rightOffsetData, mEntityTable.mDataColumns["double:RightOffset"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(rightOffsetData, rightOffsetData + count);
            
            delete[] rightOffsetData;
            
            return result;
        }
        
        double GetUpOffset(int cameraIndex)
        {
            if (cameraIndex < 0 || cameraIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:UpOffset")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:UpOffset"].begin() + cameraIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllUpOffset()
        {
            const int count = GetCount();
            
            double* upOffsetData = new double[count];
            if (mEntityTable.column_exists("double:UpOffset")) {
                memcpy(upOffsetData, mEntityTable.mDataColumns["double:UpOffset"].begin(), count * sizeof(double));
            }
            
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
        double mColor_X;
        double mColor_Y;
        double mColor_Z;
        double mColorUvScaling_X;
        double mColorUvScaling_Y;
        double mColorUvOffset_X;
        double mColorUvOffset_Y;
        double mNormalUvScaling_X;
        double mNormalUvScaling_Y;
        double mNormalUvOffset_X;
        double mNormalUvOffset_Y;
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
            return mEntityTable.get_count();
        }
        
        Material* Get(int materialIndex)
        {
            Material* material = new Material();
            material->mIndex = materialIndex;
            material->mName = GetName(materialIndex);
            material->mMaterialCategory = GetMaterialCategory(materialIndex);
            material->mColor_X = GetColor_X(materialIndex);
            material->mColor_Y = GetColor_Y(materialIndex);
            material->mColor_Z = GetColor_Z(materialIndex);
            material->mColorUvScaling_X = GetColorUvScaling_X(materialIndex);
            material->mColorUvScaling_Y = GetColorUvScaling_Y(materialIndex);
            material->mColorUvOffset_X = GetColorUvOffset_X(materialIndex);
            material->mColorUvOffset_Y = GetColorUvOffset_Y(materialIndex);
            material->mNormalUvScaling_X = GetNormalUvScaling_X(materialIndex);
            material->mNormalUvScaling_Y = GetNormalUvScaling_Y(materialIndex);
            material->mNormalUvOffset_X = GetNormalUvOffset_X(materialIndex);
            material->mNormalUvOffset_Y = GetNormalUvOffset_Y(materialIndex);
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
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsMaterialCategory = mEntityTable.column_exists("string:MaterialCategory");
            bool existsColor_X = mEntityTable.column_exists("double:Color.X");
            bool existsColor_Y = mEntityTable.column_exists("double:Color.Y");
            bool existsColor_Z = mEntityTable.column_exists("double:Color.Z");
            bool existsColorUvScaling_X = mEntityTable.column_exists("double:ColorUvScaling.X");
            bool existsColorUvScaling_Y = mEntityTable.column_exists("double:ColorUvScaling.Y");
            bool existsColorUvOffset_X = mEntityTable.column_exists("double:ColorUvOffset.X");
            bool existsColorUvOffset_Y = mEntityTable.column_exists("double:ColorUvOffset.Y");
            bool existsNormalUvScaling_X = mEntityTable.column_exists("double:NormalUvScaling.X");
            bool existsNormalUvScaling_Y = mEntityTable.column_exists("double:NormalUvScaling.Y");
            bool existsNormalUvOffset_X = mEntityTable.column_exists("double:NormalUvOffset.X");
            bool existsNormalUvOffset_Y = mEntityTable.column_exists("double:NormalUvOffset.Y");
            bool existsNormalAmount = mEntityTable.column_exists("double:NormalAmount");
            bool existsGlossiness = mEntityTable.column_exists("double:Glossiness");
            bool existsSmoothness = mEntityTable.column_exists("double:Smoothness");
            bool existsTransparency = mEntityTable.column_exists("double:Transparency");
            bool existsColorTextureFile = mEntityTable.column_exists("index:Vim.Asset:ColorTextureFile");
            bool existsNormalTextureFile = mEntityTable.column_exists("index:Vim.Asset:NormalTextureFile");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Material>* material = new std::vector<Material>();
            material->reserve(count);
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            const std::vector<int>& materialCategoryData = mEntityTable.column_exists("string:MaterialCategory") ? mEntityTable.mStringColumns["string:MaterialCategory"] : std::vector<int>();
            
            double* color_XData = new double[count];
            if (mEntityTable.column_exists("double:Color.X")) {
                memcpy(color_XData, mEntityTable.mDataColumns["double:Color.X"].begin(), count * sizeof(double));
            }
            
            double* color_YData = new double[count];
            if (mEntityTable.column_exists("double:Color.Y")) {
                memcpy(color_YData, mEntityTable.mDataColumns["double:Color.Y"].begin(), count * sizeof(double));
            }
            
            double* color_ZData = new double[count];
            if (mEntityTable.column_exists("double:Color.Z")) {
                memcpy(color_ZData, mEntityTable.mDataColumns["double:Color.Z"].begin(), count * sizeof(double));
            }
            
            double* colorUvScaling_XData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvScaling.X")) {
                memcpy(colorUvScaling_XData, mEntityTable.mDataColumns["double:ColorUvScaling.X"].begin(), count * sizeof(double));
            }
            
            double* colorUvScaling_YData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvScaling.Y")) {
                memcpy(colorUvScaling_YData, mEntityTable.mDataColumns["double:ColorUvScaling.Y"].begin(), count * sizeof(double));
            }
            
            double* colorUvOffset_XData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvOffset.X")) {
                memcpy(colorUvOffset_XData, mEntityTable.mDataColumns["double:ColorUvOffset.X"].begin(), count * sizeof(double));
            }
            
            double* colorUvOffset_YData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvOffset.Y")) {
                memcpy(colorUvOffset_YData, mEntityTable.mDataColumns["double:ColorUvOffset.Y"].begin(), count * sizeof(double));
            }
            
            double* normalUvScaling_XData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvScaling.X")) {
                memcpy(normalUvScaling_XData, mEntityTable.mDataColumns["double:NormalUvScaling.X"].begin(), count * sizeof(double));
            }
            
            double* normalUvScaling_YData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvScaling.Y")) {
                memcpy(normalUvScaling_YData, mEntityTable.mDataColumns["double:NormalUvScaling.Y"].begin(), count * sizeof(double));
            }
            
            double* normalUvOffset_XData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvOffset.X")) {
                memcpy(normalUvOffset_XData, mEntityTable.mDataColumns["double:NormalUvOffset.X"].begin(), count * sizeof(double));
            }
            
            double* normalUvOffset_YData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvOffset.Y")) {
                memcpy(normalUvOffset_YData, mEntityTable.mDataColumns["double:NormalUvOffset.Y"].begin(), count * sizeof(double));
            }
            
            double* normalAmountData = new double[count];
            if (mEntityTable.column_exists("double:NormalAmount")) {
                memcpy(normalAmountData, mEntityTable.mDataColumns["double:NormalAmount"].begin(), count * sizeof(double));
            }
            
            double* glossinessData = new double[count];
            if (mEntityTable.column_exists("double:Glossiness")) {
                memcpy(glossinessData, mEntityTable.mDataColumns["double:Glossiness"].begin(), count * sizeof(double));
            }
            
            double* smoothnessData = new double[count];
            if (mEntityTable.column_exists("double:Smoothness")) {
                memcpy(smoothnessData, mEntityTable.mDataColumns["double:Smoothness"].begin(), count * sizeof(double));
            }
            
            double* transparencyData = new double[count];
            if (mEntityTable.column_exists("double:Transparency")) {
                memcpy(transparencyData, mEntityTable.mDataColumns["double:Transparency"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& colorTextureFileData = mEntityTable.column_exists("index:Vim.Asset:ColorTextureFile") ? mEntityTable.mIndexColumns["index:Vim.Asset:ColorTextureFile"] : std::vector<int>();
            const std::vector<int>& normalTextureFileData = mEntityTable.column_exists("index:Vim.Asset:NormalTextureFile") ? mEntityTable.mIndexColumns["index:Vim.Asset:NormalTextureFile"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                Material entity;
                entity.mIndex = i;
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsMaterialCategory)
                    entity.mMaterialCategory = &mStrings[materialCategoryData[i]];
                if (existsColor_X)
                    entity.mColor_X = color_XData[i];
                if (existsColor_Y)
                    entity.mColor_Y = color_YData[i];
                if (existsColor_Z)
                    entity.mColor_Z = color_ZData[i];
                if (existsColorUvScaling_X)
                    entity.mColorUvScaling_X = colorUvScaling_XData[i];
                if (existsColorUvScaling_Y)
                    entity.mColorUvScaling_Y = colorUvScaling_YData[i];
                if (existsColorUvOffset_X)
                    entity.mColorUvOffset_X = colorUvOffset_XData[i];
                if (existsColorUvOffset_Y)
                    entity.mColorUvOffset_Y = colorUvOffset_YData[i];
                if (existsNormalUvScaling_X)
                    entity.mNormalUvScaling_X = normalUvScaling_XData[i];
                if (existsNormalUvScaling_Y)
                    entity.mNormalUvScaling_Y = normalUvScaling_YData[i];
                if (existsNormalUvOffset_X)
                    entity.mNormalUvOffset_X = normalUvOffset_XData[i];
                if (existsNormalUvOffset_Y)
                    entity.mNormalUvOffset_Y = normalUvOffset_YData[i];
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
            
            delete[] color_XData;
            delete[] color_YData;
            delete[] color_ZData;
            delete[] colorUvScaling_XData;
            delete[] colorUvScaling_YData;
            delete[] colorUvOffset_XData;
            delete[] colorUvOffset_YData;
            delete[] normalUvScaling_XData;
            delete[] normalUvScaling_YData;
            delete[] normalUvOffset_XData;
            delete[] normalUvOffset_YData;
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
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][materialIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:MaterialCategory")) {
                return &mStrings[mEntityTable.mStringColumns["string:MaterialCategory"][materialIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllMaterialCategory()
        {
            const int count = GetCount();
            
            const std::vector<int>& materialCategoryData = mEntityTable.column_exists("string:MaterialCategory") ? mEntityTable.mStringColumns["string:MaterialCategory"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[materialCategoryData[i]]);
            }
            
            return result;
        }
        
        double GetColor_X(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Color.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Color.X"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColor_X()
        {
            const int count = GetCount();
            
            double* color_XData = new double[count];
            if (mEntityTable.column_exists("double:Color.X")) {
                memcpy(color_XData, mEntityTable.mDataColumns["double:Color.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(color_XData, color_XData + count);
            
            delete[] color_XData;
            
            return result;
        }
        
        double GetColor_Y(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Color.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Color.Y"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColor_Y()
        {
            const int count = GetCount();
            
            double* color_YData = new double[count];
            if (mEntityTable.column_exists("double:Color.Y")) {
                memcpy(color_YData, mEntityTable.mDataColumns["double:Color.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(color_YData, color_YData + count);
            
            delete[] color_YData;
            
            return result;
        }
        
        double GetColor_Z(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Color.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Color.Z"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColor_Z()
        {
            const int count = GetCount();
            
            double* color_ZData = new double[count];
            if (mEntityTable.column_exists("double:Color.Z")) {
                memcpy(color_ZData, mEntityTable.mDataColumns["double:Color.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(color_ZData, color_ZData + count);
            
            delete[] color_ZData;
            
            return result;
        }
        
        double GetColorUvScaling_X(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ColorUvScaling.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ColorUvScaling.X"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColorUvScaling_X()
        {
            const int count = GetCount();
            
            double* colorUvScaling_XData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvScaling.X")) {
                memcpy(colorUvScaling_XData, mEntityTable.mDataColumns["double:ColorUvScaling.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(colorUvScaling_XData, colorUvScaling_XData + count);
            
            delete[] colorUvScaling_XData;
            
            return result;
        }
        
        double GetColorUvScaling_Y(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ColorUvScaling.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ColorUvScaling.Y"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColorUvScaling_Y()
        {
            const int count = GetCount();
            
            double* colorUvScaling_YData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvScaling.Y")) {
                memcpy(colorUvScaling_YData, mEntityTable.mDataColumns["double:ColorUvScaling.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(colorUvScaling_YData, colorUvScaling_YData + count);
            
            delete[] colorUvScaling_YData;
            
            return result;
        }
        
        double GetColorUvOffset_X(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ColorUvOffset.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ColorUvOffset.X"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColorUvOffset_X()
        {
            const int count = GetCount();
            
            double* colorUvOffset_XData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvOffset.X")) {
                memcpy(colorUvOffset_XData, mEntityTable.mDataColumns["double:ColorUvOffset.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(colorUvOffset_XData, colorUvOffset_XData + count);
            
            delete[] colorUvOffset_XData;
            
            return result;
        }
        
        double GetColorUvOffset_Y(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:ColorUvOffset.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:ColorUvOffset.Y"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllColorUvOffset_Y()
        {
            const int count = GetCount();
            
            double* colorUvOffset_YData = new double[count];
            if (mEntityTable.column_exists("double:ColorUvOffset.Y")) {
                memcpy(colorUvOffset_YData, mEntityTable.mDataColumns["double:ColorUvOffset.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(colorUvOffset_YData, colorUvOffset_YData + count);
            
            delete[] colorUvOffset_YData;
            
            return result;
        }
        
        double GetNormalUvScaling_X(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NormalUvScaling.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalUvScaling.X"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNormalUvScaling_X()
        {
            const int count = GetCount();
            
            double* normalUvScaling_XData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvScaling.X")) {
                memcpy(normalUvScaling_XData, mEntityTable.mDataColumns["double:NormalUvScaling.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(normalUvScaling_XData, normalUvScaling_XData + count);
            
            delete[] normalUvScaling_XData;
            
            return result;
        }
        
        double GetNormalUvScaling_Y(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NormalUvScaling.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalUvScaling.Y"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNormalUvScaling_Y()
        {
            const int count = GetCount();
            
            double* normalUvScaling_YData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvScaling.Y")) {
                memcpy(normalUvScaling_YData, mEntityTable.mDataColumns["double:NormalUvScaling.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(normalUvScaling_YData, normalUvScaling_YData + count);
            
            delete[] normalUvScaling_YData;
            
            return result;
        }
        
        double GetNormalUvOffset_X(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NormalUvOffset.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalUvOffset.X"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNormalUvOffset_X()
        {
            const int count = GetCount();
            
            double* normalUvOffset_XData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvOffset.X")) {
                memcpy(normalUvOffset_XData, mEntityTable.mDataColumns["double:NormalUvOffset.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(normalUvOffset_XData, normalUvOffset_XData + count);
            
            delete[] normalUvOffset_XData;
            
            return result;
        }
        
        double GetNormalUvOffset_Y(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NormalUvOffset.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalUvOffset.Y"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNormalUvOffset_Y()
        {
            const int count = GetCount();
            
            double* normalUvOffset_YData = new double[count];
            if (mEntityTable.column_exists("double:NormalUvOffset.Y")) {
                memcpy(normalUvOffset_YData, mEntityTable.mDataColumns["double:NormalUvOffset.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(normalUvOffset_YData, normalUvOffset_YData + count);
            
            delete[] normalUvOffset_YData;
            
            return result;
        }
        
        double GetNormalAmount(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:NormalAmount")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:NormalAmount"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllNormalAmount()
        {
            const int count = GetCount();
            
            double* normalAmountData = new double[count];
            if (mEntityTable.column_exists("double:NormalAmount")) {
                memcpy(normalAmountData, mEntityTable.mDataColumns["double:NormalAmount"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(normalAmountData, normalAmountData + count);
            
            delete[] normalAmountData;
            
            return result;
        }
        
        double GetGlossiness(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Glossiness")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Glossiness"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllGlossiness()
        {
            const int count = GetCount();
            
            double* glossinessData = new double[count];
            if (mEntityTable.column_exists("double:Glossiness")) {
                memcpy(glossinessData, mEntityTable.mDataColumns["double:Glossiness"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(glossinessData, glossinessData + count);
            
            delete[] glossinessData;
            
            return result;
        }
        
        double GetSmoothness(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Smoothness")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Smoothness"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllSmoothness()
        {
            const int count = GetCount();
            
            double* smoothnessData = new double[count];
            if (mEntityTable.column_exists("double:Smoothness")) {
                memcpy(smoothnessData, mEntityTable.mDataColumns["double:Smoothness"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(smoothnessData, smoothnessData + count);
            
            delete[] smoothnessData;
            
            return result;
        }
        
        double GetTransparency(int materialIndex)
        {
            if (materialIndex < 0 || materialIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Transparency")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Transparency"].begin() + materialIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllTransparency()
        {
            const int count = GetCount();
            
            double* transparencyData = new double[count];
            if (mEntityTable.column_exists("double:Transparency")) {
                memcpy(transparencyData, mEntityTable.mDataColumns["double:Transparency"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(transparencyData, transparencyData + count);
            
            delete[] transparencyData;
            
            return result;
        }
        
        int GetColorTextureFileIndex(int materialIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Asset:ColorTextureFile")) {
                return -1;
            }
            
            if (materialIndex < 0 || materialIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Asset:ColorTextureFile"][materialIndex];
        }
        
        int GetNormalTextureFileIndex(int materialIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Asset:NormalTextureFile")) {
                return -1;
            }
            
            if (materialIndex < 0 || materialIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Asset:NormalTextureFile"][materialIndex];
        }
        
        int GetElementIndex(int materialIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsArea = mEntityTable.column_exists("double:Area");
            bool existsVolume = mEntityTable.column_exists("double:Volume");
            bool existsIsPaint = mEntityTable.column_exists("byte:IsPaint");
            bool existsMaterial = mEntityTable.column_exists("index:Vim.Material:Material");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<MaterialInElement>* materialInElement = new std::vector<MaterialInElement>();
            materialInElement->reserve(count);
            
            double* areaData = new double[count];
            if (mEntityTable.column_exists("double:Area")) {
                memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));
            }
            
            double* volumeData = new double[count];
            if (mEntityTable.column_exists("double:Volume")) {
                memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));
            }
            
            bfast::byte* isPaintData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPaint")) {
                memcpy(isPaintData, mEntityTable.mDataColumns["byte:IsPaint"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& materialData = mEntityTable.column_exists("index:Vim.Material:Material") ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:Area")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Area"].begin() + materialInElementIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllArea()
        {
            const int count = GetCount();
            
            double* areaData = new double[count];
            if (mEntityTable.column_exists("double:Area")) {
                memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(areaData, areaData + count);
            
            delete[] areaData;
            
            return result;
        }
        
        double GetVolume(int materialInElementIndex)
        {
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Volume")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Volume"].begin() + materialInElementIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllVolume()
        {
            const int count = GetCount();
            
            double* volumeData = new double[count];
            if (mEntityTable.column_exists("double:Volume")) {
                memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(volumeData, volumeData + count);
            
            delete[] volumeData;
            
            return result;
        }
        
        bool GetIsPaint(int materialInElementIndex)
        {
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsPaint")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsPaint"].begin() + materialInElementIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsPaint()
        {
            const int count = GetCount();
            
            bfast::byte* isPaintData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsPaint")) {
                memcpy(isPaintData, mEntityTable.mDataColumns["byte:IsPaint"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isPaintData, isPaintData + count);
            
            delete[] isPaintData;
            
            return result;
        }
        
        int GetMaterialIndex(int materialInElementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Material:Material")) {
                return -1;
            }
            
            if (materialInElementIndex < 0 || materialInElementIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Material:Material"][materialInElementIndex];
        }
        
        int GetElementIndex(int materialInElementIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsOrderIndex = mEntityTable.column_exists("int:OrderIndex");
            bool existsWidth = mEntityTable.column_exists("double:Width");
            bool existsMaterialFunctionAssignment = mEntityTable.column_exists("string:MaterialFunctionAssignment");
            bool existsMaterial = mEntityTable.column_exists("index:Vim.Material:Material");
            bool existsCompoundStructure = mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure");
            
            const int count = GetCount();
            
            std::vector<CompoundStructureLayer>* compoundStructureLayer = new std::vector<CompoundStructureLayer>();
            compoundStructureLayer->reserve(count);
            
            int* orderIndexData = new int[count];
            if (mEntityTable.column_exists("int:OrderIndex")) {
                memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));
            }
            
            double* widthData = new double[count];
            if (mEntityTable.column_exists("double:Width")) {
                memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& materialFunctionAssignmentData = mEntityTable.column_exists("string:MaterialFunctionAssignment") ? mEntityTable.mStringColumns["string:MaterialFunctionAssignment"] : std::vector<int>();
            
            const std::vector<int>& materialData = mEntityTable.column_exists("index:Vim.Material:Material") ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : std::vector<int>();
            const std::vector<int>& compoundStructureData = mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure") ? mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:OrderIndex")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:OrderIndex"].begin() + compoundStructureLayerIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllOrderIndex()
        {
            const int count = GetCount();
            
            int* orderIndexData = new int[count];
            if (mEntityTable.column_exists("int:OrderIndex")) {
                memcpy(orderIndexData, mEntityTable.mDataColumns["int:OrderIndex"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(orderIndexData, orderIndexData + count);
            
            delete[] orderIndexData;
            
            return result;
        }
        
        double GetWidth(int compoundStructureLayerIndex)
        {
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Width")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Width"].begin() + compoundStructureLayerIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllWidth()
        {
            const int count = GetCount();
            
            double* widthData = new double[count];
            if (mEntityTable.column_exists("double:Width")) {
                memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(widthData, widthData + count);
            
            delete[] widthData;
            
            return result;
        }
        
        const std::string* GetMaterialFunctionAssignment(int compoundStructureLayerIndex)
        {
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:MaterialFunctionAssignment")) {
                return &mStrings[mEntityTable.mStringColumns["string:MaterialFunctionAssignment"][compoundStructureLayerIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllMaterialFunctionAssignment()
        {
            const int count = GetCount();
            
            const std::vector<int>& materialFunctionAssignmentData = mEntityTable.column_exists("string:MaterialFunctionAssignment") ? mEntityTable.mStringColumns["string:MaterialFunctionAssignment"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Material:Material")) {
                return -1;
            }
            
            if (compoundStructureLayerIndex < 0 || compoundStructureLayerIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Material:Material"][compoundStructureLayerIndex];
        }
        
        int GetCompoundStructureIndex(int compoundStructureLayerIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.CompoundStructure:CompoundStructure")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsWidth = mEntityTable.column_exists("double:Width");
            bool existsStructuralLayer = mEntityTable.column_exists("index:Vim.CompoundStructureLayer:StructuralLayer");
            
            const int count = GetCount();
            
            std::vector<CompoundStructure>* compoundStructure = new std::vector<CompoundStructure>();
            compoundStructure->reserve(count);
            
            double* widthData = new double[count];
            if (mEntityTable.column_exists("double:Width")) {
                memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& structuralLayerData = mEntityTable.column_exists("index:Vim.CompoundStructureLayer:StructuralLayer") ? mEntityTable.mIndexColumns["index:Vim.CompoundStructureLayer:StructuralLayer"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:Width")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Width"].begin() + compoundStructureIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllWidth()
        {
            const int count = GetCount();
            
            double* widthData = new double[count];
            if (mEntityTable.column_exists("double:Width")) {
                memcpy(widthData, mEntityTable.mDataColumns["double:Width"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(widthData, widthData + count);
            
            delete[] widthData;
            
            return result;
        }
        
        int GetStructuralLayerIndex(int compoundStructureIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.CompoundStructureLayer:StructuralLayer")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Node>* node = new std::vector<Node>();
            node->reserve(count);
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        float mBox_Min_X;
        float mBox_Min_Y;
        float mBox_Min_Z;
        float mBox_Max_X;
        float mBox_Max_Y;
        float mBox_Max_Z;
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
            return mEntityTable.get_count();
        }
        
        Geometry* Get(int geometryIndex)
        {
            Geometry* geometry = new Geometry();
            geometry->mIndex = geometryIndex;
            geometry->mBox_Min_X = GetBox_Min_X(geometryIndex);
            geometry->mBox_Min_Y = GetBox_Min_Y(geometryIndex);
            geometry->mBox_Min_Z = GetBox_Min_Z(geometryIndex);
            geometry->mBox_Max_X = GetBox_Max_X(geometryIndex);
            geometry->mBox_Max_Y = GetBox_Max_Y(geometryIndex);
            geometry->mBox_Max_Z = GetBox_Max_Z(geometryIndex);
            geometry->mVertexCount = GetVertexCount(geometryIndex);
            geometry->mFaceCount = GetFaceCount(geometryIndex);
            return geometry;
        }
        
        std::vector<Geometry>* GetAll()
        {
            bool existsBox_Min_X = mEntityTable.column_exists("float:Box.Min.X");
            bool existsBox_Min_Y = mEntityTable.column_exists("float:Box.Min.Y");
            bool existsBox_Min_Z = mEntityTable.column_exists("float:Box.Min.Z");
            bool existsBox_Max_X = mEntityTable.column_exists("float:Box.Max.X");
            bool existsBox_Max_Y = mEntityTable.column_exists("float:Box.Max.Y");
            bool existsBox_Max_Z = mEntityTable.column_exists("float:Box.Max.Z");
            bool existsVertexCount = mEntityTable.column_exists("int:VertexCount");
            bool existsFaceCount = mEntityTable.column_exists("int:FaceCount");
            
            const int count = GetCount();
            
            std::vector<Geometry>* geometry = new std::vector<Geometry>();
            geometry->reserve(count);
            
            float* box_Min_XData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.X")) {
                memcpy(box_Min_XData, mEntityTable.mDataColumns["float:Box.Min.X"].begin(), count * sizeof(float));
            }
            
            float* box_Min_YData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.Y")) {
                memcpy(box_Min_YData, mEntityTable.mDataColumns["float:Box.Min.Y"].begin(), count * sizeof(float));
            }
            
            float* box_Min_ZData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.Z")) {
                memcpy(box_Min_ZData, mEntityTable.mDataColumns["float:Box.Min.Z"].begin(), count * sizeof(float));
            }
            
            float* box_Max_XData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.X")) {
                memcpy(box_Max_XData, mEntityTable.mDataColumns["float:Box.Max.X"].begin(), count * sizeof(float));
            }
            
            float* box_Max_YData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.Y")) {
                memcpy(box_Max_YData, mEntityTable.mDataColumns["float:Box.Max.Y"].begin(), count * sizeof(float));
            }
            
            float* box_Max_ZData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.Z")) {
                memcpy(box_Max_ZData, mEntityTable.mDataColumns["float:Box.Max.Z"].begin(), count * sizeof(float));
            }
            
            int* vertexCountData = new int[count];
            if (mEntityTable.column_exists("int:VertexCount")) {
                memcpy(vertexCountData, mEntityTable.mDataColumns["int:VertexCount"].begin(), count * sizeof(int));
            }
            
            int* faceCountData = new int[count];
            if (mEntityTable.column_exists("int:FaceCount")) {
                memcpy(faceCountData, mEntityTable.mDataColumns["int:FaceCount"].begin(), count * sizeof(int));
            }
            
            for (int i = 0; i < count; ++i)
            {
                Geometry entity;
                entity.mIndex = i;
                if (existsBox_Min_X)
                    entity.mBox_Min_X = box_Min_XData[i];
                if (existsBox_Min_Y)
                    entity.mBox_Min_Y = box_Min_YData[i];
                if (existsBox_Min_Z)
                    entity.mBox_Min_Z = box_Min_ZData[i];
                if (existsBox_Max_X)
                    entity.mBox_Max_X = box_Max_XData[i];
                if (existsBox_Max_Y)
                    entity.mBox_Max_Y = box_Max_YData[i];
                if (existsBox_Max_Z)
                    entity.mBox_Max_Z = box_Max_ZData[i];
                if (existsVertexCount)
                    entity.mVertexCount = vertexCountData[i];
                if (existsFaceCount)
                    entity.mFaceCount = faceCountData[i];
                geometry->push_back(entity);
            }
            
            delete[] box_Min_XData;
            delete[] box_Min_YData;
            delete[] box_Min_ZData;
            delete[] box_Max_XData;
            delete[] box_Max_YData;
            delete[] box_Max_ZData;
            delete[] vertexCountData;
            delete[] faceCountData;
            
            return geometry;
        }
        
        float GetBox_Min_X(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Min.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Min.X"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Min_X()
        {
            const int count = GetCount();
            
            float* box_Min_XData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.X")) {
                memcpy(box_Min_XData, mEntityTable.mDataColumns["float:Box.Min.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Min_XData, box_Min_XData + count);
            
            delete[] box_Min_XData;
            
            return result;
        }
        
        float GetBox_Min_Y(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Min.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Min.Y"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Min_Y()
        {
            const int count = GetCount();
            
            float* box_Min_YData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.Y")) {
                memcpy(box_Min_YData, mEntityTable.mDataColumns["float:Box.Min.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Min_YData, box_Min_YData + count);
            
            delete[] box_Min_YData;
            
            return result;
        }
        
        float GetBox_Min_Z(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Min.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Min.Z"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Min_Z()
        {
            const int count = GetCount();
            
            float* box_Min_ZData = new float[count];
            if (mEntityTable.column_exists("float:Box.Min.Z")) {
                memcpy(box_Min_ZData, mEntityTable.mDataColumns["float:Box.Min.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Min_ZData, box_Min_ZData + count);
            
            delete[] box_Min_ZData;
            
            return result;
        }
        
        float GetBox_Max_X(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Max.X")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Max.X"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Max_X()
        {
            const int count = GetCount();
            
            float* box_Max_XData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.X")) {
                memcpy(box_Max_XData, mEntityTable.mDataColumns["float:Box.Max.X"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Max_XData, box_Max_XData + count);
            
            delete[] box_Max_XData;
            
            return result;
        }
        
        float GetBox_Max_Y(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Max.Y")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Max.Y"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Max_Y()
        {
            const int count = GetCount();
            
            float* box_Max_YData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.Y")) {
                memcpy(box_Max_YData, mEntityTable.mDataColumns["float:Box.Max.Y"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Max_YData, box_Max_YData + count);
            
            delete[] box_Max_YData;
            
            return result;
        }
        
        float GetBox_Max_Z(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("float:Box.Max.Z")) {
                return *reinterpret_cast<float*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["float:Box.Max.Z"].begin() + geometryIndex * sizeof(float)));
            }
            
            return {};
        }
        
        const std::vector<float>* GetAllBox_Max_Z()
        {
            const int count = GetCount();
            
            float* box_Max_ZData = new float[count];
            if (mEntityTable.column_exists("float:Box.Max.Z")) {
                memcpy(box_Max_ZData, mEntityTable.mDataColumns["float:Box.Max.Z"].begin(), count * sizeof(float));
            }
            
            std::vector<float>* result = new std::vector<float>(box_Max_ZData, box_Max_ZData + count);
            
            delete[] box_Max_ZData;
            
            return result;
        }
        
        int GetVertexCount(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:VertexCount")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:VertexCount"].begin() + geometryIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllVertexCount()
        {
            const int count = GetCount();
            
            int* vertexCountData = new int[count];
            if (mEntityTable.column_exists("int:VertexCount")) {
                memcpy(vertexCountData, mEntityTable.mDataColumns["int:VertexCount"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(vertexCountData, vertexCountData + count);
            
            delete[] vertexCountData;
            
            return result;
        }
        
        int GetFaceCount(int geometryIndex)
        {
            if (geometryIndex < 0 || geometryIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:FaceCount")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:FaceCount"].begin() + geometryIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllFaceCount()
        {
            const int count = GetCount();
            
            int* faceCountData = new int[count];
            if (mEntityTable.column_exists("int:FaceCount")) {
                memcpy(faceCountData, mEntityTable.mDataColumns["int:FaceCount"].begin(), count * sizeof(int));
            }
            
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
            return mEntityTable.get_count();
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
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Shape>* shape = new std::vector<Shape>();
            shape->reserve(count);
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<ShapeCollection>* shapeCollection = new std::vector<ShapeCollection>();
            shapeCollection->reserve(count);
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsShape = mEntityTable.column_exists("index:Vim.Shape:Shape");
            bool existsShapeCollection = mEntityTable.column_exists("index:Vim.ShapeCollection:ShapeCollection");
            
            const int count = GetCount();
            
            std::vector<ShapeInShapeCollection>* shapeInShapeCollection = new std::vector<ShapeInShapeCollection>();
            shapeInShapeCollection->reserve(count);
            
            const std::vector<int>& shapeData = mEntityTable.column_exists("index:Vim.Shape:Shape") ? mEntityTable.mIndexColumns["index:Vim.Shape:Shape"] : std::vector<int>();
            const std::vector<int>& shapeCollectionData = mEntityTable.column_exists("index:Vim.ShapeCollection:ShapeCollection") ? mEntityTable.mIndexColumns["index:Vim.ShapeCollection:ShapeCollection"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Shape:Shape")) {
                return -1;
            }
            
            if (shapeInShapeCollectionIndex < 0 || shapeInShapeCollectionIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Shape:Shape"][shapeInShapeCollectionIndex];
        }
        
        int GetShapeCollectionIndex(int shapeInShapeCollectionIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.ShapeCollection:ShapeCollection")) {
                return -1;
            }
            
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
        
        int mFamilyTypeIndex;
        FamilyType* mFamilyType;
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
            return mEntityTable.get_count();
        }
        
        System* Get(int systemIndex)
        {
            System* system = new System();
            system->mIndex = systemIndex;
            system->mSystemType = GetSystemType(systemIndex);
            system->mFamilyTypeIndex = GetFamilyTypeIndex(systemIndex);
            system->mElementIndex = GetElementIndex(systemIndex);
            return system;
        }
        
        std::vector<System>* GetAll()
        {
            bool existsSystemType = mEntityTable.column_exists("int:SystemType");
            bool existsFamilyType = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<System>* system = new std::vector<System>();
            system->reserve(count);
            
            int* systemTypeData = new int[count];
            if (mEntityTable.column_exists("int:SystemType")) {
                memcpy(systemTypeData, mEntityTable.mDataColumns["int:SystemType"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& familyTypeData = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType") ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                System entity;
                entity.mIndex = i;
                if (existsSystemType)
                    entity.mSystemType = systemTypeData[i];
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
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
            
            if (mEntityTable.column_exists("int:SystemType")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:SystemType"].begin() + systemIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllSystemType()
        {
            const int count = GetCount();
            
            int* systemTypeData = new int[count];
            if (mEntityTable.column_exists("int:SystemType")) {
                memcpy(systemTypeData, mEntityTable.mDataColumns["int:SystemType"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(systemTypeData, systemTypeData + count);
            
            delete[] systemTypeData;
            
            return result;
        }
        
        int GetFamilyTypeIndex(int systemIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.FamilyType:FamilyType")) {
                return -1;
            }
            
            if (systemIndex < 0 || systemIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][systemIndex];
        }
        
        int GetElementIndex(int systemIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsRoles = mEntityTable.column_exists("int:Roles");
            bool existsSystem = mEntityTable.column_exists("index:Vim.System:System");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<ElementInSystem>* elementInSystem = new std::vector<ElementInSystem>();
            elementInSystem->reserve(count);
            
            int* rolesData = new int[count];
            if (mEntityTable.column_exists("int:Roles")) {
                memcpy(rolesData, mEntityTable.mDataColumns["int:Roles"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& systemData = mEntityTable.column_exists("index:Vim.System:System") ? mEntityTable.mIndexColumns["index:Vim.System:System"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:Roles")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Roles"].begin() + elementInSystemIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllRoles()
        {
            const int count = GetCount();
            
            int* rolesData = new int[count];
            if (mEntityTable.column_exists("int:Roles")) {
                memcpy(rolesData, mEntityTable.mDataColumns["int:Roles"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(rolesData, rolesData + count);
            
            delete[] rolesData;
            
            return result;
        }
        
        int GetSystemIndex(int elementInSystemIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.System:System")) {
                return -1;
            }
            
            if (elementInSystemIndex < 0 || elementInSystemIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.System:System"][elementInSystemIndex];
        }
        
        int GetElementIndex(int elementInSystemIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsGuid = mEntityTable.column_exists("string:Guid");
            bool existsSeverity = mEntityTable.column_exists("string:Severity");
            bool existsDescription = mEntityTable.column_exists("string:Description");
            bool existsBimDocument = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument");
            
            const int count = GetCount();
            
            std::vector<Warning>* warning = new std::vector<Warning>();
            warning->reserve(count);
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
            const std::vector<int>& severityData = mEntityTable.column_exists("string:Severity") ? mEntityTable.mStringColumns["string:Severity"] : std::vector<int>();
            
            const std::vector<int>& descriptionData = mEntityTable.column_exists("string:Description") ? mEntityTable.mStringColumns["string:Description"] : std::vector<int>();
            
            const std::vector<int>& bimDocumentData = mEntityTable.column_exists("index:Vim.BimDocument:BimDocument") ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Guid")) {
                return &mStrings[mEntityTable.mStringColumns["string:Guid"][warningIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllGuid()
        {
            const int count = GetCount();
            
            const std::vector<int>& guidData = mEntityTable.column_exists("string:Guid") ? mEntityTable.mStringColumns["string:Guid"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Severity")) {
                return &mStrings[mEntityTable.mStringColumns["string:Severity"][warningIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllSeverity()
        {
            const int count = GetCount();
            
            const std::vector<int>& severityData = mEntityTable.column_exists("string:Severity") ? mEntityTable.mStringColumns["string:Severity"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("string:Description")) {
                return &mStrings[mEntityTable.mStringColumns["string:Description"][warningIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllDescription()
        {
            const int count = GetCount();
            
            const std::vector<int>& descriptionData = mEntityTable.column_exists("string:Description") ? mEntityTable.mStringColumns["string:Description"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.BimDocument:BimDocument")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsWarning = mEntityTable.column_exists("index:Vim.Warning:Warning");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<ElementInWarning>* elementInWarning = new std::vector<ElementInWarning>();
            elementInWarning->reserve(count);
            
            const std::vector<int>& warningData = mEntityTable.column_exists("index:Vim.Warning:Warning") ? mEntityTable.mIndexColumns["index:Vim.Warning:Warning"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            if (!mEntityTable.column_exists("index:Vim.Warning:Warning")) {
                return -1;
            }
            
            if (elementInWarningIndex < 0 || elementInWarningIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Warning:Warning"][elementInWarningIndex];
        }
        
        int GetElementIndex(int elementInWarningIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        double mPosition_X;
        double mPosition_Y;
        double mPosition_Z;
        double mSharedPosition_X;
        double mSharedPosition_Y;
        double mSharedPosition_Z;
        
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
            return mEntityTable.get_count();
        }
        
        BasePoint* Get(int basePointIndex)
        {
            BasePoint* basePoint = new BasePoint();
            basePoint->mIndex = basePointIndex;
            basePoint->mIsSurveyPoint = GetIsSurveyPoint(basePointIndex);
            basePoint->mPosition_X = GetPosition_X(basePointIndex);
            basePoint->mPosition_Y = GetPosition_Y(basePointIndex);
            basePoint->mPosition_Z = GetPosition_Z(basePointIndex);
            basePoint->mSharedPosition_X = GetSharedPosition_X(basePointIndex);
            basePoint->mSharedPosition_Y = GetSharedPosition_Y(basePointIndex);
            basePoint->mSharedPosition_Z = GetSharedPosition_Z(basePointIndex);
            basePoint->mElementIndex = GetElementIndex(basePointIndex);
            return basePoint;
        }
        
        std::vector<BasePoint>* GetAll()
        {
            bool existsIsSurveyPoint = mEntityTable.column_exists("byte:IsSurveyPoint");
            bool existsPosition_X = mEntityTable.column_exists("double:Position.X");
            bool existsPosition_Y = mEntityTable.column_exists("double:Position.Y");
            bool existsPosition_Z = mEntityTable.column_exists("double:Position.Z");
            bool existsSharedPosition_X = mEntityTable.column_exists("double:SharedPosition.X");
            bool existsSharedPosition_Y = mEntityTable.column_exists("double:SharedPosition.Y");
            bool existsSharedPosition_Z = mEntityTable.column_exists("double:SharedPosition.Z");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<BasePoint>* basePoint = new std::vector<BasePoint>();
            basePoint->reserve(count);
            
            bfast::byte* isSurveyPointData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSurveyPoint")) {
                memcpy(isSurveyPointData, mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin(), count * sizeof(bfast::byte));
            }
            
            double* position_XData = new double[count];
            if (mEntityTable.column_exists("double:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["double:Position.X"].begin(), count * sizeof(double));
            }
            
            double* position_YData = new double[count];
            if (mEntityTable.column_exists("double:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["double:Position.Y"].begin(), count * sizeof(double));
            }
            
            double* position_ZData = new double[count];
            if (mEntityTable.column_exists("double:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["double:Position.Z"].begin(), count * sizeof(double));
            }
            
            double* sharedPosition_XData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.X")) {
                memcpy(sharedPosition_XData, mEntityTable.mDataColumns["double:SharedPosition.X"].begin(), count * sizeof(double));
            }
            
            double* sharedPosition_YData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.Y")) {
                memcpy(sharedPosition_YData, mEntityTable.mDataColumns["double:SharedPosition.Y"].begin(), count * sizeof(double));
            }
            
            double* sharedPosition_ZData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.Z")) {
                memcpy(sharedPosition_ZData, mEntityTable.mDataColumns["double:SharedPosition.Z"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                BasePoint entity;
                entity.mIndex = i;
                if (existsIsSurveyPoint)
                    entity.mIsSurveyPoint = isSurveyPointData[i];
                if (existsPosition_X)
                    entity.mPosition_X = position_XData[i];
                if (existsPosition_Y)
                    entity.mPosition_Y = position_YData[i];
                if (existsPosition_Z)
                    entity.mPosition_Z = position_ZData[i];
                if (existsSharedPosition_X)
                    entity.mSharedPosition_X = sharedPosition_XData[i];
                if (existsSharedPosition_Y)
                    entity.mSharedPosition_Y = sharedPosition_YData[i];
                if (existsSharedPosition_Z)
                    entity.mSharedPosition_Z = sharedPosition_ZData[i];
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                basePoint->push_back(entity);
            }
            
            delete[] isSurveyPointData;
            delete[] position_XData;
            delete[] position_YData;
            delete[] position_ZData;
            delete[] sharedPosition_XData;
            delete[] sharedPosition_YData;
            delete[] sharedPosition_ZData;
            
            return basePoint;
        }
        
        bool GetIsSurveyPoint(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsSurveyPoint")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin() + basePointIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsSurveyPoint()
        {
            const int count = GetCount();
            
            bfast::byte* isSurveyPointData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsSurveyPoint")) {
                memcpy(isSurveyPointData, mEntityTable.mDataColumns["byte:IsSurveyPoint"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isSurveyPointData, isSurveyPointData + count);
            
            delete[] isSurveyPointData;
            
            return result;
        }
        
        double GetPosition_X(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Position.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Position.X"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllPosition_X()
        {
            const int count = GetCount();
            
            double* position_XData = new double[count];
            if (mEntityTable.column_exists("double:Position.X")) {
                memcpy(position_XData, mEntityTable.mDataColumns["double:Position.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(position_XData, position_XData + count);
            
            delete[] position_XData;
            
            return result;
        }
        
        double GetPosition_Y(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Position.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Position.Y"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllPosition_Y()
        {
            const int count = GetCount();
            
            double* position_YData = new double[count];
            if (mEntityTable.column_exists("double:Position.Y")) {
                memcpy(position_YData, mEntityTable.mDataColumns["double:Position.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(position_YData, position_YData + count);
            
            delete[] position_YData;
            
            return result;
        }
        
        double GetPosition_Z(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Position.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Position.Z"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllPosition_Z()
        {
            const int count = GetCount();
            
            double* position_ZData = new double[count];
            if (mEntityTable.column_exists("double:Position.Z")) {
                memcpy(position_ZData, mEntityTable.mDataColumns["double:Position.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(position_ZData, position_ZData + count);
            
            delete[] position_ZData;
            
            return result;
        }
        
        double GetSharedPosition_X(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:SharedPosition.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:SharedPosition.X"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllSharedPosition_X()
        {
            const int count = GetCount();
            
            double* sharedPosition_XData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.X")) {
                memcpy(sharedPosition_XData, mEntityTable.mDataColumns["double:SharedPosition.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(sharedPosition_XData, sharedPosition_XData + count);
            
            delete[] sharedPosition_XData;
            
            return result;
        }
        
        double GetSharedPosition_Y(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:SharedPosition.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:SharedPosition.Y"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllSharedPosition_Y()
        {
            const int count = GetCount();
            
            double* sharedPosition_YData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.Y")) {
                memcpy(sharedPosition_YData, mEntityTable.mDataColumns["double:SharedPosition.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(sharedPosition_YData, sharedPosition_YData + count);
            
            delete[] sharedPosition_YData;
            
            return result;
        }
        
        double GetSharedPosition_Z(int basePointIndex)
        {
            if (basePointIndex < 0 || basePointIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:SharedPosition.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:SharedPosition.Z"].begin() + basePointIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllSharedPosition_Z()
        {
            const int count = GetCount();
            
            double* sharedPosition_ZData = new double[count];
            if (mEntityTable.column_exists("double:SharedPosition.Z")) {
                memcpy(sharedPosition_ZData, mEntityTable.mDataColumns["double:SharedPosition.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(sharedPosition_ZData, sharedPosition_ZData + count);
            
            delete[] sharedPosition_ZData;
            
            return result;
        }
        
        int GetElementIndex(int basePointIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsNew = mEntityTable.column_exists("int:New");
            bool existsExisting = mEntityTable.column_exists("int:Existing");
            bool existsDemolished = mEntityTable.column_exists("int:Demolished");
            bool existsTemporary = mEntityTable.column_exists("int:Temporary");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<PhaseFilter>* phaseFilter = new std::vector<PhaseFilter>();
            phaseFilter->reserve(count);
            
            int* newData = new int[count];
            if (mEntityTable.column_exists("int:New")) {
                memcpy(newData, mEntityTable.mDataColumns["int:New"].begin(), count * sizeof(int));
            }
            
            int* existingData = new int[count];
            if (mEntityTable.column_exists("int:Existing")) {
                memcpy(existingData, mEntityTable.mDataColumns["int:Existing"].begin(), count * sizeof(int));
            }
            
            int* demolishedData = new int[count];
            if (mEntityTable.column_exists("int:Demolished")) {
                memcpy(demolishedData, mEntityTable.mDataColumns["int:Demolished"].begin(), count * sizeof(int));
            }
            
            int* temporaryData = new int[count];
            if (mEntityTable.column_exists("int:Temporary")) {
                memcpy(temporaryData, mEntityTable.mDataColumns["int:Temporary"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("int:New")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:New"].begin() + phaseFilterIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllNew()
        {
            const int count = GetCount();
            
            int* newData = new int[count];
            if (mEntityTable.column_exists("int:New")) {
                memcpy(newData, mEntityTable.mDataColumns["int:New"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(newData, newData + count);
            
            delete[] newData;
            
            return result;
        }
        
        int GetExisting(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:Existing")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Existing"].begin() + phaseFilterIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllExisting()
        {
            const int count = GetCount();
            
            int* existingData = new int[count];
            if (mEntityTable.column_exists("int:Existing")) {
                memcpy(existingData, mEntityTable.mDataColumns["int:Existing"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(existingData, existingData + count);
            
            delete[] existingData;
            
            return result;
        }
        
        int GetDemolished(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:Demolished")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Demolished"].begin() + phaseFilterIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllDemolished()
        {
            const int count = GetCount();
            
            int* demolishedData = new int[count];
            if (mEntityTable.column_exists("int:Demolished")) {
                memcpy(demolishedData, mEntityTable.mDataColumns["int:Demolished"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(demolishedData, demolishedData + count);
            
            delete[] demolishedData;
            
            return result;
        }
        
        int GetTemporary(int phaseFilterIndex)
        {
            if (phaseFilterIndex < 0 || phaseFilterIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:Temporary")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:Temporary"].begin() + phaseFilterIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllTemporary()
        {
            const int count = GetCount();
            
            int* temporaryData = new int[count];
            if (mEntityTable.column_exists("int:Temporary")) {
                memcpy(temporaryData, mEntityTable.mDataColumns["int:Temporary"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(temporaryData, temporaryData + count);
            
            delete[] temporaryData;
            
            return result;
        }
        
        int GetElementIndex(int phaseFilterIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
        double mStartPoint_X;
        double mStartPoint_Y;
        double mStartPoint_Z;
        double mEndPoint_X;
        double mEndPoint_Y;
        double mEndPoint_Z;
        bool mIsCurved;
        double mExtents_Min_X;
        double mExtents_Min_Y;
        double mExtents_Min_Z;
        double mExtents_Max_X;
        double mExtents_Max_Y;
        double mExtents_Max_Z;
        
        int mFamilyTypeIndex;
        FamilyType* mFamilyType;
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
            return mEntityTable.get_count();
        }
        
        Grid* Get(int gridIndex)
        {
            Grid* grid = new Grid();
            grid->mIndex = gridIndex;
            grid->mStartPoint_X = GetStartPoint_X(gridIndex);
            grid->mStartPoint_Y = GetStartPoint_Y(gridIndex);
            grid->mStartPoint_Z = GetStartPoint_Z(gridIndex);
            grid->mEndPoint_X = GetEndPoint_X(gridIndex);
            grid->mEndPoint_Y = GetEndPoint_Y(gridIndex);
            grid->mEndPoint_Z = GetEndPoint_Z(gridIndex);
            grid->mIsCurved = GetIsCurved(gridIndex);
            grid->mExtents_Min_X = GetExtents_Min_X(gridIndex);
            grid->mExtents_Min_Y = GetExtents_Min_Y(gridIndex);
            grid->mExtents_Min_Z = GetExtents_Min_Z(gridIndex);
            grid->mExtents_Max_X = GetExtents_Max_X(gridIndex);
            grid->mExtents_Max_Y = GetExtents_Max_Y(gridIndex);
            grid->mExtents_Max_Z = GetExtents_Max_Z(gridIndex);
            grid->mFamilyTypeIndex = GetFamilyTypeIndex(gridIndex);
            grid->mElementIndex = GetElementIndex(gridIndex);
            return grid;
        }
        
        std::vector<Grid>* GetAll()
        {
            bool existsStartPoint_X = mEntityTable.column_exists("double:StartPoint.X");
            bool existsStartPoint_Y = mEntityTable.column_exists("double:StartPoint.Y");
            bool existsStartPoint_Z = mEntityTable.column_exists("double:StartPoint.Z");
            bool existsEndPoint_X = mEntityTable.column_exists("double:EndPoint.X");
            bool existsEndPoint_Y = mEntityTable.column_exists("double:EndPoint.Y");
            bool existsEndPoint_Z = mEntityTable.column_exists("double:EndPoint.Z");
            bool existsIsCurved = mEntityTable.column_exists("byte:IsCurved");
            bool existsExtents_Min_X = mEntityTable.column_exists("double:Extents.Min.X");
            bool existsExtents_Min_Y = mEntityTable.column_exists("double:Extents.Min.Y");
            bool existsExtents_Min_Z = mEntityTable.column_exists("double:Extents.Min.Z");
            bool existsExtents_Max_X = mEntityTable.column_exists("double:Extents.Max.X");
            bool existsExtents_Max_Y = mEntityTable.column_exists("double:Extents.Max.Y");
            bool existsExtents_Max_Z = mEntityTable.column_exists("double:Extents.Max.Z");
            bool existsFamilyType = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Grid>* grid = new std::vector<Grid>();
            grid->reserve(count);
            
            double* startPoint_XData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.X")) {
                memcpy(startPoint_XData, mEntityTable.mDataColumns["double:StartPoint.X"].begin(), count * sizeof(double));
            }
            
            double* startPoint_YData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.Y")) {
                memcpy(startPoint_YData, mEntityTable.mDataColumns["double:StartPoint.Y"].begin(), count * sizeof(double));
            }
            
            double* startPoint_ZData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.Z")) {
                memcpy(startPoint_ZData, mEntityTable.mDataColumns["double:StartPoint.Z"].begin(), count * sizeof(double));
            }
            
            double* endPoint_XData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.X")) {
                memcpy(endPoint_XData, mEntityTable.mDataColumns["double:EndPoint.X"].begin(), count * sizeof(double));
            }
            
            double* endPoint_YData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.Y")) {
                memcpy(endPoint_YData, mEntityTable.mDataColumns["double:EndPoint.Y"].begin(), count * sizeof(double));
            }
            
            double* endPoint_ZData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.Z")) {
                memcpy(endPoint_ZData, mEntityTable.mDataColumns["double:EndPoint.Z"].begin(), count * sizeof(double));
            }
            
            bfast::byte* isCurvedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsCurved")) {
                memcpy(isCurvedData, mEntityTable.mDataColumns["byte:IsCurved"].begin(), count * sizeof(bfast::byte));
            }
            
            double* extents_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                memcpy(extents_Min_XData, mEntityTable.mDataColumns["double:Extents.Min.X"].begin(), count * sizeof(double));
            }
            
            double* extents_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                memcpy(extents_Min_YData, mEntityTable.mDataColumns["double:Extents.Min.Y"].begin(), count * sizeof(double));
            }
            
            double* extents_Min_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                memcpy(extents_Min_ZData, mEntityTable.mDataColumns["double:Extents.Min.Z"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                memcpy(extents_Max_XData, mEntityTable.mDataColumns["double:Extents.Max.X"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                memcpy(extents_Max_YData, mEntityTable.mDataColumns["double:Extents.Max.Y"].begin(), count * sizeof(double));
            }
            
            double* extents_Max_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                memcpy(extents_Max_ZData, mEntityTable.mDataColumns["double:Extents.Max.Z"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& familyTypeData = mEntityTable.column_exists("index:Vim.FamilyType:FamilyType") ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                Grid entity;
                entity.mIndex = i;
                if (existsStartPoint_X)
                    entity.mStartPoint_X = startPoint_XData[i];
                if (existsStartPoint_Y)
                    entity.mStartPoint_Y = startPoint_YData[i];
                if (existsStartPoint_Z)
                    entity.mStartPoint_Z = startPoint_ZData[i];
                if (existsEndPoint_X)
                    entity.mEndPoint_X = endPoint_XData[i];
                if (existsEndPoint_Y)
                    entity.mEndPoint_Y = endPoint_YData[i];
                if (existsEndPoint_Z)
                    entity.mEndPoint_Z = endPoint_ZData[i];
                if (existsIsCurved)
                    entity.mIsCurved = isCurvedData[i];
                if (existsExtents_Min_X)
                    entity.mExtents_Min_X = extents_Min_XData[i];
                if (existsExtents_Min_Y)
                    entity.mExtents_Min_Y = extents_Min_YData[i];
                if (existsExtents_Min_Z)
                    entity.mExtents_Min_Z = extents_Min_ZData[i];
                if (existsExtents_Max_X)
                    entity.mExtents_Max_X = extents_Max_XData[i];
                if (existsExtents_Max_Y)
                    entity.mExtents_Max_Y = extents_Max_YData[i];
                if (existsExtents_Max_Z)
                    entity.mExtents_Max_Z = extents_Max_ZData[i];
                entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                grid->push_back(entity);
            }
            
            delete[] startPoint_XData;
            delete[] startPoint_YData;
            delete[] startPoint_ZData;
            delete[] endPoint_XData;
            delete[] endPoint_YData;
            delete[] endPoint_ZData;
            delete[] isCurvedData;
            delete[] extents_Min_XData;
            delete[] extents_Min_YData;
            delete[] extents_Min_ZData;
            delete[] extents_Max_XData;
            delete[] extents_Max_YData;
            delete[] extents_Max_ZData;
            
            return grid;
        }
        
        double GetStartPoint_X(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:StartPoint.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:StartPoint.X"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllStartPoint_X()
        {
            const int count = GetCount();
            
            double* startPoint_XData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.X")) {
                memcpy(startPoint_XData, mEntityTable.mDataColumns["double:StartPoint.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(startPoint_XData, startPoint_XData + count);
            
            delete[] startPoint_XData;
            
            return result;
        }
        
        double GetStartPoint_Y(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:StartPoint.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:StartPoint.Y"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllStartPoint_Y()
        {
            const int count = GetCount();
            
            double* startPoint_YData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.Y")) {
                memcpy(startPoint_YData, mEntityTable.mDataColumns["double:StartPoint.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(startPoint_YData, startPoint_YData + count);
            
            delete[] startPoint_YData;
            
            return result;
        }
        
        double GetStartPoint_Z(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:StartPoint.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:StartPoint.Z"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllStartPoint_Z()
        {
            const int count = GetCount();
            
            double* startPoint_ZData = new double[count];
            if (mEntityTable.column_exists("double:StartPoint.Z")) {
                memcpy(startPoint_ZData, mEntityTable.mDataColumns["double:StartPoint.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(startPoint_ZData, startPoint_ZData + count);
            
            delete[] startPoint_ZData;
            
            return result;
        }
        
        double GetEndPoint_X(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:EndPoint.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:EndPoint.X"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllEndPoint_X()
        {
            const int count = GetCount();
            
            double* endPoint_XData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.X")) {
                memcpy(endPoint_XData, mEntityTable.mDataColumns["double:EndPoint.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(endPoint_XData, endPoint_XData + count);
            
            delete[] endPoint_XData;
            
            return result;
        }
        
        double GetEndPoint_Y(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:EndPoint.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:EndPoint.Y"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllEndPoint_Y()
        {
            const int count = GetCount();
            
            double* endPoint_YData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.Y")) {
                memcpy(endPoint_YData, mEntityTable.mDataColumns["double:EndPoint.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(endPoint_YData, endPoint_YData + count);
            
            delete[] endPoint_YData;
            
            return result;
        }
        
        double GetEndPoint_Z(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:EndPoint.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:EndPoint.Z"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllEndPoint_Z()
        {
            const int count = GetCount();
            
            double* endPoint_ZData = new double[count];
            if (mEntityTable.column_exists("double:EndPoint.Z")) {
                memcpy(endPoint_ZData, mEntityTable.mDataColumns["double:EndPoint.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(endPoint_ZData, endPoint_ZData + count);
            
            delete[] endPoint_ZData;
            
            return result;
        }
        
        bool GetIsCurved(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("byte:IsCurved")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsCurved"].begin() + gridIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsCurved()
        {
            const int count = GetCount();
            
            bfast::byte* isCurvedData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsCurved")) {
                memcpy(isCurvedData, mEntityTable.mDataColumns["byte:IsCurved"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isCurvedData, isCurvedData + count);
            
            delete[] isCurvedData;
            
            return result;
        }
        
        double GetExtents_Min_X(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.X"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_X()
        {
            const int count = GetCount();
            
            double* extents_Min_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.X")) {
                memcpy(extents_Min_XData, mEntityTable.mDataColumns["double:Extents.Min.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_XData, extents_Min_XData + count);
            
            delete[] extents_Min_XData;
            
            return result;
        }
        
        double GetExtents_Min_Y(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.Y"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_Y()
        {
            const int count = GetCount();
            
            double* extents_Min_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Y")) {
                memcpy(extents_Min_YData, mEntityTable.mDataColumns["double:Extents.Min.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_YData, extents_Min_YData + count);
            
            delete[] extents_Min_YData;
            
            return result;
        }
        
        double GetExtents_Min_Z(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Min.Z"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Min_Z()
        {
            const int count = GetCount();
            
            double* extents_Min_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Min.Z")) {
                memcpy(extents_Min_ZData, mEntityTable.mDataColumns["double:Extents.Min.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Min_ZData, extents_Min_ZData + count);
            
            delete[] extents_Min_ZData;
            
            return result;
        }
        
        double GetExtents_Max_X(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.X"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_X()
        {
            const int count = GetCount();
            
            double* extents_Max_XData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.X")) {
                memcpy(extents_Max_XData, mEntityTable.mDataColumns["double:Extents.Max.X"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_XData, extents_Max_XData + count);
            
            delete[] extents_Max_XData;
            
            return result;
        }
        
        double GetExtents_Max_Y(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.Y"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_Y()
        {
            const int count = GetCount();
            
            double* extents_Max_YData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Y")) {
                memcpy(extents_Max_YData, mEntityTable.mDataColumns["double:Extents.Max.Y"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_YData, extents_Max_YData + count);
            
            delete[] extents_Max_YData;
            
            return result;
        }
        
        double GetExtents_Max_Z(int gridIndex)
        {
            if (gridIndex < 0 || gridIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Extents.Max.Z"].begin() + gridIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllExtents_Max_Z()
        {
            const int count = GetCount();
            
            double* extents_Max_ZData = new double[count];
            if (mEntityTable.column_exists("double:Extents.Max.Z")) {
                memcpy(extents_Max_ZData, mEntityTable.mDataColumns["double:Extents.Max.Z"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(extents_Max_ZData, extents_Max_ZData + count);
            
            delete[] extents_Max_ZData;
            
            return result;
        }
        
        int GetFamilyTypeIndex(int gridIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.FamilyType:FamilyType")) {
                return -1;
            }
            
            if (gridIndex < 0 || gridIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"][gridIndex];
        }
        
        int GetElementIndex(int gridIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsValue = mEntityTable.column_exists("double:Value");
            bool existsPerimeter = mEntityTable.column_exists("double:Perimeter");
            bool existsNumber = mEntityTable.column_exists("string:Number");
            bool existsIsGrossInterior = mEntityTable.column_exists("byte:IsGrossInterior");
            bool existsAreaScheme = mEntityTable.column_exists("index:Vim.AreaScheme:AreaScheme");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Area>* area = new std::vector<Area>();
            area->reserve(count);
            
            double* valueData = new double[count];
            if (mEntityTable.column_exists("double:Value")) {
                memcpy(valueData, mEntityTable.mDataColumns["double:Value"].begin(), count * sizeof(double));
            }
            
            double* perimeterData = new double[count];
            if (mEntityTable.column_exists("double:Perimeter")) {
                memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));
            }
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
            bfast::byte* isGrossInteriorData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsGrossInterior")) {
                memcpy(isGrossInteriorData, mEntityTable.mDataColumns["byte:IsGrossInterior"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& areaSchemeData = mEntityTable.column_exists("index:Vim.AreaScheme:AreaScheme") ? mEntityTable.mIndexColumns["index:Vim.AreaScheme:AreaScheme"] : std::vector<int>();
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("double:Value")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Value"].begin() + areaIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllValue()
        {
            const int count = GetCount();
            
            double* valueData = new double[count];
            if (mEntityTable.column_exists("double:Value")) {
                memcpy(valueData, mEntityTable.mDataColumns["double:Value"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(valueData, valueData + count);
            
            delete[] valueData;
            
            return result;
        }
        
        double GetPerimeter(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("double:Perimeter")) {
                return *reinterpret_cast<double*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["double:Perimeter"].begin() + areaIndex * sizeof(double)));
            }
            
            return {};
        }
        
        const std::vector<double>* GetAllPerimeter()
        {
            const int count = GetCount();
            
            double* perimeterData = new double[count];
            if (mEntityTable.column_exists("double:Perimeter")) {
                memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count * sizeof(double));
            }
            
            std::vector<double>* result = new std::vector<double>(perimeterData, perimeterData + count);
            
            delete[] perimeterData;
            
            return result;
        }
        
        const std::string* GetNumber(int areaIndex)
        {
            if (areaIndex < 0 || areaIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Number")) {
                return &mStrings[mEntityTable.mStringColumns["string:Number"][areaIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllNumber()
        {
            const int count = GetCount();
            
            const std::vector<int>& numberData = mEntityTable.column_exists("string:Number") ? mEntityTable.mStringColumns["string:Number"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsGrossInterior")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsGrossInterior"].begin() + areaIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsGrossInterior()
        {
            const int count = GetCount();
            
            bfast::byte* isGrossInteriorData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsGrossInterior")) {
                memcpy(isGrossInteriorData, mEntityTable.mDataColumns["byte:IsGrossInterior"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isGrossInteriorData, isGrossInteriorData + count);
            
            delete[] isGrossInteriorData;
            
            return result;
        }
        
        int GetAreaSchemeIndex(int areaIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.AreaScheme:AreaScheme")) {
                return -1;
            }
            
            if (areaIndex < 0 || areaIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.AreaScheme:AreaScheme"][areaIndex];
        }
        
        int GetElementIndex(int areaIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
            return mEntityTable.get_count();
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
            bool existsIsGrossBuildingArea = mEntityTable.column_exists("byte:IsGrossBuildingArea");
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<AreaScheme>* areaScheme = new std::vector<AreaScheme>();
            areaScheme->reserve(count);
            
            bfast::byte* isGrossBuildingAreaData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsGrossBuildingArea")) {
                memcpy(isGrossBuildingAreaData, mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin(), count * sizeof(bfast::byte));
            }
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
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
            
            if (mEntityTable.column_exists("byte:IsGrossBuildingArea")) {
                return static_cast<bool>(*reinterpret_cast<bfast::byte*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin() + areaSchemeIndex * sizeof(bfast::byte))));
            }
            
            return {};
        }
        
        const std::vector<bool>* GetAllIsGrossBuildingArea()
        {
            const int count = GetCount();
            
            bfast::byte* isGrossBuildingAreaData = new bfast::byte[count];
            if (mEntityTable.column_exists("byte:IsGrossBuildingArea")) {
                memcpy(isGrossBuildingAreaData, mEntityTable.mDataColumns["byte:IsGrossBuildingArea"].begin(), count * sizeof(bfast::byte));
            }
            
            std::vector<bool>* result = new std::vector<bool>(isGrossBuildingAreaData, isGrossBuildingAreaData + count);
            
            delete[] isGrossBuildingAreaData;
            
            return result;
        }
        
        int GetElementIndex(int areaSchemeIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
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
    
    class Schedule
    {
    public:
        int mIndex;
        
        int mElementIndex;
        Element* mElement;
        
        Schedule() {}
    };
    
    class ScheduleTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ScheduleTable(EntityTable& entityTable, std::vector<std::string>& strings):
            mEntityTable(entityTable), mStrings(strings) {}
        
        int GetCount()
        {
            return mEntityTable.get_count();
        }
        
        Schedule* Get(int scheduleIndex)
        {
            Schedule* schedule = new Schedule();
            schedule->mIndex = scheduleIndex;
            schedule->mElementIndex = GetElementIndex(scheduleIndex);
            return schedule;
        }
        
        std::vector<Schedule>* GetAll()
        {
            bool existsElement = mEntityTable.column_exists("index:Vim.Element:Element");
            
            const int count = GetCount();
            
            std::vector<Schedule>* schedule = new std::vector<Schedule>();
            schedule->reserve(count);
            
            const std::vector<int>& elementData = mEntityTable.column_exists("index:Vim.Element:Element") ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                Schedule entity;
                entity.mIndex = i;
                entity.mElementIndex = existsElement ? elementData[i] : -1;
                schedule->push_back(entity);
            }
            
            return schedule;
        }
        
        int GetElementIndex(int scheduleIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Element:Element")) {
                return -1;
            }
            
            if (scheduleIndex < 0 || scheduleIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Element:Element"][scheduleIndex];
        }
        
    };
    
    static ScheduleTable* GetScheduleTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.Schedule") == scene.mEntityTables.end())
            return {};
        
        return new ScheduleTable(scene.mEntityTables["Vim.Schedule"], scene.mStrings);
    }
    
    class ScheduleColumn
    {
    public:
        int mIndex;
        const std::string* mName;
        int mColumnIndex;
        
        int mScheduleIndex;
        Schedule* mSchedule;
        
        ScheduleColumn() {}
    };
    
    class ScheduleColumnTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ScheduleColumnTable(EntityTable& entityTable, std::vector<std::string>& strings):
            mEntityTable(entityTable), mStrings(strings) {}
        
        int GetCount()
        {
            return mEntityTable.get_count();
        }
        
        ScheduleColumn* Get(int scheduleColumnIndex)
        {
            ScheduleColumn* scheduleColumn = new ScheduleColumn();
            scheduleColumn->mIndex = scheduleColumnIndex;
            scheduleColumn->mName = GetName(scheduleColumnIndex);
            scheduleColumn->mColumnIndex = GetColumnIndex(scheduleColumnIndex);
            scheduleColumn->mScheduleIndex = GetScheduleIndex(scheduleColumnIndex);
            return scheduleColumn;
        }
        
        std::vector<ScheduleColumn>* GetAll()
        {
            bool existsName = mEntityTable.column_exists("string:Name");
            bool existsColumnIndex = mEntityTable.column_exists("int:ColumnIndex");
            bool existsSchedule = mEntityTable.column_exists("index:Vim.Schedule:Schedule");
            
            const int count = GetCount();
            
            std::vector<ScheduleColumn>* scheduleColumn = new std::vector<ScheduleColumn>();
            scheduleColumn->reserve(count);
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            int* columnIndexData = new int[count];
            if (mEntityTable.column_exists("int:ColumnIndex")) {
                memcpy(columnIndexData, mEntityTable.mDataColumns["int:ColumnIndex"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& scheduleData = mEntityTable.column_exists("index:Vim.Schedule:Schedule") ? mEntityTable.mIndexColumns["index:Vim.Schedule:Schedule"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                ScheduleColumn entity;
                entity.mIndex = i;
                if (existsName)
                    entity.mName = &mStrings[nameData[i]];
                if (existsColumnIndex)
                    entity.mColumnIndex = columnIndexData[i];
                entity.mScheduleIndex = existsSchedule ? scheduleData[i] : -1;
                scheduleColumn->push_back(entity);
            }
            
            delete[] columnIndexData;
            
            return scheduleColumn;
        }
        
        const std::string* GetName(int scheduleColumnIndex)
        {
            if (scheduleColumnIndex < 0 || scheduleColumnIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Name")) {
                return &mStrings[mEntityTable.mStringColumns["string:Name"][scheduleColumnIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllName()
        {
            const int count = GetCount();
            
            const std::vector<int>& nameData = mEntityTable.column_exists("string:Name") ? mEntityTable.mStringColumns["string:Name"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[nameData[i]]);
            }
            
            return result;
        }
        
        int GetColumnIndex(int scheduleColumnIndex)
        {
            if (scheduleColumnIndex < 0 || scheduleColumnIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:ColumnIndex")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:ColumnIndex"].begin() + scheduleColumnIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllColumnIndex()
        {
            const int count = GetCount();
            
            int* columnIndexData = new int[count];
            if (mEntityTable.column_exists("int:ColumnIndex")) {
                memcpy(columnIndexData, mEntityTable.mDataColumns["int:ColumnIndex"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(columnIndexData, columnIndexData + count);
            
            delete[] columnIndexData;
            
            return result;
        }
        
        int GetScheduleIndex(int scheduleColumnIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.Schedule:Schedule")) {
                return -1;
            }
            
            if (scheduleColumnIndex < 0 || scheduleColumnIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.Schedule:Schedule"][scheduleColumnIndex];
        }
        
    };
    
    static ScheduleColumnTable* GetScheduleColumnTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ScheduleColumn") == scene.mEntityTables.end())
            return {};
        
        return new ScheduleColumnTable(scene.mEntityTables["Vim.ScheduleColumn"], scene.mStrings);
    }
    
    class ScheduleCell
    {
    public:
        int mIndex;
        const std::string* mValue;
        int mRowIndex;
        
        int mScheduleColumnIndex;
        ScheduleColumn* mScheduleColumn;
        
        ScheduleCell() {}
    };
    
    class ScheduleCellTable
    {
        EntityTable& mEntityTable;
        std::vector<std::string>& mStrings;
    public:
        ScheduleCellTable(EntityTable& entityTable, std::vector<std::string>& strings):
            mEntityTable(entityTable), mStrings(strings) {}
        
        int GetCount()
        {
            return mEntityTable.get_count();
        }
        
        ScheduleCell* Get(int scheduleCellIndex)
        {
            ScheduleCell* scheduleCell = new ScheduleCell();
            scheduleCell->mIndex = scheduleCellIndex;
            scheduleCell->mValue = GetValue(scheduleCellIndex);
            scheduleCell->mRowIndex = GetRowIndex(scheduleCellIndex);
            scheduleCell->mScheduleColumnIndex = GetScheduleColumnIndex(scheduleCellIndex);
            return scheduleCell;
        }
        
        std::vector<ScheduleCell>* GetAll()
        {
            bool existsValue = mEntityTable.column_exists("string:Value");
            bool existsRowIndex = mEntityTable.column_exists("int:RowIndex");
            bool existsScheduleColumn = mEntityTable.column_exists("index:Vim.ScheduleColumn:ScheduleColumn");
            
            const int count = GetCount();
            
            std::vector<ScheduleCell>* scheduleCell = new std::vector<ScheduleCell>();
            scheduleCell->reserve(count);
            
            const std::vector<int>& valueData = mEntityTable.column_exists("string:Value") ? mEntityTable.mStringColumns["string:Value"] : std::vector<int>();
            
            int* rowIndexData = new int[count];
            if (mEntityTable.column_exists("int:RowIndex")) {
                memcpy(rowIndexData, mEntityTable.mDataColumns["int:RowIndex"].begin(), count * sizeof(int));
            }
            
            const std::vector<int>& scheduleColumnData = mEntityTable.column_exists("index:Vim.ScheduleColumn:ScheduleColumn") ? mEntityTable.mIndexColumns["index:Vim.ScheduleColumn:ScheduleColumn"] : std::vector<int>();
            
            for (int i = 0; i < count; ++i)
            {
                ScheduleCell entity;
                entity.mIndex = i;
                if (existsValue)
                    entity.mValue = &mStrings[valueData[i]];
                if (existsRowIndex)
                    entity.mRowIndex = rowIndexData[i];
                entity.mScheduleColumnIndex = existsScheduleColumn ? scheduleColumnData[i] : -1;
                scheduleCell->push_back(entity);
            }
            
            delete[] rowIndexData;
            
            return scheduleCell;
        }
        
        const std::string* GetValue(int scheduleCellIndex)
        {
            if (scheduleCellIndex < 0 || scheduleCellIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("string:Value")) {
                return &mStrings[mEntityTable.mStringColumns["string:Value"][scheduleCellIndex]];
            }
            
            return {};
        }
        
        const std::vector<const std::string*>* GetAllValue()
        {
            const int count = GetCount();
            
            const std::vector<int>& valueData = mEntityTable.column_exists("string:Value") ? mEntityTable.mStringColumns["string:Value"] : std::vector<int>();
            
            std::vector<const std::string*>* result = new std::vector<const std::string*>();
            result->reserve(count);
            
            for (int i = 0; i < count; ++i)
            {
                result->push_back(&mStrings[valueData[i]]);
            }
            
            return result;
        }
        
        int GetRowIndex(int scheduleCellIndex)
        {
            if (scheduleCellIndex < 0 || scheduleCellIndex >= GetCount())
                return {};
            
            if (mEntityTable.column_exists("int:RowIndex")) {
                return *reinterpret_cast<int*>(const_cast<bfast::byte*>(mEntityTable.mDataColumns["int:RowIndex"].begin() + scheduleCellIndex * sizeof(int)));
            }
            
            return {};
        }
        
        const std::vector<int>* GetAllRowIndex()
        {
            const int count = GetCount();
            
            int* rowIndexData = new int[count];
            if (mEntityTable.column_exists("int:RowIndex")) {
                memcpy(rowIndexData, mEntityTable.mDataColumns["int:RowIndex"].begin(), count * sizeof(int));
            }
            
            std::vector<int>* result = new std::vector<int>(rowIndexData, rowIndexData + count);
            
            delete[] rowIndexData;
            
            return result;
        }
        
        int GetScheduleColumnIndex(int scheduleCellIndex)
        {
            if (!mEntityTable.column_exists("index:Vim.ScheduleColumn:ScheduleColumn")) {
                return -1;
            }
            
            if (scheduleCellIndex < 0 || scheduleCellIndex >= GetCount())
                return -1;
            
            return mEntityTable.mIndexColumns["index:Vim.ScheduleColumn:ScheduleColumn"][scheduleCellIndex];
        }
        
    };
    
    static ScheduleCellTable* GetScheduleCellTable(VimScene& scene)
    {
        if (scene.mEntityTables.find("Vim.ScheduleCell") == scene.mEntityTables.end())
            return {};
        
        return new ScheduleCellTable(scene.mEntityTables["Vim.ScheduleCell"], scene.mStrings);
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
        mSchedule = GetScheduleTable(scene);
        mScheduleColumn = GetScheduleColumnTable(scene);
        mScheduleCell = GetScheduleCellTable(scene);
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
        delete mSchedule;
        delete mScheduleColumn;
        delete mScheduleCell;
    }
}

#endif
