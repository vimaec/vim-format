using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents the element information.
    /// </summary>
    public class ElementDigest
    {
        /// <summary>
        /// The index of the element in the VIM scene.
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// Element ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Element unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// Element name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Element's BIM document name.
        /// </summary>
        public string BimDocumentName { get; set; }

        /// <summary>
        /// Element category name (localized)
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Element built-in category name (non-localized)
        /// </summary>
        public string BuiltInCategoryName { get; set; }

        /// <summary>
        /// Element family name
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Element family type name
        /// </summary>
        public string FamilyTypeName { get; set; }

        /// <summary>
        /// Determines whether the element represents a family instance.
        /// </summary>
        public bool IsFamilyInstance { get; set; }

        /// <summary>
        /// Determines whether the element represents a family type.
        /// </summary>
        public bool IsFamilyType { get; set; }

        /// <summary>
        /// Determines whether the element represents a family.
        /// </summary>
        public bool IsFamily { get; set; }

        /// <summary>
        /// Determines whether the element represents a system.
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// The reference to the BIM Document to which the element belongs (i.e. a foreign key on BimDocumentDigest.VimIndex)
        /// </summary>
        public int Ref_BimDocumentDigest_VimIndex { get; set; }

        /// <summary>
        /// The reference to the Level to which the element belongs (i.e. a foreign key on LevelDigest.VimIndex)
        /// </summary>
        public int Ref_LevelDigest_VimIndex { get; set; }

        /// <summary>
        /// The reference to the Room in which the element belongs. (i.e. a foreign key on RoomDigest.VimIndex)
        /// </summary>
        public int Ref_RoomDigest_VimIndex { get; set; }

        /// <summary>
        /// The element's family instance parameters (may only contain items if IsFamilyInstance is true)
        /// </summary>
        public List<ParameterDigest> FamilyInstanceParameters { get; set; } = new List<ParameterDigest>();

        /// <summary>
        /// The element's family type parameters (may only contain items if IsFamilyInstance or IsFamilyType is true)
        /// </summary>
        public List<ParameterDigest> FamilyTypeParameters { get; set; } = new List<ParameterDigest>();

        /// <summary>
        /// The element's family parameters (may only contain items if IsFamilyInstance or IsFamilyType or IsFamily is true)
        /// </summary>
        public List<ParameterDigest> FamilyParameters { get; set; } = new List<ParameterDigest>();

        /// <summary>
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public ElementDigest() {}

        /// <summary>
        /// Returns a collection of element digests for each element in the given VIM scene.
        /// </summary>
        public static IEnumerable<ElementDigest> GetElementDigestCollection(VimScene vimScene)
        {
            var result = new List<ElementDigest>();

            for (var elementIndex = 0; elementIndex < vimScene.DocumentModel.NumElement; ++elementIndex)
            {
                var elementInfo = vimScene.DocumentModel.GetElementInfo(elementIndex);
                
                var elementDigest = new ElementDigest
                {
                    VimIndex = elementInfo.ElementIndex,
                    ElementId = elementInfo.ElementId,
                    ElementUniqueId = elementInfo.ElementUniqueId,
                    Name = elementInfo.ElementName,
                    BimDocumentName = elementInfo.BimDocument.Name,
                    CategoryName = elementInfo.CategoryName,
                    BuiltInCategoryName = elementInfo.CategoryBuiltInName,
                    FamilyName = elementInfo.FamilyName,
                    FamilyTypeName = elementInfo.FamilyTypeName,
                    IsFamilyInstance = elementInfo.IsFamilyInstance,
                    IsFamilyType = elementInfo.IsFamilyType,
                    IsFamily = elementInfo.IsFamily,
                    IsSystem = elementInfo.IsSystem,
                    Ref_BimDocumentDigest_VimIndex = elementInfo.BimDocumentIndex,
                    Ref_LevelDigest_VimIndex = elementInfo.LevelIndex,
                    Ref_RoomDigest_VimIndex = elementInfo.RoomIndex,
                };

                // Add the parameters to the element. NOTE: this generates a lot of duplicated strings.
                var parameters = elementInfo.GetScopedParameters();

                if (parameters.TryGetValue(ElementInfo.ParameterScope.FamilyInstance, out var familyInstanceParameters))
                {
                    elementDigest.FamilyInstanceParameters.AddRange(
                        familyInstanceParameters.Select(ParameterDigest.CreateFromParameter));
                }

                if (parameters.TryGetValue(ElementInfo.ParameterScope.FamilyType, out var familyTypeParameters))
                {
                    elementDigest.FamilyTypeParameters.AddRange(
                        familyTypeParameters.Select(ParameterDigest.CreateFromParameter));
                }

                if (parameters.TryGetValue(ElementInfo.ParameterScope.Family, out var familyParameters))
                {
                    elementDigest.FamilyParameters.AddRange(
                        familyParameters.Select(ParameterDigest.CreateFromParameter));
                }

                result.Add(elementDigest);
            }

            return result;
        }
    }

    /// <summary>
    /// Represents parameter information for an element.
    /// </summary>
    public class ParameterDigest
    {
        /// <summary>
        /// The group under which this parameter is displayed
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// The GUID of the parameter
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The raw value of the parameter
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The display value of the parameter
        /// </summary>
        public string DisplayValue { get; set; }

        /// <summary>
        /// The display units of the parameter, concatenated with pipe characters: label|type|spec
        /// </summary>
        public string DisplayUnit { get; set; }

        /// <summary>
        /// The type of the parameter
        /// </summary>
        public string ParameterType { get; set; }

        /// <summary>
        /// The storage type of the parameter
        /// </summary>
        public string StorageType { get; set; }

        /// <summary>
        /// Determines whether the parameter is shared
        /// </summary>
        public bool IsShared { get; set; }

        /// <summary>
        /// Determines whether the parameter is bound to the project
        /// </summary>
        public bool IsProject { get; set; }

        /// <summary>
        /// Determines whether the parameter is built-in
        /// </summary>
        public bool IsBuiltIn { get; set; }

        /// <summary>
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public ParameterDigest() { }

        /// <summary>
        /// Returns a ParameterDigest from the given parameter.
        /// </summary>
        public static ParameterDigest CreateFromParameter(Parameter p)
        {
            var descriptor = p.ParameterDescriptor;
            var displayUnit = descriptor.DisplayUnit;

            var (value, displayValue) = p.Values;

            return new ParameterDigest()
            {
                Group = descriptor.Group,
                Guid = descriptor.Guid,
                Name = descriptor.Name,
                Value = value,
                DisplayValue = displayValue,
                DisplayUnit = string.Join("|", displayUnit.Label, displayUnit.Type, displayUnit.Spec),
                ParameterType = descriptor.ParameterType,
                StorageType = descriptor.GetParameterDescriptorStorageType().ToString("G"),
                IsShared = descriptor.IsShared,
                IsProject = descriptor.IsProject,
                IsBuiltIn = descriptor.IsBuiltIn,
            };
        }
    }
}
