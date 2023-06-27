import 'dart:core';
import 'vim.dart';

class Vector3 {
  final double x;
  final double y;
  final double z;

  const Vector3(this.x, this.y, this.z);
}

class DocumentModel {
  final DisplayUnitTable? displayUnit;
  final ParameterDescriptorTable? parameterDescriptor;
  final ParameterTable? parameter;
  final ElementTable? element;
  final LevelTable? level;
  final RoomTable? room;
  final BimDocumentTable? bimDocument;
  final CategoryTable? category;
  final FamilyTable? family;
  final FamilyTypeTable? familyType;
  final FamilyInstanceTable? familyInstance;
  final NodeTable? node;

  static DisplayUnitTable? _getDisplayUnitTable(VimScene scene) {
    final table = scene.entities["Vim.DisplayUnit"];
    return table == null ? null : DisplayUnitTable._(table, scene.strings);
  }

  static ParameterDescriptorTable? _getParameterDescriptorTable(VimScene scene) {
    final column = scene.entities["Vim.ParameterDescriptor"];
    return column == null ? null : ParameterDescriptorTable._(column, scene.strings);
  }

  static ParameterTable? _getParameterTable(VimScene scene) {
    final column = scene.entities["Vim.Parameter"];
    return column == null ? null : ParameterTable._(column, scene.strings);
  }

  static ElementTable? _getElementTable(VimScene scene) {
    final index = scene.entities["Vim.Element"];
    return index == null ? null : ElementTable._(index, scene.strings);
  }

  static LevelTable? _getLevelTable(VimScene scene) {
    final index = scene.entities["Vim.Level"];
    return index == null ? null : LevelTable._(index, scene.strings);
  }

  static RoomTable? _getRoomTable(VimScene scene) {
    final vimNode = scene.entities["Vim.Room"];
    return vimNode == null ? null : RoomTable._(vimNode, scene.strings);
  }

  static BimDocumentTable? _getBimDocumentTable(VimScene scene) {
    final column = scene.entities["Vim.BimDocument"];
    return column == null ? null : BimDocumentTable._(column, scene.strings);
  }

  static CategoryTable? _getCategoryTable(VimScene scene) {
    final index = scene.entities["Vim.Category"];
    return index == null ? null : CategoryTable._(index, scene.strings);
  }

  static FamilyTable? _getFamilyTable(VimScene scene) {
    final index = scene.entities["Vim.Family"];
    return index == null ? null : FamilyTable._(index, scene.strings);
  }

  static FamilyTypeTable? _getFamilyTypeTable(VimScene scene) {
    final index = scene.entities["Vim.FamilyType"];
    return index == null ? null : FamilyTypeTable._(index, scene.strings);
  }

  static FamilyInstanceTable? _getFamilyInstanceTable(VimScene scene) {
    final vimNode = scene.entities["Vim.FamilyInstance"];
    return vimNode == null ? null : FamilyInstanceTable._(vimNode, scene.strings);
  }

  static NodeTable? _getNodeTable(VimScene scene) {
    final vimNode = scene.entities["Vim.Node"];
    return vimNode == null ? null : NodeTable._(vimNode, scene.strings);
  }

  DocumentModel(VimScene scene)
      : displayUnit = _getDisplayUnitTable(scene),
        parameterDescriptor = _getParameterDescriptorTable(scene),
        parameter = _getParameterTable(scene),
        element = _getElementTable(scene),
        level = _getLevelTable(scene),
        room = _getRoomTable(scene),
        bimDocument = _getBimDocumentTable(scene),
        category = _getCategoryTable(scene),
        family = _getFamilyTable(scene),
        familyType = _getFamilyTypeTable(scene),
        familyInstance = _getFamilyInstanceTable(scene),
        node = _getNodeTable(scene);
}

abstract class _DocumentModelTable {
  final EntityTable _entityTable;
  final List<String> _strings;

  String? _getStringByIndex(List<int>? data, int index) {
    if (data == null || data.isEmpty) return null;
    return _strings[data.length == 1 ? data.first : data[index]];
  }

  bool? _getBoolByIndex(List<int>? data, int index) {
    if (data == null || data.isEmpty) return null;
    return data.length == 1 ? data.first != 0 : data[index] != 0;
  }

  T? _getNumByIndex<T extends num>(List<T>? data, int index) {
    if (data == null || data.isEmpty) return null;
    return data.length == 1 ? data.first : data[index];
  }

  _DocumentModelTable(this._entityTable, this._strings);

  int countBy(String column) {
    return _entityTable.columnDataCount(column);
  }

  String? getStringColumnValue(String column, int index) {
    final data = _entityTable.columnData<int>(column, index: index);
    return _getStringByIndex(data, index);
  }

  bool? getBoolColumnValue(String column, int index) {
    final data = _entityTable.columnData<int>(column, index: index);
    return _getBoolByIndex(data, index);
  }

  T? getNumColumnValue<T extends num>(String column, int index) {
    final data = _entityTable.columnData<T>(column, index: index);
    return _getNumByIndex<T>(data, index);
  }

  List<String> getStringColumn(String column) {
    final data = _entityTable.columnData<int>(column);
    if (data == null || data.isEmpty) return [];
    return data.map((index) => _strings[index]).toList();
  }

  List<bool> getBoolColumn(String column) {
    final data = _entityTable.columnData<int>(column);
    if (data == null || data.isEmpty) return [];
    return data.map((i) => i != 0).toList();
  }

  List<T> getNumColumn<T extends num>(String column) {
    final data = _entityTable.columnData<T>(column);
    if (data == null || data.isEmpty) return [];
    return data;
  }
}

class DisplayUnit {
  final int index;
  final String? spec;
  final String? type;
  final String? label;

  const DisplayUnit({
    required this.index,
    required this.spec,
    required this.type,
    required this.label,
  });
}

class DisplayUnitTable extends _DocumentModelTable {
  DisplayUnitTable._(super._entityTable, super._strings);

  int count() => countBy("string:Spec");

  DisplayUnit get(int displayUnitIndex) {
    return DisplayUnit(
      index: displayUnitIndex,
      spec: getSpec(displayUnitIndex),
      type: getType(displayUnitIndex),
      label: getLabel(displayUnitIndex),
    );
  }

  List<DisplayUnit> getAll() {
    final specData = _entityTable.columnData<int>("string:Spec");
    final typeData = _entityTable.columnData<int>("string:Type");
    final labelData = _entityTable.columnData<int>("string:Label");

    final int length = count();
    final displayUnit = <DisplayUnit>[];
    for (int i = 0; i < length; ++i) {
      displayUnit.add(DisplayUnit(
        index: i,
        spec: _getStringByIndex(specData, i),
        type: _getStringByIndex(typeData, i),
        label: _getStringByIndex(labelData, i),
      ));
    }
    return displayUnit;
  }

  String? getSpec(int displayUnitIndex) => getStringColumnValue("string:Spec", displayUnitIndex);
  List<String> getAllSpec() => getStringColumn("string:Spec");

  String? getType(int displayUnitIndex) => getStringColumnValue("string:Type", displayUnitIndex);
  List<String> getAllType() => getStringColumn("string:Type");

  String? getLabel(int displayUnitIndex) => getStringColumnValue("string:Label", displayUnitIndex);
  List<String> getAllLabel() => getStringColumn("string:Label");
}

class ParameterDescriptor {
  final int index;
  final String? name;
  final String? group;
  final String? parameterType;
  final bool? isInstance;
  final bool? isShared;
  final bool? isReadOnly;
  final int? flags;
  final String? guid;

  final int displayUnitIndex;

  ParameterDescriptor({
    required this.index,
    required this.name,
    required this.group,
    required this.parameterType,
    required this.isInstance,
    required this.isShared,
    required this.isReadOnly,
    required this.flags,
    required this.guid,
    required this.displayUnitIndex,
  });
  //final DisplayUnit? mDisplayUnit;
}

class ParameterDescriptorTable extends _DocumentModelTable {
  ParameterDescriptorTable._(super._entityTable, super._strings);

  int count() => countBy("string:Name");

  ParameterDescriptor get(int parameterDescriptorIndex) {
    return ParameterDescriptor(
      index: parameterDescriptorIndex,
      name: getName(parameterDescriptorIndex),
      group: getGroup(parameterDescriptorIndex),
      parameterType: getParameterType(parameterDescriptorIndex),
      isInstance: getIsInstance(parameterDescriptorIndex),
      isShared: getIsShared(parameterDescriptorIndex),
      isReadOnly: getIsReadOnly(parameterDescriptorIndex),
      flags: getFlags(parameterDescriptorIndex),
      guid: getGuid(parameterDescriptorIndex),
      displayUnitIndex: getDisplayUnitIndex(parameterDescriptorIndex),
    );
  }

  // List<ParameterDescriptor> getAll()
  // {
  //     bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
  //     bool existsGroup = mEntityTable.mStringColumns.find("string:Group") == mEntityTable.mStringColumns.end();
  //     bool existsParameterType = mEntityTable.mStringColumns.find("string:ParameterType") == mEntityTable.mStringColumns.end();
  //     bool existsIsInstance = _entityTable.columns("byte:IsInstance");
  //     bool existsIsShared = _entityTable.columns("byte:IsShared");
  //     bool existsIsReadOnly = _entityTable.columns("byte:IsReadOnly");
  //     bool existsFlags = _entityTable.columns("int:Flags");
  //     bool existsGuid = mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end();
  //     bool existsDisplayUnit = _entityTable.columns("index:Vim.DisplayUnit:DisplayUnit");

  //     final int length = count();

  //     List<ParameterDescriptor> parameterDescriptor = new List<ParameterDescriptor>();
  //     parameterDescriptor->reserve(count);

  //     final List<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : List<int>();

  //     final List<int>& groupData = existsGroup ? mEntityTable.mStringColumns["string:Group"] : List<int>();

  //     final List<int>& parameterTypeData = existsParameterType ? mEntityTable.mStringColumns["string:ParameterType"] : List<int>();

  //     bfast::byte isInstanceData = new bfast::byte[count];
  //     if (existsIsInstance) memcpy(isInstanceData, mEntityTable.mDataColumns["byte:IsInstance"].begin(), count  sizeof(bfast::byte));

  //     bfast::byte isSharedData = new bfast::byte[count];
  //     if (existsIsShared) memcpy(isSharedData, mEntityTable.mDataColumns["byte:IsShared"].begin(), count  sizeof(bfast::byte));

  //     bfast::byte isReadOnlyData = new bfast::byte[count];
  //     if (existsIsReadOnly) memcpy(isReadOnlyData, mEntityTable.mDataColumns["byte:IsReadOnly"].begin(), count  sizeof(bfast::byte));

  //     int flagsData = new int[count];
  //     if (existsFlags) memcpy(flagsData, mEntityTable.mDataColumns["int:Flags"].begin(), count  sizeof(int));

  //     final List<int>& guidData = existsGuid ? mEntityTable.mStringColumns["string:Guid"] : List<int>();

  //     final List<int>& displayUnitData = existsDisplayUnit ? mEntityTable.mIndexColumns["index:Vim.DisplayUnit:DisplayUnit"] : List<int>();

  //     for (int i = 0; i < length; ++i)
  //     {
  //         ParameterDescriptor entity;
  //         entity.mIndex = i;
  //         if (existsName)
  //             entity.mName = &mStrings[nameData[i]];
  //         if (existsGroup)
  //             entity.mGroup = &mStrings[groupData[i]];
  //         if (existsParameterType)
  //             entity.mParameterType = &mStrings[parameterTypeData[i]];
  //         if (existsIsInstance)
  //             entity.mIsInstance = isInstanceData[i];
  //         if (existsIsShared)
  //             entity.mIsShared = isSharedData[i];
  //         if (existsIsReadOnly)
  //             entity.mIsReadOnly = isReadOnlyData[i];
  //         if (existsFlags)
  //             entity.mFlags = flagsData[i];
  //         if (existsGuid)
  //             entity.mGuid = &mStrings[guidData[i]];
  //         entity.mDisplayUnitIndex = existsDisplayUnit ? displayUnitData[i] : -1;
  //         parameterDescriptor->push_back(entity);
  //     }

  //     delete[] isInstanceData;
  //     delete[] isSharedData;
  //     delete[] isReadOnlyData;
  //     delete[] flagsData;

  //     return parameterDescriptor;
  // }

  String? getName(int parameterDescriptorIndex) => getStringColumnValue("string:Name", parameterDescriptorIndex);
  List<String> getAllName() => getStringColumn("string:Name");

  String? getGroup(int parameterDescriptorIndex) => getStringColumnValue("string:Group", parameterDescriptorIndex);
  List<String> getAllGroup() => getStringColumn("string:Group");

  String? getParameterType(int parameterDescriptorIndex) => getStringColumnValue("string:ParameterType", parameterDescriptorIndex);
  List<String> getAllParameterType() => getStringColumn("string:ParameterType");

  bool? getIsInstance(int parameterDescriptorIndex) => getBoolColumnValue("byte:IsInstance", parameterDescriptorIndex);
  List<bool> getAllIsInstance() => getBoolColumn("byte:IsInstance");

  bool? getIsShared(int parameterDescriptorIndex) => getBoolColumnValue("byte:IsShared", parameterDescriptorIndex);
  List<bool> getAllIsShared() => getBoolColumn("byte:IsShared");

  bool? getIsReadOnly(int parameterDescriptorIndex) => getBoolColumnValue("byte:IsReadOnly", parameterDescriptorIndex);
  List<bool> getAllIsReadOnly() => getBoolColumn("byte:IsReadOnly");

  int? getFlags(int parameterDescriptorIndex) => getNumColumnValue("int:Flags", parameterDescriptorIndex);
  List<int> getAllFlags() => getNumColumn<int>("int:Flags");

  String? getGuid(int parameterDescriptorIndex) => getStringColumnValue("string:Guid", parameterDescriptorIndex);
  List<String> getAllGuid() => getStringColumn("string:Guid");

  int getDisplayUnitIndex(int parameterDescriptorIndex) => getNumColumnValue("index:Vim.DisplayUnit:DisplayUnit", parameterDescriptorIndex) ?? -1;
}

class Parameter {
  final int index;
  final String? value;

  final int parameterDescriptorIndex;
  // final ParameterDescriptor? parameterDescriptor;
  final int elementIndex;
  // final Element? element;

  Parameter({
    required this.index,
    required this.value,
    required this.parameterDescriptorIndex,
    required this.elementIndex,
  });
}

class ParameterTable extends _DocumentModelTable {
  ParameterTable._(super._entityTable, super._strings);

  int count() => countBy("string:Value");

  Parameter get(int parameterIndex) {
    return Parameter(
      index: parameterIndex,
      value: getValue(parameterIndex),
      parameterDescriptorIndex: getParameterDescriptorIndex(parameterIndex),
      elementIndex: getElementIndex(parameterIndex),
    );
  }

  List<Parameter> getAll() {
    final columnValue = _entityTable.columnData<int>("string:Value");
    final columnParameterDescriptor = _entityTable.columnData<int>("index:Vim.ParameterDescriptor:ParameterDescriptor");
    final columnElement = _entityTable.columnData<int>("index:Vim.Element:Element");

    final int length = count();

    final parameter = <Parameter>[];
    for (int i = 0; i < length; ++i) {
      parameter.add(Parameter(
        index: i,
        value: _getStringByIndex(columnValue, i),
        parameterDescriptorIndex: _getNumByIndex(columnParameterDescriptor, i) ?? -1,
        elementIndex: _getNumByIndex(columnElement, i) ?? -1,
      ));
    }
    return parameter;
  }

  String? getValue(int parameterIndex) => getStringColumnValue("string:Value", parameterIndex);
  List<String> getAllValue() => getStringColumn("string:Value");

  int getParameterDescriptorIndex(int parameterIndex) => getNumColumnValue("index:Vim.ParameterDescriptor:ParameterDescriptor", parameterIndex) ?? -1;
  int getElementIndex(int parameterIndex) => getNumColumnValue("index:Vim.Element:Element", parameterIndex) ?? -1;
}

class Element {
  final int index;
  final int? id;
  final String? type;
  final String? name;
  final String? uniqueId;
  final Vector3? location;
  final String? familyName;
  final bool? isPinned;

  final int levelIndex;
  // final Level? mLevel;
  final int phaseCreatedIndex;
  // final Phase? mPhaseCreated;
  final int phaseDemolishedIndex;
  // final Phase? mPhaseDemolished;
  final int categoryIndex;
  // final Category? mCategory;
  final int worksetIndex;
  // final Workset? mWorkset;
  final int designOptionIndex;
  // final DesignOption? mDesignOption;
  final int ownerViewIndex;
  // final View? mOwnerView;
  final int groupIndex;
  // final Group? mGroup;
  final int assemblyInstanceIndex;
  // final AssemblyInstance? mAssemblyInstance;
  final int bimDocumentIndex;
  // final BimDocument? mBimDocument;
  final int roomIndex;

  Element({
    required this.index,
    required this.id,
    required this.type,
    required this.name,
    required this.uniqueId,
    required this.location,
    required this.familyName,
    required this.isPinned,
    required this.levelIndex,
    required this.phaseCreatedIndex,
    required this.phaseDemolishedIndex,
    required this.categoryIndex,
    required this.worksetIndex,
    required this.designOptionIndex,
    required this.ownerViewIndex,
    required this.groupIndex,
    required this.assemblyInstanceIndex,
    required this.bimDocumentIndex,
    required this.roomIndex,
  });
  // final Room? mRoom;
}

class ElementTable extends _DocumentModelTable {
  ElementTable._(super._entityTable, super._strings);

  int count() => countBy("int:Id");

  Element get(int elementIndex) {
    return Element(
      index: elementIndex,
      id: getId(elementIndex),
      type: getType(elementIndex),
      name: getName(elementIndex),
      uniqueId: getUniqueId(elementIndex),
      location: getLocation(elementIndex),
      familyName: getFamilyName(elementIndex),
      isPinned: getIsPinned(elementIndex),
      levelIndex: getLevelIndex(elementIndex),
      phaseCreatedIndex: getPhaseCreatedIndex(elementIndex),
      phaseDemolishedIndex: getPhaseDemolishedIndex(elementIndex),
      categoryIndex: getCategoryIndex(elementIndex),
      worksetIndex: getWorksetIndex(elementIndex),
      designOptionIndex: getDesignOptionIndex(elementIndex),
      ownerViewIndex: getOwnerViewIndex(elementIndex),
      groupIndex: getGroupIndex(elementIndex),
      assemblyInstanceIndex: getAssemblyInstanceIndex(elementIndex),
      bimDocumentIndex: getBimDocumentIndex(elementIndex),
      roomIndex: getRoomIndex(elementIndex),
    );
  }

//     List<Element> getAll()
//     {
//         bool existsId = _entityTable.columns("int:Id");
//         bool existsType = mEntityTable.mStringColumns.find("string:Type") == mEntityTable.mStringColumns.end();
//         bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
//         bool existsUniqueId = mEntityTable.mStringColumns.find("string:UniqueId") == mEntityTable.mStringColumns.end();
//         bool existsLocationX = _entityTable.columns("float:Location.X");
//         bool existsLocationY = _entityTable.columns("float:Location.Y");
//         bool existsLocationZ = _entityTable.columns("float:Location.Z");
//         bool existsFamilyName = mEntityTable.mStringColumns.find("string:FamilyName") == mEntityTable.mStringColumns.end();
//         bool existsIsPinned = _entityTable.columns("byte:IsPinned");
//         bool existsLevel = _entityTable.columns("index:Vim.Level:Level");
//         bool existsPhaseCreated = _entityTable.columns("index:Vim.Phase:PhaseCreated");
//         bool existsPhaseDemolished = _entityTable.columns("index:Vim.Phase:PhaseDemolished");
//         bool existsCategory = _entityTable.columns("index:Vim.Category:Category");
//         bool existsWorkset = _entityTable.columns("index:Vim.Workset:Workset");
//         bool existsDesignOption = _entityTable.columns("index:Vim.DesignOption:DesignOption");
//         bool existsOwnerView = _entityTable.columns("index:Vim.View:OwnerView");
//         bool existsGroup = _entityTable.columns("index:Vim.Group:Group");
//         bool existsAssemblyInstance = _entityTable.columns("index:Vim.AssemblyInstance:AssemblyInstance");
//         bool existsBimDocument = _entityTable.columns("index:Vim.BimDocument:BimDocument");
//         bool existsRoom = _entityTable.columns("index:Vim.Room:Room");

//         final int length = count();

//         List<Element> element = new List<Element>();
//         element->reserve(count);

//         int idData = new int[count];
//         if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count  sizeof(int));

//         final List<int>& typeData = existsType ? mEntityTable.mStringColumns["string:Type"] : List<int>();

//         final List<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : List<int>();

//         final List<int>& uniqueIdData = existsUniqueId ? mEntityTable.mStringColumns["string:UniqueId"] : List<int>();

//         Vector3Converter locationConverter;
//         ByteRangePtr locationData = new ByteRangePtr[locationConverter.getSize()];
//         if (existsLocationX && existsLocationY && existsLocationZ) for (int i = 0; i < locationConverter.getSize(); i++)
//             locationData[i] = &mEntityTable.mDataColumns["float:Location" + locationConverter.getColumns()[i]];

//         final List<int>& familyNameData = existsFamilyName ? mEntityTable.mStringColumns["string:FamilyName"] : List<int>();

//         bfast::byte isPinnedData = new bfast::byte[count];
//         if (existsIsPinned) memcpy(isPinnedData, mEntityTable.mDataColumns["byte:IsPinned"].begin(), count  sizeof(bfast::byte));

//         final List<int>& levelData = existsLevel ? mEntityTable.mIndexColumns["index:Vim.Level:Level"] : List<int>();
//         final List<int>& phaseCreatedData = existsPhaseCreated ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseCreated"] : List<int>();
//         final List<int>& phaseDemolishedData = existsPhaseDemolished ? mEntityTable.mIndexColumns["index:Vim.Phase:PhaseDemolished"] : List<int>();
//         final List<int>& categoryData = existsCategory ? mEntityTable.mIndexColumns["index:Vim.Category:Category"] : List<int>();
//         final List<int>& worksetData = existsWorkset ? mEntityTable.mIndexColumns["index:Vim.Workset:Workset"] : List<int>();
//         final List<int>& designOptionData = existsDesignOption ? mEntityTable.mIndexColumns["index:Vim.DesignOption:DesignOption"] : List<int>();
//         final List<int>& ownerViewData = existsOwnerView ? mEntityTable.mIndexColumns["index:Vim.View:OwnerView"] : List<int>();
//         final List<int>& groupData = existsGroup ? mEntityTable.mIndexColumns["index:Vim.Group:Group"] : List<int>();
//         final List<int>& assemblyInstanceData = existsAssemblyInstance ? mEntityTable.mIndexColumns["index:Vim.AssemblyInstance:AssemblyInstance"] : List<int>();
//         final List<int>& bimDocumentData = existsBimDocument ? mEntityTable.mIndexColumns["index:Vim.BimDocument:BimDocument"] : List<int>();
//         final List<int>& roomData = existsRoom ? mEntityTable.mIndexColumns["index:Vim.Room:Room"] : List<int>();

//         for (int i = 0; i < length; ++i)
//         {
//             Element entity;
//             entity.mIndex = i;
//             if (existsId)
//                 entity.mId = idData[i];
//             if (existsType)
//                 entity.mType = &mStrings[typeData[i]];
//             if (existsName)
//                 entity.mName = &mStrings[nameData[i]];
//             if (existsUniqueId)
//                 entity.mUniqueId = &mStrings[uniqueIdData[i]];
//             if (existsLocationX && existsLocationY && existsLocationZ)
//                 locationConverter.ConvertFromColumns(&entity.mLocation, locationData, i);
//             if (existsFamilyName)
//                 entity.mFamilyName = &mStrings[familyNameData[i]];
//             if (existsIsPinned)
//                 entity.mIsPinned = isPinnedData[i];
//             entity.mLevelIndex = existsLevel ? levelData[i] : -1;
//             entity.mPhaseCreatedIndex = existsPhaseCreated ? phaseCreatedData[i] : -1;
//             entity.mPhaseDemolishedIndex = existsPhaseDemolished ? phaseDemolishedData[i] : -1;
//             entity.mCategoryIndex = existsCategory ? categoryData[i] : -1;
//             entity.mWorksetIndex = existsWorkset ? worksetData[i] : -1;
//             entity.mDesignOptionIndex = existsDesignOption ? designOptionData[i] : -1;
//             entity.mOwnerViewIndex = existsOwnerView ? ownerViewData[i] : -1;
//             entity.mGroupIndex = existsGroup ? groupData[i] : -1;
//             entity.mAssemblyInstanceIndex = existsAssemblyInstance ? assemblyInstanceData[i] : -1;
//             entity.mBimDocumentIndex = existsBimDocument ? bimDocumentData[i] : -1;
//             entity.mRoomIndex = existsRoom ? roomData[i] : -1;
//             element->push_back(entity);
//         }

//         delete[] idData;
//         delete[] locationData;
//         delete[] isPinnedData;

//         return element;
//     }

  int? getId(int elementIndex) => getNumColumnValue("int:Id", elementIndex);
  List<int> getAllId() => getNumColumn("int:Id");

  String? getType(int elementIndex) => getStringColumnValue("string:Type", elementIndex);
  List<String> getAllType() => getStringColumn("string:Type");

  String? getName(int elementIndex) => getStringColumnValue("string:Name", elementIndex);
  List<String> getAllName() => getStringColumn("string:Name");

  String? getUniqueId(int elementIndex) => getStringColumnValue("string:UniqueId", elementIndex);
  List<String> getAllUniqueId() => getStringColumn("string:UniqueId");

  Vector3? getLocation(int elementIndex) {
    final x = getNumColumnValue<double>("float:Location.X", elementIndex);
    final y = getNumColumnValue<double>("float:Location.Y", elementIndex);
    final z = getNumColumnValue<double>("float:Location.Z", elementIndex);
    return (x != null && y != null && z != null) ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllLocation() {
    final x = getNumColumn<double>("float:Location.X");
    final y = getNumColumn<double>("float:Location.Y");
    final z = getNumColumn<double>("float:Location.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  String? getFamilyName(int elementIndex) => getStringColumnValue("string:FamilyName", elementIndex);
  List<String> getAllFamilyName() => getStringColumn("string:FamilyName");

  bool? getIsPinned(int elementIndex) => getBoolColumnValue("byte:IsPinned", elementIndex);
  List<bool> getAllIsPinned() => getBoolColumn("byte:IsPinned");

  int getLevelIndex(int elementIndex) => getNumColumnValue("index:Vim.Level:Level", elementIndex) ?? -1;
  int getPhaseCreatedIndex(int elementIndex) => getNumColumnValue("index:Vim.Phase:PhaseCreated", elementIndex) ?? -1;
  int getPhaseDemolishedIndex(int elementIndex) => getNumColumnValue("index:Vim.Phase:PhaseDemolished", elementIndex) ?? -1;
  int getCategoryIndex(int elementIndex) => getNumColumnValue("index:Vim.Category:Category", elementIndex) ?? -1;
  int getWorksetIndex(int elementIndex) => getNumColumnValue("index:Vim.Workset:Workset", elementIndex) ?? -1;
  int getDesignOptionIndex(int elementIndex) => getNumColumnValue("index:Vim.DesignOption:DesignOption", elementIndex) ?? -1;
  int getOwnerViewIndex(int elementIndex) => getNumColumnValue("index:Vim.View:OwnerView", elementIndex) ?? -1;
  int getGroupIndex(int elementIndex) => getNumColumnValue("index:Vim.Group:Group", elementIndex) ?? -1;
  int getAssemblyInstanceIndex(int elementIndex) => getNumColumnValue("index:Vim.AssemblyInstance:AssemblyInstance", elementIndex) ?? -1;
  int getBimDocumentIndex(int elementIndex) => getNumColumnValue("index:Vim.BimDocument:BimDocument", elementIndex) ?? -1;
  int getRoomIndex(int elementIndex) => getNumColumnValue("index:Vim.Room:Room", elementIndex) ?? -1;
}

class Level {
  final int index;
  final double? elevation;

  final int familyTypeIndex;
  // final FamilyType? mFamilyType;
  final int elementIndex;

  Level({
    required this.index,
    required this.elevation,
    required this.familyTypeIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class LevelTable extends _DocumentModelTable {
  LevelTable._(super._entityTable, super._strings);

  int count() => countBy("double:Elevation");

  Level get(int levelIndex) {
    return Level(
      index: levelIndex,
      elevation: getElevation(levelIndex),
      familyTypeIndex: getFamilyTypeIndex(levelIndex),
      elementIndex: getElementIndex(levelIndex),
    );
  }

//     List<Level> getAll()
//     {
//         bool existsElevation = _entityTable.columns("double:Elevation");
//         bool existsFamilyType = _entityTable.columns("index:Vim.FamilyType:FamilyType");
//         bool existsElement = _entityTable.columns("index:Vim.Element:Element");

//         final int length = count();

//         List<Level> level = new List<Level>();
//         level->reserve(count);

//         double elevationData = new double[count];
//         if (existsElevation) memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count  sizeof(double));

//         final List<int>& familyTypeData = existsFamilyType ? mEntityTable.mIndexColumns["index:Vim.FamilyType:FamilyType"] : List<int>();
//         final List<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : List<int>();

//         for (int i = 0; i < length; ++i)
//         {
//             Level entity;
//             entity.mIndex = i;
//             if (existsElevation)
//                 entity.mElevation = elevationData[i];
//             entity.mFamilyTypeIndex = existsFamilyType ? familyTypeData[i] : -1;
//             entity.mElementIndex = existsElement ? elementData[i] : -1;
//             level->push_back(entity);
//         }

//         delete[] elevationData;

//         return level;
//     }

  double? getElevation(int levelIndex) => getNumColumnValue<double>("double:Elevation", levelIndex);
  List<double> getAllElevation() => getNumColumn<double>("double:Elevation");

  int getFamilyTypeIndex(int levelIndex) => getNumColumnValue("index:Vim.FamilyType:FamilyType", levelIndex) ?? -1;
  int getElementIndex(int levelIndex) => getNumColumnValue("index:Vim.Element:Element", levelIndex) ?? -1;
}

class Room {
  final int index;
  final double? baseOffset;
  final double? limitOffset;
  final double? unboundedHeight;
  final double? volume;
  final double? perimeter;
  final double? area;
  final String? number;

  final int upperLimitIndex;
  // final Level? mUpperLimit;
  final int elementIndex;

  Room({
    required this.index,
    required this.baseOffset,
    required this.limitOffset,
    required this.unboundedHeight,
    required this.volume,
    required this.perimeter,
    required this.area,
    required this.number,
    required this.upperLimitIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class RoomTable extends _DocumentModelTable {
  RoomTable._(super._entityTable, super._strings);

  int count() => countBy("double:BaseOffset");

  Room get(int roomIndex) {
    return Room(
      index: roomIndex,
      baseOffset: getBaseOffset(roomIndex),
      limitOffset: getLimitOffset(roomIndex),
      unboundedHeight: getUnboundedHeight(roomIndex),
      volume: getVolume(roomIndex),
      perimeter: getPerimeter(roomIndex),
      area: getArea(roomIndex),
      number: getNumber(roomIndex),
      upperLimitIndex: getUpperLimitIndex(roomIndex),
      elementIndex: getElementIndex(roomIndex),
    );
  }

//     List<Room> getAll()
//     {
//         bool existsBaseOffset = _entityTable.columns("double:BaseOffset");
//         bool existsLimitOffset = _entityTable.columns("double:LimitOffset");
//         bool existsUnboundedHeight = _entityTable.columns("double:UnboundedHeight");
//         bool existsVolume = _entityTable.columns("double:Volume");
//         bool existsPerimeter = _entityTable.columns("double:Perimeter");
//         bool existsArea = _entityTable.columns("double:Area");
//         bool existsNumber = mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end();
//         bool existsUpperLimit = _entityTable.columns("index:Vim.Level:UpperLimit");
//         bool existsElement = _entityTable.columns("index:Vim.Element:Element");

//         final int length = count();

//         List<Room> room = new List<Room>();
//         room->reserve(count);

//         double baseOffsetData = new double[count];
//         if (existsBaseOffset) memcpy(baseOffsetData, mEntityTable.mDataColumns["double:BaseOffset"].begin(), count  sizeof(double));

//         double limitOffsetData = new double[count];
//         if (existsLimitOffset) memcpy(limitOffsetData, mEntityTable.mDataColumns["double:LimitOffset"].begin(), count  sizeof(double));

//         double unboundedHeightData = new double[count];
//         if (existsUnboundedHeight) memcpy(unboundedHeightData, mEntityTable.mDataColumns["double:UnboundedHeight"].begin(), count  sizeof(double));

//         double volumeData = new double[count];
//         if (existsVolume) memcpy(volumeData, mEntityTable.mDataColumns["double:Volume"].begin(), count  sizeof(double));

//         double perimeterData = new double[count];
//         if (existsPerimeter) memcpy(perimeterData, mEntityTable.mDataColumns["double:Perimeter"].begin(), count  sizeof(double));

//         double areaData = new double[count];
//         if (existsArea) memcpy(areaData, mEntityTable.mDataColumns["double:Area"].begin(), count  sizeof(double));

//         final List<int>& numberData = existsNumber ? mEntityTable.mStringColumns["string:Number"] : List<int>();

//         final List<int>& upperLimitData = existsUpperLimit ? mEntityTable.mIndexColumns["index:Vim.Level:UpperLimit"] : List<int>();
//         final List<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : List<int>();

//         for (int i = 0; i < length; ++i)
//         {
//             Room entity;
//             entity.mIndex = i;
//             if (existsBaseOffset)
//                 entity.mBaseOffset = baseOffsetData[i];
//             if (existsLimitOffset)
//                 entity.mLimitOffset = limitOffsetData[i];
//             if (existsUnboundedHeight)
//                 entity.mUnboundedHeight = unboundedHeightData[i];
//             if (existsVolume)
//                 entity.mVolume = volumeData[i];
//             if (existsPerimeter)
//                 entity.mPerimeter = perimeterData[i];
//             if (existsArea)
//                 entity.mArea = areaData[i];
//             if (existsNumber)
//                 entity.mNumber = &mStrings[numberData[i]];
//             entity.mUpperLimitIndex = existsUpperLimit ? upperLimitData[i] : -1;
//             entity.mElementIndex = existsElement ? elementData[i] : -1;
//             room->push_back(entity);
//         }

//         delete[] baseOffsetData;
//         delete[] limitOffsetData;
//         delete[] unboundedHeightData;
//         delete[] volumeData;
//         delete[] perimeterData;
//         delete[] areaData;

//         return room;
//     }

  double? getBaseOffset(int roomIndex) => getNumColumnValue<double>("double:BaseOffset", roomIndex);
  List<double> getAllBaseOffset() => getNumColumn<double>("double:BaseOffset");

  double? getLimitOffset(int roomIndex) => getNumColumnValue<double>("double:LimitOffset", roomIndex);
  List<double> getAllLimitOffset() => getNumColumn<double>("double:LimitOffset");

  double? getUnboundedHeight(int roomIndex) => getNumColumnValue<double>("double:UnboundedHeight", roomIndex);
  List<double> getAllUnboundedHeight() => getNumColumn<double>("double:UnboundedHeight");

  double? getVolume(int roomIndex) => getNumColumnValue<double>("double:Volume", roomIndex);
  List<double> getAllVolume() => getNumColumn<double>("double:Volume");

  double? getPerimeter(int roomIndex) => getNumColumnValue<double>("double:Perimeter", roomIndex);
  List<double> getAllPerimeter() => getNumColumn<double>("double:Perimeter");

  double? getArea(int roomIndex) => getNumColumnValue<double>("double:Area", roomIndex);
  List<double> getAllArea() => getNumColumn<double>("double:Area");

  String? getNumber(int roomIndex) => getStringColumnValue("string:Number", roomIndex);
  List<String> getAllNumber() => getStringColumn("string:Number");

  int getUpperLimitIndex(int roomIndex) => getNumColumnValue("index:Vim.Level:UpperLimit", roomIndex) ?? -1;
  int getElementIndex(int roomIndex) => getNumColumnValue("index:Vim.Element:Element", roomIndex) ?? -1;
}

class BimDocument {
  final int index;
  final String? title;
  final bool? isMetric;
  final String? guid;
  final int? numSaves;
  final bool? isLinked;
  final bool? isDetached;
  final bool? isWorkshared;
  final String? pathName;
  final double? latitude;
  final double? longitude;
  final double? timeZone;
  final String? placeName;
  final String? weatherStationName;
  final double? elevation;
  final String? projectLocation;
  final String? issueDate;
  final String? status;
  final String? clientName;
  final String? address;
  final String? name;
  final String? number;
  final String? author;
  final String? buildingName;
  final String? organizationName;
  final String? organizationDescription;
  final String? product;
  final String? version;
  final String? user;

  final int activeViewIndex;
  // final View? mActiveView;
  final int ownerFamilyIndex;
  // final Family? mOwnerFamily;
  final int parentIndex;
  // final BimDocument? mParent;
  final int elementIndex;

  BimDocument({
    required this.index,
    required this.title,
    required this.isMetric,
    required this.guid,
    required this.numSaves,
    required this.isLinked,
    required this.isDetached,
    required this.isWorkshared,
    required this.pathName,
    required this.latitude,
    required this.longitude,
    required this.timeZone,
    required this.placeName,
    required this.weatherStationName,
    required this.elevation,
    required this.projectLocation,
    required this.issueDate,
    required this.status,
    required this.clientName,
    required this.address,
    required this.name,
    required this.number,
    required this.author,
    required this.buildingName,
    required this.organizationName,
    required this.organizationDescription,
    required this.product,
    required this.version,
    required this.user,
    required this.activeViewIndex,
    required this.ownerFamilyIndex,
    required this.parentIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class BimDocumentTable extends _DocumentModelTable {
  BimDocumentTable._(super._entityTable, super._strings);

  int count() => countBy("string:Title");

  BimDocument get(int bimDocumentIndex) {
    return BimDocument(
      index: bimDocumentIndex,
      title: getTitle(bimDocumentIndex),
      isMetric: getIsMetric(bimDocumentIndex),
      guid: getGuid(bimDocumentIndex),
      numSaves: getNumSaves(bimDocumentIndex),
      isLinked: getIsLinked(bimDocumentIndex),
      isDetached: getIsDetached(bimDocumentIndex),
      isWorkshared: getIsWorkshared(bimDocumentIndex),
      pathName: getPathName(bimDocumentIndex),
      latitude: getLatitude(bimDocumentIndex),
      longitude: getLongitude(bimDocumentIndex),
      timeZone: getTimeZone(bimDocumentIndex),
      placeName: getPlaceName(bimDocumentIndex),
      weatherStationName: getWeatherStationName(bimDocumentIndex),
      elevation: getElevation(bimDocumentIndex),
      projectLocation: getProjectLocation(bimDocumentIndex),
      issueDate: getIssueDate(bimDocumentIndex),
      status: getStatus(bimDocumentIndex),
      clientName: getClientName(bimDocumentIndex),
      address: getAddress(bimDocumentIndex),
      name: getName(bimDocumentIndex),
      number: getNumber(bimDocumentIndex),
      author: getAuthor(bimDocumentIndex),
      buildingName: getBuildingName(bimDocumentIndex),
      organizationName: getOrganizationName(bimDocumentIndex),
      organizationDescription: getOrganizationDescription(bimDocumentIndex),
      product: getProduct(bimDocumentIndex),
      version: getVersion(bimDocumentIndex),
      user: getUser(bimDocumentIndex),
      activeViewIndex: getActiveViewIndex(bimDocumentIndex),
      ownerFamilyIndex: getOwnerFamilyIndex(bimDocumentIndex),
      parentIndex: getParentIndex(bimDocumentIndex),
      elementIndex: getElementIndex(bimDocumentIndex),
    );
  }

  // List<BimDocument> getAll() {
  // bool existsTitle = mEntityTable.mStringColumns.find("string:Title") == mEntityTable.mStringColumns.end();
  // bool existsIsMetric = _entityTable.columns("byte:IsMetric");
  // bool existsGuid = mEntityTable.mStringColumns.find("string:Guid") == mEntityTable.mStringColumns.end();
  // bool existsNumSaves = _entityTable.columns("int:NumSaves");
  // bool existsIsLinked = _entityTable.columns("byte:IsLinked");
  // bool existsIsDetached = _entityTable.columns("byte:IsDetached");
  // bool existsIsWorkshared = _entityTable.columns("byte:IsWorkshared");
  // bool existsPathName = mEntityTable.mStringColumns.find("string:PathName") == mEntityTable.mStringColumns.end();
  // bool existsLatitude = _entityTable.columns("double:Latitude");
  // bool existsLongitude = _entityTable.columns("double:Longitude");
  // bool existsTimeZone = _entityTable.columns("double:TimeZone");
  // bool existsPlaceName = mEntityTable.mStringColumns.find("string:PlaceName") == mEntityTable.mStringColumns.end();
  // bool existsWeatherStationName = mEntityTable.mStringColumns.find("string:WeatherStationName") == mEntityTable.mStringColumns.end();
  // bool existsElevation = _entityTable.columns("double:Elevation");
  // bool existsProjectLocation = mEntityTable.mStringColumns.find("string:ProjectLocation") == mEntityTable.mStringColumns.end();
  // bool existsIssueDate = mEntityTable.mStringColumns.find("string:IssueDate") == mEntityTable.mStringColumns.end();
  // bool existsStatus = mEntityTable.mStringColumns.find("string:Status") == mEntityTable.mStringColumns.end();
  // bool existsClientName = mEntityTable.mStringColumns.find("string:ClientName") == mEntityTable.mStringColumns.end();
  // bool existsAddress = mEntityTable.mStringColumns.find("string:Address") == mEntityTable.mStringColumns.end();
  // bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
  // bool existsNumber = mEntityTable.mStringColumns.find("string:Number") == mEntityTable.mStringColumns.end();
  // bool existsAuthor = mEntityTable.mStringColumns.find("string:Author") == mEntityTable.mStringColumns.end();
  // bool existsBuildingName = mEntityTable.mStringColumns.find("string:BuildingName") == mEntityTable.mStringColumns.end();
  // bool existsOrganizationName = mEntityTable.mStringColumns.find("string:OrganizationName") == mEntityTable.mStringColumns.end();
  // bool existsOrganizationDescription = mEntityTable.mStringColumns.find("string:OrganizationDescription") == mEntityTable.mStringColumns.end();
  // bool existsProduct = mEntityTable.mStringColumns.find("string:Product") == mEntityTable.mStringColumns.end();
  // bool existsVersion = mEntityTable.mStringColumns.find("string:Version") == mEntityTable.mStringColumns.end();
  // bool existsUser = mEntityTable.mStringColumns.find("string:User") == mEntityTable.mStringColumns.end();
  // bool existsActiveView = _entityTable.columns("index:Vim.View:ActiveView");
  // bool existsOwnerFamily = _entityTable.columns("index:Vim.Family:OwnerFamily");
  // bool existsParent = _entityTable.columns("index:Vim.BimDocument:Parent");
  // bool existsElement = _entityTable.columns("index:Vim.Element:Element");

  // final int length = count();

  // final bimDocument = <BimDocument>[];
  // bimDocument->reserve(count);

  // final List<int>& titleData = existsTitle ? mEntityTable.mStringColumns["string:Title"] : List<int>();

  // bfast::byte isMetricData = new bfast::byte[count];
  // if (existsIsMetric) memcpy(isMetricData, mEntityTable.mDataColumns["byte:IsMetric"].begin(), count  sizeof(bfast::byte));

  // final List<int>& guidData = existsGuid ? mEntityTable.mStringColumns["string:Guid"] : List<int>();

  // int numSavesData = new int[count];
  // if (existsNumSaves) memcpy(numSavesData, mEntityTable.mDataColumns["int:NumSaves"].begin(), count  sizeof(int));

  // bfast::byte isLinkedData = new bfast::byte[count];
  // if (existsIsLinked) memcpy(isLinkedData, mEntityTable.mDataColumns["byte:IsLinked"].begin(), count  sizeof(bfast::byte));

  // bfast::byte isDetachedData = new bfast::byte[count];
  // if (existsIsDetached) memcpy(isDetachedData, mEntityTable.mDataColumns["byte:IsDetached"].begin(), count  sizeof(bfast::byte));

  // bfast::byte isWorksharedData = new bfast::byte[count];
  // if (existsIsWorkshared) memcpy(isWorksharedData, mEntityTable.mDataColumns["byte:IsWorkshared"].begin(), count  sizeof(bfast::byte));

  // final List<int>& pathNameData = existsPathName ? mEntityTable.mStringColumns["string:PathName"] : List<int>();

  // double latitudeData = new double[count];
  // if (existsLatitude) memcpy(latitudeData, mEntityTable.mDataColumns["double:Latitude"].begin(), count  sizeof(double));

  // double longitudeData = new double[count];
  // if (existsLongitude) memcpy(longitudeData, mEntityTable.mDataColumns["double:Longitude"].begin(), count  sizeof(double));

  // double timeZoneData = new double[count];
  // if (existsTimeZone) memcpy(timeZoneData, mEntityTable.mDataColumns["double:TimeZone"].begin(), count  sizeof(double));

  // final List<int>& placeNameData = existsPlaceName ? mEntityTable.mStringColumns["string:PlaceName"] : List<int>();

  // final List<int>& weatherStationNameData = existsWeatherStationName ? mEntityTable.mStringColumns["string:WeatherStationName"] : List<int>();

  // double elevationData = new double[count];
  // if (existsElevation) memcpy(elevationData, mEntityTable.mDataColumns["double:Elevation"].begin(), count  sizeof(double));

  // final List<int>& projectLocationData = existsProjectLocation ? mEntityTable.mStringColumns["string:ProjectLocation"] : List<int>();

  // final List<int>& issueDateData = existsIssueDate ? mEntityTable.mStringColumns["string:IssueDate"] : List<int>();

  // final List<int>& statusData = existsStatus ? mEntityTable.mStringColumns["string:Status"] : List<int>();

  // final List<int>& clientNameData = existsClientName ? mEntityTable.mStringColumns["string:ClientName"] : List<int>();

  // final List<int>& addressData = existsAddress ? mEntityTable.mStringColumns["string:Address"] : List<int>();

  // final List<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : List<int>();

  // final List<int>& numberData = existsNumber ? mEntityTable.mStringColumns["string:Number"] : List<int>();

  // final List<int>& authorData = existsAuthor ? mEntityTable.mStringColumns["string:Author"] : List<int>();

  // final List<int>& buildingNameData = existsBuildingName ? mEntityTable.mStringColumns["string:BuildingName"] : List<int>();

  // final List<int>& organizationNameData = existsOrganizationName ? mEntityTable.mStringColumns["string:OrganizationName"] : List<int>();

  // final List<int>& organizationDescriptionData = existsOrganizationDescription ? mEntityTable.mStringColumns["string:OrganizationDescription"] : List<int>();

  // final List<int>& productData = existsProduct ? mEntityTable.mStringColumns["string:Product"] : List<int>();

  // final List<int>& versionData = existsVersion ? mEntityTable.mStringColumns["string:Version"] : List<int>();

  // final List<int>& userData = existsUser ? mEntityTable.mStringColumns["string:User"] : List<int>();

  // final List<int>& activeViewData = existsActiveView ? mEntityTable.mIndexColumns["index:Vim.View:ActiveView"] : List<int>();
  // final List<int>& ownerFamilyData = existsOwnerFamily ? mEntityTable.mIndexColumns["index:Vim.Family:OwnerFamily"] : List<int>();
  // final List<int>& parentData = existsParent ? mEntityTable.mIndexColumns["index:Vim.BimDocument:Parent"] : List<int>();
  // final List<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : List<int>();

  // for (int i = 0; i < length; ++i)
  // {
  //     BimDocument entity;
  //     entity.mIndex = i;
  //     if (existsTitle)
  //         entity.mTitle = &mStrings[titleData[i]];
  //     if (existsIsMetric)
  //         entity.mIsMetric = isMetricData[i];
  //     if (existsGuid)
  //         entity.mGuid = &mStrings[guidData[i]];
  //     if (existsNumSaves)
  //         entity.mNumSaves = numSavesData[i];
  //     if (existsIsLinked)
  //         entity.mIsLinked = isLinkedData[i];
  //     if (existsIsDetached)
  //         entity.mIsDetached = isDetachedData[i];
  //     if (existsIsWorkshared)
  //         entity.mIsWorkshared = isWorksharedData[i];
  //     if (existsPathName)
  //         entity.mPathName = &mStrings[pathNameData[i]];
  //     if (existsLatitude)
  //         entity.mLatitude = latitudeData[i];
  //     if (existsLongitude)
  //         entity.mLongitude = longitudeData[i];
  //     if (existsTimeZone)
  //         entity.mTimeZone = timeZoneData[i];
  //     if (existsPlaceName)
  //         entity.mPlaceName = &mStrings[placeNameData[i]];
  //     if (existsWeatherStationName)
  //         entity.mWeatherStationName = &mStrings[weatherStationNameData[i]];
  //     if (existsElevation)
  //         entity.mElevation = elevationData[i];
  //     if (existsProjectLocation)
  //         entity.mProjectLocation = &mStrings[projectLocationData[i]];
  //     if (existsIssueDate)
  //         entity.mIssueDate = &mStrings[issueDateData[i]];
  //     if (existsStatus)
  //         entity.mStatus = &mStrings[statusData[i]];
  //     if (existsClientName)
  //         entity.mClientName = &mStrings[clientNameData[i]];
  //     if (existsAddress)
  //         entity.mAddress = &mStrings[addressData[i]];
  //     if (existsName)
  //         entity.mName = &mStrings[nameData[i]];
  //     if (existsNumber)
  //         entity.mNumber = &mStrings[numberData[i]];
  //     if (existsAuthor)
  //         entity.mAuthor = &mStrings[authorData[i]];
  //     if (existsBuildingName)
  //         entity.mBuildingName = &mStrings[buildingNameData[i]];
  //     if (existsOrganizationName)
  //         entity.mOrganizationName = &mStrings[organizationNameData[i]];
  //     if (existsOrganizationDescription)
  //         entity.mOrganizationDescription = &mStrings[organizationDescriptionData[i]];
  //     if (existsProduct)
  //         entity.mProduct = &mStrings[productData[i]];
  //     if (existsVersion)
  //         entity.mVersion = &mStrings[versionData[i]];
  //     if (existsUser)
  //         entity.mUser = &mStrings[userData[i]];
  //     entity.mActiveViewIndex = existsActiveView ? activeViewData[i] : -1;
  //     entity.mOwnerFamilyIndex = existsOwnerFamily ? ownerFamilyData[i] : -1;
  //     entity.mParentIndex = existsParent ? parentData[i] : -1;
  //     entity.mElementIndex = existsElement ? elementData[i] : -1;
  //     bimDocument->push_back(entity);
  // }
  //   return bimDocument;
  // }

  String? getTitle(int bimDocumentIndex) => getStringColumnValue("string:Title", bimDocumentIndex);
  List<String> getAllTitle() => getStringColumn("string:Title");

  bool? getIsMetric(int bimDocumentIndex) => getBoolColumnValue("byte:IsMetric", bimDocumentIndex);
  List<bool> getAllIsMetric() => getBoolColumn("byte:IsMetric");

  String? getGuid(int bimDocumentIndex) => getStringColumnValue("string:Guid", bimDocumentIndex);
  List<String> getAllGuid() => getStringColumn("string:Guid");

  int? getNumSaves(int bimDocumentIndex) => getNumColumnValue<int>("int:NumSaves", bimDocumentIndex);
  List<int> getAllNumSaves() => getNumColumn<int>("int:NumSaves");

  bool? getIsLinked(int bimDocumentIndex) => getBoolColumnValue("byte:IsLinked", bimDocumentIndex);
  List<bool> getAllIsLinked() => getBoolColumn("byte:IsLinked");

  bool? getIsDetached(int bimDocumentIndex) => getBoolColumnValue("byte:IsDetached", bimDocumentIndex);
  List<bool> getAllIsDetached() => getBoolColumn("byte:IsDetached");

  bool? getIsWorkshared(int bimDocumentIndex) => getBoolColumnValue("byte:IsWorkshared", bimDocumentIndex);
  List<bool> getAllIsWorkshared() => getBoolColumn("byte:IsWorkshared");

  String? getPathName(int bimDocumentIndex) => getStringColumnValue("string:PathName", bimDocumentIndex);
  List<String> getAllPathName() => getStringColumn("string:PathName");

  double? getLatitude(int bimDocumentIndex) => getNumColumnValue<double>("double:Latitude", bimDocumentIndex);
  List<double> getAllLatitude() => getNumColumn<double>("double:Latitude");

  double? getLongitude(int bimDocumentIndex) => getNumColumnValue<double>("double:Longitude", bimDocumentIndex);
  List<double> getAllLongitude() => getNumColumn<double>("double:Longitude");

  double? getTimeZone(int bimDocumentIndex) => getNumColumnValue<double>("double:TimeZone", bimDocumentIndex);

  List<double> getAllTimeZone() => getNumColumn<double>("double:TimeZone");

  String? getPlaceName(int bimDocumentIndex) => getStringColumnValue("string:PlaceName", bimDocumentIndex);
  List<String> getAllPlaceName() => getStringColumn("string:PlaceName");

  String? getWeatherStationName(int bimDocumentIndex) => getStringColumnValue("string:WeatherStationName", bimDocumentIndex);
  List<String> getAllWeatherStationName() => getStringColumn("string:WeatherStationName");

  double? getElevation(int bimDocumentIndex) => getNumColumnValue<double>("double:Elevation", bimDocumentIndex);
  List<double> getAllElevation() => getNumColumn<double>("double:Elevation");

  String? getProjectLocation(int bimDocumentIndex) => getStringColumnValue("string:ProjectLocation", bimDocumentIndex);
  List<String> getAllProjectLocation() => getStringColumn("string:ProjectLocation");

  String? getIssueDate(int bimDocumentIndex) => getStringColumnValue("string:IssueDate", bimDocumentIndex);
  List<String> getAllIssueDate() => getStringColumn("string:IssueDate");

  String? getStatus(int bimDocumentIndex) => getStringColumnValue("string:Status", bimDocumentIndex);
  List<String> getAllStatus() => getStringColumn("string:Status");

  String? getClientName(int bimDocumentIndex) => getStringColumnValue("string:ClientName", bimDocumentIndex);
  List<String> getAllClientName() => getStringColumn("string:ClientName");

  String? getAddress(int bimDocumentIndex) => getStringColumnValue("string:Address", bimDocumentIndex);
  List<String> getAllAddress() => getStringColumn("string:Address");

  String? getName(int bimDocumentIndex) => getStringColumnValue("string:Name", bimDocumentIndex);
  List<String> getAllName() => getStringColumn("string:Name");

  String? getNumber(int bimDocumentIndex) => getStringColumnValue("string:Number", bimDocumentIndex);
  List<String> getAllNumber() => getStringColumn("string:Number");

  String? getAuthor(int bimDocumentIndex) => getStringColumnValue("string:Author", bimDocumentIndex);
  List<String> getAllAuthor() => getStringColumn("string:Author");

  String? getBuildingName(int bimDocumentIndex) => getStringColumnValue("string:BuildingName", bimDocumentIndex);
  List<String> getAllBuildingName() => getStringColumn("string:BuildingName");

  String? getOrganizationName(int bimDocumentIndex) => getStringColumnValue("string:OrganizationName", bimDocumentIndex);
  List<String> getAllOrganizationName() => getStringColumn("string:OrganizationName");

  String? getOrganizationDescription(int bimDocumentIndex) => getStringColumnValue("string:OrganizationDescription", bimDocumentIndex);
  List<String> getAllOrganizationDescription() => getStringColumn("string:OrganizationDescription");

  String? getProduct(int bimDocumentIndex) => getStringColumnValue("string:Product", bimDocumentIndex);
  List<String> getAllProduct() => getStringColumn("string:Product");

  String? getVersion(int bimDocumentIndex) => getStringColumnValue("string:Version", bimDocumentIndex);
  List<String> getAllVersion() => getStringColumn("string:Version");

  String? getUser(int bimDocumentIndex) => getStringColumnValue("string:User", bimDocumentIndex);
  List<String> getAllUser() => getStringColumn("string:User");

  int getActiveViewIndex(int bimDocumentIndex) => getNumColumnValue("index:Vim.View:ActiveView", bimDocumentIndex) ?? -1;
  int getOwnerFamilyIndex(int bimDocumentIndex) => getNumColumnValue("index:Vim.Family:OwnerFamily", bimDocumentIndex) ?? -1;
  int getParentIndex(int bimDocumentIndex) => getNumColumnValue("index:Vim.BimDocument:Parent", bimDocumentIndex) ?? -1;
  int getElementIndex(int bimDocumentIndex) => getNumColumnValue("index:Vim.Element:Element", bimDocumentIndex) ?? -1;
}

class Category {
  final int index;
  final String? name;
  final int? id;
  final String? categoryType;
  final Vector3? lineColor;
  final String? builtInCategory;

  final int parentIndex;
  // final Category? mParent;
  final int materialIndex;

  Category({
    required this.index,
    required this.name,
    required this.id,
    required this.categoryType,
    required this.lineColor,
    required this.builtInCategory,
    required this.parentIndex,
    required this.materialIndex,
  });
  // final Material? mMaterial;
}

class CategoryTable extends _DocumentModelTable {
  CategoryTable._(super._entityTable, super._strings);

  int count() => countBy("string:Name");

  Category get(int categoryIndex) {
    return Category(
      index: categoryIndex,
      name: getName(categoryIndex),
      id: getId(categoryIndex),
      categoryType: getCategoryType(categoryIndex),
      lineColor: getLineColor(categoryIndex),
      builtInCategory: getBuiltInCategory(categoryIndex),
      parentIndex: getParentIndex(categoryIndex),
      materialIndex: getMaterialIndex(categoryIndex),
    );
  }

//     List<Category> getAll()
//     {
//         bool existsName = mEntityTable.mStringColumns.find("string:Name") == mEntityTable.mStringColumns.end();
//         bool existsId = _entityTable.columns("int:Id");
//         bool existsCategoryType = mEntityTable.mStringColumns.find("string:CategoryType") == mEntityTable.mStringColumns.end();
//         bool existsLineColorX = _entityTable.columns("double:LineColor.X");
//         bool existsLineColorY = _entityTable.columns("double:LineColor.Y");
//         bool existsLineColorZ = _entityTable.columns("double:LineColor.Z");
//         bool existsBuiltInCategory = mEntityTable.mStringColumns.find("string:BuiltInCategory") == mEntityTable.mStringColumns.end();
//         bool existsParent = _entityTable.columns("index:Vim.Category:Parent");
//         bool existsMaterial = _entityTable.columns("index:Vim.Material:Material");

//         final int length = count();

//         List<Category> category = new List<Category>();
//         category->reserve(count);

//         final List<int>& nameData = existsName ? mEntityTable.mStringColumns["string:Name"] : List<int>();

//         int idData = new int[count];
//         if (existsId) memcpy(idData, mEntityTable.mDataColumns["int:Id"].begin(), count  sizeof(int));

//         final List<int>& categoryTypeData = existsCategoryType ? mEntityTable.mStringColumns["string:CategoryType"] : List<int>();

//         DVector3Converter lineColorConverter;
//         ByteRangePtr lineColorData = new ByteRangePtr[lineColorConverter.getSize()];
//         if (existsLineColorX && existsLineColorY && existsLineColorZ) for (int i = 0; i < lineColorConverter.getSize(); i++)
//             lineColorData[i] = &mEntityTable.mDataColumns["double:LineColor" + lineColorConverter.getColumns()[i]];

//         final List<int>& builtInCategoryData = existsBuiltInCategory ? mEntityTable.mStringColumns["string:BuiltInCategory"] : List<int>();

//         final List<int>& parentData = existsParent ? mEntityTable.mIndexColumns["index:Vim.Category:Parent"] : List<int>();
//         final List<int>& materialData = existsMaterial ? mEntityTable.mIndexColumns["index:Vim.Material:Material"] : List<int>();

//         for (int i = 0; i < length; ++i)
//         {
//             Category entity;
//             entity.mIndex = i;
//             if (existsName)
//                 entity.mName = &mStrings[nameData[i]];
//             if (existsId)
//                 entity.mId = idData[i];
//             if (existsCategoryType)
//                 entity.mCategoryType = &mStrings[categoryTypeData[i]];
//             if (existsLineColorX && existsLineColorY && existsLineColorZ)
//                 lineColorConverter.ConvertFromColumns(&entity.mLineColor, lineColorData, i);
//             if (existsBuiltInCategory)
//                 entity.mBuiltInCategory = &mStrings[builtInCategoryData[i]];
//             entity.mParentIndex = existsParent ? parentData[i] : -1;
//             entity.mMaterialIndex = existsMaterial ? materialData[i] : -1;
//             category->push_back(entity);
//         }

//         delete[] idData;
//         delete[] lineColorData;

//         return category;
//     }

  String? getName(int categoryIndex) => getStringColumnValue("string:Name", categoryIndex);
  List<String> getAllName() => getStringColumn("string:Name");

  int? getId(int categoryIndex) => getNumColumnValue("int:Id", categoryIndex);
  List<int> getAllId() => getNumColumn("int:Id");

  String? getCategoryType(int categoryIndex) => getStringColumnValue("string:CategoryType", categoryIndex);
  List<String> getAllCategoryType() => getStringColumn("string:CategoryType");

  Vector3? getLineColor(int categoryIndex) {
    final x = getNumColumnValue<double>("double:LineColor.X", categoryIndex);
    final y = getNumColumnValue<double>("double:LineColor.Y", categoryIndex);
    final z = getNumColumnValue<double>("double:LineColor.Z", categoryIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllLineColor() {
    final x = getNumColumn<double>("double:LineColor.X");
    final y = getNumColumn<double>("double:LineColor.Y");
    final z = getNumColumn<double>("double:LineColor.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  String? getBuiltInCategory(int categoryIndex) => getStringColumnValue("string:BuiltInCategory", categoryIndex);
  List<String> getAllBuiltInCategory() => getStringColumn("string:BuiltInCategory");

  int getParentIndex(int categoryIndex) => getNumColumnValue("index:Vim.Category:Parent", categoryIndex) ?? -1;
  int getMaterialIndex(int categoryIndex) => getNumColumnValue("index:Vim.Material:Material", categoryIndex) ?? -1;
}

class Family {
  final int index;
  final String? structuralMaterialType;
  final String? structuralSectionShape;
  final bool? isSystemFamily;
  final bool? isInPlace;

  final int familyCategoryIndex;
  // final Category? mFamilyCategory;
  final int elementIndex;

  const Family({
    required this.index,
    required this.structuralMaterialType,
    required this.structuralSectionShape,
    required this.isSystemFamily,
    required this.isInPlace,
    required this.familyCategoryIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class FamilyTable extends _DocumentModelTable {
  FamilyTable._(super._entityTable, super._strings);

  int count() => countBy("string:StructuralMaterialType");

  Family get(int familyIndex) {
    return Family(
      index: familyIndex,
      structuralMaterialType: getStructuralMaterialType(familyIndex),
      structuralSectionShape: getStructuralSectionShape(familyIndex),
      isSystemFamily: getIsSystemFamily(familyIndex),
      isInPlace: getIsInPlace(familyIndex),
      familyCategoryIndex: getFamilyCategoryIndex(familyIndex),
      elementIndex: getElementIndex(familyIndex),
    );
  }

//     List<Family> getAll()
//     {
//         bool existsStructuralMaterialType = mEntityTable.mStringColumns.find("string:StructuralMaterialType") == mEntityTable.mStringColumns.end();
//         bool existsStructuralSectionShape = mEntityTable.mStringColumns.find("string:StructuralSectionShape") == mEntityTable.mStringColumns.end();
//         bool existsIsSystemFamily = _entityTable.columns("byte:IsSystemFamily");
//         bool existsIsInPlace = _entityTable.columns("byte:IsInPlace");
//         bool existsFamilyCategory = _entityTable.columns("index:Vim.Category:FamilyCategory");
//         bool existsElement = _entityTable.columns("index:Vim.Element:Element");

//         final int length = count();

//         List<Family> family = new List<Family>();
//         family->reserve(count);

//         final List<int>& structuralMaterialTypeData = existsStructuralMaterialType ? mEntityTable.mStringColumns["string:StructuralMaterialType"] : List<int>();

//         final List<int>& structuralSectionShapeData = existsStructuralSectionShape ? mEntityTable.mStringColumns["string:StructuralSectionShape"] : List<int>();

//         bfast::byte isSystemFamilyData = new bfast::byte[count];
//         if (existsIsSystemFamily) memcpy(isSystemFamilyData, mEntityTable.mDataColumns["byte:IsSystemFamily"].begin(), count  sizeof(bfast::byte));

//         bfast::byte isInPlaceData = new bfast::byte[count];
//         if (existsIsInPlace) memcpy(isInPlaceData, mEntityTable.mDataColumns["byte:IsInPlace"].begin(), count  sizeof(bfast::byte));

//         final List<int>& familyCategoryData = existsFamilyCategory ? mEntityTable.mIndexColumns["index:Vim.Category:FamilyCategory"] : List<int>();
//         final List<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : List<int>();

//         for (int i = 0; i < length; ++i)
//         {
//             Family entity;
//             entity.mIndex = i;
//             if (existsStructuralMaterialType)
//                 entity.mStructuralMaterialType = &mStrings[structuralMaterialTypeData[i]];
//             if (existsStructuralSectionShape)
//                 entity.mStructuralSectionShape = &mStrings[structuralSectionShapeData[i]];
//             if (existsIsSystemFamily)
//                 entity.mIsSystemFamily = isSystemFamilyData[i];
//             if (existsIsInPlace)
//                 entity.mIsInPlace = isInPlaceData[i];
//             entity.mFamilyCategoryIndex = existsFamilyCategory ? familyCategoryData[i] : -1;
//             entity.mElementIndex = existsElement ? elementData[i] : -1;
//             family->push_back(entity);
//         }

//         delete[] isSystemFamilyData;
//         delete[] isInPlaceData;

//         return family;
//     }

  String? getStructuralMaterialType(int familyIndex) => getStringColumnValue("string:StructuralMaterialType", familyIndex);
  List<String> getAllStructuralMaterialType() => getStringColumn("string:StructuralMaterialType");

  String? getStructuralSectionShape(int familyIndex) => getStringColumnValue("string:StructuralSectionShape", familyIndex);
  List<String> getAllStructuralSectionShape() => getStringColumn("string:StructuralSectionShape");

  bool? getIsSystemFamily(int familyIndex) => getBoolColumnValue("byte:IsSystemFamily", familyIndex);
  List<bool> getAllIsSystemFamily() => getBoolColumn("byte:IsSystemFamily");

  bool? getIsInPlace(int familyIndex) => getBoolColumnValue("byte:IsInPlace", familyIndex);
  List<bool> getAllIsInPlace() => getBoolColumn("byte:IsInPlace");

  int getFamilyCategoryIndex(int familyIndex) => getNumColumnValue("index:Vim.Category:FamilyCategory", familyIndex) ?? -1;
  int getElementIndex(int familyIndex) => getNumColumnValue("index:Vim.Element:Element", familyIndex) ?? -1;
}

class FamilyType {
  final int index;
  final bool? isSystemFamilyType;

  final int familyIndex;
  // final Family? mFamily;
  final int compoundStructureIndex;
  // final CompoundStructure? mCompoundStructure;
  final int elementIndex;

  FamilyType({
    required this.index,
    required this.isSystemFamilyType,
    required this.familyIndex,
    required this.compoundStructureIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class FamilyTypeTable extends _DocumentModelTable {
  FamilyTypeTable._(super._entityTable, super._strings);

  int count() => countBy("byte:IsSystemFamilyType");

  FamilyType get(int familyTypeIndex) {
    return FamilyType(
      index: familyTypeIndex,
      isSystemFamilyType: getIsSystemFamilyType(familyTypeIndex),
      familyIndex: getFamilyIndex(familyTypeIndex),
      compoundStructureIndex: getCompoundStructureIndex(familyTypeIndex),
      elementIndex: getElementIndex(familyTypeIndex),
    );
  }

  // List<FamilyType> getAll()
  // {
  //     bool existsIsSystemFamilyType = _entityTable.columns("byte:IsSystemFamilyType");
  //     bool existsFamily = _entityTable.columns("index:Vim.Family:Family");
  //     bool existsCompoundStructure = _entityTable.columns("index:Vim.CompoundStructure:CompoundStructure");
  //     bool existsElement = _entityTable.columns("index:Vim.Element:Element");

  //     final int length = count();

  //     List<FamilyType> familyType = new List<FamilyType>();
  //     familyType->reserve(count);

  //     bfast::byte isSystemFamilyTypeData = new bfast::byte[count];
  //     if (existsIsSystemFamilyType) memcpy(isSystemFamilyTypeData, mEntityTable.mDataColumns["byte:IsSystemFamilyType"].begin(), count  sizeof(bfast::byte));

  //     final List<int>& familyData = existsFamily ? mEntityTable.mIndexColumns["index:Vim.Family:Family"] : List<int>();
  //     final List<int>& compoundStructureData = existsCompoundStructure ? mEntityTable.mIndexColumns["index:Vim.CompoundStructure:CompoundStructure"] : List<int>();
  //     final List<int>& elementData = existsElement ? mEntityTable.mIndexColumns["index:Vim.Element:Element"] : List<int>();

  //     for (int i = 0; i < length; ++i)
  //     {
  //         FamilyType entity;
  //         entity.mIndex = i;
  //         if (existsIsSystemFamilyType)
  //             entity.mIsSystemFamilyType = isSystemFamilyTypeData[i];
  //         entity.mFamilyIndex = existsFamily ? familyData[i] : -1;
  //         entity.mCompoundStructureIndex = existsCompoundStructure ? compoundStructureData[i] : -1;
  //         entity.mElementIndex = existsElement ? elementData[i] : -1;
  //         familyType->push_back(entity);
  //     }

  //     delete[] isSystemFamilyTypeData;

  //     return familyType;
  // }

  bool? getIsSystemFamilyType(int familyTypeIndex) => getBoolColumnValue("byte:IsSystemFamilyType", familyTypeIndex);
  List<bool> getAllIsSystemFamilyType() => getBoolColumn("byte:IsSystemFamilyType");

  int getFamilyIndex(int familyTypeIndex) => getNumColumnValue("index:Vim.Family:Family", familyTypeIndex) ?? -1;
  int getCompoundStructureIndex(int familyTypeIndex) => getNumColumnValue("index:Vim.CompoundStructure:CompoundStructure", familyTypeIndex) ?? -1;
  int getElementIndex(int familyTypeIndex) => getNumColumnValue("index:Vim.Element:Element", familyTypeIndex) ?? -1;
}

class FamilyInstance {
  final int index;
  final bool? facingFlipped;
  final Vector3? facingOrientation;
  final bool? handFlipped;
  final bool? mirrored;
  final bool? hasModifiedGeometry;
  final double? scale;
  final Vector3? basisX;
  final Vector3? basisY;
  final Vector3? basisZ;
  final Vector3? translation;
  final Vector3? handOrientation;

  final int familyTypeIndex;
  // final FamilyType? mFamilyType;
  final int hostIndex;
  // final Element? mHost;
  final int fromRoomIndex;
  // final Room? mFromRoom;
  final int toRoomIndex;
  // final Room? mToRoom;
  final int elementIndex;

  FamilyInstance({
    required this.index,
    required this.facingFlipped,
    required this.facingOrientation,
    required this.handFlipped,
    required this.mirrored,
    required this.hasModifiedGeometry,
    required this.scale,
    required this.basisX,
    required this.basisY,
    required this.basisZ,
    required this.translation,
    required this.handOrientation,
    required this.familyTypeIndex,
    required this.hostIndex,
    required this.fromRoomIndex,
    required this.toRoomIndex,
    required this.elementIndex,
  });
  // final Element? mElement;
}

class FamilyInstanceTable extends _DocumentModelTable {
  FamilyInstanceTable._(super._entityTable, super._strings);

  int count() => countBy("byte:FacingFlipped");

  FamilyInstance get(int familyInstanceIndex) {
    return FamilyInstance(
      index: familyInstanceIndex,
      facingFlipped: getFacingFlipped(familyInstanceIndex),
      facingOrientation: getFacingOrientation(familyInstanceIndex),
      handFlipped: getHandFlipped(familyInstanceIndex),
      mirrored: getMirrored(familyInstanceIndex),
      hasModifiedGeometry: getHasModifiedGeometry(familyInstanceIndex),
      scale: getScale(familyInstanceIndex),
      basisX: getBasisX(familyInstanceIndex),
      basisY: getBasisY(familyInstanceIndex),
      basisZ: getBasisZ(familyInstanceIndex),
      translation: getTranslation(familyInstanceIndex),
      handOrientation: getHandOrientation(familyInstanceIndex),
      familyTypeIndex: getFamilyTypeIndex(familyInstanceIndex),
      hostIndex: getHostIndex(familyInstanceIndex),
      fromRoomIndex: getFromRoomIndex(familyInstanceIndex),
      toRoomIndex: getToRoomIndex(familyInstanceIndex),
      elementIndex: getElementIndex(familyInstanceIndex),
    );
  }

  List<FamilyInstance> getAll() {
    final facingFlipped = _entityTable.columnData<int>("byte:FacingFlipped");
    final facingOrientationX = _entityTable.columnData<double>("float:FacingOrientation.X");
    final facingOrientationY = _entityTable.columnData<double>("float:FacingOrientation.Y");
    final facingOrientationZ = _entityTable.columnData<double>("float:FacingOrientation.Z");
    final handFlipped = _entityTable.columnData<int>("byte:HandFlipped");
    final mirrored = _entityTable.columnData<int>("byte:Mirrored");
    final hasModifiedGeometry = _entityTable.columnData<int>("byte:HasModifiedGeometry");
    final scale = _entityTable.columnData<double>("float:Scale");
    final basisXX = _entityTable.columnData<double>("float:BasisX.X");
    final basisXY = _entityTable.columnData<double>("float:BasisX.Y");
    final basisXZ = _entityTable.columnData<double>("float:BasisX.Z");
    final basisYX = _entityTable.columnData<double>("float:BasisY.X");
    final basisYY = _entityTable.columnData<double>("float:BasisY.Y");
    final basisYZ = _entityTable.columnData<double>("float:BasisY.Z");
    final basisZX = _entityTable.columnData<double>("float:BasisZ.X");
    final basisZY = _entityTable.columnData<double>("float:BasisZ.Y");
    final basisZZ = _entityTable.columnData<double>("float:BasisZ.Z");
    final translationX = _entityTable.columnData<double>("float:Translation.X");
    final translationY = _entityTable.columnData<double>("float:Translation.Y");
    final translationZ = _entityTable.columnData<double>("float:Translation.Z");
    final handOrientationX = _entityTable.columnData<double>("float:HandOrientation.X");
    final handOrientationY = _entityTable.columnData<double>("float:HandOrientation.Y");
    final handOrientationZ = _entityTable.columnData<double>("float:HandOrientation.Z");
    final familyType = _entityTable.columnData<int>("index:Vim.FamilyType:FamilyType");
    final host = _entityTable.columnData<int>("index:Vim.Element:Host");
    final fromRoom = _entityTable.columnData<int>("index:Vim.Room:FromRoom");
    final toRoom = _entityTable.columnData<int>("index:Vim.Room:ToRoom");
    final element = _entityTable.columnData<int>("index:Vim.Element:Element");

    final int length = count();
    final familyInstance = <FamilyInstance>[];
    for (int i = 0; i < length; ++i) {
      final foX = _getNumByIndex<double>(facingOrientationX, i);
      final foY = _getNumByIndex<double>(facingOrientationY, i);
      final foZ = _getNumByIndex<double>(facingOrientationZ, i);

      final bXX = _getNumByIndex<double>(basisXX, i);
      final bXY = _getNumByIndex<double>(basisXY, i);
      final bXZ = _getNumByIndex<double>(basisXZ, i);

      final bYX = _getNumByIndex<double>(basisYX, i);
      final bYY = _getNumByIndex<double>(basisYY, i);
      final bYZ = _getNumByIndex<double>(basisYZ, i);

      final bZX = _getNumByIndex<double>(basisZX, i);
      final bZY = _getNumByIndex<double>(basisZY, i);
      final bZZ = _getNumByIndex<double>(basisZZ, i);

      final tX = _getNumByIndex<double>(translationX, i);
      final tY = _getNumByIndex<double>(translationY, i);
      final tZ = _getNumByIndex<double>(translationZ, i);

      final hoX = _getNumByIndex<double>(handOrientationX, i);
      final hoY = _getNumByIndex<double>(handOrientationY, i);
      final hoZ = _getNumByIndex<double>(handOrientationZ, i);

      familyInstance.add(FamilyInstance(
        index: i,
        facingFlipped: _getBoolByIndex(facingFlipped, i),
        facingOrientation: foX != null && foY != null && foZ != null ? Vector3(foX, foY, foZ) : null,
        handFlipped: _getBoolByIndex(handFlipped, i),
        mirrored: _getBoolByIndex(mirrored, i),
        hasModifiedGeometry: _getBoolByIndex(hasModifiedGeometry, i),
        scale: _getNumByIndex<double>(scale, i),
        basisX: bXX != null && bXY != null && bXZ != null ? Vector3(bXX, bXY, bXZ) : null,
        basisY: bYX != null && bYY != null && bYZ != null ? Vector3(bYX, bYY, bYZ) : null,
        basisZ: bZX != null && bZY != null && bZZ != null ? Vector3(bZX, bZY, bZZ) : null,
        translation: tX != null && tY != null && tZ != null ? Vector3(tX, tY, tZ) : null,
        handOrientation: hoX != null && hoY != null && hoZ != null ? Vector3(hoX, hoY, hoZ) : null,
        familyTypeIndex: _getNumByIndex(familyType, i) ?? -1,
        hostIndex: _getNumByIndex(host, i) ?? -1,
        fromRoomIndex: _getNumByIndex(fromRoom, i) ?? -1,
        toRoomIndex: _getNumByIndex(toRoom, i) ?? -1,
        elementIndex: _getNumByIndex(element, i) ?? -1,
      ));
    }
    return familyInstance;
  }

  bool? getFacingFlipped(int familyInstanceIndex) => getBoolColumnValue("byte:FacingFlipped", familyInstanceIndex);
  List<bool> getAllFacingFlipped() => getBoolColumn("byte:FacingFlipped");

  Vector3? getFacingOrientation(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:FacingOrientation.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:FacingOrientation.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:FacingOrientation.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllFacingOrientation() {
    final x = getNumColumn<double>("float:FacingOrientation.X");
    final y = getNumColumn<double>("float:FacingOrientation.Y");
    final z = getNumColumn<double>("float:FacingOrientation.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  bool? getHandFlipped(int familyInstanceIndex) => getBoolColumnValue("byte:HandFlipped", familyInstanceIndex);
  List<bool> getAllHandFlipped() => getBoolColumn("byte:HandFlipped");

  bool? getMirrored(int familyInstanceIndex) => getBoolColumnValue("byte:Mirrored", familyInstanceIndex);
  List<bool> getAllMirrored() => getBoolColumn("byte:Mirrored");

  bool? getHasModifiedGeometry(int familyInstanceIndex) => getBoolColumnValue("byte:HasModifiedGeometry", familyInstanceIndex);
  List<bool> getAllHasModifiedGeometry() => getBoolColumn("byte:HasModifiedGeometry");

  double? getScale(int familyInstanceIndex) => getNumColumnValue("float:Scale", familyInstanceIndex);
  List<double> getAllScale() => getNumColumn("float:Scale");

  Vector3? getBasisX(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:BasisX.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:BasisX.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:BasisX.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllBasisX() {
    final x = getNumColumn<double>("float:BasisX.X");
    final y = getNumColumn<double>("float:BasisX.Y");
    final z = getNumColumn<double>("float:BasisX.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  Vector3? getBasisY(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:BasisY.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:BasisY.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:BasisY.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllBasisY() {
    final x = getNumColumn<double>("float:BasisY.X");
    final y = getNumColumn<double>("float:BasisY.Y");
    final z = getNumColumn<double>("float:BasisY.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  Vector3? getBasisZ(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:BasisZ.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:BasisZ.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:BasisZ.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllBasisZ() {
    final x = getNumColumn<double>("float:BasisZ.X");
    final y = getNumColumn<double>("float:BasisZ.Y");
    final z = getNumColumn<double>("float:BasisZ.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  Vector3? getTranslation(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:Translation.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:Translation.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:Translation.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllTranslation() {
    final x = getNumColumn<double>("float:Translation.X");
    final y = getNumColumn<double>("float:Translation.Y");
    final z = getNumColumn<double>("float:Translation.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  Vector3? getHandOrientation(int familyInstanceIndex) {
    final x = getNumColumnValue<double>("float:HandOrientation.X", familyInstanceIndex);
    final y = getNumColumnValue<double>("float:HandOrientation.Y", familyInstanceIndex);
    final z = getNumColumnValue<double>("float:HandOrientation.Z", familyInstanceIndex);
    return x != null && y != null && z != null ? Vector3(x, y, z) : null;
  }

  List<Vector3> getAllHandOrientation() {
    final x = getNumColumn<double>("float:HandOrientation.X");
    final y = getNumColumn<double>("float:HandOrientation.Y");
    final z = getNumColumn<double>("float:HandOrientation.Z");
    if (x.isEmpty || y.isEmpty || z.isEmpty || x.length != y.length || y.length != z.length) return [];

    final result = <Vector3>[];
    for (int i = 0; i < x.length; ++i) {
      result.add(Vector3(x[i], y[i], z[i]));
    }
    return result;
  }

  int getFamilyTypeIndex(int familyInstanceIndex) => getNumColumnValue("index:Vim.FamilyType:FamilyType", familyInstanceIndex) ?? -1;
  int getHostIndex(int familyInstanceIndex) => getNumColumnValue("index:Vim.Element:Host", familyInstanceIndex) ?? -1;
  int getFromRoomIndex(int familyInstanceIndex) => getNumColumnValue("index:Vim.Room:FromRoom", familyInstanceIndex) ?? -1;
  int getToRoomIndex(int familyInstanceIndex) => getNumColumnValue("index:Vim.Room:ToRoom", familyInstanceIndex) ?? -1;
  int getElementIndex(int familyInstanceIndex) => getNumColumnValue("index:Vim.Element:Element", familyInstanceIndex) ?? -1;
}

class Node {
  final int index;
  final int elementIndex;
  // final Element? mElement;
  const Node({
    required this.index,
    required this.elementIndex,
  });
}

class NodeTable extends _DocumentModelTable {
  NodeTable._(super._entityTable, super._strings);

  int count() => countBy("index:Vim.Element:Element");

  Node get(int nodeIndex) {
    return Node(
      index: nodeIndex,
      elementIndex: getElementIndex(nodeIndex),
    );
  }

  List<Node> getAll() {
    final elementData = getNumColumn<int>("index:Vim.Element:Element");
    final int length = count();
    final nodes = <Node>[];
    for (int i = 0; i < length; ++i) {
      nodes.add(Node(
        index: i,
        elementIndex: _getNumByIndex(elementData, i) ?? -1,
      ));
    }
    return nodes;
  }

  int getElementIndex(int nodeIndex) => getNumColumnValue("index:Vim.Element:Element", nodeIndex) ?? -1;
}
