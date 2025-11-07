// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public partial class VimEntityTableSet
    {
        public void Initialize(bool inParallel = true)
        {
            Tables[VimEntityTableNames.Asset] = AssetTable = new AssetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Asset), StringTable, this);
            Tables[VimEntityTableNames.DisplayUnit] = DisplayUnitTable = new DisplayUnitTable(GetEntityTableDataOrEmpty(VimEntityTableNames.DisplayUnit), StringTable, this);
            Tables[VimEntityTableNames.ParameterDescriptor] = ParameterDescriptorTable = new ParameterDescriptorTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ParameterDescriptor), StringTable, this);
            Tables[VimEntityTableNames.Parameter] = ParameterTable = new ParameterTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Parameter), StringTable, this);
            Tables[VimEntityTableNames.Element] = ElementTable = new ElementTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Element), StringTable, this);
            Tables[VimEntityTableNames.Workset] = WorksetTable = new WorksetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Workset), StringTable, this);
            Tables[VimEntityTableNames.AssemblyInstance] = AssemblyInstanceTable = new AssemblyInstanceTable(GetEntityTableDataOrEmpty(VimEntityTableNames.AssemblyInstance), StringTable, this);
            Tables[VimEntityTableNames.Group] = GroupTable = new GroupTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Group), StringTable, this);
            Tables[VimEntityTableNames.DesignOption] = DesignOptionTable = new DesignOptionTable(GetEntityTableDataOrEmpty(VimEntityTableNames.DesignOption), StringTable, this);
            Tables[VimEntityTableNames.Level] = LevelTable = new LevelTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Level), StringTable, this);
            Tables[VimEntityTableNames.Phase] = PhaseTable = new PhaseTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Phase), StringTable, this);
            Tables[VimEntityTableNames.Room] = RoomTable = new RoomTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Room), StringTable, this);
            Tables[VimEntityTableNames.BimDocument] = BimDocumentTable = new BimDocumentTable(GetEntityTableDataOrEmpty(VimEntityTableNames.BimDocument), StringTable, this);
            Tables[VimEntityTableNames.DisplayUnitInBimDocument] = DisplayUnitInBimDocumentTable = new DisplayUnitInBimDocumentTable(GetEntityTableDataOrEmpty(VimEntityTableNames.DisplayUnitInBimDocument), StringTable, this);
            Tables[VimEntityTableNames.PhaseOrderInBimDocument] = PhaseOrderInBimDocumentTable = new PhaseOrderInBimDocumentTable(GetEntityTableDataOrEmpty(VimEntityTableNames.PhaseOrderInBimDocument), StringTable, this);
            Tables[VimEntityTableNames.Category] = CategoryTable = new CategoryTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Category), StringTable, this);
            Tables[VimEntityTableNames.Family] = FamilyTable = new FamilyTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Family), StringTable, this);
            Tables[VimEntityTableNames.FamilyType] = FamilyTypeTable = new FamilyTypeTable(GetEntityTableDataOrEmpty(VimEntityTableNames.FamilyType), StringTable, this);
            Tables[VimEntityTableNames.FamilyInstance] = FamilyInstanceTable = new FamilyInstanceTable(GetEntityTableDataOrEmpty(VimEntityTableNames.FamilyInstance), StringTable, this);
            Tables[VimEntityTableNames.View] = ViewTable = new ViewTable(GetEntityTableDataOrEmpty(VimEntityTableNames.View), StringTable, this);
            Tables[VimEntityTableNames.ElementInView] = ElementInViewTable = new ElementInViewTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ElementInView), StringTable, this);
            Tables[VimEntityTableNames.ShapeInView] = ShapeInViewTable = new ShapeInViewTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ShapeInView), StringTable, this);
            Tables[VimEntityTableNames.AssetInView] = AssetInViewTable = new AssetInViewTable(GetEntityTableDataOrEmpty(VimEntityTableNames.AssetInView), StringTable, this);
            Tables[VimEntityTableNames.AssetInViewSheet] = AssetInViewSheetTable = new AssetInViewSheetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.AssetInViewSheet), StringTable, this);
            Tables[VimEntityTableNames.LevelInView] = LevelInViewTable = new LevelInViewTable(GetEntityTableDataOrEmpty(VimEntityTableNames.LevelInView), StringTable, this);
            Tables[VimEntityTableNames.Camera] = CameraTable = new CameraTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Camera), StringTable, this);
            Tables[VimEntityTableNames.Material] = MaterialTable = new MaterialTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Material), StringTable, this);
            Tables[VimEntityTableNames.MaterialInElement] = MaterialInElementTable = new MaterialInElementTable(GetEntityTableDataOrEmpty(VimEntityTableNames.MaterialInElement), StringTable, this);
            Tables[VimEntityTableNames.CompoundStructureLayer] = CompoundStructureLayerTable = new CompoundStructureLayerTable(GetEntityTableDataOrEmpty(VimEntityTableNames.CompoundStructureLayer), StringTable, this);
            Tables[VimEntityTableNames.CompoundStructure] = CompoundStructureTable = new CompoundStructureTable(GetEntityTableDataOrEmpty(VimEntityTableNames.CompoundStructure), StringTable, this);
            Tables[VimEntityTableNames.Node] = NodeTable = new NodeTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Node), StringTable, this);
            Tables[VimEntityTableNames.Geometry] = GeometryTable = new GeometryTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Geometry), StringTable, this);
            Tables[VimEntityTableNames.Shape] = ShapeTable = new ShapeTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Shape), StringTable, this);
            Tables[VimEntityTableNames.ShapeCollection] = ShapeCollectionTable = new ShapeCollectionTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ShapeCollection), StringTable, this);
            Tables[VimEntityTableNames.ShapeInShapeCollection] = ShapeInShapeCollectionTable = new ShapeInShapeCollectionTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ShapeInShapeCollection), StringTable, this);
            Tables[VimEntityTableNames.System] = SystemTable = new SystemTable(GetEntityTableDataOrEmpty(VimEntityTableNames.System), StringTable, this);
            Tables[VimEntityTableNames.ElementInSystem] = ElementInSystemTable = new ElementInSystemTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ElementInSystem), StringTable, this);
            Tables[VimEntityTableNames.Warning] = WarningTable = new WarningTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Warning), StringTable, this);
            Tables[VimEntityTableNames.ElementInWarning] = ElementInWarningTable = new ElementInWarningTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ElementInWarning), StringTable, this);
            Tables[VimEntityTableNames.BasePoint] = BasePointTable = new BasePointTable(GetEntityTableDataOrEmpty(VimEntityTableNames.BasePoint), StringTable, this);
            Tables[VimEntityTableNames.PhaseFilter] = PhaseFilterTable = new PhaseFilterTable(GetEntityTableDataOrEmpty(VimEntityTableNames.PhaseFilter), StringTable, this);
            Tables[VimEntityTableNames.Grid] = GridTable = new GridTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Grid), StringTable, this);
            Tables[VimEntityTableNames.Area] = AreaTable = new AreaTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Area), StringTable, this);
            Tables[VimEntityTableNames.AreaScheme] = AreaSchemeTable = new AreaSchemeTable(GetEntityTableDataOrEmpty(VimEntityTableNames.AreaScheme), StringTable, this);
            Tables[VimEntityTableNames.Schedule] = ScheduleTable = new ScheduleTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Schedule), StringTable, this);
            Tables[VimEntityTableNames.ScheduleColumn] = ScheduleColumnTable = new ScheduleColumnTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ScheduleColumn), StringTable, this);
            Tables[VimEntityTableNames.ScheduleCell] = ScheduleCellTable = new ScheduleCellTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ScheduleCell), StringTable, this);
            Tables[VimEntityTableNames.ViewSheetSet] = ViewSheetSetTable = new ViewSheetSetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ViewSheetSet), StringTable, this);
            Tables[VimEntityTableNames.ViewSheet] = ViewSheetTable = new ViewSheetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ViewSheet), StringTable, this);
            Tables[VimEntityTableNames.ViewSheetInViewSheetSet] = ViewSheetInViewSheetSetTable = new ViewSheetInViewSheetSetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ViewSheetInViewSheetSet), StringTable, this);
            Tables[VimEntityTableNames.ViewInViewSheetSet] = ViewInViewSheetSetTable = new ViewInViewSheetSetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ViewInViewSheetSet), StringTable, this);
            Tables[VimEntityTableNames.ViewInViewSheet] = ViewInViewSheetTable = new ViewInViewSheetTable(GetEntityTableDataOrEmpty(VimEntityTableNames.ViewInViewSheet), StringTable, this);
            Tables[VimEntityTableNames.Site] = SiteTable = new SiteTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Site), StringTable, this);
            Tables[VimEntityTableNames.Building] = BuildingTable = new BuildingTable(GetEntityTableDataOrEmpty(VimEntityTableNames.Building), StringTable, this);
            
            // Initialize element index maps
            ElementIndexMaps = new VimElementIndexMaps(this, inParallel);
            
        } // end of Initialize
        
        public AssetTable AssetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Asset GetAsset(int index) => AssetTable?.Get(index);
        public DisplayUnitTable DisplayUnitTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.DisplayUnit GetDisplayUnit(int index) => DisplayUnitTable?.Get(index);
        public ParameterDescriptorTable ParameterDescriptorTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ParameterDescriptor GetParameterDescriptor(int index) => ParameterDescriptorTable?.Get(index);
        public ParameterTable ParameterTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Parameter GetParameter(int index) => ParameterTable?.Get(index);
        public ElementTable ElementTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Element GetElement(int index) => ElementTable?.Get(index);
        public WorksetTable WorksetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Workset GetWorkset(int index) => WorksetTable?.Get(index);
        public AssemblyInstanceTable AssemblyInstanceTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.AssemblyInstance GetAssemblyInstance(int index) => AssemblyInstanceTable?.Get(index);
        public GroupTable GroupTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Group GetGroup(int index) => GroupTable?.Get(index);
        public DesignOptionTable DesignOptionTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.DesignOption GetDesignOption(int index) => DesignOptionTable?.Get(index);
        public LevelTable LevelTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Level GetLevel(int index) => LevelTable?.Get(index);
        public PhaseTable PhaseTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Phase GetPhase(int index) => PhaseTable?.Get(index);
        public RoomTable RoomTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Room GetRoom(int index) => RoomTable?.Get(index);
        public BimDocumentTable BimDocumentTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => BimDocumentTable?.Get(index);
        public DisplayUnitInBimDocumentTable DisplayUnitInBimDocumentTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.DisplayUnitInBimDocument GetDisplayUnitInBimDocument(int index) => DisplayUnitInBimDocumentTable?.Get(index);
        public PhaseOrderInBimDocumentTable PhaseOrderInBimDocumentTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.PhaseOrderInBimDocument GetPhaseOrderInBimDocument(int index) => PhaseOrderInBimDocumentTable?.Get(index);
        public CategoryTable CategoryTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Category GetCategory(int index) => CategoryTable?.Get(index);
        public FamilyTable FamilyTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Family GetFamily(int index) => FamilyTable?.Get(index);
        public FamilyTypeTable FamilyTypeTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => FamilyTypeTable?.Get(index);
        public FamilyInstanceTable FamilyInstanceTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.FamilyInstance GetFamilyInstance(int index) => FamilyInstanceTable?.Get(index);
        public ViewTable ViewTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.View GetView(int index) => ViewTable?.Get(index);
        public ElementInViewTable ElementInViewTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ElementInView GetElementInView(int index) => ElementInViewTable?.Get(index);
        public ShapeInViewTable ShapeInViewTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ShapeInView GetShapeInView(int index) => ShapeInViewTable?.Get(index);
        public AssetInViewTable AssetInViewTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.AssetInView GetAssetInView(int index) => AssetInViewTable?.Get(index);
        public AssetInViewSheetTable AssetInViewSheetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.AssetInViewSheet GetAssetInViewSheet(int index) => AssetInViewSheetTable?.Get(index);
        public LevelInViewTable LevelInViewTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.LevelInView GetLevelInView(int index) => LevelInViewTable?.Get(index);
        public CameraTable CameraTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Camera GetCamera(int index) => CameraTable?.Get(index);
        public MaterialTable MaterialTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Material GetMaterial(int index) => MaterialTable?.Get(index);
        public MaterialInElementTable MaterialInElementTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.MaterialInElement GetMaterialInElement(int index) => MaterialInElementTable?.Get(index);
        public CompoundStructureLayerTable CompoundStructureLayerTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.CompoundStructureLayer GetCompoundStructureLayer(int index) => CompoundStructureLayerTable?.Get(index);
        public CompoundStructureTable CompoundStructureTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.CompoundStructure GetCompoundStructure(int index) => CompoundStructureTable?.Get(index);
        public NodeTable NodeTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Node GetNode(int index) => NodeTable?.Get(index);
        public GeometryTable GeometryTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Geometry GetGeometry(int index) => GeometryTable?.Get(index);
        public ShapeTable ShapeTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Shape GetShape(int index) => ShapeTable?.Get(index);
        public ShapeCollectionTable ShapeCollectionTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ShapeCollection GetShapeCollection(int index) => ShapeCollectionTable?.Get(index);
        public ShapeInShapeCollectionTable ShapeInShapeCollectionTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ShapeInShapeCollection GetShapeInShapeCollection(int index) => ShapeInShapeCollectionTable?.Get(index);
        public SystemTable SystemTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.System GetSystem(int index) => SystemTable?.Get(index);
        public ElementInSystemTable ElementInSystemTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ElementInSystem GetElementInSystem(int index) => ElementInSystemTable?.Get(index);
        public WarningTable WarningTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Warning GetWarning(int index) => WarningTable?.Get(index);
        public ElementInWarningTable ElementInWarningTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ElementInWarning GetElementInWarning(int index) => ElementInWarningTable?.Get(index);
        public BasePointTable BasePointTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.BasePoint GetBasePoint(int index) => BasePointTable?.Get(index);
        public PhaseFilterTable PhaseFilterTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.PhaseFilter GetPhaseFilter(int index) => PhaseFilterTable?.Get(index);
        public GridTable GridTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Grid GetGrid(int index) => GridTable?.Get(index);
        public AreaTable AreaTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Area GetArea(int index) => AreaTable?.Get(index);
        public AreaSchemeTable AreaSchemeTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.AreaScheme GetAreaScheme(int index) => AreaSchemeTable?.Get(index);
        public ScheduleTable ScheduleTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Schedule GetSchedule(int index) => ScheduleTable?.Get(index);
        public ScheduleColumnTable ScheduleColumnTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ScheduleColumn GetScheduleColumn(int index) => ScheduleColumnTable?.Get(index);
        public ScheduleCellTable ScheduleCellTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ScheduleCell GetScheduleCell(int index) => ScheduleCellTable?.Get(index);
        public ViewSheetSetTable ViewSheetSetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ViewSheetSet GetViewSheetSet(int index) => ViewSheetSetTable?.Get(index);
        public ViewSheetTable ViewSheetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ViewSheet GetViewSheet(int index) => ViewSheetTable?.Get(index);
        public ViewSheetInViewSheetSetTable ViewSheetInViewSheetSetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ViewSheetInViewSheetSet GetViewSheetInViewSheetSet(int index) => ViewSheetInViewSheetSetTable?.Get(index);
        public ViewInViewSheetSetTable ViewInViewSheetSetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ViewInViewSheetSet GetViewInViewSheetSet(int index) => ViewInViewSheetSetTable?.Get(index);
        public ViewInViewSheetTable ViewInViewSheetTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.ViewInViewSheet GetViewInViewSheet(int index) => ViewInViewSheetTable?.Get(index);
        public SiteTable SiteTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Site GetSite(int index) => SiteTable?.Get(index);
        public BuildingTable BuildingTable { get; private set; } // can be null
        public Vim.Format.ObjectModel.Building GetBuilding(int index) => BuildingTable?.Get(index);
        
        public static HashSet<string> GetElementKindTableNames()
            => new HashSet<string>()
            {
            VimEntityTableNames.AssemblyInstance,
            VimEntityTableNames.Group,
            VimEntityTableNames.DesignOption,
            VimEntityTableNames.Level,
            VimEntityTableNames.Phase,
            VimEntityTableNames.Room,
            VimEntityTableNames.BimDocument,
            VimEntityTableNames.Family,
            VimEntityTableNames.FamilyType,
            VimEntityTableNames.FamilyInstance,
            VimEntityTableNames.View,
            VimEntityTableNames.Material,
            VimEntityTableNames.System,
            VimEntityTableNames.BasePoint,
            VimEntityTableNames.PhaseFilter,
            VimEntityTableNames.Grid,
            VimEntityTableNames.Area,
            VimEntityTableNames.AreaScheme,
            VimEntityTableNames.Schedule,
            VimEntityTableNames.ViewSheetSet,
            VimEntityTableNames.ViewSheet,
            VimEntityTableNames.Site,
            VimEntityTableNames.Building,
            };
        
        // Returns an array defining a 1:1 association of Element to its ElementKind
        public ElementKind[] GetElementKinds()
        {
            var elementKinds = new ElementKind[ElementTable?.RowCount ?? 0];
            
            if (elementKinds.Length == 0) return elementKinds;
            
            // Initialize all element kinds to unknown
            for (var i = 0; i < elementKinds.Length; ++i) { elementKinds[i] = ElementKind.Unknown; }
            
            // Populate the element kinds from the relevant entity tables
            for (var i = 0; i < (AssemblyInstanceTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AssemblyInstanceTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.AssemblyInstance;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (GroupTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = GroupTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Group;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (DesignOptionTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = DesignOptionTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.DesignOption;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (LevelTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = LevelTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Level;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (PhaseTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = PhaseTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Phase;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (RoomTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = RoomTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Room;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BimDocumentTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BimDocumentTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.BimDocument;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Family;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyTypeTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyTypeTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.FamilyType;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyInstanceTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyInstanceTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.FamilyInstance;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.View;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (MaterialTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = MaterialTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Material;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (SystemTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = SystemTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.System;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BasePointTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BasePointTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.BasePoint;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (PhaseFilterTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = PhaseFilterTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.PhaseFilter;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (GridTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = GridTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Grid;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (AreaTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AreaTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Area;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (AreaSchemeTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AreaSchemeTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.AreaScheme;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ScheduleTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ScheduleTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Schedule;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewSheetSetTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewSheetSetTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.ViewSheetSet;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewSheetTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewSheetTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.ViewSheet;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (SiteTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = SiteTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Site;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BuildingTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BuildingTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Building;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            return elementKinds;
        } // GetElementKinds()
    } // end of partial class VimEntityTableSet
    
    public partial class AssetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Asset>
    {
        
        public const string TableName = VimEntityTableNames.Asset;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AssetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_BufferName = GetStringColumnValues("string:BufferName") ?? Array.Empty<String>();
        }
        
        public String[] Column_BufferName { get; }
        public String GetBufferName(int index, String @default = "") => Column_BufferName.ElementAtOrDefault(index, @default);
        // Object Getter
        public Vim.Format.ObjectModel.Asset Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Asset();
            r.Index = index;
            r.BufferName = GetBufferName(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Asset> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetTable 
    
    public partial class DisplayUnitTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.DisplayUnit>
    {
        
        public const string TableName = VimEntityTableNames.DisplayUnit;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public DisplayUnitTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Spec = GetStringColumnValues("string:Spec") ?? Array.Empty<String>();
            Column_Type = GetStringColumnValues("string:Type") ?? Array.Empty<String>();
            Column_Label = GetStringColumnValues("string:Label") ?? Array.Empty<String>();
        }
        
        public String[] Column_Spec { get; }
        public String GetSpec(int index, String @default = "") => Column_Spec.ElementAtOrDefault(index, @default);
        public String[] Column_Type { get; }
        public String GetType(int index, String @default = "") => Column_Type.ElementAtOrDefault(index, @default);
        public String[] Column_Label { get; }
        public String GetLabel(int index, String @default = "") => Column_Label.ElementAtOrDefault(index, @default);
        // Object Getter
        public Vim.Format.ObjectModel.DisplayUnit Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.DisplayUnit();
            r.Index = index;
            r.Spec = GetSpec(index);
            r.Type = GetType(index);
            r.Label = GetLabel(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.DisplayUnit> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DisplayUnitTable 
    
    public partial class ParameterDescriptorTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ParameterDescriptor>
    {
        
        public const string TableName = VimEntityTableNames.ParameterDescriptor;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ParameterDescriptorTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Group = GetStringColumnValues("string:Group") ?? Array.Empty<String>();
            Column_ParameterType = GetStringColumnValues("string:ParameterType") ?? Array.Empty<String>();
            Column_IsInstance = GetDataColumnValues<Boolean>("byte:IsInstance") ?? Array.Empty<Boolean>();
            Column_IsShared = GetDataColumnValues<Boolean>("byte:IsShared") ?? Array.Empty<Boolean>();
            Column_IsReadOnly = GetDataColumnValues<Boolean>("byte:IsReadOnly") ?? Array.Empty<Boolean>();
            Column_Flags = GetDataColumnValues<Int32>("int:Flags") ?? Array.Empty<Int32>();
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_StorageType = GetDataColumnValues<Int32>("int:StorageType") ?? Array.Empty<Int32>();
            Column_DisplayUnitIndex = GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Group { get; }
        public String GetGroup(int index, String @default = "") => Column_Group.ElementAtOrDefault(index, @default);
        public String[] Column_ParameterType { get; }
        public String GetParameterType(int index, String @default = "") => Column_ParameterType.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsInstance { get; }
        public Boolean GetIsInstance(int index, Boolean @default = default) => Column_IsInstance.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsShared { get; }
        public Boolean GetIsShared(int index, Boolean @default = default) => Column_IsShared.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsReadOnly { get; }
        public Boolean GetIsReadOnly(int index, Boolean @default = default) => Column_IsReadOnly.ElementAtOrDefault(index, @default);
        public Int32[] Column_Flags { get; }
        public Int32 GetFlags(int index, Int32 @default = default) => Column_Flags.ElementAtOrDefault(index, @default);
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public Int32[] Column_StorageType { get; }
        public Int32 GetStorageType(int index, Int32 @default = default) => Column_StorageType.ElementAtOrDefault(index, @default);
        public int[] Column_DisplayUnitIndex { get; }
        public int GetDisplayUnitIndex(int index) => Column_DisplayUnitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.DisplayUnit GetDisplayUnit(int index) => _GetReferencedDisplayUnit(GetDisplayUnitIndex(index));
        private Vim.Format.ObjectModel.DisplayUnit _GetReferencedDisplayUnit(int referencedIndex) => ParentTableSet.GetDisplayUnit(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ParameterDescriptor Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ParameterDescriptor();
            r.Index = index;
            r.Name = GetName(index);
            r.Group = GetGroup(index);
            r.ParameterType = GetParameterType(index);
            r.IsInstance = GetIsInstance(index);
            r.IsShared = GetIsShared(index);
            r.IsReadOnly = GetIsReadOnly(index);
            r.Flags = GetFlags(index);
            r.Guid = GetGuid(index);
            r.StorageType = GetStorageType(index);
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetDisplayUnitIndex(index), _GetReferencedDisplayUnit);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ParameterDescriptor> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ParameterDescriptorTable 
    
    public partial class ParameterTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Parameter>
    {
        
        public const string TableName = VimEntityTableNames.Parameter;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ParameterTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetStringColumnValues("string:Value") ?? Array.Empty<String>();
            Column_ParameterDescriptorIndex = GetIndexColumnValues("index:Vim.ParameterDescriptor:ParameterDescriptor") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Value { get; }
        public String GetValue(int index, String @default = "") => Column_Value.ElementAtOrDefault(index, @default);
        public int[] Column_ParameterDescriptorIndex { get; }
        public int GetParameterDescriptorIndex(int index) => Column_ParameterDescriptorIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ParameterDescriptor GetParameterDescriptor(int index) => _GetReferencedParameterDescriptor(GetParameterDescriptorIndex(index));
        private Vim.Format.ObjectModel.ParameterDescriptor _GetReferencedParameterDescriptor(int referencedIndex) => ParentTableSet.GetParameterDescriptor(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Parameter Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Parameter();
            r.Index = index;
            r.Value = GetValue(index);
            r._ParameterDescriptor = new Relation<Vim.Format.ObjectModel.ParameterDescriptor>(GetParameterDescriptorIndex(index), _GetReferencedParameterDescriptor);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Parameter> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ParameterTable 
    
    public partial class ElementTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Element>
    {
        
        public const string TableName = VimEntityTableNames.Element;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ElementTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = (GetDataColumnValues<Int64>("long:Id") ?? GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v).ToArray()) ?? Array.Empty<Int64>();
            Column_Type = GetStringColumnValues("string:Type") ?? Array.Empty<String>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_UniqueId = GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>();
            Column_Location_X = GetDataColumnValues<Single>("float:Location.X") ?? Array.Empty<Single>();
            Column_Location_Y = GetDataColumnValues<Single>("float:Location.Y") ?? Array.Empty<Single>();
            Column_Location_Z = GetDataColumnValues<Single>("float:Location.Z") ?? Array.Empty<Single>();
            Column_FamilyName = GetStringColumnValues("string:FamilyName") ?? Array.Empty<String>();
            Column_IsPinned = GetDataColumnValues<Boolean>("byte:IsPinned") ?? Array.Empty<Boolean>();
            Column_LevelIndex = GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>();
            Column_PhaseCreatedIndex = GetIndexColumnValues("index:Vim.Phase:PhaseCreated") ?? Array.Empty<int>();
            Column_PhaseDemolishedIndex = GetIndexColumnValues("index:Vim.Phase:PhaseDemolished") ?? Array.Empty<int>();
            Column_CategoryIndex = GetIndexColumnValues("index:Vim.Category:Category") ?? Array.Empty<int>();
            Column_WorksetIndex = GetIndexColumnValues("index:Vim.Workset:Workset") ?? Array.Empty<int>();
            Column_DesignOptionIndex = GetIndexColumnValues("index:Vim.DesignOption:DesignOption") ?? Array.Empty<int>();
            Column_OwnerViewIndex = GetIndexColumnValues("index:Vim.View:OwnerView") ?? Array.Empty<int>();
            Column_GroupIndex = GetIndexColumnValues("index:Vim.Group:Group") ?? Array.Empty<int>();
            Column_AssemblyInstanceIndex = GetIndexColumnValues("index:Vim.AssemblyInstance:AssemblyInstance") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
            Column_RoomIndex = GetIndexColumnValues("index:Vim.Room:Room") ?? Array.Empty<int>();
        }
        
        public Int64[] Column_Id { get; }
        public Int64 GetId(int index, Int64 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_Type { get; }
        public String GetType(int index, String @default = "") => Column_Type.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_UniqueId { get; }
        public String GetUniqueId(int index, String @default = "") => Column_UniqueId.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_X { get; }
        public Single GetLocation_X(int index, Single @default = default) => Column_Location_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_Y { get; }
        public Single GetLocation_Y(int index, Single @default = default) => Column_Location_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_Z { get; }
        public Single GetLocation_Z(int index, Single @default = default) => Column_Location_Z.ElementAtOrDefault(index, @default);
        public String[] Column_FamilyName { get; }
        public String GetFamilyName(int index, String @default = "") => Column_FamilyName.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsPinned { get; }
        public Boolean GetIsPinned(int index, Boolean @default = default) => Column_IsPinned.ElementAtOrDefault(index, @default);
        public int[] Column_LevelIndex { get; }
        public int GetLevelIndex(int index) => Column_LevelIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Level GetLevel(int index) => _GetReferencedLevel(GetLevelIndex(index));
        private Vim.Format.ObjectModel.Level _GetReferencedLevel(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_PhaseCreatedIndex { get; }
        public int GetPhaseCreatedIndex(int index) => Column_PhaseCreatedIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Phase GetPhaseCreated(int index) => _GetReferencedPhaseCreated(GetPhaseCreatedIndex(index));
        private Vim.Format.ObjectModel.Phase _GetReferencedPhaseCreated(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_PhaseDemolishedIndex { get; }
        public int GetPhaseDemolishedIndex(int index) => Column_PhaseDemolishedIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Phase GetPhaseDemolished(int index) => _GetReferencedPhaseDemolished(GetPhaseDemolishedIndex(index));
        private Vim.Format.ObjectModel.Phase _GetReferencedPhaseDemolished(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_CategoryIndex { get; }
        public int GetCategoryIndex(int index) => Column_CategoryIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Category GetCategory(int index) => _GetReferencedCategory(GetCategoryIndex(index));
        private Vim.Format.ObjectModel.Category _GetReferencedCategory(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_WorksetIndex { get; }
        public int GetWorksetIndex(int index) => Column_WorksetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Workset GetWorkset(int index) => _GetReferencedWorkset(GetWorksetIndex(index));
        private Vim.Format.ObjectModel.Workset _GetReferencedWorkset(int referencedIndex) => ParentTableSet.GetWorkset(referencedIndex);
        public int[] Column_DesignOptionIndex { get; }
        public int GetDesignOptionIndex(int index) => Column_DesignOptionIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.DesignOption GetDesignOption(int index) => _GetReferencedDesignOption(GetDesignOptionIndex(index));
        private Vim.Format.ObjectModel.DesignOption _GetReferencedDesignOption(int referencedIndex) => ParentTableSet.GetDesignOption(referencedIndex);
        public int[] Column_OwnerViewIndex { get; }
        public int GetOwnerViewIndex(int index) => Column_OwnerViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetOwnerView(int index) => _GetReferencedOwnerView(GetOwnerViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedOwnerView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_GroupIndex { get; }
        public int GetGroupIndex(int index) => Column_GroupIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Group GetGroup(int index) => _GetReferencedGroup(GetGroupIndex(index));
        private Vim.Format.ObjectModel.Group _GetReferencedGroup(int referencedIndex) => ParentTableSet.GetGroup(referencedIndex);
        public int[] Column_AssemblyInstanceIndex { get; }
        public int GetAssemblyInstanceIndex(int index) => Column_AssemblyInstanceIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.AssemblyInstance GetAssemblyInstance(int index) => _GetReferencedAssemblyInstance(GetAssemblyInstanceIndex(index));
        private Vim.Format.ObjectModel.AssemblyInstance _GetReferencedAssemblyInstance(int referencedIndex) => ParentTableSet.GetAssemblyInstance(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        public int[] Column_RoomIndex { get; }
        public int GetRoomIndex(int index) => Column_RoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Room GetRoom(int index) => _GetReferencedRoom(GetRoomIndex(index));
        private Vim.Format.ObjectModel.Room _GetReferencedRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Element Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Element();
            r.Index = index;
            r.Id = GetId(index);
            r.Type = GetType(index);
            r.Name = GetName(index);
            r.UniqueId = GetUniqueId(index);
            r.Location_X = GetLocation_X(index);
            r.Location_Y = GetLocation_Y(index);
            r.Location_Z = GetLocation_Z(index);
            r.FamilyName = GetFamilyName(index);
            r.IsPinned = GetIsPinned(index);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetLevelIndex(index), _GetReferencedLevel);
            r._PhaseCreated = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseCreatedIndex(index), _GetReferencedPhaseCreated);
            r._PhaseDemolished = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseDemolishedIndex(index), _GetReferencedPhaseDemolished);
            r._Category = new Relation<Vim.Format.ObjectModel.Category>(GetCategoryIndex(index), _GetReferencedCategory);
            r._Workset = new Relation<Vim.Format.ObjectModel.Workset>(GetWorksetIndex(index), _GetReferencedWorkset);
            r._DesignOption = new Relation<Vim.Format.ObjectModel.DesignOption>(GetDesignOptionIndex(index), _GetReferencedDesignOption);
            r._OwnerView = new Relation<Vim.Format.ObjectModel.View>(GetOwnerViewIndex(index), _GetReferencedOwnerView);
            r._Group = new Relation<Vim.Format.ObjectModel.Group>(GetGroupIndex(index), _GetReferencedGroup);
            r._AssemblyInstance = new Relation<Vim.Format.ObjectModel.AssemblyInstance>(GetAssemblyInstanceIndex(index), _GetReferencedAssemblyInstance);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            r._Room = new Relation<Vim.Format.ObjectModel.Room>(GetRoomIndex(index), _GetReferencedRoom);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Element> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementTable 
    
    public partial class WorksetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Workset>
    {
        
        public const string TableName = VimEntityTableNames.Workset;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public WorksetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Kind = GetStringColumnValues("string:Kind") ?? Array.Empty<String>();
            Column_IsOpen = GetDataColumnValues<Boolean>("byte:IsOpen") ?? Array.Empty<Boolean>();
            Column_IsEditable = GetDataColumnValues<Boolean>("byte:IsEditable") ?? Array.Empty<Boolean>();
            Column_Owner = GetStringColumnValues("string:Owner") ?? Array.Empty<String>();
            Column_UniqueId = GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_Id { get; }
        public Int32 GetId(int index, Int32 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Kind { get; }
        public String GetKind(int index, String @default = "") => Column_Kind.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsOpen { get; }
        public Boolean GetIsOpen(int index, Boolean @default = default) => Column_IsOpen.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsEditable { get; }
        public Boolean GetIsEditable(int index, Boolean @default = default) => Column_IsEditable.ElementAtOrDefault(index, @default);
        public String[] Column_Owner { get; }
        public String GetOwner(int index, String @default = "") => Column_Owner.ElementAtOrDefault(index, @default);
        public String[] Column_UniqueId { get; }
        public String GetUniqueId(int index, String @default = "") => Column_UniqueId.ElementAtOrDefault(index, @default);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Workset Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Workset();
            r.Index = index;
            r.Id = GetId(index);
            r.Name = GetName(index);
            r.Kind = GetKind(index);
            r.IsOpen = GetIsOpen(index);
            r.IsEditable = GetIsEditable(index);
            r.Owner = GetOwner(index);
            r.UniqueId = GetUniqueId(index);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Workset> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class WorksetTable 
    
    public partial class AssemblyInstanceTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.AssemblyInstance>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.AssemblyInstance;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AssemblyInstanceTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssemblyTypeName = GetStringColumnValues("string:AssemblyTypeName") ?? Array.Empty<String>();
            Column_Position_X = GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>();
            Column_Position_Y = GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>();
            Column_Position_Z = GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_AssemblyTypeName { get; }
        public String GetAssemblyTypeName(int index, String @default = "") => Column_AssemblyTypeName.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_X { get; }
        public Single GetPosition_X(int index, Single @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Y { get; }
        public Single GetPosition_Y(int index, Single @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Z { get; }
        public Single GetPosition_Z(int index, Single @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.AssemblyInstance Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.AssemblyInstance();
            r.Index = index;
            r.AssemblyTypeName = GetAssemblyTypeName(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.AssemblyInstance> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssemblyInstanceTable 
    
    public partial class GroupTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Group>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Group;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public GroupTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_GroupType = GetStringColumnValues("string:GroupType") ?? Array.Empty<String>();
            Column_Position_X = GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>();
            Column_Position_Y = GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>();
            Column_Position_Z = GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_GroupType { get; }
        public String GetGroupType(int index, String @default = "") => Column_GroupType.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_X { get; }
        public Single GetPosition_X(int index, Single @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Y { get; }
        public Single GetPosition_Y(int index, Single @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Z { get; }
        public Single GetPosition_Z(int index, Single @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Group Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Group();
            r.Index = index;
            r.GroupType = GetGroupType(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Group> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GroupTable 
    
    public partial class DesignOptionTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.DesignOption>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.DesignOption;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public DesignOptionTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsPrimary = GetDataColumnValues<Boolean>("byte:IsPrimary") ?? Array.Empty<Boolean>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsPrimary { get; }
        public Boolean GetIsPrimary(int index, Boolean @default = default) => Column_IsPrimary.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.DesignOption Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.DesignOption();
            r.Index = index;
            r.IsPrimary = GetIsPrimary(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.DesignOption> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DesignOptionTable 
    
    public partial class LevelTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Level>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Level;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public LevelTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_ProjectElevation = GetDataColumnValues<Double>("double:ProjectElevation") ?? Array.Empty<Double>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_BuildingIndex = GetIndexColumnValues("index:Vim.Building:Building") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public Double[] Column_ProjectElevation { get; }
        public Double GetProjectElevation(int index, Double @default = default) => Column_ProjectElevation.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_BuildingIndex { get; }
        public int GetBuildingIndex(int index) => Column_BuildingIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Building GetBuilding(int index) => _GetReferencedBuilding(GetBuildingIndex(index));
        private Vim.Format.ObjectModel.Building _GetReferencedBuilding(int referencedIndex) => ParentTableSet.GetBuilding(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Level Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Level();
            r.Index = index;
            r.Elevation = GetElevation(index);
            r.ProjectElevation = GetProjectElevation(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Building = new Relation<Vim.Format.ObjectModel.Building>(GetBuildingIndex(index), _GetReferencedBuilding);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Level> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class LevelTable 
    
    public partial class PhaseTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Phase>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Phase;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Phase Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Phase();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Phase> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseTable 
    
    public partial class RoomTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Room>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Room;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public RoomTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_BaseOffset = GetDataColumnValues<Double>("double:BaseOffset") ?? Array.Empty<Double>();
            Column_LimitOffset = GetDataColumnValues<Double>("double:LimitOffset") ?? Array.Empty<Double>();
            Column_UnboundedHeight = GetDataColumnValues<Double>("double:UnboundedHeight") ?? Array.Empty<Double>();
            Column_Volume = GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>();
            Column_Perimeter = GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>();
            Column_Area = GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_UpperLimitIndex = GetIndexColumnValues("index:Vim.Level:UpperLimit") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_BaseOffset { get; }
        public Double GetBaseOffset(int index, Double @default = default) => Column_BaseOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_LimitOffset { get; }
        public Double GetLimitOffset(int index, Double @default = default) => Column_LimitOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_UnboundedHeight { get; }
        public Double GetUnboundedHeight(int index, Double @default = default) => Column_UnboundedHeight.ElementAtOrDefault(index, @default);
        public Double[] Column_Volume { get; }
        public Double GetVolume(int index, Double @default = default) => Column_Volume.ElementAtOrDefault(index, @default);
        public Double[] Column_Perimeter { get; }
        public Double GetPerimeter(int index, Double @default = default) => Column_Perimeter.ElementAtOrDefault(index, @default);
        public Double[] Column_Area { get; }
        public Double GetArea(int index, Double @default = default) => Column_Area.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public int[] Column_UpperLimitIndex { get; }
        public int GetUpperLimitIndex(int index) => Column_UpperLimitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Level GetUpperLimit(int index) => _GetReferencedUpperLimit(GetUpperLimitIndex(index));
        private Vim.Format.ObjectModel.Level _GetReferencedUpperLimit(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Room Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Room();
            r.Index = index;
            r.BaseOffset = GetBaseOffset(index);
            r.LimitOffset = GetLimitOffset(index);
            r.UnboundedHeight = GetUnboundedHeight(index);
            r.Volume = GetVolume(index);
            r.Perimeter = GetPerimeter(index);
            r.Area = GetArea(index);
            r.Number = GetNumber(index);
            r._UpperLimit = new Relation<Vim.Format.ObjectModel.Level>(GetUpperLimitIndex(index), _GetReferencedUpperLimit);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Room> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class RoomTable 
    
    public partial class BimDocumentTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.BimDocument>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.BimDocument;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public BimDocumentTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Title = GetStringColumnValues("string:Title") ?? Array.Empty<String>();
            Column_IsMetric = GetDataColumnValues<Boolean>("byte:IsMetric") ?? Array.Empty<Boolean>();
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_NumSaves = GetDataColumnValues<Int32>("int:NumSaves") ?? Array.Empty<Int32>();
            Column_IsLinked = GetDataColumnValues<Boolean>("byte:IsLinked") ?? Array.Empty<Boolean>();
            Column_IsDetached = GetDataColumnValues<Boolean>("byte:IsDetached") ?? Array.Empty<Boolean>();
            Column_IsWorkshared = GetDataColumnValues<Boolean>("byte:IsWorkshared") ?? Array.Empty<Boolean>();
            Column_PathName = GetStringColumnValues("string:PathName") ?? Array.Empty<String>();
            Column_Latitude = GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>();
            Column_Longitude = GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>();
            Column_TimeZone = GetDataColumnValues<Double>("double:TimeZone") ?? Array.Empty<Double>();
            Column_PlaceName = GetStringColumnValues("string:PlaceName") ?? Array.Empty<String>();
            Column_WeatherStationName = GetStringColumnValues("string:WeatherStationName") ?? Array.Empty<String>();
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_ProjectLocation = GetStringColumnValues("string:ProjectLocation") ?? Array.Empty<String>();
            Column_IssueDate = GetStringColumnValues("string:IssueDate") ?? Array.Empty<String>();
            Column_Status = GetStringColumnValues("string:Status") ?? Array.Empty<String>();
            Column_ClientName = GetStringColumnValues("string:ClientName") ?? Array.Empty<String>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_Author = GetStringColumnValues("string:Author") ?? Array.Empty<String>();
            Column_BuildingName = GetStringColumnValues("string:BuildingName") ?? Array.Empty<String>();
            Column_OrganizationName = GetStringColumnValues("string:OrganizationName") ?? Array.Empty<String>();
            Column_OrganizationDescription = GetStringColumnValues("string:OrganizationDescription") ?? Array.Empty<String>();
            Column_Product = GetStringColumnValues("string:Product") ?? Array.Empty<String>();
            Column_Version = GetStringColumnValues("string:Version") ?? Array.Empty<String>();
            Column_User = GetStringColumnValues("string:User") ?? Array.Empty<String>();
            Column_FileLength = GetDataColumnValues<Int64>("long:FileLength") ?? Array.Empty<Int64>();
            Column_ActiveViewIndex = GetIndexColumnValues("index:Vim.View:ActiveView") ?? Array.Empty<int>();
            Column_OwnerFamilyIndex = GetIndexColumnValues("index:Vim.Family:OwnerFamily") ?? Array.Empty<int>();
            Column_ParentIndex = GetIndexColumnValues("index:Vim.BimDocument:Parent") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Title { get; }
        public String GetTitle(int index, String @default = "") => Column_Title.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsMetric { get; }
        public Boolean GetIsMetric(int index, Boolean @default = default) => Column_IsMetric.ElementAtOrDefault(index, @default);
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public Int32[] Column_NumSaves { get; }
        public Int32 GetNumSaves(int index, Int32 @default = default) => Column_NumSaves.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsLinked { get; }
        public Boolean GetIsLinked(int index, Boolean @default = default) => Column_IsLinked.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsDetached { get; }
        public Boolean GetIsDetached(int index, Boolean @default = default) => Column_IsDetached.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsWorkshared { get; }
        public Boolean GetIsWorkshared(int index, Boolean @default = default) => Column_IsWorkshared.ElementAtOrDefault(index, @default);
        public String[] Column_PathName { get; }
        public String GetPathName(int index, String @default = "") => Column_PathName.ElementAtOrDefault(index, @default);
        public Double[] Column_Latitude { get; }
        public Double GetLatitude(int index, Double @default = default) => Column_Latitude.ElementAtOrDefault(index, @default);
        public Double[] Column_Longitude { get; }
        public Double GetLongitude(int index, Double @default = default) => Column_Longitude.ElementAtOrDefault(index, @default);
        public Double[] Column_TimeZone { get; }
        public Double GetTimeZone(int index, Double @default = default) => Column_TimeZone.ElementAtOrDefault(index, @default);
        public String[] Column_PlaceName { get; }
        public String GetPlaceName(int index, String @default = "") => Column_PlaceName.ElementAtOrDefault(index, @default);
        public String[] Column_WeatherStationName { get; }
        public String GetWeatherStationName(int index, String @default = "") => Column_WeatherStationName.ElementAtOrDefault(index, @default);
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public String[] Column_ProjectLocation { get; }
        public String GetProjectLocation(int index, String @default = "") => Column_ProjectLocation.ElementAtOrDefault(index, @default);
        public String[] Column_IssueDate { get; }
        public String GetIssueDate(int index, String @default = "") => Column_IssueDate.ElementAtOrDefault(index, @default);
        public String[] Column_Status { get; }
        public String GetStatus(int index, String @default = "") => Column_Status.ElementAtOrDefault(index, @default);
        public String[] Column_ClientName { get; }
        public String GetClientName(int index, String @default = "") => Column_ClientName.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public String[] Column_Author { get; }
        public String GetAuthor(int index, String @default = "") => Column_Author.ElementAtOrDefault(index, @default);
        public String[] Column_BuildingName { get; }
        public String GetBuildingName(int index, String @default = "") => Column_BuildingName.ElementAtOrDefault(index, @default);
        public String[] Column_OrganizationName { get; }
        public String GetOrganizationName(int index, String @default = "") => Column_OrganizationName.ElementAtOrDefault(index, @default);
        public String[] Column_OrganizationDescription { get; }
        public String GetOrganizationDescription(int index, String @default = "") => Column_OrganizationDescription.ElementAtOrDefault(index, @default);
        public String[] Column_Product { get; }
        public String GetProduct(int index, String @default = "") => Column_Product.ElementAtOrDefault(index, @default);
        public String[] Column_Version { get; }
        public String GetVersion(int index, String @default = "") => Column_Version.ElementAtOrDefault(index, @default);
        public String[] Column_User { get; }
        public String GetUser(int index, String @default = "") => Column_User.ElementAtOrDefault(index, @default);
        public Int64[] Column_FileLength { get; }
        public Int64 GetFileLength(int index, Int64 @default = default) => Column_FileLength.ElementAtOrDefault(index, @default);
        public int[] Column_ActiveViewIndex { get; }
        public int GetActiveViewIndex(int index) => Column_ActiveViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetActiveView(int index) => _GetReferencedActiveView(GetActiveViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedActiveView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_OwnerFamilyIndex { get; }
        public int GetOwnerFamilyIndex(int index) => Column_OwnerFamilyIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Family GetOwnerFamily(int index) => _GetReferencedOwnerFamily(GetOwnerFamilyIndex(index));
        private Vim.Format.ObjectModel.Family _GetReferencedOwnerFamily(int referencedIndex) => ParentTableSet.GetFamily(referencedIndex);
        public int[] Column_ParentIndex { get; }
        public int GetParentIndex(int index) => Column_ParentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetParent(int index) => _GetReferencedParent(GetParentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedParent(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.BimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.BimDocument();
            r.Index = index;
            r.Title = GetTitle(index);
            r.IsMetric = GetIsMetric(index);
            r.Guid = GetGuid(index);
            r.NumSaves = GetNumSaves(index);
            r.IsLinked = GetIsLinked(index);
            r.IsDetached = GetIsDetached(index);
            r.IsWorkshared = GetIsWorkshared(index);
            r.PathName = GetPathName(index);
            r.Latitude = GetLatitude(index);
            r.Longitude = GetLongitude(index);
            r.TimeZone = GetTimeZone(index);
            r.PlaceName = GetPlaceName(index);
            r.WeatherStationName = GetWeatherStationName(index);
            r.Elevation = GetElevation(index);
            r.ProjectLocation = GetProjectLocation(index);
            r.IssueDate = GetIssueDate(index);
            r.Status = GetStatus(index);
            r.ClientName = GetClientName(index);
            r.Address = GetAddress(index);
            r.Name = GetName(index);
            r.Number = GetNumber(index);
            r.Author = GetAuthor(index);
            r.BuildingName = GetBuildingName(index);
            r.OrganizationName = GetOrganizationName(index);
            r.OrganizationDescription = GetOrganizationDescription(index);
            r.Product = GetProduct(index);
            r.Version = GetVersion(index);
            r.User = GetUser(index);
            r.FileLength = GetFileLength(index);
            r._ActiveView = new Relation<Vim.Format.ObjectModel.View>(GetActiveViewIndex(index), _GetReferencedActiveView);
            r._OwnerFamily = new Relation<Vim.Format.ObjectModel.Family>(GetOwnerFamilyIndex(index), _GetReferencedOwnerFamily);
            r._Parent = new Relation<Vim.Format.ObjectModel.BimDocument>(GetParentIndex(index), _GetReferencedParent);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.BimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BimDocumentTable 
    
    public partial class DisplayUnitInBimDocumentTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.DisplayUnitInBimDocument>
    {
        
        public const string TableName = VimEntityTableNames.DisplayUnitInBimDocument;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public DisplayUnitInBimDocumentTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_DisplayUnitIndex = GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public int[] Column_DisplayUnitIndex { get; }
        public int GetDisplayUnitIndex(int index) => Column_DisplayUnitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.DisplayUnit GetDisplayUnit(int index) => _GetReferencedDisplayUnit(GetDisplayUnitIndex(index));
        private Vim.Format.ObjectModel.DisplayUnit _GetReferencedDisplayUnit(int referencedIndex) => ParentTableSet.GetDisplayUnit(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.DisplayUnitInBimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.DisplayUnitInBimDocument();
            r.Index = index;
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetDisplayUnitIndex(index), _GetReferencedDisplayUnit);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.DisplayUnitInBimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DisplayUnitInBimDocumentTable 
    
    public partial class PhaseOrderInBimDocumentTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.PhaseOrderInBimDocument>
    {
        
        public const string TableName = VimEntityTableNames.PhaseOrderInBimDocument;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseOrderInBimDocumentTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_OrderIndex = GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>();
            Column_PhaseIndex = GetIndexColumnValues("index:Vim.Phase:Phase") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_OrderIndex { get; }
        public Int32 GetOrderIndex(int index, Int32 @default = default) => Column_OrderIndex.ElementAtOrDefault(index, @default);
        public int[] Column_PhaseIndex { get; }
        public int GetPhaseIndex(int index) => Column_PhaseIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Phase GetPhase(int index) => _GetReferencedPhase(GetPhaseIndex(index));
        private Vim.Format.ObjectModel.Phase _GetReferencedPhase(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.PhaseOrderInBimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.PhaseOrderInBimDocument();
            r.Index = index;
            r.OrderIndex = GetOrderIndex(index);
            r._Phase = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseIndex(index), _GetReferencedPhase);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.PhaseOrderInBimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseOrderInBimDocumentTable 
    
    public partial class CategoryTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Category>
    {
        
        public const string TableName = VimEntityTableNames.Category;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public CategoryTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Id = (GetDataColumnValues<Int64>("long:Id") ?? GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v).ToArray()) ?? Array.Empty<Int64>();
            Column_CategoryType = GetStringColumnValues("string:CategoryType") ?? Array.Empty<String>();
            Column_LineColor_X = GetDataColumnValues<Double>("double:LineColor.X") ?? Array.Empty<Double>();
            Column_LineColor_Y = GetDataColumnValues<Double>("double:LineColor.Y") ?? Array.Empty<Double>();
            Column_LineColor_Z = GetDataColumnValues<Double>("double:LineColor.Z") ?? Array.Empty<Double>();
            Column_BuiltInCategory = GetStringColumnValues("string:BuiltInCategory") ?? Array.Empty<String>();
            Column_ParentIndex = GetIndexColumnValues("index:Vim.Category:Parent") ?? Array.Empty<int>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public Int64[] Column_Id { get; }
        public Int64 GetId(int index, Int64 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_CategoryType { get; }
        public String GetCategoryType(int index, String @default = "") => Column_CategoryType.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_X { get; }
        public Double GetLineColor_X(int index, Double @default = default) => Column_LineColor_X.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_Y { get; }
        public Double GetLineColor_Y(int index, Double @default = default) => Column_LineColor_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_Z { get; }
        public Double GetLineColor_Z(int index, Double @default = default) => Column_LineColor_Z.ElementAtOrDefault(index, @default);
        public String[] Column_BuiltInCategory { get; }
        public String GetBuiltInCategory(int index, String @default = "") => Column_BuiltInCategory.ElementAtOrDefault(index, @default);
        public int[] Column_ParentIndex { get; }
        public int GetParentIndex(int index) => Column_ParentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Category GetParent(int index) => _GetReferencedParent(GetParentIndex(index));
        private Vim.Format.ObjectModel.Category _GetReferencedParent(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Vim.Format.ObjectModel.Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Category Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Category();
            r.Index = index;
            r.Name = GetName(index);
            r.Id = GetId(index);
            r.CategoryType = GetCategoryType(index);
            r.LineColor_X = GetLineColor_X(index);
            r.LineColor_Y = GetLineColor_Y(index);
            r.LineColor_Z = GetLineColor_Z(index);
            r.BuiltInCategory = GetBuiltInCategory(index);
            r._Parent = new Relation<Vim.Format.ObjectModel.Category>(GetParentIndex(index), _GetReferencedParent);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Category> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CategoryTable 
    
    public partial class FamilyTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Family>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Family;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_StructuralMaterialType = GetStringColumnValues("string:StructuralMaterialType") ?? Array.Empty<String>();
            Column_StructuralSectionShape = GetStringColumnValues("string:StructuralSectionShape") ?? Array.Empty<String>();
            Column_IsSystemFamily = GetDataColumnValues<Boolean>("byte:IsSystemFamily") ?? Array.Empty<Boolean>();
            Column_IsInPlace = GetDataColumnValues<Boolean>("byte:IsInPlace") ?? Array.Empty<Boolean>();
            Column_FamilyCategoryIndex = GetIndexColumnValues("index:Vim.Category:FamilyCategory") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_StructuralMaterialType { get; }
        public String GetStructuralMaterialType(int index, String @default = "") => Column_StructuralMaterialType.ElementAtOrDefault(index, @default);
        public String[] Column_StructuralSectionShape { get; }
        public String GetStructuralSectionShape(int index, String @default = "") => Column_StructuralSectionShape.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsSystemFamily { get; }
        public Boolean GetIsSystemFamily(int index, Boolean @default = default) => Column_IsSystemFamily.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsInPlace { get; }
        public Boolean GetIsInPlace(int index, Boolean @default = default) => Column_IsInPlace.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyCategoryIndex { get; }
        public int GetFamilyCategoryIndex(int index) => Column_FamilyCategoryIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Category GetFamilyCategory(int index) => _GetReferencedFamilyCategory(GetFamilyCategoryIndex(index));
        private Vim.Format.ObjectModel.Category _GetReferencedFamilyCategory(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Family Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Family();
            r.Index = index;
            r.StructuralMaterialType = GetStructuralMaterialType(index);
            r.StructuralSectionShape = GetStructuralSectionShape(index);
            r.IsSystemFamily = GetIsSystemFamily(index);
            r.IsInPlace = GetIsInPlace(index);
            r._FamilyCategory = new Relation<Vim.Format.ObjectModel.Category>(GetFamilyCategoryIndex(index), _GetReferencedFamilyCategory);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Family> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyTable 
    
    public partial class FamilyTypeTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.FamilyType>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.FamilyType;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyTypeTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsSystemFamilyType = GetDataColumnValues<Boolean>("byte:IsSystemFamilyType") ?? Array.Empty<Boolean>();
            Column_FamilyIndex = GetIndexColumnValues("index:Vim.Family:Family") ?? Array.Empty<int>();
            Column_CompoundStructureIndex = GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsSystemFamilyType { get; }
        public Boolean GetIsSystemFamilyType(int index, Boolean @default = default) => Column_IsSystemFamilyType.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyIndex { get; }
        public int GetFamilyIndex(int index) => Column_FamilyIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Family GetFamily(int index) => _GetReferencedFamily(GetFamilyIndex(index));
        private Vim.Format.ObjectModel.Family _GetReferencedFamily(int referencedIndex) => ParentTableSet.GetFamily(referencedIndex);
        public int[] Column_CompoundStructureIndex { get; }
        public int GetCompoundStructureIndex(int index) => Column_CompoundStructureIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.CompoundStructure GetCompoundStructure(int index) => _GetReferencedCompoundStructure(GetCompoundStructureIndex(index));
        private Vim.Format.ObjectModel.CompoundStructure _GetReferencedCompoundStructure(int referencedIndex) => ParentTableSet.GetCompoundStructure(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.FamilyType Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.FamilyType();
            r.Index = index;
            r.IsSystemFamilyType = GetIsSystemFamilyType(index);
            r._Family = new Relation<Vim.Format.ObjectModel.Family>(GetFamilyIndex(index), _GetReferencedFamily);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetCompoundStructureIndex(index), _GetReferencedCompoundStructure);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.FamilyType> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyTypeTable 
    
    public partial class FamilyInstanceTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.FamilyInstance>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.FamilyInstance;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyInstanceTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_FacingFlipped = GetDataColumnValues<Boolean>("byte:FacingFlipped") ?? Array.Empty<Boolean>();
            Column_FacingOrientation_X = GetDataColumnValues<Single>("float:FacingOrientation.X") ?? Array.Empty<Single>();
            Column_FacingOrientation_Y = GetDataColumnValues<Single>("float:FacingOrientation.Y") ?? Array.Empty<Single>();
            Column_FacingOrientation_Z = GetDataColumnValues<Single>("float:FacingOrientation.Z") ?? Array.Empty<Single>();
            Column_HandFlipped = GetDataColumnValues<Boolean>("byte:HandFlipped") ?? Array.Empty<Boolean>();
            Column_Mirrored = GetDataColumnValues<Boolean>("byte:Mirrored") ?? Array.Empty<Boolean>();
            Column_HasModifiedGeometry = GetDataColumnValues<Boolean>("byte:HasModifiedGeometry") ?? Array.Empty<Boolean>();
            Column_Scale = GetDataColumnValues<Single>("float:Scale") ?? Array.Empty<Single>();
            Column_BasisX_X = GetDataColumnValues<Single>("float:BasisX.X") ?? Array.Empty<Single>();
            Column_BasisX_Y = GetDataColumnValues<Single>("float:BasisX.Y") ?? Array.Empty<Single>();
            Column_BasisX_Z = GetDataColumnValues<Single>("float:BasisX.Z") ?? Array.Empty<Single>();
            Column_BasisY_X = GetDataColumnValues<Single>("float:BasisY.X") ?? Array.Empty<Single>();
            Column_BasisY_Y = GetDataColumnValues<Single>("float:BasisY.Y") ?? Array.Empty<Single>();
            Column_BasisY_Z = GetDataColumnValues<Single>("float:BasisY.Z") ?? Array.Empty<Single>();
            Column_BasisZ_X = GetDataColumnValues<Single>("float:BasisZ.X") ?? Array.Empty<Single>();
            Column_BasisZ_Y = GetDataColumnValues<Single>("float:BasisZ.Y") ?? Array.Empty<Single>();
            Column_BasisZ_Z = GetDataColumnValues<Single>("float:BasisZ.Z") ?? Array.Empty<Single>();
            Column_Translation_X = GetDataColumnValues<Single>("float:Translation.X") ?? Array.Empty<Single>();
            Column_Translation_Y = GetDataColumnValues<Single>("float:Translation.Y") ?? Array.Empty<Single>();
            Column_Translation_Z = GetDataColumnValues<Single>("float:Translation.Z") ?? Array.Empty<Single>();
            Column_HandOrientation_X = GetDataColumnValues<Single>("float:HandOrientation.X") ?? Array.Empty<Single>();
            Column_HandOrientation_Y = GetDataColumnValues<Single>("float:HandOrientation.Y") ?? Array.Empty<Single>();
            Column_HandOrientation_Z = GetDataColumnValues<Single>("float:HandOrientation.Z") ?? Array.Empty<Single>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_HostIndex = GetIndexColumnValues("index:Vim.Element:Host") ?? Array.Empty<int>();
            Column_FromRoomIndex = GetIndexColumnValues("index:Vim.Room:FromRoom") ?? Array.Empty<int>();
            Column_ToRoomIndex = GetIndexColumnValues("index:Vim.Room:ToRoom") ?? Array.Empty<int>();
            Column_SuperComponentIndex = GetIndexColumnValues("index:Vim.Element:SuperComponent") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_FacingFlipped { get; }
        public Boolean GetFacingFlipped(int index, Boolean @default = default) => Column_FacingFlipped.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_X { get; }
        public Single GetFacingOrientation_X(int index, Single @default = default) => Column_FacingOrientation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_Y { get; }
        public Single GetFacingOrientation_Y(int index, Single @default = default) => Column_FacingOrientation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_Z { get; }
        public Single GetFacingOrientation_Z(int index, Single @default = default) => Column_FacingOrientation_Z.ElementAtOrDefault(index, @default);
        public Boolean[] Column_HandFlipped { get; }
        public Boolean GetHandFlipped(int index, Boolean @default = default) => Column_HandFlipped.ElementAtOrDefault(index, @default);
        public Boolean[] Column_Mirrored { get; }
        public Boolean GetMirrored(int index, Boolean @default = default) => Column_Mirrored.ElementAtOrDefault(index, @default);
        public Boolean[] Column_HasModifiedGeometry { get; }
        public Boolean GetHasModifiedGeometry(int index, Boolean @default = default) => Column_HasModifiedGeometry.ElementAtOrDefault(index, @default);
        public Single[] Column_Scale { get; }
        public Single GetScale(int index, Single @default = default) => Column_Scale.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_X { get; }
        public Single GetBasisX_X(int index, Single @default = default) => Column_BasisX_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_Y { get; }
        public Single GetBasisX_Y(int index, Single @default = default) => Column_BasisX_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_Z { get; }
        public Single GetBasisX_Z(int index, Single @default = default) => Column_BasisX_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_X { get; }
        public Single GetBasisY_X(int index, Single @default = default) => Column_BasisY_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_Y { get; }
        public Single GetBasisY_Y(int index, Single @default = default) => Column_BasisY_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_Z { get; }
        public Single GetBasisY_Z(int index, Single @default = default) => Column_BasisY_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_X { get; }
        public Single GetBasisZ_X(int index, Single @default = default) => Column_BasisZ_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_Y { get; }
        public Single GetBasisZ_Y(int index, Single @default = default) => Column_BasisZ_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_Z { get; }
        public Single GetBasisZ_Z(int index, Single @default = default) => Column_BasisZ_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_X { get; }
        public Single GetTranslation_X(int index, Single @default = default) => Column_Translation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_Y { get; }
        public Single GetTranslation_Y(int index, Single @default = default) => Column_Translation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_Z { get; }
        public Single GetTranslation_Z(int index, Single @default = default) => Column_Translation_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_X { get; }
        public Single GetHandOrientation_X(int index, Single @default = default) => Column_HandOrientation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_Y { get; }
        public Single GetHandOrientation_Y(int index, Single @default = default) => Column_HandOrientation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_Z { get; }
        public Single GetHandOrientation_Z(int index, Single @default = default) => Column_HandOrientation_Z.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_HostIndex { get; }
        public int GetHostIndex(int index) => Column_HostIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetHost(int index) => _GetReferencedHost(GetHostIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedHost(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        public int[] Column_FromRoomIndex { get; }
        public int GetFromRoomIndex(int index) => Column_FromRoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Room GetFromRoom(int index) => _GetReferencedFromRoom(GetFromRoomIndex(index));
        private Vim.Format.ObjectModel.Room _GetReferencedFromRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        public int[] Column_ToRoomIndex { get; }
        public int GetToRoomIndex(int index) => Column_ToRoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Room GetToRoom(int index) => _GetReferencedToRoom(GetToRoomIndex(index));
        private Vim.Format.ObjectModel.Room _GetReferencedToRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        public int[] Column_SuperComponentIndex { get; }
        public int GetSuperComponentIndex(int index) => Column_SuperComponentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetSuperComponent(int index) => _GetReferencedSuperComponent(GetSuperComponentIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedSuperComponent(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.FamilyInstance Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.FamilyInstance();
            r.Index = index;
            r.FacingFlipped = GetFacingFlipped(index);
            r.FacingOrientation_X = GetFacingOrientation_X(index);
            r.FacingOrientation_Y = GetFacingOrientation_Y(index);
            r.FacingOrientation_Z = GetFacingOrientation_Z(index);
            r.HandFlipped = GetHandFlipped(index);
            r.Mirrored = GetMirrored(index);
            r.HasModifiedGeometry = GetHasModifiedGeometry(index);
            r.Scale = GetScale(index);
            r.BasisX_X = GetBasisX_X(index);
            r.BasisX_Y = GetBasisX_Y(index);
            r.BasisX_Z = GetBasisX_Z(index);
            r.BasisY_X = GetBasisY_X(index);
            r.BasisY_Y = GetBasisY_Y(index);
            r.BasisY_Z = GetBasisY_Z(index);
            r.BasisZ_X = GetBasisZ_X(index);
            r.BasisZ_Y = GetBasisZ_Y(index);
            r.BasisZ_Z = GetBasisZ_Z(index);
            r.Translation_X = GetTranslation_X(index);
            r.Translation_Y = GetTranslation_Y(index);
            r.Translation_Z = GetTranslation_Z(index);
            r.HandOrientation_X = GetHandOrientation_X(index);
            r.HandOrientation_Y = GetHandOrientation_Y(index);
            r.HandOrientation_Z = GetHandOrientation_Z(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Host = new Relation<Vim.Format.ObjectModel.Element>(GetHostIndex(index), _GetReferencedHost);
            r._FromRoom = new Relation<Vim.Format.ObjectModel.Room>(GetFromRoomIndex(index), _GetReferencedFromRoom);
            r._ToRoom = new Relation<Vim.Format.ObjectModel.Room>(GetToRoomIndex(index), _GetReferencedToRoom);
            r._SuperComponent = new Relation<Vim.Format.ObjectModel.Element>(GetSuperComponentIndex(index), _GetReferencedSuperComponent);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.FamilyInstance> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyInstanceTable 
    
    public partial class ViewTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.View>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.View;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Title = GetStringColumnValues("string:Title") ?? Array.Empty<String>();
            Column_ViewType = GetStringColumnValues("string:ViewType") ?? Array.Empty<String>();
            Column_Up_X = GetDataColumnValues<Double>("double:Up.X") ?? Array.Empty<Double>();
            Column_Up_Y = GetDataColumnValues<Double>("double:Up.Y") ?? Array.Empty<Double>();
            Column_Up_Z = GetDataColumnValues<Double>("double:Up.Z") ?? Array.Empty<Double>();
            Column_Right_X = GetDataColumnValues<Double>("double:Right.X") ?? Array.Empty<Double>();
            Column_Right_Y = GetDataColumnValues<Double>("double:Right.Y") ?? Array.Empty<Double>();
            Column_Right_Z = GetDataColumnValues<Double>("double:Right.Z") ?? Array.Empty<Double>();
            Column_Origin_X = GetDataColumnValues<Double>("double:Origin.X") ?? Array.Empty<Double>();
            Column_Origin_Y = GetDataColumnValues<Double>("double:Origin.Y") ?? Array.Empty<Double>();
            Column_Origin_Z = GetDataColumnValues<Double>("double:Origin.Z") ?? Array.Empty<Double>();
            Column_ViewDirection_X = GetDataColumnValues<Double>("double:ViewDirection.X") ?? Array.Empty<Double>();
            Column_ViewDirection_Y = GetDataColumnValues<Double>("double:ViewDirection.Y") ?? Array.Empty<Double>();
            Column_ViewDirection_Z = GetDataColumnValues<Double>("double:ViewDirection.Z") ?? Array.Empty<Double>();
            Column_ViewPosition_X = GetDataColumnValues<Double>("double:ViewPosition.X") ?? Array.Empty<Double>();
            Column_ViewPosition_Y = GetDataColumnValues<Double>("double:ViewPosition.Y") ?? Array.Empty<Double>();
            Column_ViewPosition_Z = GetDataColumnValues<Double>("double:ViewPosition.Z") ?? Array.Empty<Double>();
            Column_Scale = GetDataColumnValues<Double>("double:Scale") ?? Array.Empty<Double>();
            Column_Outline_Min_X = GetDataColumnValues<Double>("double:Outline.Min.X") ?? Array.Empty<Double>();
            Column_Outline_Min_Y = GetDataColumnValues<Double>("double:Outline.Min.Y") ?? Array.Empty<Double>();
            Column_Outline_Max_X = GetDataColumnValues<Double>("double:Outline.Max.X") ?? Array.Empty<Double>();
            Column_Outline_Max_Y = GetDataColumnValues<Double>("double:Outline.Max.Y") ?? Array.Empty<Double>();
            Column_DetailLevel = GetDataColumnValues<Int32>("int:DetailLevel") ?? Array.Empty<Int32>();
            Column_CameraIndex = GetIndexColumnValues("index:Vim.Camera:Camera") ?? Array.Empty<int>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Title { get; }
        public String GetTitle(int index, String @default = "") => Column_Title.ElementAtOrDefault(index, @default);
        public String[] Column_ViewType { get; }
        public String GetViewType(int index, String @default = "") => Column_ViewType.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_X { get; }
        public Double GetUp_X(int index, Double @default = default) => Column_Up_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_Y { get; }
        public Double GetUp_Y(int index, Double @default = default) => Column_Up_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_Z { get; }
        public Double GetUp_Z(int index, Double @default = default) => Column_Up_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_X { get; }
        public Double GetRight_X(int index, Double @default = default) => Column_Right_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_Y { get; }
        public Double GetRight_Y(int index, Double @default = default) => Column_Right_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_Z { get; }
        public Double GetRight_Z(int index, Double @default = default) => Column_Right_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_X { get; }
        public Double GetOrigin_X(int index, Double @default = default) => Column_Origin_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_Y { get; }
        public Double GetOrigin_Y(int index, Double @default = default) => Column_Origin_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_Z { get; }
        public Double GetOrigin_Z(int index, Double @default = default) => Column_Origin_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_X { get; }
        public Double GetViewDirection_X(int index, Double @default = default) => Column_ViewDirection_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_Y { get; }
        public Double GetViewDirection_Y(int index, Double @default = default) => Column_ViewDirection_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_Z { get; }
        public Double GetViewDirection_Z(int index, Double @default = default) => Column_ViewDirection_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_X { get; }
        public Double GetViewPosition_X(int index, Double @default = default) => Column_ViewPosition_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_Y { get; }
        public Double GetViewPosition_Y(int index, Double @default = default) => Column_ViewPosition_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_Z { get; }
        public Double GetViewPosition_Z(int index, Double @default = default) => Column_ViewPosition_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Scale { get; }
        public Double GetScale(int index, Double @default = default) => Column_Scale.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Min_X { get; }
        public Double GetOutline_Min_X(int index, Double @default = default) => Column_Outline_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Min_Y { get; }
        public Double GetOutline_Min_Y(int index, Double @default = default) => Column_Outline_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Max_X { get; }
        public Double GetOutline_Max_X(int index, Double @default = default) => Column_Outline_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Max_Y { get; }
        public Double GetOutline_Max_Y(int index, Double @default = default) => Column_Outline_Max_Y.ElementAtOrDefault(index, @default);
        public Int32[] Column_DetailLevel { get; }
        public Int32 GetDetailLevel(int index, Int32 @default = default) => Column_DetailLevel.ElementAtOrDefault(index, @default);
        public int[] Column_CameraIndex { get; }
        public int GetCameraIndex(int index) => Column_CameraIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Camera GetCamera(int index) => _GetReferencedCamera(GetCameraIndex(index));
        private Vim.Format.ObjectModel.Camera _GetReferencedCamera(int referencedIndex) => ParentTableSet.GetCamera(referencedIndex);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.View Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.View();
            r.Index = index;
            r.Title = GetTitle(index);
            r.ViewType = GetViewType(index);
            r.Up_X = GetUp_X(index);
            r.Up_Y = GetUp_Y(index);
            r.Up_Z = GetUp_Z(index);
            r.Right_X = GetRight_X(index);
            r.Right_Y = GetRight_Y(index);
            r.Right_Z = GetRight_Z(index);
            r.Origin_X = GetOrigin_X(index);
            r.Origin_Y = GetOrigin_Y(index);
            r.Origin_Z = GetOrigin_Z(index);
            r.ViewDirection_X = GetViewDirection_X(index);
            r.ViewDirection_Y = GetViewDirection_Y(index);
            r.ViewDirection_Z = GetViewDirection_Z(index);
            r.ViewPosition_X = GetViewPosition_X(index);
            r.ViewPosition_Y = GetViewPosition_Y(index);
            r.ViewPosition_Z = GetViewPosition_Z(index);
            r.Scale = GetScale(index);
            r.Outline_Min_X = GetOutline_Min_X(index);
            r.Outline_Min_Y = GetOutline_Min_Y(index);
            r.Outline_Max_X = GetOutline_Max_X(index);
            r.Outline_Max_Y = GetOutline_Max_Y(index);
            r.DetailLevel = GetDetailLevel(index);
            r._Camera = new Relation<Vim.Format.ObjectModel.Camera>(GetCameraIndex(index), _GetReferencedCamera);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.View> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewTable 
    
    public partial class ElementInViewTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ElementInView>
    {
        
        public const string TableName = VimEntityTableNames.ElementInView;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInViewTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ElementInView Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ElementInView();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ElementInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInViewTable 
    
    public partial class ShapeInViewTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ShapeInView>
    {
        
        public const string TableName = VimEntityTableNames.ShapeInView;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeInViewTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ShapeIndex = GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public int[] Column_ShapeIndex { get; }
        public int GetShapeIndex(int index) => Column_ShapeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Shape GetShape(int index) => _GetReferencedShape(GetShapeIndex(index));
        private Vim.Format.ObjectModel.Shape _GetReferencedShape(int referencedIndex) => ParentTableSet.GetShape(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ShapeInView Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ShapeInView();
            r.Index = index;
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeIndex(index), _GetReferencedShape);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ShapeInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeInViewTable 
    
    public partial class AssetInViewTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.AssetInView>
    {
        
        public const string TableName = VimEntityTableNames.AssetInView;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AssetInViewTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssetIndex = GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public int[] Column_AssetIndex { get; }
        public int GetAssetIndex(int index) => Column_AssetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Asset GetAsset(int index) => _GetReferencedAsset(GetAssetIndex(index));
        private Vim.Format.ObjectModel.Asset _GetReferencedAsset(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.AssetInView Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.AssetInView();
            r.Index = index;
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetIndex(index), _GetReferencedAsset);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.AssetInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetInViewTable 
    
    public partial class AssetInViewSheetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.AssetInViewSheet>
    {
        
        public const string TableName = VimEntityTableNames.AssetInViewSheet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AssetInViewSheetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssetIndex = GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>();
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
        }
        
        public int[] Column_AssetIndex { get; }
        public int GetAssetIndex(int index) => Column_AssetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Asset GetAsset(int index) => _GetReferencedAsset(GetAssetIndex(index));
        private Vim.Format.ObjectModel.Asset _GetReferencedAsset(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private Vim.Format.ObjectModel.ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.AssetInViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.AssetInViewSheet();
            r.Index = index;
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetIndex(index), _GetReferencedAsset);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.AssetInViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetInViewSheetTable 
    
    public partial class LevelInViewTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.LevelInView>
    {
        
        public const string TableName = VimEntityTableNames.LevelInView;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public LevelInViewTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Extents_Min_X = GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>();
            Column_Extents_Min_Y = GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>();
            Column_Extents_Min_Z = GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>();
            Column_Extents_Max_X = GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>();
            Column_Extents_Max_Y = GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>();
            Column_Extents_Max_Z = GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>();
            Column_LevelIndex = GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Extents_Min_X { get; }
        public Double GetExtents_Min_X(int index, Double @default = default) => Column_Extents_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Y { get; }
        public Double GetExtents_Min_Y(int index, Double @default = default) => Column_Extents_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Z { get; }
        public Double GetExtents_Min_Z(int index, Double @default = default) => Column_Extents_Min_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_X { get; }
        public Double GetExtents_Max_X(int index, Double @default = default) => Column_Extents_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Y { get; }
        public Double GetExtents_Max_Y(int index, Double @default = default) => Column_Extents_Max_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Z { get; }
        public Double GetExtents_Max_Z(int index, Double @default = default) => Column_Extents_Max_Z.ElementAtOrDefault(index, @default);
        public int[] Column_LevelIndex { get; }
        public int GetLevelIndex(int index) => Column_LevelIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Level GetLevel(int index) => _GetReferencedLevel(GetLevelIndex(index));
        private Vim.Format.ObjectModel.Level _GetReferencedLevel(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.LevelInView Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.LevelInView();
            r.Index = index;
            r.Extents_Min_X = GetExtents_Min_X(index);
            r.Extents_Min_Y = GetExtents_Min_Y(index);
            r.Extents_Min_Z = GetExtents_Min_Z(index);
            r.Extents_Max_X = GetExtents_Max_X(index);
            r.Extents_Max_Y = GetExtents_Max_Y(index);
            r.Extents_Max_Z = GetExtents_Max_Z(index);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetLevelIndex(index), _GetReferencedLevel);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.LevelInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class LevelInViewTable 
    
    public partial class CameraTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Camera>
    {
        
        public const string TableName = VimEntityTableNames.Camera;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public CameraTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>();
            Column_IsPerspective = GetDataColumnValues<Int32>("int:IsPerspective") ?? Array.Empty<Int32>();
            Column_VerticalExtent = GetDataColumnValues<Double>("double:VerticalExtent") ?? Array.Empty<Double>();
            Column_HorizontalExtent = GetDataColumnValues<Double>("double:HorizontalExtent") ?? Array.Empty<Double>();
            Column_FarDistance = GetDataColumnValues<Double>("double:FarDistance") ?? Array.Empty<Double>();
            Column_NearDistance = GetDataColumnValues<Double>("double:NearDistance") ?? Array.Empty<Double>();
            Column_TargetDistance = GetDataColumnValues<Double>("double:TargetDistance") ?? Array.Empty<Double>();
            Column_RightOffset = GetDataColumnValues<Double>("double:RightOffset") ?? Array.Empty<Double>();
            Column_UpOffset = GetDataColumnValues<Double>("double:UpOffset") ?? Array.Empty<Double>();
        }
        
        public Int32[] Column_Id { get; }
        public Int32 GetId(int index, Int32 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public Int32[] Column_IsPerspective { get; }
        public Int32 GetIsPerspective(int index, Int32 @default = default) => Column_IsPerspective.ElementAtOrDefault(index, @default);
        public Double[] Column_VerticalExtent { get; }
        public Double GetVerticalExtent(int index, Double @default = default) => Column_VerticalExtent.ElementAtOrDefault(index, @default);
        public Double[] Column_HorizontalExtent { get; }
        public Double GetHorizontalExtent(int index, Double @default = default) => Column_HorizontalExtent.ElementAtOrDefault(index, @default);
        public Double[] Column_FarDistance { get; }
        public Double GetFarDistance(int index, Double @default = default) => Column_FarDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_NearDistance { get; }
        public Double GetNearDistance(int index, Double @default = default) => Column_NearDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_TargetDistance { get; }
        public Double GetTargetDistance(int index, Double @default = default) => Column_TargetDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_RightOffset { get; }
        public Double GetRightOffset(int index, Double @default = default) => Column_RightOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_UpOffset { get; }
        public Double GetUpOffset(int index, Double @default = default) => Column_UpOffset.ElementAtOrDefault(index, @default);
        // Object Getter
        public Vim.Format.ObjectModel.Camera Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Camera();
            r.Index = index;
            r.Id = GetId(index);
            r.IsPerspective = GetIsPerspective(index);
            r.VerticalExtent = GetVerticalExtent(index);
            r.HorizontalExtent = GetHorizontalExtent(index);
            r.FarDistance = GetFarDistance(index);
            r.NearDistance = GetNearDistance(index);
            r.TargetDistance = GetTargetDistance(index);
            r.RightOffset = GetRightOffset(index);
            r.UpOffset = GetUpOffset(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Camera> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CameraTable 
    
    public partial class MaterialTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Material>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Material;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public MaterialTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_MaterialCategory = GetStringColumnValues("string:MaterialCategory") ?? Array.Empty<String>();
            Column_Color_X = GetDataColumnValues<Double>("double:Color.X") ?? Array.Empty<Double>();
            Column_Color_Y = GetDataColumnValues<Double>("double:Color.Y") ?? Array.Empty<Double>();
            Column_Color_Z = GetDataColumnValues<Double>("double:Color.Z") ?? Array.Empty<Double>();
            Column_ColorUvScaling_X = GetDataColumnValues<Double>("double:ColorUvScaling.X") ?? Array.Empty<Double>();
            Column_ColorUvScaling_Y = GetDataColumnValues<Double>("double:ColorUvScaling.Y") ?? Array.Empty<Double>();
            Column_ColorUvOffset_X = GetDataColumnValues<Double>("double:ColorUvOffset.X") ?? Array.Empty<Double>();
            Column_ColorUvOffset_Y = GetDataColumnValues<Double>("double:ColorUvOffset.Y") ?? Array.Empty<Double>();
            Column_NormalUvScaling_X = GetDataColumnValues<Double>("double:NormalUvScaling.X") ?? Array.Empty<Double>();
            Column_NormalUvScaling_Y = GetDataColumnValues<Double>("double:NormalUvScaling.Y") ?? Array.Empty<Double>();
            Column_NormalUvOffset_X = GetDataColumnValues<Double>("double:NormalUvOffset.X") ?? Array.Empty<Double>();
            Column_NormalUvOffset_Y = GetDataColumnValues<Double>("double:NormalUvOffset.Y") ?? Array.Empty<Double>();
            Column_NormalAmount = GetDataColumnValues<Double>("double:NormalAmount") ?? Array.Empty<Double>();
            Column_Glossiness = GetDataColumnValues<Double>("double:Glossiness") ?? Array.Empty<Double>();
            Column_Smoothness = GetDataColumnValues<Double>("double:Smoothness") ?? Array.Empty<Double>();
            Column_Transparency = GetDataColumnValues<Double>("double:Transparency") ?? Array.Empty<Double>();
            Column_ColorTextureFileIndex = GetIndexColumnValues("index:Vim.Asset:ColorTextureFile") ?? Array.Empty<int>();
            Column_NormalTextureFileIndex = GetIndexColumnValues("index:Vim.Asset:NormalTextureFile") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_MaterialCategory { get; }
        public String GetMaterialCategory(int index, String @default = "") => Column_MaterialCategory.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_X { get; }
        public Double GetColor_X(int index, Double @default = default) => Column_Color_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_Y { get; }
        public Double GetColor_Y(int index, Double @default = default) => Column_Color_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_Z { get; }
        public Double GetColor_Z(int index, Double @default = default) => Column_Color_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvScaling_X { get; }
        public Double GetColorUvScaling_X(int index, Double @default = default) => Column_ColorUvScaling_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvScaling_Y { get; }
        public Double GetColorUvScaling_Y(int index, Double @default = default) => Column_ColorUvScaling_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvOffset_X { get; }
        public Double GetColorUvOffset_X(int index, Double @default = default) => Column_ColorUvOffset_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvOffset_Y { get; }
        public Double GetColorUvOffset_Y(int index, Double @default = default) => Column_ColorUvOffset_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvScaling_X { get; }
        public Double GetNormalUvScaling_X(int index, Double @default = default) => Column_NormalUvScaling_X.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvScaling_Y { get; }
        public Double GetNormalUvScaling_Y(int index, Double @default = default) => Column_NormalUvScaling_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvOffset_X { get; }
        public Double GetNormalUvOffset_X(int index, Double @default = default) => Column_NormalUvOffset_X.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvOffset_Y { get; }
        public Double GetNormalUvOffset_Y(int index, Double @default = default) => Column_NormalUvOffset_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalAmount { get; }
        public Double GetNormalAmount(int index, Double @default = default) => Column_NormalAmount.ElementAtOrDefault(index, @default);
        public Double[] Column_Glossiness { get; }
        public Double GetGlossiness(int index, Double @default = default) => Column_Glossiness.ElementAtOrDefault(index, @default);
        public Double[] Column_Smoothness { get; }
        public Double GetSmoothness(int index, Double @default = default) => Column_Smoothness.ElementAtOrDefault(index, @default);
        public Double[] Column_Transparency { get; }
        public Double GetTransparency(int index, Double @default = default) => Column_Transparency.ElementAtOrDefault(index, @default);
        public int[] Column_ColorTextureFileIndex { get; }
        public int GetColorTextureFileIndex(int index) => Column_ColorTextureFileIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Asset GetColorTextureFile(int index) => _GetReferencedColorTextureFile(GetColorTextureFileIndex(index));
        private Vim.Format.ObjectModel.Asset _GetReferencedColorTextureFile(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_NormalTextureFileIndex { get; }
        public int GetNormalTextureFileIndex(int index) => Column_NormalTextureFileIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Asset GetNormalTextureFile(int index) => _GetReferencedNormalTextureFile(GetNormalTextureFileIndex(index));
        private Vim.Format.ObjectModel.Asset _GetReferencedNormalTextureFile(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Material Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Material();
            r.Index = index;
            r.Name = GetName(index);
            r.MaterialCategory = GetMaterialCategory(index);
            r.Color_X = GetColor_X(index);
            r.Color_Y = GetColor_Y(index);
            r.Color_Z = GetColor_Z(index);
            r.ColorUvScaling_X = GetColorUvScaling_X(index);
            r.ColorUvScaling_Y = GetColorUvScaling_Y(index);
            r.ColorUvOffset_X = GetColorUvOffset_X(index);
            r.ColorUvOffset_Y = GetColorUvOffset_Y(index);
            r.NormalUvScaling_X = GetNormalUvScaling_X(index);
            r.NormalUvScaling_Y = GetNormalUvScaling_Y(index);
            r.NormalUvOffset_X = GetNormalUvOffset_X(index);
            r.NormalUvOffset_Y = GetNormalUvOffset_Y(index);
            r.NormalAmount = GetNormalAmount(index);
            r.Glossiness = GetGlossiness(index);
            r.Smoothness = GetSmoothness(index);
            r.Transparency = GetTransparency(index);
            r._ColorTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetColorTextureFileIndex(index), _GetReferencedColorTextureFile);
            r._NormalTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetNormalTextureFileIndex(index), _GetReferencedNormalTextureFile);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Material> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class MaterialTable 
    
    public partial class MaterialInElementTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.MaterialInElement>
    {
        
        public const string TableName = VimEntityTableNames.MaterialInElement;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public MaterialInElementTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Area = GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>();
            Column_Volume = GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>();
            Column_IsPaint = GetDataColumnValues<Boolean>("byte:IsPaint") ?? Array.Empty<Boolean>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Area { get; }
        public Double GetArea(int index, Double @default = default) => Column_Area.ElementAtOrDefault(index, @default);
        public Double[] Column_Volume { get; }
        public Double GetVolume(int index, Double @default = default) => Column_Volume.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsPaint { get; }
        public Boolean GetIsPaint(int index, Boolean @default = default) => Column_IsPaint.ElementAtOrDefault(index, @default);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Vim.Format.ObjectModel.Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.MaterialInElement Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.MaterialInElement();
            r.Index = index;
            r.Area = GetArea(index);
            r.Volume = GetVolume(index);
            r.IsPaint = GetIsPaint(index);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.MaterialInElement> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class MaterialInElementTable 
    
    public partial class CompoundStructureLayerTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.CompoundStructureLayer>
    {
        
        public const string TableName = VimEntityTableNames.CompoundStructureLayer;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public CompoundStructureLayerTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_OrderIndex = GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>();
            Column_Width = GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>();
            Column_MaterialFunctionAssignment = GetStringColumnValues("string:MaterialFunctionAssignment") ?? Array.Empty<String>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
            Column_CompoundStructureIndex = GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_OrderIndex { get; }
        public Int32 GetOrderIndex(int index, Int32 @default = default) => Column_OrderIndex.ElementAtOrDefault(index, @default);
        public Double[] Column_Width { get; }
        public Double GetWidth(int index, Double @default = default) => Column_Width.ElementAtOrDefault(index, @default);
        public String[] Column_MaterialFunctionAssignment { get; }
        public String GetMaterialFunctionAssignment(int index, String @default = "") => Column_MaterialFunctionAssignment.ElementAtOrDefault(index, @default);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Vim.Format.ObjectModel.Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        public int[] Column_CompoundStructureIndex { get; }
        public int GetCompoundStructureIndex(int index) => Column_CompoundStructureIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.CompoundStructure GetCompoundStructure(int index) => _GetReferencedCompoundStructure(GetCompoundStructureIndex(index));
        private Vim.Format.ObjectModel.CompoundStructure _GetReferencedCompoundStructure(int referencedIndex) => ParentTableSet.GetCompoundStructure(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.CompoundStructureLayer Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.CompoundStructureLayer();
            r.Index = index;
            r.OrderIndex = GetOrderIndex(index);
            r.Width = GetWidth(index);
            r.MaterialFunctionAssignment = GetMaterialFunctionAssignment(index);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetCompoundStructureIndex(index), _GetReferencedCompoundStructure);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.CompoundStructureLayer> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CompoundStructureLayerTable 
    
    public partial class CompoundStructureTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.CompoundStructure>
    {
        
        public const string TableName = VimEntityTableNames.CompoundStructure;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public CompoundStructureTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Width = GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>();
            Column_StructuralLayerIndex = GetIndexColumnValues("index:Vim.CompoundStructureLayer:StructuralLayer") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Width { get; }
        public Double GetWidth(int index, Double @default = default) => Column_Width.ElementAtOrDefault(index, @default);
        public int[] Column_StructuralLayerIndex { get; }
        public int GetStructuralLayerIndex(int index) => Column_StructuralLayerIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.CompoundStructureLayer GetStructuralLayer(int index) => _GetReferencedStructuralLayer(GetStructuralLayerIndex(index));
        private Vim.Format.ObjectModel.CompoundStructureLayer _GetReferencedStructuralLayer(int referencedIndex) => ParentTableSet.GetCompoundStructureLayer(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.CompoundStructure Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.CompoundStructure();
            r.Index = index;
            r.Width = GetWidth(index);
            r._StructuralLayer = new Relation<Vim.Format.ObjectModel.CompoundStructureLayer>(GetStructuralLayerIndex(index), _GetReferencedStructuralLayer);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.CompoundStructure> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CompoundStructureTable 
    
    public partial class NodeTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Node>
    {
        
        public const string TableName = VimEntityTableNames.Node;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public NodeTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Node Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Node();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Node> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class NodeTable 
    
    public partial class GeometryTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Geometry>
    {
        
        public const string TableName = VimEntityTableNames.Geometry;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public GeometryTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Box_Min_X = GetDataColumnValues<Single>("float:Box.Min.X") ?? Array.Empty<Single>();
            Column_Box_Min_Y = GetDataColumnValues<Single>("float:Box.Min.Y") ?? Array.Empty<Single>();
            Column_Box_Min_Z = GetDataColumnValues<Single>("float:Box.Min.Z") ?? Array.Empty<Single>();
            Column_Box_Max_X = GetDataColumnValues<Single>("float:Box.Max.X") ?? Array.Empty<Single>();
            Column_Box_Max_Y = GetDataColumnValues<Single>("float:Box.Max.Y") ?? Array.Empty<Single>();
            Column_Box_Max_Z = GetDataColumnValues<Single>("float:Box.Max.Z") ?? Array.Empty<Single>();
            Column_VertexCount = GetDataColumnValues<Int32>("int:VertexCount") ?? Array.Empty<Int32>();
            Column_FaceCount = GetDataColumnValues<Int32>("int:FaceCount") ?? Array.Empty<Int32>();
        }
        
        public Single[] Column_Box_Min_X { get; }
        public Single GetBox_Min_X(int index, Single @default = default) => Column_Box_Min_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Min_Y { get; }
        public Single GetBox_Min_Y(int index, Single @default = default) => Column_Box_Min_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Min_Z { get; }
        public Single GetBox_Min_Z(int index, Single @default = default) => Column_Box_Min_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_X { get; }
        public Single GetBox_Max_X(int index, Single @default = default) => Column_Box_Max_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_Y { get; }
        public Single GetBox_Max_Y(int index, Single @default = default) => Column_Box_Max_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_Z { get; }
        public Single GetBox_Max_Z(int index, Single @default = default) => Column_Box_Max_Z.ElementAtOrDefault(index, @default);
        public Int32[] Column_VertexCount { get; }
        public Int32 GetVertexCount(int index, Int32 @default = default) => Column_VertexCount.ElementAtOrDefault(index, @default);
        public Int32[] Column_FaceCount { get; }
        public Int32 GetFaceCount(int index, Int32 @default = default) => Column_FaceCount.ElementAtOrDefault(index, @default);
        // Object Getter
        public Vim.Format.ObjectModel.Geometry Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Geometry();
            r.Index = index;
            r.Box_Min_X = GetBox_Min_X(index);
            r.Box_Min_Y = GetBox_Min_Y(index);
            r.Box_Min_Z = GetBox_Min_Z(index);
            r.Box_Max_X = GetBox_Max_X(index);
            r.Box_Max_Y = GetBox_Max_Y(index);
            r.Box_Max_Z = GetBox_Max_Z(index);
            r.VertexCount = GetVertexCount(index);
            r.FaceCount = GetFaceCount(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Geometry> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GeometryTable 
    
    public partial class ShapeTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Shape>
    {
        
        public const string TableName = VimEntityTableNames.Shape;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Shape Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Shape();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Shape> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeTable 
    
    public partial class ShapeCollectionTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ShapeCollection>
    {
        
        public const string TableName = VimEntityTableNames.ShapeCollection;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeCollectionTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ShapeCollection Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ShapeCollection();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ShapeCollection> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeCollectionTable 
    
    public partial class ShapeInShapeCollectionTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ShapeInShapeCollection>
    {
        
        public const string TableName = VimEntityTableNames.ShapeInShapeCollection;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeInShapeCollectionTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ShapeIndex = GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>();
            Column_ShapeCollectionIndex = GetIndexColumnValues("index:Vim.ShapeCollection:ShapeCollection") ?? Array.Empty<int>();
        }
        
        public int[] Column_ShapeIndex { get; }
        public int GetShapeIndex(int index) => Column_ShapeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Shape GetShape(int index) => _GetReferencedShape(GetShapeIndex(index));
        private Vim.Format.ObjectModel.Shape _GetReferencedShape(int referencedIndex) => ParentTableSet.GetShape(referencedIndex);
        public int[] Column_ShapeCollectionIndex { get; }
        public int GetShapeCollectionIndex(int index) => Column_ShapeCollectionIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ShapeCollection GetShapeCollection(int index) => _GetReferencedShapeCollection(GetShapeCollectionIndex(index));
        private Vim.Format.ObjectModel.ShapeCollection _GetReferencedShapeCollection(int referencedIndex) => ParentTableSet.GetShapeCollection(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ShapeInShapeCollection Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ShapeInShapeCollection();
            r.Index = index;
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeIndex(index), _GetReferencedShape);
            r._ShapeCollection = new Relation<Vim.Format.ObjectModel.ShapeCollection>(GetShapeCollectionIndex(index), _GetReferencedShapeCollection);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ShapeInShapeCollection> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeInShapeCollectionTable 
    
    public partial class SystemTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.System>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.System;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public SystemTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_SystemType = GetDataColumnValues<Int32>("int:SystemType") ?? Array.Empty<Int32>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_SystemType { get; }
        public Int32 GetSystemType(int index, Int32 @default = default) => Column_SystemType.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.System Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.System();
            r.Index = index;
            r.SystemType = GetSystemType(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.System> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class SystemTable 
    
    public partial class ElementInSystemTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ElementInSystem>
    {
        
        public const string TableName = VimEntityTableNames.ElementInSystem;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInSystemTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Roles = GetDataColumnValues<Int32>("int:Roles") ?? Array.Empty<Int32>();
            Column_SystemIndex = GetIndexColumnValues("index:Vim.System:System") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_Roles { get; }
        public Int32 GetRoles(int index, Int32 @default = default) => Column_Roles.ElementAtOrDefault(index, @default);
        public int[] Column_SystemIndex { get; }
        public int GetSystemIndex(int index) => Column_SystemIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.System GetSystem(int index) => _GetReferencedSystem(GetSystemIndex(index));
        private Vim.Format.ObjectModel.System _GetReferencedSystem(int referencedIndex) => ParentTableSet.GetSystem(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ElementInSystem Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ElementInSystem();
            r.Index = index;
            r.Roles = GetRoles(index);
            r._System = new Relation<Vim.Format.ObjectModel.System>(GetSystemIndex(index), _GetReferencedSystem);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ElementInSystem> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInSystemTable 
    
    public partial class WarningTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Warning>
    {
        
        public const string TableName = VimEntityTableNames.Warning;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public WarningTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_Severity = GetStringColumnValues("string:Severity") ?? Array.Empty<String>();
            Column_Description = GetStringColumnValues("string:Description") ?? Array.Empty<String>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public String[] Column_Severity { get; }
        public String GetSeverity(int index, String @default = "") => Column_Severity.ElementAtOrDefault(index, @default);
        public String[] Column_Description { get; }
        public String GetDescription(int index, String @default = "") => Column_Description.ElementAtOrDefault(index, @default);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private Vim.Format.ObjectModel.BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Warning Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Warning();
            r.Index = index;
            r.Guid = GetGuid(index);
            r.Severity = GetSeverity(index);
            r.Description = GetDescription(index);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Warning> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class WarningTable 
    
    public partial class ElementInWarningTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ElementInWarning>
    {
        
        public const string TableName = VimEntityTableNames.ElementInWarning;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInWarningTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_WarningIndex = GetIndexColumnValues("index:Vim.Warning:Warning") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_WarningIndex { get; }
        public int GetWarningIndex(int index) => Column_WarningIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Warning GetWarning(int index) => _GetReferencedWarning(GetWarningIndex(index));
        private Vim.Format.ObjectModel.Warning _GetReferencedWarning(int referencedIndex) => ParentTableSet.GetWarning(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ElementInWarning Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ElementInWarning();
            r.Index = index;
            r._Warning = new Relation<Vim.Format.ObjectModel.Warning>(GetWarningIndex(index), _GetReferencedWarning);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ElementInWarning> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInWarningTable 
    
    public partial class BasePointTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.BasePoint>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.BasePoint;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public BasePointTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsSurveyPoint = GetDataColumnValues<Boolean>("byte:IsSurveyPoint") ?? Array.Empty<Boolean>();
            Column_Position_X = GetDataColumnValues<Double>("double:Position.X") ?? Array.Empty<Double>();
            Column_Position_Y = GetDataColumnValues<Double>("double:Position.Y") ?? Array.Empty<Double>();
            Column_Position_Z = GetDataColumnValues<Double>("double:Position.Z") ?? Array.Empty<Double>();
            Column_SharedPosition_X = GetDataColumnValues<Double>("double:SharedPosition.X") ?? Array.Empty<Double>();
            Column_SharedPosition_Y = GetDataColumnValues<Double>("double:SharedPosition.Y") ?? Array.Empty<Double>();
            Column_SharedPosition_Z = GetDataColumnValues<Double>("double:SharedPosition.Z") ?? Array.Empty<Double>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsSurveyPoint { get; }
        public Boolean GetIsSurveyPoint(int index, Boolean @default = default) => Column_IsSurveyPoint.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_X { get; }
        public Double GetPosition_X(int index, Double @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_Y { get; }
        public Double GetPosition_Y(int index, Double @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_Z { get; }
        public Double GetPosition_Z(int index, Double @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_X { get; }
        public Double GetSharedPosition_X(int index, Double @default = default) => Column_SharedPosition_X.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_Y { get; }
        public Double GetSharedPosition_Y(int index, Double @default = default) => Column_SharedPosition_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_Z { get; }
        public Double GetSharedPosition_Z(int index, Double @default = default) => Column_SharedPosition_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.BasePoint Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.BasePoint();
            r.Index = index;
            r.IsSurveyPoint = GetIsSurveyPoint(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r.SharedPosition_X = GetSharedPosition_X(index);
            r.SharedPosition_Y = GetSharedPosition_Y(index);
            r.SharedPosition_Z = GetSharedPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.BasePoint> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BasePointTable 
    
    public partial class PhaseFilterTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.PhaseFilter>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.PhaseFilter;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseFilterTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_New = GetDataColumnValues<Int32>("int:New") ?? Array.Empty<Int32>();
            Column_Existing = GetDataColumnValues<Int32>("int:Existing") ?? Array.Empty<Int32>();
            Column_Demolished = GetDataColumnValues<Int32>("int:Demolished") ?? Array.Empty<Int32>();
            Column_Temporary = GetDataColumnValues<Int32>("int:Temporary") ?? Array.Empty<Int32>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_New { get; }
        public Int32 GetNew(int index, Int32 @default = default) => Column_New.ElementAtOrDefault(index, @default);
        public Int32[] Column_Existing { get; }
        public Int32 GetExisting(int index, Int32 @default = default) => Column_Existing.ElementAtOrDefault(index, @default);
        public Int32[] Column_Demolished { get; }
        public Int32 GetDemolished(int index, Int32 @default = default) => Column_Demolished.ElementAtOrDefault(index, @default);
        public Int32[] Column_Temporary { get; }
        public Int32 GetTemporary(int index, Int32 @default = default) => Column_Temporary.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.PhaseFilter Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.PhaseFilter();
            r.Index = index;
            r.New = GetNew(index);
            r.Existing = GetExisting(index);
            r.Demolished = GetDemolished(index);
            r.Temporary = GetTemporary(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.PhaseFilter> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseFilterTable 
    
    public partial class GridTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Grid>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Grid;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public GridTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_StartPoint_X = GetDataColumnValues<Double>("double:StartPoint.X") ?? Array.Empty<Double>();
            Column_StartPoint_Y = GetDataColumnValues<Double>("double:StartPoint.Y") ?? Array.Empty<Double>();
            Column_StartPoint_Z = GetDataColumnValues<Double>("double:StartPoint.Z") ?? Array.Empty<Double>();
            Column_EndPoint_X = GetDataColumnValues<Double>("double:EndPoint.X") ?? Array.Empty<Double>();
            Column_EndPoint_Y = GetDataColumnValues<Double>("double:EndPoint.Y") ?? Array.Empty<Double>();
            Column_EndPoint_Z = GetDataColumnValues<Double>("double:EndPoint.Z") ?? Array.Empty<Double>();
            Column_IsCurved = GetDataColumnValues<Boolean>("byte:IsCurved") ?? Array.Empty<Boolean>();
            Column_Extents_Min_X = GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>();
            Column_Extents_Min_Y = GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>();
            Column_Extents_Min_Z = GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>();
            Column_Extents_Max_X = GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>();
            Column_Extents_Max_Y = GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>();
            Column_Extents_Max_Z = GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_StartPoint_X { get; }
        public Double GetStartPoint_X(int index, Double @default = default) => Column_StartPoint_X.ElementAtOrDefault(index, @default);
        public Double[] Column_StartPoint_Y { get; }
        public Double GetStartPoint_Y(int index, Double @default = default) => Column_StartPoint_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_StartPoint_Z { get; }
        public Double GetStartPoint_Z(int index, Double @default = default) => Column_StartPoint_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_X { get; }
        public Double GetEndPoint_X(int index, Double @default = default) => Column_EndPoint_X.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_Y { get; }
        public Double GetEndPoint_Y(int index, Double @default = default) => Column_EndPoint_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_Z { get; }
        public Double GetEndPoint_Z(int index, Double @default = default) => Column_EndPoint_Z.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsCurved { get; }
        public Boolean GetIsCurved(int index, Boolean @default = default) => Column_IsCurved.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_X { get; }
        public Double GetExtents_Min_X(int index, Double @default = default) => Column_Extents_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Y { get; }
        public Double GetExtents_Min_Y(int index, Double @default = default) => Column_Extents_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Z { get; }
        public Double GetExtents_Min_Z(int index, Double @default = default) => Column_Extents_Min_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_X { get; }
        public Double GetExtents_Max_X(int index, Double @default = default) => Column_Extents_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Y { get; }
        public Double GetExtents_Max_Y(int index, Double @default = default) => Column_Extents_Max_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Z { get; }
        public Double GetExtents_Max_Z(int index, Double @default = default) => Column_Extents_Max_Z.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Grid Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Grid();
            r.Index = index;
            r.StartPoint_X = GetStartPoint_X(index);
            r.StartPoint_Y = GetStartPoint_Y(index);
            r.StartPoint_Z = GetStartPoint_Z(index);
            r.EndPoint_X = GetEndPoint_X(index);
            r.EndPoint_Y = GetEndPoint_Y(index);
            r.EndPoint_Z = GetEndPoint_Z(index);
            r.IsCurved = GetIsCurved(index);
            r.Extents_Min_X = GetExtents_Min_X(index);
            r.Extents_Min_Y = GetExtents_Min_Y(index);
            r.Extents_Min_Z = GetExtents_Min_Z(index);
            r.Extents_Max_X = GetExtents_Max_X(index);
            r.Extents_Max_Y = GetExtents_Max_Y(index);
            r.Extents_Max_Z = GetExtents_Max_Z(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Grid> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GridTable 
    
    public partial class AreaTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Area>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Area;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AreaTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetDataColumnValues<Double>("double:Value") ?? Array.Empty<Double>();
            Column_Perimeter = GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_IsGrossInterior = GetDataColumnValues<Boolean>("byte:IsGrossInterior") ?? Array.Empty<Boolean>();
            Column_AreaSchemeIndex = GetIndexColumnValues("index:Vim.AreaScheme:AreaScheme") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Value { get; }
        public Double GetValue(int index, Double @default = default) => Column_Value.ElementAtOrDefault(index, @default);
        public Double[] Column_Perimeter { get; }
        public Double GetPerimeter(int index, Double @default = default) => Column_Perimeter.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsGrossInterior { get; }
        public Boolean GetIsGrossInterior(int index, Boolean @default = default) => Column_IsGrossInterior.ElementAtOrDefault(index, @default);
        public int[] Column_AreaSchemeIndex { get; }
        public int GetAreaSchemeIndex(int index) => Column_AreaSchemeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.AreaScheme GetAreaScheme(int index) => _GetReferencedAreaScheme(GetAreaSchemeIndex(index));
        private Vim.Format.ObjectModel.AreaScheme _GetReferencedAreaScheme(int referencedIndex) => ParentTableSet.GetAreaScheme(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Area Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Area();
            r.Index = index;
            r.Value = GetValue(index);
            r.Perimeter = GetPerimeter(index);
            r.Number = GetNumber(index);
            r.IsGrossInterior = GetIsGrossInterior(index);
            r._AreaScheme = new Relation<Vim.Format.ObjectModel.AreaScheme>(GetAreaSchemeIndex(index), _GetReferencedAreaScheme);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Area> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AreaTable 
    
    public partial class AreaSchemeTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.AreaScheme>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.AreaScheme;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public AreaSchemeTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsGrossBuildingArea = GetDataColumnValues<Boolean>("byte:IsGrossBuildingArea") ?? Array.Empty<Boolean>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsGrossBuildingArea { get; }
        public Boolean GetIsGrossBuildingArea(int index, Boolean @default = default) => Column_IsGrossBuildingArea.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.AreaScheme Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.AreaScheme();
            r.Index = index;
            r.IsGrossBuildingArea = GetIsGrossBuildingArea(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.AreaScheme> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AreaSchemeTable 
    
    public partial class ScheduleTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Schedule>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Schedule;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Schedule Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Schedule();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Schedule> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleTable 
    
    public partial class ScheduleColumnTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ScheduleColumn>
    {
        
        public const string TableName = VimEntityTableNames.ScheduleColumn;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleColumnTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_ColumnIndex = GetDataColumnValues<Int32>("int:ColumnIndex") ?? Array.Empty<Int32>();
            Column_ScheduleIndex = GetIndexColumnValues("index:Vim.Schedule:Schedule") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public Int32[] Column_ColumnIndex { get; }
        public Int32 GetColumnIndex(int index, Int32 @default = default) => Column_ColumnIndex.ElementAtOrDefault(index, @default);
        public int[] Column_ScheduleIndex { get; }
        public int GetScheduleIndex(int index) => Column_ScheduleIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Schedule GetSchedule(int index) => _GetReferencedSchedule(GetScheduleIndex(index));
        private Vim.Format.ObjectModel.Schedule _GetReferencedSchedule(int referencedIndex) => ParentTableSet.GetSchedule(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ScheduleColumn Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ScheduleColumn();
            r.Index = index;
            r.Name = GetName(index);
            r.ColumnIndex = GetColumnIndex(index);
            r._Schedule = new Relation<Vim.Format.ObjectModel.Schedule>(GetScheduleIndex(index), _GetReferencedSchedule);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ScheduleColumn> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleColumnTable 
    
    public partial class ScheduleCellTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ScheduleCell>
    {
        
        public const string TableName = VimEntityTableNames.ScheduleCell;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleCellTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetStringColumnValues("string:Value") ?? Array.Empty<String>();
            Column_RowIndex = GetDataColumnValues<Int32>("int:RowIndex") ?? Array.Empty<Int32>();
            Column_ScheduleColumnIndex = GetIndexColumnValues("index:Vim.ScheduleColumn:ScheduleColumn") ?? Array.Empty<int>();
        }
        
        public String[] Column_Value { get; }
        public String GetValue(int index, String @default = "") => Column_Value.ElementAtOrDefault(index, @default);
        public Int32[] Column_RowIndex { get; }
        public Int32 GetRowIndex(int index, Int32 @default = default) => Column_RowIndex.ElementAtOrDefault(index, @default);
        public int[] Column_ScheduleColumnIndex { get; }
        public int GetScheduleColumnIndex(int index) => Column_ScheduleColumnIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ScheduleColumn GetScheduleColumn(int index) => _GetReferencedScheduleColumn(GetScheduleColumnIndex(index));
        private Vim.Format.ObjectModel.ScheduleColumn _GetReferencedScheduleColumn(int referencedIndex) => ParentTableSet.GetScheduleColumn(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ScheduleCell Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ScheduleCell();
            r.Index = index;
            r.Value = GetValue(index);
            r.RowIndex = GetRowIndex(index);
            r._ScheduleColumn = new Relation<Vim.Format.ObjectModel.ScheduleColumn>(GetScheduleColumnIndex(index), _GetReferencedScheduleColumn);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ScheduleCell> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleCellTable 
    
    public partial class ViewSheetSetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ViewSheetSet>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.ViewSheetSet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetSetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ViewSheetSet();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetSetTable 
    
    public partial class ViewSheetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ViewSheet>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.ViewSheet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private Vim.Format.ObjectModel.FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ViewSheet();
            r.Index = index;
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetTable 
    
    public partial class ViewSheetInViewSheetSetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ViewSheetInViewSheetSet>
    {
        
        public const string TableName = VimEntityTableNames.ViewSheetInViewSheetSet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetInViewSheetSetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
            Column_ViewSheetSetIndex = GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private Vim.Format.ObjectModel.ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        public int[] Column_ViewSheetSetIndex { get; }
        public int GetViewSheetSetIndex(int index) => Column_ViewSheetSetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ViewSheetSet GetViewSheetSet(int index) => _GetReferencedViewSheetSet(GetViewSheetSetIndex(index));
        private Vim.Format.ObjectModel.ViewSheetSet _GetReferencedViewSheetSet(int referencedIndex) => ParentTableSet.GetViewSheetSet(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ViewSheetInViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ViewSheetInViewSheetSet();
            r.Index = index;
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewSheetSetIndex(index), _GetReferencedViewSheetSet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ViewSheetInViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetInViewSheetSetTable 
    
    public partial class ViewInViewSheetSetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ViewInViewSheetSet>
    {
        
        public const string TableName = VimEntityTableNames.ViewInViewSheetSet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewInViewSheetSetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ViewSheetSetIndex = GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ViewSheetSetIndex { get; }
        public int GetViewSheetSetIndex(int index) => Column_ViewSheetSetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ViewSheetSet GetViewSheetSet(int index) => _GetReferencedViewSheetSet(GetViewSheetSetIndex(index));
        private Vim.Format.ObjectModel.ViewSheetSet _GetReferencedViewSheetSet(int referencedIndex) => ParentTableSet.GetViewSheetSet(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ViewInViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ViewInViewSheetSet();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewSheetSetIndex(index), _GetReferencedViewSheetSet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ViewInViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewInViewSheetSetTable 
    
    public partial class ViewInViewSheetTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.ViewInViewSheet>
    {
        
        public const string TableName = VimEntityTableNames.ViewInViewSheet;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public ViewInViewSheetTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private Vim.Format.ObjectModel.View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private Vim.Format.ObjectModel.ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.ViewInViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.ViewInViewSheet();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.ViewInViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewInViewSheetTable 
    
    public partial class SiteTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Site>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Site;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public SiteTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Latitude = GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>();
            Column_Longitude = GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Latitude { get; }
        public Double GetLatitude(int index, Double @default = default) => Column_Latitude.ElementAtOrDefault(index, @default);
        public Double[] Column_Longitude { get; }
        public Double GetLongitude(int index, Double @default = default) => Column_Longitude.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Site Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Site();
            r.Index = index;
            r.Latitude = GetLatitude(index);
            r.Longitude = GetLongitude(index);
            r.Address = GetAddress(index);
            r.Elevation = GetElevation(index);
            r.Number = GetNumber(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Site> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class SiteTable 
    
    public partial class BuildingTable : VimEntityTable, IEnumerable<Vim.Format.ObjectModel.Building>, IElementKindTable
    {
        
        public const string TableName = VimEntityTableNames.Building;
        
        public VimEntityTableSet ParentTableSet { get; } // can be null
        
        public BuildingTable(VimEntityTableData tableData, string[] stringTable, VimEntityTableSet parentTableSet = null) : base(tableData, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_TerrainElevation = GetDataColumnValues<Double>("double:TerrainElevation") ?? Array.Empty<Double>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_SiteIndex = GetIndexColumnValues("index:Vim.Site:Site") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public Double[] Column_TerrainElevation { get; }
        public Double GetTerrainElevation(int index, Double @default = default) => Column_TerrainElevation.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public int[] Column_SiteIndex { get; }
        public int GetSiteIndex(int index) => Column_SiteIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Site GetSite(int index) => _GetReferencedSite(GetSiteIndex(index));
        private Vim.Format.ObjectModel.Site _GetReferencedSite(int referencedIndex) => ParentTableSet.GetSite(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Vim.Format.ObjectModel.Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Vim.Format.ObjectModel.Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Vim.Format.ObjectModel.Building Get(int index)
        {
            if (index < 0) return null;
            var r = new Vim.Format.ObjectModel.Building();
            r.Index = index;
            r.Elevation = GetElevation(index);
            r.TerrainElevation = GetTerrainElevation(index);
            r.Address = GetAddress(index);
            r._Site = new Relation<Vim.Format.ObjectModel.Site>(GetSiteIndex(index), _GetReferencedSite);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vim.Format.ObjectModel.Building> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BuildingTable 
    
    public static class VimEntitySetBuilderExtensions
    {
        public static VimEntityTableBuilder ToAssetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Asset> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Asset);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BufferName; }
                tb.AddStringColumn("string:BufferName", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToDisplayUnitTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnit> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.DisplayUnit);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Spec; }
                tb.AddStringColumn("string:Spec", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Type; }
                tb.AddStringColumn("string:Type", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Label; }
                tb.AddStringColumn("string:Label", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToParameterDescriptorTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ParameterDescriptor> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ParameterDescriptor);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Group; }
                tb.AddStringColumn("string:Group", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ParameterType; }
                tb.AddStringColumn("string:ParameterType", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsInstance; }
                tb.AddDataColumn("byte:IsInstance", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsShared; }
                tb.AddDataColumn("byte:IsShared", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsReadOnly; }
                tb.AddDataColumn("byte:IsReadOnly", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Flags; }
                tb.AddDataColumn("int:Flags", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StorageType; }
                tb.AddDataColumn("int:StorageType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DisplayUnit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToParameterTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Parameter> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Parameter);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddStringColumn("string:Value", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ParameterDescriptor?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ParameterDescriptor:ParameterDescriptor", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToElementTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Element> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Element);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("long:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Type; }
                tb.AddStringColumn("string:Type", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UniqueId; }
                tb.AddStringColumn("string:UniqueId", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_X; }
                tb.AddDataColumn("float:Location.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_Y; }
                tb.AddDataColumn("float:Location.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_Z; }
                tb.AddDataColumn("float:Location.Z", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FamilyName; }
                tb.AddStringColumn("string:FamilyName", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPinned; }
                tb.AddDataColumn("byte:IsPinned", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Level?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:Level", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._PhaseCreated?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:PhaseCreated", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._PhaseDemolished?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:PhaseDemolished", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Category?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:Category", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Workset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Workset:Workset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DesignOption?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DesignOption:DesignOption", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._OwnerView?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:OwnerView", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Group?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Group:Group", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._AssemblyInstance?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.AssemblyInstance:AssemblyInstance", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Room?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:Room", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToWorksetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Workset> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Workset);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("int:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Kind; }
                tb.AddStringColumn("string:Kind", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsOpen; }
                tb.AddDataColumn("byte:IsOpen", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsEditable; }
                tb.AddDataColumn("byte:IsEditable", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Owner; }
                tb.AddStringColumn("string:Owner", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UniqueId; }
                tb.AddStringColumn("string:UniqueId", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToAssemblyInstanceTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.AssemblyInstance> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.AssemblyInstance);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].AssemblyTypeName; }
                tb.AddStringColumn("string:AssemblyTypeName", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("float:Position.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("float:Position.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("float:Position.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToGroupTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Group> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Group);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].GroupType; }
                tb.AddStringColumn("string:GroupType", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("float:Position.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("float:Position.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("float:Position.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToDesignOptionTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.DesignOption> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.DesignOption);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPrimary; }
                tb.AddDataColumn("byte:IsPrimary", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToLevelTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Level> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Level);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ProjectElevation; }
                tb.AddDataColumn("double:ProjectElevation", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Building?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Building:Building", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToPhaseTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Phase> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Phase);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToRoomTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Room> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Room);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BaseOffset; }
                tb.AddDataColumn("double:BaseOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LimitOffset; }
                tb.AddDataColumn("double:LimitOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UnboundedHeight; }
                tb.AddDataColumn("double:UnboundedHeight", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Volume; }
                tb.AddDataColumn("double:Volume", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Perimeter; }
                tb.AddDataColumn("double:Perimeter", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Area; }
                tb.AddDataColumn("double:Area", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._UpperLimit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:UpperLimit", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToBimDocumentTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.BimDocument> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.BimDocument);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Title; }
                tb.AddStringColumn("string:Title", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsMetric; }
                tb.AddDataColumn("byte:IsMetric", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NumSaves; }
                tb.AddDataColumn("int:NumSaves", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsLinked; }
                tb.AddDataColumn("byte:IsLinked", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsDetached; }
                tb.AddDataColumn("byte:IsDetached", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsWorkshared; }
                tb.AddDataColumn("byte:IsWorkshared", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].PathName; }
                tb.AddStringColumn("string:PathName", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Latitude; }
                tb.AddDataColumn("double:Latitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Longitude; }
                tb.AddDataColumn("double:Longitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TimeZone; }
                tb.AddDataColumn("double:TimeZone", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].PlaceName; }
                tb.AddStringColumn("string:PlaceName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].WeatherStationName; }
                tb.AddStringColumn("string:WeatherStationName", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ProjectLocation; }
                tb.AddStringColumn("string:ProjectLocation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IssueDate; }
                tb.AddStringColumn("string:IssueDate", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Status; }
                tb.AddStringColumn("string:Status", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ClientName; }
                tb.AddStringColumn("string:ClientName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Author; }
                tb.AddStringColumn("string:Author", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BuildingName; }
                tb.AddStringColumn("string:BuildingName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrganizationName; }
                tb.AddStringColumn("string:OrganizationName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrganizationDescription; }
                tb.AddStringColumn("string:OrganizationDescription", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Product; }
                tb.AddStringColumn("string:Product", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Version; }
                tb.AddStringColumn("string:Version", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].User; }
                tb.AddStringColumn("string:User", columnData);
            }
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FileLength; }
                tb.AddDataColumn("long:FileLength", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ActiveView?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:ActiveView", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._OwnerFamily?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Family:OwnerFamily", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Parent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:Parent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToDisplayUnitInBimDocumentTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnitInBimDocument> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.DisplayUnitInBimDocument);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DisplayUnit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToPhaseOrderInBimDocumentTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseOrderInBimDocument> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.PhaseOrderInBimDocument);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrderIndex; }
                tb.AddDataColumn("int:OrderIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Phase?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:Phase", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToCategoryTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Category> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Category);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("long:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].CategoryType; }
                tb.AddStringColumn("string:CategoryType", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_X; }
                tb.AddDataColumn("double:LineColor.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_Y; }
                tb.AddDataColumn("double:LineColor.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_Z; }
                tb.AddDataColumn("double:LineColor.Z", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BuiltInCategory; }
                tb.AddStringColumn("string:BuiltInCategory", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Parent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:Parent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToFamilyTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Family> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Family);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StructuralMaterialType; }
                tb.AddStringColumn("string:StructuralMaterialType", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StructuralSectionShape; }
                tb.AddStringColumn("string:StructuralSectionShape", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSystemFamily; }
                tb.AddDataColumn("byte:IsSystemFamily", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsInPlace; }
                tb.AddDataColumn("byte:IsInPlace", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyCategory?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:FamilyCategory", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToFamilyTypeTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyType> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.FamilyType);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSystemFamilyType; }
                tb.AddDataColumn("byte:IsSystemFamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Family?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Family:Family", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._CompoundStructure?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToFamilyInstanceTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyInstance> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.FamilyInstance);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingFlipped; }
                tb.AddDataColumn("byte:FacingFlipped", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_X; }
                tb.AddDataColumn("float:FacingOrientation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_Y; }
                tb.AddDataColumn("float:FacingOrientation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_Z; }
                tb.AddDataColumn("float:FacingOrientation.Z", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandFlipped; }
                tb.AddDataColumn("byte:HandFlipped", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Mirrored; }
                tb.AddDataColumn("byte:Mirrored", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HasModifiedGeometry; }
                tb.AddDataColumn("byte:HasModifiedGeometry", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Scale; }
                tb.AddDataColumn("float:Scale", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_X; }
                tb.AddDataColumn("float:BasisX.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_Y; }
                tb.AddDataColumn("float:BasisX.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_Z; }
                tb.AddDataColumn("float:BasisX.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_X; }
                tb.AddDataColumn("float:BasisY.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_Y; }
                tb.AddDataColumn("float:BasisY.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_Z; }
                tb.AddDataColumn("float:BasisY.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_X; }
                tb.AddDataColumn("float:BasisZ.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_Y; }
                tb.AddDataColumn("float:BasisZ.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_Z; }
                tb.AddDataColumn("float:BasisZ.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_X; }
                tb.AddDataColumn("float:Translation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_Y; }
                tb.AddDataColumn("float:Translation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_Z; }
                tb.AddDataColumn("float:Translation.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_X; }
                tb.AddDataColumn("float:HandOrientation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_Y; }
                tb.AddDataColumn("float:HandOrientation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_Z; }
                tb.AddDataColumn("float:HandOrientation.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Host?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Host", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FromRoom?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:FromRoom", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ToRoom?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:ToRoom", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._SuperComponent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:SuperComponent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.View> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.View);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Title; }
                tb.AddStringColumn("string:Title", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewType; }
                tb.AddStringColumn("string:ViewType", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_X; }
                tb.AddDataColumn("double:Up.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_Y; }
                tb.AddDataColumn("double:Up.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_Z; }
                tb.AddDataColumn("double:Up.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_X; }
                tb.AddDataColumn("double:Right.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_Y; }
                tb.AddDataColumn("double:Right.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_Z; }
                tb.AddDataColumn("double:Right.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_X; }
                tb.AddDataColumn("double:Origin.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_Y; }
                tb.AddDataColumn("double:Origin.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_Z; }
                tb.AddDataColumn("double:Origin.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_X; }
                tb.AddDataColumn("double:ViewDirection.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_Y; }
                tb.AddDataColumn("double:ViewDirection.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_Z; }
                tb.AddDataColumn("double:ViewDirection.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_X; }
                tb.AddDataColumn("double:ViewPosition.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_Y; }
                tb.AddDataColumn("double:ViewPosition.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_Z; }
                tb.AddDataColumn("double:ViewPosition.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Scale; }
                tb.AddDataColumn("double:Scale", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Min_X; }
                tb.AddDataColumn("double:Outline.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Min_Y; }
                tb.AddDataColumn("double:Outline.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Max_X; }
                tb.AddDataColumn("double:Outline.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Max_Y; }
                tb.AddDataColumn("double:Outline.Max.Y", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].DetailLevel; }
                tb.AddDataColumn("int:DetailLevel", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Camera?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Camera:Camera", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToElementInViewTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInView> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ElementInView);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToShapeInViewTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInView> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ShapeInView);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Shape?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Shape:Shape", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToAssetInViewTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInView> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.AssetInView);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Asset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:Asset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToAssetInViewSheetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInViewSheet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.AssetInViewSheet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Asset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:Asset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToLevelInViewTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.LevelInView> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.LevelInView);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_X; }
                tb.AddDataColumn("double:Extents.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Y; }
                tb.AddDataColumn("double:Extents.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Z; }
                tb.AddDataColumn("double:Extents.Min.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_X; }
                tb.AddDataColumn("double:Extents.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Y; }
                tb.AddDataColumn("double:Extents.Max.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Z; }
                tb.AddDataColumn("double:Extents.Max.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Level?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:Level", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToCameraTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Camera> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Camera);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("int:Id", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPerspective; }
                tb.AddDataColumn("int:IsPerspective", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].VerticalExtent; }
                tb.AddDataColumn("double:VerticalExtent", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HorizontalExtent; }
                tb.AddDataColumn("double:HorizontalExtent", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FarDistance; }
                tb.AddDataColumn("double:FarDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NearDistance; }
                tb.AddDataColumn("double:NearDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TargetDistance; }
                tb.AddDataColumn("double:TargetDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].RightOffset; }
                tb.AddDataColumn("double:RightOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UpOffset; }
                tb.AddDataColumn("double:UpOffset", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToMaterialTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Material> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Material);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].MaterialCategory; }
                tb.AddStringColumn("string:MaterialCategory", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_X; }
                tb.AddDataColumn("double:Color.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_Y; }
                tb.AddDataColumn("double:Color.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_Z; }
                tb.AddDataColumn("double:Color.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvScaling_X; }
                tb.AddDataColumn("double:ColorUvScaling.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvScaling_Y; }
                tb.AddDataColumn("double:ColorUvScaling.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvOffset_X; }
                tb.AddDataColumn("double:ColorUvOffset.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvOffset_Y; }
                tb.AddDataColumn("double:ColorUvOffset.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvScaling_X; }
                tb.AddDataColumn("double:NormalUvScaling.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvScaling_Y; }
                tb.AddDataColumn("double:NormalUvScaling.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvOffset_X; }
                tb.AddDataColumn("double:NormalUvOffset.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvOffset_Y; }
                tb.AddDataColumn("double:NormalUvOffset.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalAmount; }
                tb.AddDataColumn("double:NormalAmount", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Glossiness; }
                tb.AddDataColumn("double:Glossiness", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Smoothness; }
                tb.AddDataColumn("double:Smoothness", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Transparency; }
                tb.AddDataColumn("double:Transparency", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ColorTextureFile?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:ColorTextureFile", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._NormalTextureFile?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:NormalTextureFile", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToMaterialInElementTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.MaterialInElement> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.MaterialInElement);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Area; }
                tb.AddDataColumn("double:Area", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Volume; }
                tb.AddDataColumn("double:Volume", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPaint; }
                tb.AddDataColumn("byte:IsPaint", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToCompoundStructureLayerTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructureLayer> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.CompoundStructureLayer);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrderIndex; }
                tb.AddDataColumn("int:OrderIndex", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Width; }
                tb.AddDataColumn("double:Width", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].MaterialFunctionAssignment; }
                tb.AddStringColumn("string:MaterialFunctionAssignment", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._CompoundStructure?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToCompoundStructureTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructure> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.CompoundStructure);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Width; }
                tb.AddDataColumn("double:Width", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._StructuralLayer?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructureLayer:StructuralLayer", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToNodeTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Node> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Node);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToGeometryTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Geometry> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Geometry);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_X; }
                tb.AddDataColumn("float:Box.Min.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_Y; }
                tb.AddDataColumn("float:Box.Min.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_Z; }
                tb.AddDataColumn("float:Box.Min.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_X; }
                tb.AddDataColumn("float:Box.Max.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_Y; }
                tb.AddDataColumn("float:Box.Max.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_Z; }
                tb.AddDataColumn("float:Box.Max.Z", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].VertexCount; }
                tb.AddDataColumn("int:VertexCount", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FaceCount; }
                tb.AddDataColumn("int:FaceCount", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToShapeTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Shape> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Shape);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToShapeCollectionTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeCollection> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ShapeCollection);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToShapeInShapeCollectionTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInShapeCollection> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ShapeInShapeCollection);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Shape?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Shape:Shape", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ShapeCollection?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ShapeCollection:ShapeCollection", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToSystemTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.System> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.System);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SystemType; }
                tb.AddDataColumn("int:SystemType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToElementInSystemTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInSystem> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ElementInSystem);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Roles; }
                tb.AddDataColumn("int:Roles", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._System?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.System:System", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToWarningTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Warning> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Warning);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Severity; }
                tb.AddStringColumn("string:Severity", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Description; }
                tb.AddStringColumn("string:Description", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToElementInWarningTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInWarning> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ElementInWarning);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Warning?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Warning:Warning", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToBasePointTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.BasePoint> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.BasePoint);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSurveyPoint; }
                tb.AddDataColumn("byte:IsSurveyPoint", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("double:Position.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("double:Position.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("double:Position.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_X; }
                tb.AddDataColumn("double:SharedPosition.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_Y; }
                tb.AddDataColumn("double:SharedPosition.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_Z; }
                tb.AddDataColumn("double:SharedPosition.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToPhaseFilterTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseFilter> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.PhaseFilter);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].New; }
                tb.AddDataColumn("int:New", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Existing; }
                tb.AddDataColumn("int:Existing", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Demolished; }
                tb.AddDataColumn("int:Demolished", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Temporary; }
                tb.AddDataColumn("int:Temporary", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToGridTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Grid> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Grid);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_X; }
                tb.AddDataColumn("double:StartPoint.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_Y; }
                tb.AddDataColumn("double:StartPoint.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_Z; }
                tb.AddDataColumn("double:StartPoint.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_X; }
                tb.AddDataColumn("double:EndPoint.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_Y; }
                tb.AddDataColumn("double:EndPoint.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_Z; }
                tb.AddDataColumn("double:EndPoint.Z", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsCurved; }
                tb.AddDataColumn("byte:IsCurved", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_X; }
                tb.AddDataColumn("double:Extents.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Y; }
                tb.AddDataColumn("double:Extents.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Z; }
                tb.AddDataColumn("double:Extents.Min.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_X; }
                tb.AddDataColumn("double:Extents.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Y; }
                tb.AddDataColumn("double:Extents.Max.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Z; }
                tb.AddDataColumn("double:Extents.Max.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToAreaTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Area> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Area);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddDataColumn("double:Value", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Perimeter; }
                tb.AddDataColumn("double:Perimeter", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsGrossInterior; }
                tb.AddDataColumn("byte:IsGrossInterior", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._AreaScheme?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.AreaScheme:AreaScheme", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToAreaSchemeTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.AreaScheme> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.AreaScheme);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsGrossBuildingArea; }
                tb.AddDataColumn("byte:IsGrossBuildingArea", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToScheduleTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Schedule> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Schedule);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToScheduleColumnTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleColumn> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ScheduleColumn);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColumnIndex; }
                tb.AddDataColumn("int:ColumnIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Schedule?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Schedule:Schedule", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToScheduleCellTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleCell> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ScheduleCell);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddStringColumn("string:Value", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].RowIndex; }
                tb.AddDataColumn("int:RowIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ScheduleColumn?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ScheduleColumn:ScheduleColumn", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewSheetSetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetSet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ViewSheetSet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewSheetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ViewSheet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewSheetInViewSheetSetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetInViewSheetSet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ViewSheetInViewSheetSet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheetSet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewInViewSheetSetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheetSet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ViewInViewSheetSet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheetSet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToViewInViewSheetTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheet> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.ViewInViewSheet);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToSiteTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Site> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Site);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Latitude; }
                tb.AddDataColumn("double:Latitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Longitude; }
                tb.AddDataColumn("double:Longitude", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static VimEntityTableBuilder ToBuildingTableBuilder(this VimEntitySetBuilder<Vim.Format.ObjectModel.Building> entitySetBuilder)
        {
            var tb = new VimEntityTableBuilder(VimEntityTableNames.Building);
            var entities = entitySetBuilder.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TerrainElevation; }
                tb.AddDataColumn("double:TerrainElevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Site?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Site:Site", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
    } // VimEntitySetBuilderExtensions
    
    public partial class VimBuilder
    {
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Asset> AssetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Asset>(VimEntityTableNames.Asset);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnit> DisplayUnitBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnit>(VimEntityTableNames.DisplayUnit);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ParameterDescriptor> ParameterDescriptorBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ParameterDescriptor>(VimEntityTableNames.ParameterDescriptor);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Parameter> ParameterBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Parameter>(VimEntityTableNames.Parameter);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Element> ElementBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Element>(VimEntityTableNames.Element);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Workset> WorksetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Workset>(VimEntityTableNames.Workset);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.AssemblyInstance> AssemblyInstanceBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.AssemblyInstance>(VimEntityTableNames.AssemblyInstance);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Group> GroupBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Group>(VimEntityTableNames.Group);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.DesignOption> DesignOptionBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.DesignOption>(VimEntityTableNames.DesignOption);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Level> LevelBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Level>(VimEntityTableNames.Level);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Phase> PhaseBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Phase>(VimEntityTableNames.Phase);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Room> RoomBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Room>(VimEntityTableNames.Room);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.BimDocument> BimDocumentBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.BimDocument>(VimEntityTableNames.BimDocument);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnitInBimDocument> DisplayUnitInBimDocumentBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.DisplayUnitInBimDocument>(VimEntityTableNames.DisplayUnitInBimDocument);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseOrderInBimDocument> PhaseOrderInBimDocumentBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseOrderInBimDocument>(VimEntityTableNames.PhaseOrderInBimDocument);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Category> CategoryBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Category>(VimEntityTableNames.Category);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Family> FamilyBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Family>(VimEntityTableNames.Family);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyType> FamilyTypeBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyType>(VimEntityTableNames.FamilyType);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyInstance> FamilyInstanceBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.FamilyInstance>(VimEntityTableNames.FamilyInstance);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.View> ViewBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.View>(VimEntityTableNames.View);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInView> ElementInViewBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInView>(VimEntityTableNames.ElementInView);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInView> ShapeInViewBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInView>(VimEntityTableNames.ShapeInView);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInView> AssetInViewBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInView>(VimEntityTableNames.AssetInView);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInViewSheet> AssetInViewSheetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.AssetInViewSheet>(VimEntityTableNames.AssetInViewSheet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.LevelInView> LevelInViewBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.LevelInView>(VimEntityTableNames.LevelInView);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Camera> CameraBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Camera>(VimEntityTableNames.Camera);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Material> MaterialBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Material>(VimEntityTableNames.Material);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.MaterialInElement> MaterialInElementBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.MaterialInElement>(VimEntityTableNames.MaterialInElement);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructureLayer> CompoundStructureLayerBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructureLayer>(VimEntityTableNames.CompoundStructureLayer);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructure> CompoundStructureBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.CompoundStructure>(VimEntityTableNames.CompoundStructure);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Node> NodeBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Node>(VimEntityTableNames.Node);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Geometry> GeometryBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Geometry>(VimEntityTableNames.Geometry);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Shape> ShapeBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Shape>(VimEntityTableNames.Shape);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeCollection> ShapeCollectionBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeCollection>(VimEntityTableNames.ShapeCollection);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInShapeCollection> ShapeInShapeCollectionBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ShapeInShapeCollection>(VimEntityTableNames.ShapeInShapeCollection);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.System> SystemBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.System>(VimEntityTableNames.System);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInSystem> ElementInSystemBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInSystem>(VimEntityTableNames.ElementInSystem);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Warning> WarningBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Warning>(VimEntityTableNames.Warning);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInWarning> ElementInWarningBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ElementInWarning>(VimEntityTableNames.ElementInWarning);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.BasePoint> BasePointBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.BasePoint>(VimEntityTableNames.BasePoint);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseFilter> PhaseFilterBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.PhaseFilter>(VimEntityTableNames.PhaseFilter);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Grid> GridBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Grid>(VimEntityTableNames.Grid);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Area> AreaBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Area>(VimEntityTableNames.Area);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.AreaScheme> AreaSchemeBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.AreaScheme>(VimEntityTableNames.AreaScheme);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Schedule> ScheduleBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Schedule>(VimEntityTableNames.Schedule);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleColumn> ScheduleColumnBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleColumn>(VimEntityTableNames.ScheduleColumn);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleCell> ScheduleCellBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ScheduleCell>(VimEntityTableNames.ScheduleCell);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetSet> ViewSheetSetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetSet>(VimEntityTableNames.ViewSheetSet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheet> ViewSheetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheet>(VimEntityTableNames.ViewSheet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetInViewSheetSet> ViewSheetInViewSheetSetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ViewSheetInViewSheetSet>(VimEntityTableNames.ViewSheetInViewSheetSet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheetSet> ViewInViewSheetSetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheetSet>(VimEntityTableNames.ViewInViewSheetSet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheet> ViewInViewSheetBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.ViewInViewSheet>(VimEntityTableNames.ViewInViewSheet);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Site> SiteBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Site>(VimEntityTableNames.Site);
        public readonly VimEntitySetBuilder<Vim.Format.ObjectModel.Building> BuildingBuilder = new VimEntitySetBuilder<Vim.Format.ObjectModel.Building>(VimEntityTableNames.Building);
        
        public List<VimEntityTableBuilder> GetVimEntityTableBuilders()
        {
            var tableBuilders = new List<VimEntityTableBuilder>();
            tableBuilders.Add(AssetBuilder.ToAssetTableBuilder());
            tableBuilders.Add(DisplayUnitBuilder.ToDisplayUnitTableBuilder());
            tableBuilders.Add(ParameterDescriptorBuilder.ToParameterDescriptorTableBuilder());
            tableBuilders.Add(ParameterBuilder.ToParameterTableBuilder());
            tableBuilders.Add(ElementBuilder.ToElementTableBuilder());
            tableBuilders.Add(WorksetBuilder.ToWorksetTableBuilder());
            tableBuilders.Add(AssemblyInstanceBuilder.ToAssemblyInstanceTableBuilder());
            tableBuilders.Add(GroupBuilder.ToGroupTableBuilder());
            tableBuilders.Add(DesignOptionBuilder.ToDesignOptionTableBuilder());
            tableBuilders.Add(LevelBuilder.ToLevelTableBuilder());
            tableBuilders.Add(PhaseBuilder.ToPhaseTableBuilder());
            tableBuilders.Add(RoomBuilder.ToRoomTableBuilder());
            tableBuilders.Add(BimDocumentBuilder.ToBimDocumentTableBuilder());
            tableBuilders.Add(DisplayUnitInBimDocumentBuilder.ToDisplayUnitInBimDocumentTableBuilder());
            tableBuilders.Add(PhaseOrderInBimDocumentBuilder.ToPhaseOrderInBimDocumentTableBuilder());
            tableBuilders.Add(CategoryBuilder.ToCategoryTableBuilder());
            tableBuilders.Add(FamilyBuilder.ToFamilyTableBuilder());
            tableBuilders.Add(FamilyTypeBuilder.ToFamilyTypeTableBuilder());
            tableBuilders.Add(FamilyInstanceBuilder.ToFamilyInstanceTableBuilder());
            tableBuilders.Add(ViewBuilder.ToViewTableBuilder());
            tableBuilders.Add(ElementInViewBuilder.ToElementInViewTableBuilder());
            tableBuilders.Add(ShapeInViewBuilder.ToShapeInViewTableBuilder());
            tableBuilders.Add(AssetInViewBuilder.ToAssetInViewTableBuilder());
            tableBuilders.Add(AssetInViewSheetBuilder.ToAssetInViewSheetTableBuilder());
            tableBuilders.Add(LevelInViewBuilder.ToLevelInViewTableBuilder());
            tableBuilders.Add(CameraBuilder.ToCameraTableBuilder());
            tableBuilders.Add(MaterialBuilder.ToMaterialTableBuilder());
            tableBuilders.Add(MaterialInElementBuilder.ToMaterialInElementTableBuilder());
            tableBuilders.Add(CompoundStructureLayerBuilder.ToCompoundStructureLayerTableBuilder());
            tableBuilders.Add(CompoundStructureBuilder.ToCompoundStructureTableBuilder());
            tableBuilders.Add(NodeBuilder.ToNodeTableBuilder());
            tableBuilders.Add(GeometryBuilder.ToGeometryTableBuilder());
            tableBuilders.Add(ShapeBuilder.ToShapeTableBuilder());
            tableBuilders.Add(ShapeCollectionBuilder.ToShapeCollectionTableBuilder());
            tableBuilders.Add(ShapeInShapeCollectionBuilder.ToShapeInShapeCollectionTableBuilder());
            tableBuilders.Add(SystemBuilder.ToSystemTableBuilder());
            tableBuilders.Add(ElementInSystemBuilder.ToElementInSystemTableBuilder());
            tableBuilders.Add(WarningBuilder.ToWarningTableBuilder());
            tableBuilders.Add(ElementInWarningBuilder.ToElementInWarningTableBuilder());
            tableBuilders.Add(BasePointBuilder.ToBasePointTableBuilder());
            tableBuilders.Add(PhaseFilterBuilder.ToPhaseFilterTableBuilder());
            tableBuilders.Add(GridBuilder.ToGridTableBuilder());
            tableBuilders.Add(AreaBuilder.ToAreaTableBuilder());
            tableBuilders.Add(AreaSchemeBuilder.ToAreaSchemeTableBuilder());
            tableBuilders.Add(ScheduleBuilder.ToScheduleTableBuilder());
            tableBuilders.Add(ScheduleColumnBuilder.ToScheduleColumnTableBuilder());
            tableBuilders.Add(ScheduleCellBuilder.ToScheduleCellTableBuilder());
            tableBuilders.Add(ViewSheetSetBuilder.ToViewSheetSetTableBuilder());
            tableBuilders.Add(ViewSheetBuilder.ToViewSheetTableBuilder());
            tableBuilders.Add(ViewSheetInViewSheetSetBuilder.ToViewSheetInViewSheetSetTableBuilder());
            tableBuilders.Add(ViewInViewSheetSetBuilder.ToViewInViewSheetSetTableBuilder());
            tableBuilders.Add(ViewInViewSheetBuilder.ToViewInViewSheetTableBuilder());
            tableBuilders.Add(SiteBuilder.ToSiteTableBuilder());
            tableBuilders.Add(BuildingBuilder.ToBuildingTableBuilder());
            
            return tableBuilders;
        } // GetVimEntityTableBuilders
        
        public void Clear()
        {
            AssetBuilder.Clear();
            DisplayUnitBuilder.Clear();
            ParameterDescriptorBuilder.Clear();
            ParameterBuilder.Clear();
            ElementBuilder.Clear();
            WorksetBuilder.Clear();
            AssemblyInstanceBuilder.Clear();
            GroupBuilder.Clear();
            DesignOptionBuilder.Clear();
            LevelBuilder.Clear();
            PhaseBuilder.Clear();
            RoomBuilder.Clear();
            BimDocumentBuilder.Clear();
            DisplayUnitInBimDocumentBuilder.Clear();
            PhaseOrderInBimDocumentBuilder.Clear();
            CategoryBuilder.Clear();
            FamilyBuilder.Clear();
            FamilyTypeBuilder.Clear();
            FamilyInstanceBuilder.Clear();
            ViewBuilder.Clear();
            ElementInViewBuilder.Clear();
            ShapeInViewBuilder.Clear();
            AssetInViewBuilder.Clear();
            AssetInViewSheetBuilder.Clear();
            LevelInViewBuilder.Clear();
            CameraBuilder.Clear();
            MaterialBuilder.Clear();
            MaterialInElementBuilder.Clear();
            CompoundStructureLayerBuilder.Clear();
            CompoundStructureBuilder.Clear();
            NodeBuilder.Clear();
            GeometryBuilder.Clear();
            ShapeBuilder.Clear();
            ShapeCollectionBuilder.Clear();
            ShapeInShapeCollectionBuilder.Clear();
            SystemBuilder.Clear();
            ElementInSystemBuilder.Clear();
            WarningBuilder.Clear();
            ElementInWarningBuilder.Clear();
            BasePointBuilder.Clear();
            PhaseFilterBuilder.Clear();
            GridBuilder.Clear();
            AreaBuilder.Clear();
            AreaSchemeBuilder.Clear();
            ScheduleBuilder.Clear();
            ScheduleColumnBuilder.Clear();
            ScheduleCellBuilder.Clear();
            ViewSheetSetBuilder.Clear();
            ViewSheetBuilder.Clear();
            ViewSheetInViewSheetSetBuilder.Clear();
            ViewInViewSheetSetBuilder.Clear();
            ViewInViewSheetBuilder.Clear();
            SiteBuilder.Clear();
            BuildingBuilder.Clear();
        } // Clear
    } // VimBuilder
} // namespace
