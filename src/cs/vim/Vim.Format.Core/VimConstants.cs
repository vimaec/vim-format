using System.Collections.Generic;

namespace Vim.Format
{
    public static class BufferNames
    {
        public const string Header = "header";
        public const string Assets = "assets";
        public const string Entities = "entities";
        public const string Strings = "strings";
        public const string Geometry = "geometry";
    }

    public static class TableNames
    {
        public const string Geometry = "Vim.Geometry";
        public const string Node = "Vim.Node"; 
        public const string Asset = "Vim.Asset";
        public const string Shape = "Vim.Shape";
        public const string ShapeCollection = "Vim.ShapeCollection";
        public const string ShapeInShapeCollection = "Vim.ShapeInShapeCollection";
        public const string Material = "Vim.Material";
        public const string Family = "Vim.Family";
        public const string FamilyInstance = "Vim.FamilyInstance";
        public const string FamilyType = "Vim.FamilyType";
        public const string Element = "Vim.Element";
        public const string Face = "Vim.Face";
        public const string Category = "Vim.Category";
        public const string Level = "Vim.Level";
        public const string Phase = "Vim.Phase";
        public const string PhaseOrderInBimDocument = "Vim.PhaseOrderInBimDocument";
        public const string Room = "Vim.Room";
        public const string View = "Vim.View";
        public const string ElementInView = "Vim.ElementInView";
        public const string ShapeInView = "Vim.ShapeInView";
        public const string AssetInView = "Vim.AssetInView";
        public const string AssetInViewSheet = "Vim.AssetInViewSheet";
        public const string LevelInView = "Vim.LevelInView";
        public const string Camera = "Vim.Camera";
        public const string Workset = "Vim.Workset";
        public const string DesignOption = "Vim.DesignOption";
        public const string AssemblyInstance = "Vim.AssemblyInstance";
        public const string Group = "Vim.Group";
        public const string BimDocument = "Vim.BimDocument";
        public const string CompoundStructure = "Vim.CompoundStructure";
        public const string CompoundStructureLayer = "Vim.CompoundStructureLayer";
        public const string DisplayUnit = "Vim.DisplayUnit";
        public const string DisplayUnitInBimDocument = "Vim.DisplayUnitInBimDocument";
        public const string Parameter = "Vim.Parameter";
        public const string ParameterDescriptor = "Vim.ParameterDescriptor";
        public const string System = "Vim.System";
        public const string ElementInSystem = "Vim.ElementInSystem";
        public const string Warning = "Vim.Warning";
        public const string ElementInWarning = "Vim.ElementInWarning";
        public const string MaterialInElement = "Vim.MaterialInElement";
        public const string BasePoint = "Vim.BasePoint";
        public const string PhaseFilter = "Vim.PhaseFilter";
        public const string Grid = "Vim.Grid";
        public const string Area = "Vim.Area";
        public const string AreaScheme = "Vim.AreaScheme";
        public const string Schedule = "Vim.Schedule";
        public const string ScheduleColumn = "Vim.ScheduleColumn";
        public const string ScheduleCell = "Vim.ScheduleCell";
        public const string ViewSheetSet = "Vim.ViewSheetSet";
        public const string ViewSheet = "Vim.ViewSheet";
        public const string ViewSheetInViewSheetSet = "Vim.ViewSheetInViewSheetSet";
        public const string ViewInViewSheetSet = "Vim.ViewInViewSheetSet";
        public const string ViewInViewSheet = "Vim.ViewInViewSheet";
        public const string Site = "Vim.Site";
        public const string Building = "Vim.Building";
    }

    public static class VimConstants
    {
        public const string MainPng = "main.png";

        public const string IndexColumnNameTypePrefix = "index:";
        public const string StringColumnNameTypePrefix = "string:";

        public const string IntColumnNameTypePrefix = "int:";
        public const string UintColumnNameTypePrefix = "uint:"; // unused for now
        public const string LongColumnNameTypePrefix = "long:";
        public const string UlongColumnNameTypePrefix = "ulong:"; // unused for now
        public const string ByteColumnNameTypePrefix = "byte:";
        public const string UbyteColumNameTypePrefix = "ubyte:"; // unused for now
        public const string FloatColumnNameTypePrefix = "float:";
        public const string DoubleColumnNameTypePrefix = "double:";

        public const int NoEntityRelation = -1;
        public const long SyntheticElementId = -1;
        public const string SystemFamilyTypeElementType = "System Family Type";
        public const string SystemFamilyElementType = "System Family";
        public const string UnassignedSystem = "Unassigned System";
        public const string BimDocumentParameterHolderElementType = "BimDocument Parameter Holder";
        public const string BuildingParameterHolderElementType = "Building Parameter Holder";
        public const string SiteParameterHolderElementType = "Site Parameter Holder";

        public static HashSet<string> ComputedTableNames = new HashSet<string>
        {
            TableNames.Geometry
        };

        public static HashSet<string> NonBimNames = new HashSet<string>
        {
            TableNames.Geometry,
            TableNames.Asset,
            TableNames.Material,
        };

        public static class DisciplineNames
        {
            public const string Mechanical = nameof(Mechanical);
            public const string Architecture = nameof(Architecture);
            public const string Generic = nameof(Generic);
            public const string Electrical = nameof(Electrical);
            public const string Plumbing = nameof(Plumbing);
            public const string Structural = nameof(Structural);
        }
    }
}
