using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
internal static class GenerateRevitBuiltInCategories
{
    private record CategoryRecord(string BuiltInName, string Name, long Id, string CategoryType);

    [Test]
    public static void EmitRevitBuiltInCategories()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();
        
        var outFilePath = Path.Combine(dir, "RevitBuiltInCategory.cs");
        
        var categories = new List<CategoryRecord>();
        var lines = BuiltInCategories.ReplaceLineEndings().Split(Environment.NewLine);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var tokens = line.Split(',');
            if (tokens.Length < 4)
            {
                Assert.Fail("Unexpected number of tokens.");
                continue;
            }

            var builtIn = tokens[0];
            if (!builtIn.StartsWith("OST_"))
                continue;

            
            Assert.IsTrue(long.TryParse(tokens[1], out var id));

            var name = tokens[2];
            var type = tokens[3];

            categories.Add(new CategoryRecord(builtIn, name, id, type));
        }
        // Add the INVALID record: INVALID = -1, // 0xFFFFFFFFFFFFFFFF
        categories.Add(new CategoryRecord("INVALID", "Invalid", -1, ""));

        var cb = new CodeBuilder();
        cb.AppendLine($"// FILE ORIGINALLY GENERATED FROM {nameof(GenerateRevitBuiltInCategories)}.cs");
        cb.AppendLine("using System.Collections.Generic;");
        cb.AppendLine();

        cb.AppendLine($"namespace {nameof(Vim)}.{nameof(Format)}.{nameof(ObjectModel)}");
        cb.AppendLine("{");
        {
            // enum RevitBuiltInCategory
            cb.AppendLine("public enum RevitBuiltInCategory : long");
            cb.AppendLine("{");
            {
                foreach (var cat in categories)
                {
                    cb.AppendLine($"{cat.BuiltInName} = {cat.Id},");
                }
            }
            cb.AppendLine("}"); // enum
            cb.AppendLine();

            // static class RevitBuiltInCategoryExtensions
            cb.AppendLine("public static class RevitBuiltInCategoryExtensions");
            cb.AppendLine("{");
            {
                cb.AppendLine("public static bool TryGetEnglishName(this RevitBuiltInCategory cat, out string name)");
                cb.AppendLine("{");
                {
                    cb.AppendLine("name = string.Empty;");
                    cb.AppendLine("return RevitBuiltInCategoryToEnglishNameMap.TryGetValue(cat, out name);");
                }
                cb.AppendLine("}");
                cb.AppendLine();

                cb.AppendLine("public static bool TryGetCategoryFromEnglishName(string name, out RevitBuiltInCategory cat)");
                cb.AppendLine("{");
                {
                    cb.AppendLine("cat = RevitBuiltInCategory.INVALID;");
                    cb.AppendLine("return EnglishNameToRevitBuiltInCategoryMap.TryGetValue(name, out cat);");
                }
                cb.AppendLine("}");
                cb.AppendLine();

                cb.AppendLine("public static IReadOnlyDictionary<RevitBuiltInCategory, string> RevitBuiltInCategoryToEnglishNameMap { get; } = new Dictionary<RevitBuiltInCategory, string>()");
                cb.AppendLine("{");
                {
                    foreach (var cat in categories)
                    {
                        cb.AppendLine($"{{RevitBuiltInCategory.{cat.BuiltInName}, \"{cat.Name}\"}},");
                    }
                }
                cb.AppendLine("};");
                cb.AppendLine();

                cb.AppendLine("public static IReadOnlyDictionary<string, RevitBuiltInCategory> EnglishNameToRevitBuiltInCategoryMap { get; } = new Dictionary<string, RevitBuiltInCategory>()");
                cb.AppendLine("{");
                {
                    foreach (var cat in categories)
                    {
                        cb.AppendLine($"{{\"{cat.Name}\", RevitBuiltInCategory.{cat.BuiltInName}}},");
                    }
                }
                cb.AppendLine("};");
            }
            cb.AppendLine("}");
        }
        cb.AppendLine("}"); // namespace

        var s = cb.ToString();

        IO.CreateFileDirectory(outFilePath);
        File.WriteAllText(outFilePath, s);
    }

    // Exported using Power BI by referencing the raw Category table's BuiltInCategory, Id, Name, and CategoryType columns.
    private const string BuiltInCategories =
@"BuiltInCategory,Id,Name,CategoryType
OST_AbutmentFoundations,-2006202,Abutment Foundations,Model
OST_AbutmentFoundationTags,-2006208,Abutment Foundation Tags,Annotation
OST_AbutmentPiles,-2006203,Abutment Piles,Model
OST_AbutmentPileTags,-2006209,Abutment Pile Tags,Annotation
OST_AbutmentWalls,-2006204,Abutment Walls,Model
OST_AbutmentWallTags,-2006210,Abutment Wall Tags,Annotation
OST_AdaptivePoints,-2000900,Adaptive Points,Annotation
OST_AdaptivePoints_Lines,-2000903,Lines,Annotation
OST_AdaptivePoints_Planes,-2000902,Planes,Annotation
OST_AdaptivePoints_Points,-2000901,Points,Annotation
OST_Alignments,-2001012,Alignments,Annotation
OST_AlignmentsTags,-2001015,Alignment Tags,Annotation
OST_AlignmentStationLabels,-2001017,Alignment Station Labels,Annotation
OST_AlignmentStationLabelSets,-2001016,Alignment Station Label Sets,Annotation
OST_AnalysisDisplayStyle,-2000304,Analysis Display Style,Model
OST_AnalysisResults,-2000303,Analysis Results,Model
OST_AnalyticalMember,-2009662,Analytical Members,AnalyticalModel
OST_AnalyticalMemberCrossSection,-2001075,Cross Section,AnalyticalModel
OST_AnalyticalMemberLocalCoordSys,-2009666,Local Coordinate System,AnalyticalModel
OST_AnalyticalMemberTags,-2009663,Analytical Member Tags,Annotation
OST_AnalyticalNodes,-2009645,Analytical Nodes,AnalyticalModel
OST_AnalyticalOpening,-2009665,Analytical Openings,AnalyticalModel
OST_AnalyticalOpeningTags,-2000958,Analytical Opening Tags,Annotation
OST_AnalyticalPanel,-2009664,Analytical Panels,AnalyticalModel
OST_AnalyticalPanelLocalCoordSys,-2009667,Local Coordinate System,AnalyticalModel
OST_AnalyticalPanelTags,-2000957,Analytical Panel Tags,Annotation
OST_AnalyticalPipeConnectionLineSymbol,-2000984,Connection Line Symbol,AnalyticalModel
OST_AnalyticalPipeConnections,-2000983,Analytical Pipe Connections,AnalyticalModel
OST_AnalyticSpaces,-2008185,Analytical Spaces,AnalyticalModel
OST_AnalyticSurfaces,-2008186,Analytical Surfaces,AnalyticalModel
OST_AnnotationCrop,-2000547,Annotation Crop Boundary,Annotation
OST_AnnotationCropSpecial,-2000549,Annotation Crop Boundary,Annotation
OST_ApproachSlabs,-2006205,Approach Slabs,Model
OST_ApproachSlabTags,-2006211,Approach Slab Tags,Annotation
OST_AreaColorFill,-2000165,Color Fill,Model
OST_AreaInteriorFillVisibility,-2000163,Interior Fill,Model
OST_AreaLoads,-2005203,Area Loads,AnalyticalModel
OST_AreaLoadTags,-2005252,Area Load Tags,Annotation
OST_AreaReferenceVisibility,-2000164,Reference,Model
OST_AreaRein,-2009003,Structural Area Reinforcement,Model
OST_AreaReinBoundary,-2009006,Boundary,Model
OST_AreaReinSpanSymbol,-2009005,Structural Area Reinforcement Symbols,Annotation
OST_AreaReinTags,-2009021,Structural Area Reinforcement Tags,Annotation
OST_Areas,-2003200,Areas,Model
OST_AreaSchemeLines,-2000079,<Area Boundary>,Model
OST_AreaTags,-2005020,Area Tags,Annotation
OST_Assemblies,-2000267,Assemblies,Internal
OST_AssemblyTags,-2000268,Assembly Tags,Annotation
OST_AudioVisualDevices,-2001055,Audio Visual Devices,Model
OST_AudioVisualDevicesHiddenLines,-2001056,<Hidden Lines>,Model
OST_AudioVisualDeviceTags,-2001057,Audio Visual Device Tags,Annotation
OST_AxisOfRotation,-2000065,<Axis of Rotation>,Model
OST_BeamSystemTags,-2005130,Structural Beam System Tags,Annotation
OST_BoundaryConditions,-2005301,Boundary Conditions,AnalyticalModel
OST_BridgeAbutmentHiddenLines,-2006150,<Hidden Lines>,Model
OST_BridgeAbutments,-2006130,Abutments,Model
OST_BridgeAbutmentTags,-2006170,Abutment Tags,Annotation
OST_BridgeArches,-2006134,Arches,Model
OST_BridgeBearingHiddenLines,-2006158,<Hidden Lines>,Model
OST_BridgeBearings,-2006138,Bearings,Model
OST_BridgeBearingTags,-2006178,Bearing Tags,Annotation
OST_BridgeCables,-2006133,Bridge Cables,Model
OST_BridgeCableTags,-2006173,Bridge Cable Tags,Annotation
OST_BridgeDeckHiddenLines,-2006155,<Hidden Lines>,Model
OST_BridgeDecks,-2006135,Bridge Decks,Model
OST_BridgeDeckTags,-2006175,Bridge Deck Tags,Annotation
OST_BridgeFoundations,-2006136,Pier Foundations,Model
OST_BridgeFoundationTags,-2006176,Pier Foundation Tags,Annotation
OST_BridgeFraming,-2006241,Bridge Framing,Model
OST_BridgeFramingCrossBracing,-2006245,Cross Bracing,Model
OST_BridgeFramingCrossBracingTags,-2006278,Bridge Cross Bracing Tags,Annotation
OST_BridgeFramingDiaphragms,-2006246,Diaphragms,Model
OST_BridgeFramingDiaphragmTags,-2006279,Bridge Diaphragm Tags,Annotation
OST_BridgeFramingHiddenLines,-2006242,<Hidden Lines>,Model
OST_BridgeFramingTags,-2006243,Bridge Framing Tags,Annotation
OST_BridgeFramingTrusses,-2006248,Trusses,Model
OST_BridgeFramingTrussTags,-2006281,Bridge Truss Tags,Annotation
OST_BridgeGirders,-2006137,Primarys,Model
OST_BridgePierHiddenLines,-2006151,<Hidden Lines>,Model
OST_BridgePiers,-2006131,Piers,Model
OST_BridgePierTags,-2006171,Pier Tags,Annotation
OST_BridgeTowers,-2006132,Pier Towers,Model
OST_BridgeTowerTags,-2006172,Pier Tower Tags,Annotation
OST_BrokenSectionLine,-2000202,Broken Section Line,Annotation
OST_BuildingPad,-2001263,Pads,Model
OST_CableTray,-2008130,Cable Trays,Model
OST_CableTrayCenterLine,-2008136,Center Line,Model
OST_CableTrayDrop,-2008135,Drop,Model
OST_CableTrayFitting,-2008126,Cable Tray Fittings,Model
OST_CableTrayFittingCenterLine,-2008140,Center Line,Model
OST_CableTrayFittingTags,-2008127,Cable Tray Fitting Tags,Annotation
OST_CableTrayRiseDrop,-2008134,Rise,Model
OST_CableTrayRun,-2008150,Cable Tray Runs,Model
OST_CableTrayTags,-2008131,Cable Tray Tags,Annotation
OST_CalloutBoundary,-2000539,Callout Boundary,Annotation
OST_CalloutHeads,-2000538,Callout Heads,Annotation
OST_CalloutLeaderLine,-2000544,Callout Leader Line,Annotation
OST_Callouts,-2000537,Callouts,Annotation
OST_Camera_Lines,-2000501,Cameras,Annotation
OST_Casework,-2001000,Casework,Model
OST_CaseworkHiddenLines,-2009515,<Hidden Lines>,Model
OST_CaseworkTags,-2005001,Casework Tags,Annotation
OST_Ceilings,-2000038,Ceilings,Model
OST_CeilingsCutPattern,-2000617,Cut Pattern,Model
OST_CeilingsDefault,-2000616,Common Edges,Model
OST_CeilingsFinish1,-2000614,Finish 1 [4],Model
OST_CeilingsFinish2,-2000615,Finish 2 [5],Model
OST_CeilingsHiddenLines,-2009503,<Hidden Lines>,Model
OST_CeilingsInsulation,-2000612,Thermal/Air Layer [3],Model
OST_CeilingsMembrane,-2000610,Membrane Layer,Model
OST_CeilingsStructure,-2000611,Structure [1],Model
OST_CeilingsSubstrate,-2000613,Substrate [2],Model
OST_CeilingsSurfacePattern,-2000543,Surface Pattern,Model
OST_CeilingTags,-2005002,Ceiling Tags,Annotation
OST_CenterLines,-2000288,<Centerline>,Model
OST_CLines,-2000530,Reference Planes,Annotation
OST_ColorFillLegends,-2000550,Color Fill Legends,Annotation
OST_Columns,-2000100,Columns,Model
OST_ColumnsHiddenLines,-2009506,<Hidden Lines>,Model
OST_ColumnTags,-2001063,Column Tags,Annotation
OST_CommunicationDevices,-2008081,Communication Devices,Model
OST_CommunicationDeviceTags,-2008082,Communication Device Tags,Annotation
OST_Conduit,-2008132,Conduits,Model
OST_ConduitCenterLine,-2008139,Center Line,Model
OST_ConduitDrop,-2008138,Drop,Model
OST_ConduitFitting,-2008128,Conduit Fittings,Model
OST_ConduitFittingCenterLine,-2008141,Center Line,Model
OST_ConduitFittingTags,-2008129,Conduit Fitting Tags,Annotation
OST_ConduitRiseDrop,-2008137,Rise,Model
OST_ConduitRun,-2008149,Conduit Runs,Model
OST_ConduitTags,-2008133,Conduit Tags,Annotation
OST_ContourLabels,-2000350,Contour Labels,Annotation
OST_CoordinateSystem,-2000977,Internal Origin,Model
OST_Coordination_Model,-2000982,Coordination Model,Model
OST_Cornices,-2000181,Wall Sweeps,Model
OST_Coupler,-2009060,Structural Rebar Couplers,Model
OST_CouplerHiddenLines,-2009062,<Hidden Lines>,Model
OST_CouplerTags,-2009061,Structural Rebar Coupler Tags,Annotation
OST_CropBoundary,-2000536,Crop Boundaries,Annotation
OST_CropBoundarySpecial,-2000548,Crop Boundaries,Annotation
OST_CurtainGrids,-2000173,Curtain Grids,Model
OST_CurtainGridsCurtaSystem,-2000323,Curtain System Grids,Model
OST_CurtainGridsRoof,-2000320,Curtain Roof Grids,Model
OST_CurtainGridsWall,-2000321,Curtain Wall Grids,Model
OST_CurtainWallMullions,-2000171,Curtain Wall Mullions,Model
OST_CurtainWallMullionsHiddenLines,-2009511,<Hidden Lines>,Model
OST_CurtainWallMullionTags,-2005032,Curtain Wall Mullion Tags,Annotation
OST_CurtainWallPanels,-2000170,Curtain Panels,Model
OST_CurtainWallPanelsHiddenLines,-2009510,<Hidden Lines>,Model
OST_CurtainWallPanelTags,-2005012,Curtain Panel Tags,Annotation
OST_CurtaSystem,-2000340,Curtain Systems,Model
OST_CurtaSystemHiddenLines,-2009531,<Hidden Lines>,Model
OST_CurtaSystemTags,-2005025,Curtain System Tags,Annotation
OST_CurvesMediumLines,-2000043,<Medium Lines>,Model
OST_CurvesThinLines,-2000042,<Thin Lines>,Model
OST_CurvesWideLines,-2000044,<Wide Lines>,Model
OST_DataDevices,-2008083,Data Devices,Model
OST_DataDeviceTags,-2008084,Data Device Tags,Annotation
OST_DataExchanges,-2001107,Data Exchanges,Model
OST_DemolishedLines,-2000285,<Demolished>,Model
OST_DetailComponents,-2002000,Detail Items,Model
OST_DetailComponentsHiddenLines,-2009514,<Hidden Lines>,Model
OST_DetailComponentTags,-2005028,Detail Item Tags,Annotation
OST_Dimensions,-2000260,Dimensions,Annotation
OST_DisplacementPath,-2000223,Displacement Path,Annotation
OST_DividedSurface_Gridlines,-2003325,Gridlines,Model
OST_DividedSurface_Nodes,-2003324,Nodes,Model
OST_DividedSurface_PatternFill,-2003327,Pattern Fill,Model
OST_DividedSurface_PatternLines,-2003326,Pattern Lines,Model
OST_Doors,-2000023,Doors,Model
OST_DoorsFrameMullionProjection,-2000029,Frame/Mullion,Model
OST_DoorsGlassProjection,-2000031,Glass,Model
OST_DoorsHiddenLines,-2009501,<Hidden Lines>,Model
OST_DoorsOpeningProjection,-2000027,Opening,Model
OST_DoorsPanelProjection,-2000025,Panel,Model
OST_DoorTags,-2000460,Door Tags,Annotation
OST_DuctAccessory,-2008016,Duct Accessories,Model
OST_DuctAccessoryTags,-2008017,Duct Accessory Tags,Annotation
OST_DuctAnalyticalSegments,-2001115,Analytical Duct Segments,AnalyticalModel
OST_DuctAnalyticalSegmentTags,-2001116,Analytical Duct Segment Tags,Annotation
OST_DuctColorFillLegends,-2007004,Duct Color Fill Legends,Annotation
OST_DuctColorFills,-2008005,Duct Color Fill,Annotation
OST_DuctCurves,-2008000,Ducts,Model
OST_DuctCurvesCenterLine,-2008001,Center Line,Model
OST_DuctCurvesDrop,-2008062,Drop,Model
OST_DuctCurvesRiseDrop,-2008036,Rise,Model
OST_DuctFitting,-2008010,Duct Fittings,Model
OST_DuctFittingCenterLine,-2008066,Center Line,Model
OST_DuctFittingTags,-2008061,Duct Fitting Tags,Annotation
OST_DuctInsulations,-2008123,Duct Insulations,Model
OST_DuctInsulationsTags,-2008153,Duct Insulation Tags,Annotation
OST_DuctLinings,-2008124,Duct Linings,Model
OST_DuctLiningsTags,-2008154,Duct Lining Tags,Annotation
OST_DuctSystem,-2008015,Duct Systems,Model
OST_DuctSystem_Reference_Visibility,-2008157,Reference Lines,Model
OST_DuctTags,-2008003,Duct Tags,Annotation
OST_DuctTerminal,-2008013,Air Terminals,Model
OST_DuctTerminalTags,-2008014,Air Terminal Tags,Annotation
OST_EdgeSlab,-2001392,Slab Edges,Model
OST_ELECTRICAL_AreaBasedLoads_Boundary,-2008222,Boundary,AnalyticalModel
OST_ELECTRICAL_AreaBasedLoads_InteriorFill_Visibility,-2008226,Interior Fill,AnalyticalModel
OST_ELECTRICAL_AreaBasedLoads_Reference_Visibility,-2008227,Reference Lines,AnalyticalModel
OST_ELECTRICAL_AreaBasedLoads_Tags,-2001078,Area Based Load Tags,Annotation
OST_ElectricalAnalyticalTransformer,-2001077,Electrical Analytical Transformer,Model
OST_ElectricalCircuit,-2008037,Electrical Circuits,Model
OST_ElectricalEquipment,-2001040,Electrical Equipment,Model
OST_ElectricalEquipmentHiddenLines,-2009516,<Hidden Lines>,Model
OST_ElectricalEquipmentTags,-2005003,Electrical Equipment Tags,Annotation
OST_ElectricalFixtures,-2001060,Electrical Fixtures,Model
OST_ElectricalFixturesHiddenLines,-2009517,<Hidden Lines>,Model
OST_ElectricalFixtureTags,-2005004,Electrical Fixture Tags,Annotation
OST_ElectricalInternalCircuits,-2008152,Electrical Spare/Space Circuits,Model
OST_ElectricalLoadSet,-2001098,Electrical Analytical Load Set,Model
OST_ElectricalLoadZoneInstance,-2001020,Electrical Analytical Loads,AnalyticalModel
OST_ElectricalPowerSource,-2001026,Electrical Analytical Power Source,Model
OST_Elev,-2000535,Elevations,Annotation
OST_ElevationMarks,-2006045,Elevation Marks,Annotation
OST_Entourage,-2001370,Entourage,Model
OST_EntourageHiddenLines,-2009529,<Hidden Lines>,Model
OST_EntourageTags,-2001064,Entourage Tags,Annotation
OST_ExpansionJointHiddenLines,-2006272,<Hidden Lines>,Model
OST_ExpansionJoints,-2006271,Expansion Joints,Model
OST_ExpansionJointTags,-2006273,Expansion Joint Tags,Annotation
OST_FabricAreaBoundary,-2009029,Boundary,Model
OST_FabricAreas,-2009017,Structural Fabric Areas,Model
OST_FabricAreaSketchEnvelopeLines,-2009018,<Fabric Envelope>,Model
OST_FabricAreaSketchSheetsLines,-2009019,<Fabric Sheets>,Model
OST_FabricationContainment,-2008212,MEP Fabrication Containment,Model
OST_FabricationContainmentCenterLine,-2008214,Center Line,Model
OST_FabricationContainmentDrop,-2008219,Drop,Model
OST_FabricationContainmentRise,-2008218,Rise,Model
OST_FabricationContainmentSymbology,-2008215,Symbology,Model
OST_FabricationContainmentTags,-2008213,MEP Fabrication Containment Tags,Annotation
OST_FabricationDuctwork,-2008193,MEP Fabrication Ductwork,Model
OST_FabricationDuctworkCenterLine,-2008196,Center Line,Model
OST_FabricationDuctworkDrop,-2008206,Drop,Model
OST_FabricationDuctworkInsulation,-2008198,Insulation,Model
OST_FabricationDuctworkLining,-2008220,Lining,Model
OST_FabricationDuctworkRise,-2008205,Rise,Model
OST_FabricationDuctworkStiffeners,-2008228,MEP Fabrication Ductwork Stiffeners,Model
OST_FabricationDuctworkStiffenerTags,-2008229,MEP Fabrication Ductwork Stiffener Tags,Annotation
OST_FabricationDuctworkSymbology,-2008207,Symbology,Model
OST_FabricationDuctworkTags,-2008194,MEP Fabrication Ductwork Tags,Annotation
OST_FabricationHangers,-2008203,MEP Fabrication Hangers,Model
OST_FabricationHangerTags,-2008204,MEP Fabrication Hanger Tags,Annotation
OST_FabricationPipework,-2008208,MEP Fabrication Pipework,Model
OST_FabricationPipeworkCenterLine,-2008210,Center Line,Model
OST_FabricationPipeworkDrop,-2008217,Drop,Model
OST_FabricationPipeworkInsulation,-2008221,Insulation,Model
OST_FabricationPipeworkRise,-2008216,Rise,Model
OST_FabricationPipeworkSymbology,-2008211,Symbology,Model
OST_FabricationPipeworkTags,-2008209,MEP Fabrication Pipework Tags,Annotation
OST_FabricReinforcement,-2009016,Structural Fabric Reinforcement,Model
OST_FabricReinforcementBoundary,-2009026,Boundary,Model
OST_FabricReinforcementTags,-2009022,Structural Fabric Reinforcement Tags,Annotation
OST_FabricReinforcementWire,-2009027,Fabric Wire,Model
OST_FabricReinSpanSymbol,-2009028,Structural Fabric Reinforcement Symbols,Annotation
OST_Fascia,-2001390,Fascias,Model
OST_FasciaTags,-2001062,Fascia Tags,Annotation
OST_FilledRegion,-2000190,Filled region,Model
OST_FireAlarmDevices,-2008085,Fire Alarm Devices,Model
OST_FireAlarmDeviceTags,-2008086,Fire Alarm Device Tags,Annotation
OST_FireProtection,-2001049,Fire Protection,Model
OST_FireProtectionHiddenLines,-2001050,<Hidden Lines>,Model
OST_FireProtectionTags,-2001051,Fire Protection Tags,Annotation
OST_FlexDuctCurves,-2008020,Flex Ducts,Model
OST_FlexDuctCurvesCenterLine,-2008021,Center Line,Model
OST_FlexDuctCurvesPattern,-2008023,Pattern,Model
OST_FlexDuctTags,-2008004,Flex Duct Tags,Annotation
OST_FlexPipeCurves,-2008050,Flex Pipes,Model
OST_FlexPipeCurvesCenterLine,-2008051,Center Line,Model
OST_FlexPipeCurvesPattern,-2008053,Pattern,Model
OST_FlexPipeTags,-2008048,Flex Pipe Tags,Annotation
OST_Floors,-2000032,Floors,Model
OST_FloorsCutPattern,-2000608,Cut Pattern,Model
OST_FloorsDefault,-2000606,Common Edges,Model
OST_FloorsFinish1,-2000604,Finish 1 [4],Model
OST_FloorsFinish2,-2000605,Finish 2 [5],Model
OST_FloorsInsulation,-2000602,Thermal/Air Layer [3],Model
OST_FloorsInteriorEdges,-2000609,Folding Lines,Model
OST_FloorsMembrane,-2000600,Membrane Layer,Model
OST_FloorsSplitLines,-2001076,Split Lines,Model
OST_FloorsStructure,-2000601,Structure [1],Model
OST_FloorsSubstrate,-2000603,Substrate [2],Model
OST_FloorsSurfacePattern,-2000541,Surface Pattern,Model
OST_FloorTags,-2005026,Floor Tags,Annotation
OST_FoodServiceEquipment,-2001043,Food Service Equipment,Model
OST_FoodServiceEquipmentHiddenLines,-2001044,<Hidden Lines>,Model
OST_FoodServiceEquipmentTags,-2001045,Food Service Equipment Tags,Annotation
OST_FootingSpanDirectionSymbol,-2005111,Foundation Span Direction Symbol,Annotation
OST_Furniture,-2000080,Furniture,Model
OST_FurnitureHiddenLines,-2009505,<Hidden Lines>,Model
OST_FurnitureSystems,-2001100,Furniture Systems,Model
OST_FurnitureSystemsHiddenLines,-2009518,<Hidden Lines>,Model
OST_FurnitureSystemTags,-2005007,Furniture System Tags,Annotation
OST_FurnitureTags,-2005006,Furniture Tags,Annotation
OST_gbXML_Ceiling,-2008173,Ceilings,AnalyticalModel
OST_gbXML_ExteriorWall,-2008167,Exterior Walls,AnalyticalModel
OST_gbXML_FixedSkylight,-2008180,Fixed Skylights,AnalyticalModel
OST_gbXML_FixedWindow,-2008178,Fixed Windows,AnalyticalModel
OST_gbXML_InteriorFloor,-2008172,Interior Floors,AnalyticalModel
OST_gbXML_InteriorWall,-2008171,Interior Walls,AnalyticalModel
OST_gbXML_NonSlidingDoor,-2008183,Non-sliding Doors,AnalyticalModel
OST_GbXML_Opening,-2008095,Opening,AnalyticalModel
OST_gbXML_OpeningAir,-2008184,Air Openings,AnalyticalModel
OST_gbXML_OperableSkylight,-2008181,Operable Skylights,AnalyticalModel
OST_gbXML_OperableWindow,-2008179,Operable Windows,AnalyticalModel
OST_gbXML_RaisedFloor,-2008169,Raised Floors,AnalyticalModel
OST_gbXML_Roof,-2008168,Roofs,AnalyticalModel
OST_gbXML_Shade,-2008187,Shades,AnalyticalModel
OST_gbXML_SlabOnGrade,-2008170,Slabs on Grade,AnalyticalModel
OST_gbXML_SlidingDoor,-2008182,Sliding Doors,AnalyticalModel
OST_GbXML_SType_Exterior,-2008092,Exterior,AnalyticalModel
OST_GbXML_SType_Interior,-2008091,Interior,AnalyticalModel
OST_GbXML_SType_Shade,-2008093,Shades,AnalyticalModel
OST_GbXML_SType_Underground,-2008094,Underground,AnalyticalModel
OST_gbXML_SurfaceAir,-2008174,Air Surfaces,AnalyticalModel
OST_gbXML_UndergroundCeiling,-2008177,Underground Ceilings,AnalyticalModel
OST_gbXML_UndergroundSlab,-2008176,Underground Slabs,AnalyticalModel
OST_gbXML_UndergroundWall,-2008175,Underground Walls,AnalyticalModel
OST_GbXMLFaces,-2008090,Analytical Surfaces,AnalyticalModel
OST_GenericAnnotation,-2000150,Generic Annotations,Annotation
OST_GenericLines,-2000078,<Lines>,Model
OST_GenericModel,-2000151,Generic Models,Model
OST_GenericModelHiddenLines,-2009512,<Hidden Lines>,Model
OST_GenericModelTags,-2005013,Generic Model Tags,Annotation
OST_Girder,-2001322,Primary,Model
OST_GridChains,-2000221,Multi-segmented Grid,Annotation
OST_GridHeads,-2006040,Grid Heads,Annotation
OST_Grids,-2000220,Grids,Annotation
OST_GuideGrid,-2000107,Guide Grid,Annotation
OST_Gutter,-2001391,Gutters,Model
OST_GutterTags,-2001065,Gutter Tags,Annotation
OST_HandrailTags,-2001066,Handrail Tags,Annotation
OST_Hardscape,-2001036,Hardscape,Model
OST_HardscapeHiddenLines,-2001037,<Hidden Lines>,Model
OST_HardscapeTags,-2001038,Hardscape Tags,Annotation
OST_HiddenFloorLines,-2000607,<Hidden Lines>,Model
OST_HiddenLines,-2000286,<Hidden>,Model
OST_HiddenStructuralColumnLines,-2001334,<Hidden Lines>,Model
OST_HiddenStructuralFoundationLines,-2001302,<Hidden Lines>,Model
OST_HiddenStructuralFramingLines,-2001329,<Hidden Lines>,Model
OST_HiddenWallLines,-2000587,<Hidden Lines>,Model
OST_HorizontalBracing,-2001325,Plan Bracing,Model
OST_HVAC_Load_Building_Types,-2008120,Building Type Settings,Internal
OST_HVAC_Load_Space_Types,-2008119,Space Type Settings,Internal
OST_HVAC_Zones,-2008107,HVAC Zones,Model
OST_HVAC_Zones_Boundary,-2008108,Boundary,Model
OST_HVAC_Zones_ColorFill,-2008116,Color Fill,Model
OST_HVAC_Zones_InteriorFill_Visibility,-2008117,Interior Fill,Model
OST_HVAC_Zones_Reference_Visibility,-2008118,Reference Lines,Model
OST_ImportObjectStyles,-2000196,Imports in Families,Model
OST_InsulationLines,-2000077,<Insulation Batting Lines>,Model
OST_InternalAreaLoads,-2005207,Internal Area Loads,AnalyticalModel
OST_InternalAreaLoadTags,-2005255,Internal Area Load Tags,Annotation
OST_InternalLineLoads,-2005206,Internal Line Loads,AnalyticalModel
OST_InternalLineLoadTags,-2005254,Internal Line Load Tags,Annotation
OST_InternalLoads,-2005204,Structural Internal Loads,AnalyticalModel
OST_InternalPointLoads,-2005205,Internal Point Loads,AnalyticalModel
OST_InternalPointLoadTags,-2005253,Internal Point Load Tags,Annotation
OST_IOSModelGroups,-2000095,Model Groups,Internal
OST_Joist,-2001323,Secondary,Model
OST_KeynoteTags,-2005029,Keynote Tags,Annotation
OST_KickerBracing,-2001328,Kicker Bracing,Model
OST_LevelHeads,-2006020,Level Heads,Annotation
OST_Levels,-2000240,Levels,Annotation
OST_LightingDevices,-2008087,Lighting Devices,Model
OST_LightingDeviceTags,-2008088,Lighting Device Tags,Annotation
OST_LightingFixtures,-2001120,Lighting Fixtures,Model
OST_LightingFixturesHiddenLines,-2009519,<Hidden Lines>,Model
OST_LightingFixtureSource,-2001121,Light Source,Model
OST_LightingFixtureTags,-2005008,Lighting Fixture Tags,Annotation
OST_LineLoads,-2005202,Line Loads,AnalyticalModel
OST_LineLoadTags,-2005251,Line Load Tags,Annotation
OST_Lines,-2000051,Lines,Model
OST_LinesBeyond,-2000287,<Beyond>,Model
OST_LinesHiddenLines,-2009504,<Hidden Lines>,Model
OST_LinkAnalyticalTags,-2000955,Analytical Link Tags,Annotation
OST_LinksAnalytical,-2009657,Analytical Links,AnalyticalModel
OST_LoadCases,-2005210,Structural Load Cases,AnalyticalModel
OST_LoadCasesAccidental,-2005216,Accidental Loads,AnalyticalModel
OST_LoadCasesDead,-2005211,Dead Loads,AnalyticalModel
OST_LoadCasesLive,-2005212,Live Loads,AnalyticalModel
OST_LoadCasesRoofLive,-2005215,Roof Live Loads,AnalyticalModel
OST_LoadCasesSeismic,-2005218,Seismic Loads,AnalyticalModel
OST_LoadCasesSnow,-2005214,Snow Loads,AnalyticalModel
OST_LoadCasesTemperature,-2005217,Temperature Loads,AnalyticalModel
OST_LoadCasesWind,-2005213,Wind Loads,AnalyticalModel
OST_Loads,-2005200,Structural Loads,AnalyticalModel
OST_MaskingRegion,-2000194,Masking Region,Model
OST_Mass,-2003400,Mass,Model
OST_MassAreaFaceTags,-2003410,Mass Floor Tags,Annotation
OST_MassExteriorWall,-2003413,Mass Exterior Wall,Model
OST_MassFloor,-2003403,Mass Floor,Model
OST_MassForm,-2003404,Form,Model
OST_MassGlazing,-2003415,Mass Glazing,Model
OST_MassHiddenLines,-2009532,<Hidden Lines>,Model
OST_MassInteriorWall,-2003412,Mass Interior Wall,Model
OST_MassOpening,-2003417,Mass Opening,Model
OST_MassRoof,-2003414,Mass Roof,Model
OST_MassShade,-2003418,Mass Shade,Model
OST_MassSkylights,-2003416,Mass Skylight,Model
OST_MassTags,-2003405,Mass Tags,Annotation
OST_MassZone,-2003411,Mass Zone,Model
OST_Matchline,-2000193,Matchline,Annotation
OST_Materials,-2000700,Materials,Model
OST_MaterialTags,-2005027,Material Tags,Annotation
OST_MechanicalControlDevices,-2008232,Mechanical Control Devices,Model
OST_MechanicalControlDevicesHiddenLines,-2009550,<Hidden Lines>,Model
OST_MechanicalControlDeviceTags,-2008233,Mechanical Control Device Tags,Annotation
OST_MechanicalEquipment,-2001140,Mechanical Equipment,Model
OST_MechanicalEquipmentHiddenLines,-2009520,<Hidden Lines>,Model
OST_MechanicalEquipmentSet,-2000985,Mechanical Equipment Sets,Model
OST_MechanicalEquipmentSetBoundaryLines,-2000987,Mechanical Equipment Set Boundary Lines,Annotation
OST_MechanicalEquipmentSetTags,-2000986,Mechanical Equipment Set Tags,Annotation
OST_MechanicalEquipmentTags,-2005009,Mechanical Equipment Tags,Annotation
OST_MedicalEquipment,-2001046,Medical Equipment,Model
OST_MedicalEquipmentHiddenLines,-2001047,<Hidden Lines>,Model
OST_MedicalEquipmentTags,-2001048,Medical Equipment Tags,Annotation
OST_MEPAnalyticalAirLoop,-2001008,Air Systems,Model
OST_MEPAnalyticalBus,-2001021,Electrical Analytical Bus,Model
OST_MEPAnalyticalTransferSwitch,-2001023,Electrical Analytical Transfer Switch,Model
OST_MEPAnalyticalWaterLoop,-2001009,Water Loops,Model
OST_MEPAncillaryFraming,-2008231,MEP Ancillary Framing,Model
OST_MEPAncillaryFramingTags,-2008236,MEP Ancillary Framing Tags,Annotation
OST_MEPLoadAreas,-2001024,Electrical Load Areas,AnalyticalModel
OST_MEPLoadAreaSeparationLines,-2001033,<Area Based Load Boundary>,Model
OST_MEPSpaceColorFill,-2003605,Color Fill,Model
OST_MEPSpaceInteriorFillVisibility,-2003601,Interior,Model
OST_MEPSpaceReferenceVisibility,-2003602,Reference,Model
OST_MEPSpaces,-2003600,Spaces,Model
OST_MEPSpaceSeparationLines,-2000831,<Space Separation>,Model
OST_MEPSpaceTags,-2000485,Space Tags,Annotation
OST_MEPSystemZone,-2001001,System-Zones,AnalyticalModel
OST_MEPSystemZoneTags,-2001007,System-Zone Tags,Annotation
OST_ModelGroupTags,-2001073,Model Group Tags,Annotation
OST_MultiCategoryTags,-2005022,Multi-Category Tags,Annotation
OST_MultiReferenceAnnotations,-2000970,Multi-Rebar Annotations,Annotation
OST_NodeAnalyticalTags,-2000956,Analytical Node Tags,Annotation
OST_NurseCallDevices,-2008077,Nurse Call Devices,Model
OST_NurseCallDeviceTags,-2008078,Nurse Call Device Tags,Annotation
OST_OverheadLines,-2000284,<Overhead>,Model
OST_PadTags,-2001067,Pad Tags,Annotation
OST_PanelScheduleGraphics,-2008151,Panel Schedule Graphics,Annotation
OST_Parking,-2001180,Parking,Model
OST_ParkingHiddenLines,-2009522,<Hidden Lines>,Model
OST_ParkingTags,-2005017,Parking Tags,Annotation
OST_PartHiddenLines,-2000271,<Hidden Lines>,Model
OST_Parts,-2000269,Parts,Model
OST_PartTags,-2000270,Part Tags,Annotation
OST_PathOfTravelLines,-2000833,<Path of Travel Lines>,Model
OST_PathOfTravelTags,-2000834,Path of Travel Tags,Annotation
OST_PathRein,-2009009,Structural Path Reinforcement,Model
OST_PathReinBoundary,-2009012,Boundary,Model
OST_PathReinSpanSymbol,-2009010,Structural Path Reinforcement Symbols,Annotation
OST_PathReinTags,-2009011,Structural Path Reinforcement Tags,Annotation
OST_Phases,-2000112,Phases,Internal
OST_PierCaps,-2006219,Pier Caps,Model
OST_PierCapTags,-2006220,Pier Cap Tags,Annotation
OST_PierColumns,-2006221,Pier Columns,Model
OST_PierColumnTags,-2006222,Pier Column Tags,Annotation
OST_PierPiles,-2006225,Pier Piles,Model
OST_PierPileTags,-2006226,Pier Pile Tags,Annotation
OST_PierWalls,-2006229,Pier Walls,Model
OST_PierWallTags,-2006230,Pier Wall Tags,Annotation
OST_PipeAccessory,-2008055,Pipe Accessories,Model
OST_PipeAccessoryTags,-2008056,Pipe Accessory Tags,Annotation
OST_PipeAnalyticalSegments,-2001113,Analytical Pipe Segments,AnalyticalModel
OST_PipeAnalyticalSegmentTags,-2001114,Analytical Pipe Segment Tags,Annotation
OST_PipeColorFillLegends,-2008058,Pipe Color Fill Legends,Annotation
OST_PipeColorFills,-2008059,Pipe Color Fill,Annotation
OST_PipeCurves,-2008044,Pipes,Model
OST_PipeCurvesCenterLine,-2008045,Center Line,Model
OST_PipeCurvesDrop,-2008069,Drop,Model
OST_PipeCurvesRiseDrop,-2008054,Rise,Model
OST_PipeFitting,-2008049,Pipe Fittings,Model
OST_PipeFittingCenterLine,-2008072,Center Line,Model
OST_PipeFittingTags,-2008060,Pipe Fitting Tags,Annotation
OST_PipeHydronicSeparationSymbols,-2000988,Hydraulic Separation Symbols,Model
OST_PipeInsulations,-2008122,Pipe Insulations,Model
OST_PipeInsulationsTags,-2008155,Pipe Insulation Tags,Annotation
OST_PipeSegments,-2008163,Pipe Segments,Model
OST_PipeTags,-2008047,Pipe Tags,Annotation
OST_PipingSystem,-2008043,Piping Systems,Model
OST_PipingSystem_Reference_Visibility,-2008159,Reference Lines,Model
OST_PlaceHolderDucts,-2008160,Duct Placeholders,Model
OST_PlaceHolderPipes,-2008161,Pipe Placeholders,Model
OST_PlanRegion,-2000191,Plan Region,Annotation
OST_Planting,-2001360,Planting,Model
OST_PlantingHiddenLines,-2009528,<Hidden Lines>,Model
OST_PlantingTags,-2005021,Planting Tags,Annotation
OST_PlumbingEquipment,-2008234,Plumbing Equipment,Model
OST_PlumbingEquipmentHiddenLines,-2009551,<Hidden Lines>,Model
OST_PlumbingEquipmentTags,-2008235,Plumbing Equipment Tags,Annotation
OST_PlumbingFixtures,-2001160,Plumbing Fixtures,Model
OST_PlumbingFixturesHiddenLines,-2009521,<Hidden Lines>,Model
OST_PlumbingFixtureTags,-2005010,Plumbing Fixture Tags,Annotation
OST_PointClouds,-2010001,Point Clouds,Model
OST_PointLoads,-2005201,Point Loads,AnalyticalModel
OST_PointLoadTags,-2005250,Point Load Tags,Annotation
OST_ProjectBasePoint,-2001271,Project Base Point,Model
OST_ProjectInformation,-2003101,Project Information,Model
OST_Purlin,-2001324,Tertiary,Model
OST_RailingHandRail,-2000947,Handrails,Model
OST_RailingHandRailAboveCut,-2000951,<Above> Handrails,Model
OST_RailingSupport,-2000948,Supports,Model
OST_RailingTermination,-2000949,Terminations,Model
OST_RailingTopRail,-2000946,Top Rails,Model
OST_RailingTopRailAboveCut,-2000950,<Above> Top Rails,Model
OST_Ramps,-2000180,Ramps,Model
OST_RampsAboveCut,-2003302,Ramps Beyond Cut Line,Model
OST_RampsDownArrow,-2003308,Down Arrow,Model
OST_RampsDownText,-2003306,DOWN text,Model
OST_RampsHiddenLines,-2009509,<Hidden Lines>,Model
OST_RampsStringer,-2003303,Stringers,Model
OST_RampsStringerAboveCut,-2003304,Stringers Beyond Cut Line,Model
OST_RampsUpArrow,-2003307,Up Arrow,Model
OST_RampsUpText,-2003305,UP text,Model
OST_RampTags,-2001068,Ramp Tags,Annotation
OST_RasterImages,-2000560,Raster Images,Model
OST_Rebar,-2009000,Structural Rebar,Model
OST_RebarBendingDetails,-2001104,Structural Rebar Bending Details,Annotation
OST_RebarCover,-2009015,Rebar Cover References,Annotation
OST_RebarHiddenLines,-2009050,<Hidden Lines>,Model
OST_RebarSetToggle,-2009025,Rebar Set Toggle,Annotation
OST_RebarShape,-2009013,Rebar Shape,Model
OST_RebarSpliceLines,-2001108,Splice Location Lines,Model
OST_RebarTags,-2009020,Structural Rebar Tags,Annotation
OST_ReferenceLines,-2000083,Reference Lines,Annotation
OST_ReferencePoints,-2000710,Reference Points,Annotation
OST_ReferencePoints_Lines,-2000713,Lines,Annotation
OST_ReferencePoints_Planes,-2000712,Planes,Annotation
OST_ReferencePoints_Points,-2000711,Points,Annotation
OST_ReferenceViewer,-2000198,View References,Annotation
OST_ReferenceViewerSymbol,-2000197,View Reference,Annotation
OST_RenderRegions,-2000302,Render Regions,Annotation
OST_RevisionClouds,-2006060,Revision Clouds,Annotation
OST_RevisionCloudTags,-2006080,Revision Cloud Tags,Annotation
OST_Roads,-2001220,Roads,Model
OST_RoadsHiddenLines,-2009523,<Hidden Lines>,Model
OST_RoadTags,-2001221,Road Tags,Annotation
OST_Roofs,-2000035,Roofs,Model
OST_RoofsCutPattern,-2000597,Cut Pattern,Model
OST_RoofsDefault,-2000596,Common Edges,Model
OST_RoofsFinish1,-2000594,Finish 1 [4],Model
OST_RoofsFinish2,-2000595,Finish 2 [5],Model
OST_RoofsHiddenLines,-2009502,<Hidden Lines>,Model
OST_RoofsInsulation,-2000592,Thermal/Air Layer [3],Model
OST_RoofsInteriorEdges,-2000598,Interior Edges,Model
OST_RoofsMembrane,-2000590,Membrane Layer,Model
OST_RoofSoffit,-2001393,Roof Soffits,Model
OST_RoofSoffitTags,-2001069,Roof Soffit Tags,Annotation
OST_RoofsStructure,-2000591,Structure [1],Model
OST_RoofsSubstrate,-2000593,Substrate [2],Model
OST_RoofsSurfacePattern,-2000542,Surface Pattern,Model
OST_RoofTags,-2000266,Roof Tags,Annotation
OST_RoomColorFill,-2000551,Color Fill,Model
OST_RoomInteriorFillVisibility,-2000161,Interior Fill,Model
OST_RoomReferenceVisibility,-2000162,Reference,Model
OST_Rooms,-2000160,Rooms,Model
OST_RoomSeparationLines,-2000066,<Room Separation>,Model
OST_RoomTags,-2000480,Room Tags,Annotation
OST_RoutingPreferences,-2008125,Routing Preferences,Model
OST_RvtLinks,-2001352,RVT Links,Model
OST_RvtLinksTags,-2001074,RVT Link Tags,Annotation
OST_ScheduleGraphics,-2000570,Schedule Graphics,Annotation
OST_Schedules,-2000573,Schedules,Internal
OST_SecondaryTopographyContours,-2001343,Secondary Contours,Model
OST_SectionBox,-2000301,Section Boxes,Annotation
OST_SectionHeadMediumLines,-2000403,<Medium Lines>,Annotation
OST_SectionHeads,-2000400,Section Marks,Annotation
OST_SectionHeadThinLines,-2000401,<Thin Lines>,Annotation
OST_SectionHeadWideLines,-2000404,<Wide Lines>,Annotation
OST_SectionLine,-2000201,Section Line,Annotation
OST_Sections,-2000200,Sections,Annotation
OST_SecurityDevices,-2008079,Security Devices,Model
OST_SecurityDeviceTags,-2008080,Security Device Tags,Annotation
OST_ShaftOpening,-2000996,Shaft Openings,Model
OST_ShaftOpeningHiddenLines,-2009513,<Hidden Lines>,Model
OST_SharedBasePoint,-2001272,Survey Point,Model
OST_SheetCollections,-2001112,Sheet Collections,Model
OST_Sheets,-2003100,Sheets,Model
OST_Signage,-2001058,Signage,Model
OST_SignageHiddenLines,-2001059,<Hidden Lines>,Model
OST_SignageTags,-2001061,Signage Tags,Annotation
OST_Site,-2001260,Site,Model
OST_SiteHiddenLines,-2009524,<Hidden Lines>,Model
OST_SitePoint,-2001262,Interior Point,Model
OST_SitePointBoundary,-2001266,Boundary Point,Model
OST_SiteProperty,-2001265,Property Lines,Model
OST_SitePropertyLineSegment,-2001268,Property Line Segments,Internal
OST_SitePropertyLineSegmentTags,-2001269,Property Line Segment Tags,Annotation
OST_SitePropertyTags,-2001267,Property Tags,Annotation
OST_SiteTags,-2005016,Site Tags,Annotation
OST_SketchLines,-2000045,<Sketch>,Model
OST_SlabEdgeTags,-2001070,Slab Edge Tags,Annotation
OST_SpanDirectionSymbol,-2005110,Span Direction Symbol,Annotation
OST_SpecialityEquipment,-2001350,Specialty Equipment,Model
OST_SpecialityEquipmentHiddenLines,-2009527,<Hidden Lines>,Model
OST_SpecialityEquipmentTags,-2005014,Specialty Equipment Tags,Annotation
OST_SpotCoordinates,-2000264,Spot Coordinates,Annotation
OST_SpotElevations,-2000263,Spot Elevations,Annotation
OST_SpotElevSymbols,-2005100,Spot Elevation Symbols,Annotation
OST_SpotSlopes,-2000265,Spot Slopes,Annotation
OST_Sprinklers,-2008099,Sprinklers,Model
OST_SprinklerTags,-2008100,Sprinkler Tags,Annotation
OST_Stairs,-2000120,Stairs,Model
OST_StairsCutMarks,-2000930,Cut Marks,Model
OST_StairsCutMarksAboveCut,-2000931,<Above> Cut Marks,Model
OST_StairsDownArrows,-2000131,Down Arrows,Annotation
OST_StairsDownText,-2000129,DOWN text,Annotation
OST_StairsHiddenLines,-2009507,<Hidden Lines>,Model
OST_StairsLandings,-2000920,Landings,Model
OST_StairsLandingTags,-2000941,Stair Landing Tags,Annotation
OST_StairsNosingLines,-2000932,Nosing Lines,Model
OST_StairsNosingLinesAboveCut,-2000933,<Above> Nosing Lines,Model
OST_StairsOutlines,-2000934,Outlines,Model
OST_StairsOutlinesAboveCut,-2000935,<Above> Outlines,Model
OST_StairsPaths,-2000938,Stair Paths,Annotation
OST_StairsPathsAboveCut,-2000939,<Above> Up Arrows,Annotation
OST_StairsRailing,-2000126,Railings,Model
OST_StairsRailingAboveCut,-2000132,<Above> Railings Cut Line,Model
OST_StairsRailingBaluster,-2000127,Balusters,Model
OST_StairsRailingHiddenLines,-2009508,<Hidden Lines>,Model
OST_StairsRailingRail,-2000128,Rails,Model
OST_StairsRailingTags,-2000133,Railing Tags,Annotation
OST_StairsRiserLines,-2000936,Riser Lines,Model
OST_StairsRiserLinesAboveCut,-2000937,<Above> Riser Lines,Model
OST_StairsRuns,-2000919,Runs,Model
OST_StairsRunTags,-2000940,Stair Run Tags,Annotation
OST_StairsSupports,-2000952,Supports,Model
OST_StairsSupportsAboveCut,-2000124,<Above> Supports,Model
OST_StairsSupportTags,-2000942,Stair Support Tags,Annotation
OST_StairsTags,-2005023,Stair Tags,Annotation
OST_StairsTriserNumbers,-2000944,Stair Tread/Riser Numbers,Annotation
OST_StairsTrisers,-2000921,Treads/Risers,Model
OST_StairsUpArrows,-2000130,Up Arrows,Annotation
OST_StairsUpText,-2000125,UP text,Annotation
OST_StructConnectionAnchors,-2009039,Anchors,Model
OST_StructConnectionAnchorTags,-2009057,Anchor Tags,Annotation
OST_StructConnectionBolts,-2009041,Bolts,Model
OST_StructConnectionBoltTags,-2009056,Bolt Tags,Annotation
OST_StructConnectionHiddenLines,-2009032,<Hidden Lines>,Model
OST_StructConnectionHoles,-2009045,Holes,Model
OST_StructConnectionHoleTags,-2009063,Hole Tags,Annotation
OST_StructConnectionModifiers,-2009047,Modifiers,Model
OST_StructConnectionOthers,-2009042,Others,Model
OST_StructConnectionPlates,-2009038,Plates,Model
OST_StructConnectionPlateTags,-2009055,Plate Tags,Annotation
OST_StructConnectionProfiles,-2009037,Profiles,Model
OST_StructConnectionProfilesTags,-2009064,Profile Tags,Annotation
OST_StructConnectionReference,-2009036,Reference,Model
OST_StructConnections,-2009030,Structural Connections,Model
OST_StructConnectionShearStuds,-2009044,Shear Studs,Model
OST_StructConnectionShearStudTags,-2009058,Shear Stud Tags,Annotation
OST_StructConnectionSymbol,-2009033,Symbol,Model
OST_StructConnectionSymbols,-2006100,Connection Symbols,Annotation
OST_StructConnectionTags,-2009040,Structural Connection Tags,Annotation
OST_StructConnectionWelds,-2009046,Welds,Model
OST_StructConnectionWeldTags,-2009059,Weld Tags,Annotation
OST_StructuralAnnotations,-2006090,Structural Annotations,Annotation
OST_StructuralBracePlanReps,-2006110,Brace in Plan View Symbols,Annotation
OST_StructuralColumnLocationLine,-2001357,Location Lines,Model
OST_StructuralColumns,-2001330,Structural Columns,Model
OST_StructuralColumnStickSymbols,-2001335,Stick Symbols,Model
OST_StructuralColumnTags,-2005018,Structural Column Tags,Annotation
OST_StructuralFoundation,-2001300,Structural Foundations,Model
OST_StructuralFoundationTags,-2005019,Structural Foundation Tags,Annotation
OST_StructuralFraming,-2001320,Structural Framing,Model
OST_StructuralFramingLocationLine,-2001356,Location Lines,Model
OST_StructuralFramingOther,-2001321,Other,Model
OST_StructuralFramingSystem,-2001327,Structural Beam Systems,Model
OST_StructuralFramingTags,-2005015,Structural Framing Tags,Annotation
OST_StructuralStiffener,-2001354,Structural Stiffeners,Model
OST_StructuralStiffenerHiddenLines,-2001358,<Hidden Lines>,Model
OST_StructuralStiffenerTags,-2001355,Structural Stiffener Tags,Annotation
OST_StructuralTendonHiddenLines,-2006275,<Hidden Lines>,Model
OST_StructuralTendons,-2006274,Structural Tendons,Model
OST_StructuralTendonTags,-2006276,Structural Tendon Tags,Annotation
OST_StructuralTruss,-2001336,Structural Trusses,Model
OST_StructuralTrussStickSymbols,-2009608,Stick Symbols,Annotation
OST_SwitchSystem,-2008101,Switch System,Model
OST_TelephoneDevices,-2008075,Telephone Devices,Model
OST_TelephoneDeviceTags,-2008076,Telephone Device Tags,Annotation
OST_TemporaryStructure,-2001039,Temporary Structures,Model
OST_TemporaryStructureHiddenLines,-2001041,<Hidden Lines>,Model
OST_TemporaryStructureTags,-2001042,Temporary Structure Tags,Annotation
OST_TextNotes,-2000300,Text Notes,Annotation
OST_TitleBlockMediumLines,-2000282,<Medium Lines>,Annotation
OST_TitleBlocks,-2000280,Title Blocks,Annotation
OST_TitleBlockThinLines,-2000281,<Thin Lines>,Annotation
OST_TitleBlockWideLines,-2000283,<Wide Lines>,Annotation
OST_Topography,-2001340,Topography,Model
OST_TopographyContours,-2001342,Primary Contours,Model
OST_TopographyHiddenLines,-2009526,<Hidden Lines>,Model
OST_TopographyLink,-2001339,Topography Links,Model
OST_TopographySurface,-2001341,Triangulation Edges,Model
OST_Toposolid,-2001079,Toposolid,Model
OST_ToposolidContours,-2001081,Primary Contours,Model
OST_ToposolidCutPattern,-2001086,Cut Pattern,Model
OST_ToposolidDefault,-2001085,Common Edges,Model
OST_ToposolidFinish1,-2001090,Finish 1 [4],Model
OST_ToposolidFinish2,-2001091,Finish 2 [5],Model
OST_ToposolidFoldingLines,-2001083,Folding Lines,Model
OST_ToposolidHiddenLines,-2001080,<Hidden Lines>,Model
OST_ToposolidInsulation,-2001093,Thermal/Air Layer [3],Model
OST_ToposolidLink,-2001097,Toposolid Links,Model
OST_ToposolidLinkTags,-2001103,Toposolid Link Tags,Annotation
OST_ToposolidMembrane,-2001087,Membrane Layer,Model
OST_ToposolidSecondaryContours,-2001082,Secondary Contours,Model
OST_ToposolidSplitLines,-2001084,Split Lines,Model
OST_ToposolidStructure,-2001088,Structure [1],Model
OST_ToposolidSubstrate,-2001089,Substrate [2],Model
OST_ToposolidSurfacePattern,-2001092,Surface Pattern,Model
OST_ToposolidTags,-2001094,Toposolid Tags,Annotation
OST_TopRailTags,-2001071,Top Rail Tags,Annotation
OST_TrussChord,-2009606,Chord,Model
OST_TrussTags,-2005030,Structural Truss Tags,Annotation
OST_TrussWeb,-2009605,Web,Model
OST_VerticalBracing,-2001326,Vertical Bracing,Model
OST_VerticalCirculation,-2001052,Vertical Circulation,Model
OST_VerticalCirculationHiddenLines,-2001053,<Hidden Lines>,Model
OST_VerticalCirculationTags,-2001054,Vertical Circulation Tags,Annotation
OST_VibrationDampers,-2006263,Vibration Dampers,Model
OST_VibrationDamperTags,-2006264,Vibration Damper Tags,Annotation
OST_VibrationIsolators,-2006265,Vibration Isolators,Model
OST_VibrationIsolatorTags,-2006266,Vibration Isolator Tags,Annotation
OST_VibrationManagement,-2006261,Vibration Management,Model
OST_VibrationManagementHiddenLines,-2006262,<Hidden Lines>,Model
OST_VibrationManagementTags,-2006282,Vibration Management Tags,Annotation
OST_Viewers,-2000278,Views,Internal
OST_ViewportLabel,-2000515,View Titles,Annotation
OST_Viewports,-2000510,Viewports,Annotation
OST_Views,-2000279,Views,Internal
OST_VolumeOfInterest,-2006000,Scope Boxes,Annotation
OST_WallNonCoreLayer,-2001034,Non-Core Layers,Model
OST_Walls,-2000011,Walls,Model
OST_WallsCutPattern,-2000588,Cut Pattern,Model
OST_WallsDefault,-2000586,Common Edges,Model
OST_WallsFinish1,-2000584,Finish 1 [4],Model
OST_WallsFinish2,-2000585,Finish 2 [5],Model
OST_WallsInsulation,-2000582,Thermal/Air Layer [3],Model
OST_WallsMembrane,-2000580,Membrane Layer,Model
OST_WallsStructure,-2000581,Structure [1],Model
OST_WallsSubstrate,-2000583,Substrate [2],Model
OST_WallsSurfacePattern,-2000540,Surface Pattern,Model
OST_WallSweepTags,-2001072,Wall Sweep Tags,Annotation
OST_WallTags,-2005011,Wall Tags,Annotation
OST_WeakDims,-2000261,Automatic Sketch Dimensions,Annotation
OST_Windows,-2000014,Windows,Model
OST_WindowsFrameMullionProjection,-2000018,Frame/Mullion,Model
OST_WindowsGlassProjection,-2000016,Glass,Model
OST_WindowsHiddenLines,-2009500,<Hidden Lines>,Model
OST_WindowsOpeningProjection,-2000022,Opening,Model
OST_WindowsSillHeadProjection,-2000020,Sill/Head,Model
OST_WindowTags,-2000450,Window Tags,Annotation
OST_Wire,-2008039,Wires,Model
OST_WireHomeRunArrows,-2008089,Home Run Arrows,Model
OST_WireTags,-2008057,Wire Tags,Annotation
OST_WireTickMarks,-2008074,Wire Tick Marks,Annotation
OST_ZoneEquipment,-2001010,Zone Equipment,Model
OST_ZoneTags,-2008115,Zone Tags,Annotation
";
}
