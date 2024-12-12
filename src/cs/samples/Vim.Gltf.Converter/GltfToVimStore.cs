using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Vim.Format;
using Vim.Format.ObjectModel;
using Vim.Math3d;

namespace Vim.Gltf.Converter
{
    /// <summary>
    /// A bare-bones example of converting a GLTF into a VIM file.
    /// </summary>
    public class GltfToVimStore : ObjectModelStore
    {
        // ASSEMBLY VERSION INFO
        private static readonly Version AssemblyVersion = typeof(GltfToVimStore).Assembly.GetName().Version;
        public static readonly string VersionString = $"v{AssemblyVersion.Major}.{AssemblyVersion.Minor}.{AssemblyVersion.Build}";
        private const string GeneratorString = "Vim.Gltf.Converter";

        // NOTE: IN GLTF, units are assumed to be in meters however in VIM they are in feet.
        public const float MetersToFeet = 3.280839895f;

        /// <summary>
        /// Converts a GLTF file into a VIM file.
        /// </summary>
        public static void Convert(string gltfFilePath, string vimFilePath, float scale = MetersToFeet)
        {
            var store = new GltfToVimStore(gltfFilePath, scale);
            store.VisitGltf();
            store.WriteVim(vimFilePath);
        }

        private readonly float _scale;
        private readonly string _gltfFilePath;
        private readonly ModelRoot _gltfModelRoot;
        private readonly DisplayUnit _defaultDisplayUnit;
        private readonly Category _defaultCategory;

        /// <summary>
        /// Private constructor. Please use the static function ConvertGltfToVim to convert a GLTF file into a VIM file.
        /// </summary>
        private GltfToVimStore(string gltfFilePath, float scale = MetersToFeet)
        {
            _gltfFilePath = gltfFilePath;
            _scale = scale;
            _gltfModelRoot = ModelRoot.Load(gltfFilePath);
            _defaultDisplayUnit = StoreDefaultDisplayUnit();
            _defaultCategory = StoreDefaultCategory();
        }

        /// <summary>
        /// Visits the GLTF hierarchy and aggregates the data as VIM objects.
        /// </summary>
        private void VisitGltf()
        {
            var rootBimDocument = StoreGltfModel(_gltfFilePath, _gltfModelRoot, _defaultCategory);
            var defaultFamily = StoreDefaultFamily(rootBimDocument, _defaultCategory);
            var defaultFamilyType = StoreDefaultFamilyType(rootBimDocument, _defaultCategory, defaultFamily);

            var scene = _gltfModelRoot.DefaultScene;
            StoreGltfScene(scene, rootBimDocument, _defaultCategory);

            foreach (var node in scene.VisualChildren)
            {
                StoreGltfNodeTree(node, rootBimDocument, _defaultCategory, defaultFamilyType);
            }
        }

        /// <summary>
        /// Stores a default empty display unit entity.
        /// </summary>
        private DisplayUnit StoreDefaultDisplayUnit()
            => ObjectModelBuilder.Add(DisplayUnit.Empty);

        /// <summary>
        /// Since GLTF does not contain Category information, we will store a single default Category
        /// entity which will be applied to all elements.
        /// </summary>
        private Category StoreDefaultCategory()
            => ObjectModelBuilder.Add(new Category
            {
                Id = VimConstants.SyntheticCategoryId,
                BuiltInCategory = "GLTF_CATEGORY",
                Name = "GLTF Object",
                CategoryType = "GLTF",
            });

        /// <summary>
        /// Since GLTF does not contain Family information, we will store a single default Family
        /// entity which will be applied to the default FamilyType entity.
        /// </summary>
        private Family StoreDefaultFamily(
            BimDocument bimDocument,
            Category category)
        {
            var familyElement = ObjectModelBuilder.Add(new Element
            {
                Id = VimConstants.SyntheticElementId,
                Name = "GLTF Family",
                _BimDocument = { Index = bimDocument.Index },
                _Category = { Index = category.Index }
            }); 

            return ObjectModelBuilder.Add(new Family
            {
                _Element = { Index = familyElement.Index },
            });
        }

        /// <summary>
        /// Since GLTF does not contain FamilyType information, we will store a single default FamilyType
        /// entity which will be applied to the FamilyInstance entities created thereafter.
        /// </summary>
        private FamilyType StoreDefaultFamilyType(
            BimDocument bimDocument,
            Category category,
            Family family)
        {
            var familyTypeElement = ObjectModelBuilder.Add(new Element
            {
                Id = VimConstants.SyntheticElementId,
                Name = "GLTF Family Type",
                _BimDocument = { Index = bimDocument.Index },
                _Category = { Index = category.Index }
            });

            return ObjectModelBuilder.Add(new FamilyType
            {
                _Element = { Index = familyTypeElement.Index },
                _Family = { Index = family.Index }
            });
        }

        /// <summary>
        /// Stores the root GLTF model as a BimDocument entity.
        /// </summary>
        private BimDocument StoreGltfModel(string gltfFilePath, SharpGLTF.Schema2.ModelRoot gltfModel, Category category)
        {
            // Create a BIM document representing the GLTF model.
            var bimDocument = ObjectModelBuilder.Add(new BimDocument
            {
                Title = Path.GetFileNameWithoutExtension(gltfFilePath),
                Name = Path.GetFileName(gltfFilePath),
                PathName = gltfFilePath,
            });

            // Create an element to hold the BIM document's parameters.
            var elementEntity = ObjectModelBuilder.Add(bimDocument.CreateParameterHolderElement());
            elementEntity._Category.Index = category.Index;
            elementEntity._BimDocument.Index = bimDocument.Index;
            bimDocument._Element.Index = elementEntity.Index;

            // Store BIM document parameters as strings representing some of the gltfModel's properties.
            StoreParameter(elementEntity, nameof(gltfModel.ExtensionsUsed), string.Join(",", gltfModel.ExtensionsUsed));
            StoreParameter(elementEntity, nameof(gltfModel.ExtensionsRequired), string.Join(",", gltfModel.ExtensionsRequired));
            StoreParameter(elementEntity, nameof(gltfModel.MeshQuantizationAllowed), gltfModel.MeshQuantizationAllowed.ToString());

            return bimDocument;
        }

        /// <summary>
        /// Store the parameter values associated with the element.
        /// </summary>
        private void StoreParameter(Element elementEntity, string name, string value)
        {
            // Get (or add) the cached parameter descriptor associated with the name.
            var parameterDescriptor = ObjectModelBuilder.GetOrAdd(name, _ => new ParameterDescriptor()
            {
                Name = name,
                _DisplayUnit = { Index = _defaultDisplayUnit.Index },
            }).Entity;

            // Store the parameter value. We use AddUntracked because it doesn't waste time doing a cache lookup.
            ObjectModelBuilder.AddUntracked(new Parameter
            {
                Values = (value, value),
                _ParameterDescriptor = { Index = parameterDescriptor.Index },
                _Element = { Index = elementEntity.Index }
            });
        }

        /// <summary>
        /// Stores a GLTF scene as a VIM Site entity.
        /// </summary>
        private void StoreGltfScene(
            SharpGLTF.Schema2.Scene gltfScene,
            BimDocument bimDocument,
            Category category)
        {
            // For illustrative purposes, we'll convert a GLTF scene into a Site entity.
            var siteElement = ObjectModelBuilder.Add(new Element()
            {
                Id = gltfScene.LogicalIndex,
                Name = gltfScene.Name,
                _BimDocument = { Index = bimDocument.Index },
                _Category = { Index = category.Index },
            });

            ObjectModelBuilder.Add(new Site
            {
                _Element = { Index = siteElement.Index }
            });
        }

        /// <summary>
        /// Stores the GLTF node tree as a collection of VIM Family Instance entities.
        /// </summary>
        private void StoreGltfNodeTree(
            SharpGLTF.Schema2.Node gltfNode,
            BimDocument bimDocument,
            Category category,
            FamilyType familyType)
        {
            StoreGltfNode(gltfNode, bimDocument, category, familyType);

            foreach (var node in gltfNode.VisualChildren)
            {
                StoreGltfNodeTree(node, bimDocument, category, familyType);
            }
        }

        /// <summary>
        /// Stores a GLTF node as a VIM Family Instance entity and returns its world-space transform.
        /// </summary>
        private void StoreGltfNode(
            SharpGLTF.Schema2.Node gltfNode,
            BimDocument bimDocument,
            Category category,
            FamilyType familyType)
        {
            // Create the element associated to the node.
            var familyInstanceElement = ObjectModelBuilder.Add(new Element
            {
                Id = gltfNode.LogicalIndex,
                Name = gltfNode.Name,
                _BimDocument = { Index = bimDocument.Index },
                _Category = { Index = category.Index },
            });

            // Store some sample parameters related to the GLTF node.
            StoreParameter(familyInstanceElement, nameof(gltfNode.IsSkinJoint), gltfNode.IsSkinJoint.ToString());
            StoreParameter(familyInstanceElement, nameof(gltfNode.IsSkinSkeleton), gltfNode.IsSkinSkeleton.ToString());
            StoreParameter(familyInstanceElement, nameof(gltfNode.IsTransformAnimated), gltfNode.IsTransformAnimated.ToString());

            // Create a family instance pointing to the element and family type.
            ObjectModelBuilder.Add(new FamilyInstance
            {
                _Element = { Index = familyInstanceElement.Index },
                _FamilyType = { Index = familyType.Index }
            });

            // Store the GLTF node's mesh as a VIM mesh.
            var gltfMesh = gltfNode.Mesh;
            var vimMeshIndex = gltfMesh == null
                ? EntityRelation.None
                : StoreGltfMesh(gltfNode.Mesh, bimDocument, category);

            // Store the GLTF node's transform as a VIM instance
            var vimInstance = new DocumentBuilder.Instance
            {
                Transform = ConvertToVimMatrix(gltfNode.WorldMatrix, _scale),
                MeshIndex = vimMeshIndex,
            };
            Instances.Add(vimInstance);

            // Store the node associated to the geometric instance.
            // This ordered 1:1 relationship connects the entities with the geometric instances.
            ObjectModelBuilder.Add(new Vim.Format.ObjectModel.Node
            {
                _Element = { Index = familyInstanceElement.Index },
            });
        }

        // GLTF TO VIM AXIS SWAP
        private static readonly Matrix4x4 AxisSwap = new Matrix4x4(
            m11: -1, m12: 0, m13: 0, m14: 0,
            m21: 0, m22: 0, m23: 1, m24: 0,
            m31: 0, m32: 1, m33: 0, m34: 0,
            m41: 0, m42: 0, m43: 0, m44: 1);

        private static Matrix4x4 ConvertToVimMatrix(System.Numerics.Matrix4x4 a, float scale)
        {
            var converted = new Matrix4x4(
                m11: a.M11, m12: a.M12, m13: a.M13, m14: a.M14,
                m21: a.M21, m22: a.M22, m23: a.M23, m24: a.M24,
                m31: a.M31, m32: a.M32, m33: a.M33, m34: a.M34,
                m41: a.M41, m42: a.M42, m43: a.M43, m44: a.M44) * AxisSwap * Matrix4x4.CreateScale(scale);

            converted.M14 = 0;
            converted.M24 = 0;
            converted.M34 = 0;
            converted.M44 = 1;

            return converted;
        }

        /// <summary>
        /// Stores the GLTF mesh and its associated materials.
        /// </summary>
        private int StoreGltfMesh(
            SharpGLTF.Schema2.Mesh gltfMesh,
            BimDocument bimDocument,
            Category category)
        {
            var vertices = new List<Vector3>();
            var indices = new List<int>();
            var faceMaterials = new List<int>();

            foreach (var gltfPrimitive in gltfMesh.Primitives)
            {
                // Collect the vertices.
                var primitiveVertices = gltfPrimitive
                    .GetVertexAccessor("POSITION")
                    ?.AsVector3Array()
                    ?.Select(v => new Vector3(v.X, v.Y, v.Z))
                    .ToArray();

                if (primitiveVertices == null || primitiveVertices.Length == 0)
                    continue;

                // Collect the indices.
                var primitiveIndices = gltfPrimitive.GetIndices()?.Select(i => (int) i).ToArray();
                if (primitiveIndices == null)
                {
                    // "Indices may be null if the primitive does not used indexed drawing. In that case, the vertices are drawn sequentially"
                    primitiveIndices = new int[primitiveVertices.Length];
                    for (var i = 0; i < primitiveIndices.Length; ++i)
                        primitiveIndices[i] = i;
                }

                var indexOffset = indices.Count;
                for (var i = 0; i < primitiveIndices.Length; ++i)
                {
                    // Add an index offset as we accumulate vertices.
                    primitiveIndices[i] += indexOffset;
                }
                
                // Collect the material applied to the primitive and apply it as face materials.
                var materialIndex = StoreGltfMaterial(gltfPrimitive.Material, bimDocument, category);
                var primitiveFaceMaterials = new int[primitiveIndices.Length / 3]; // division by 3 because we're dealing with a tri-mesh.
                for (var i = 0; i < primitiveFaceMaterials.Length; ++i)
                    primitiveFaceMaterials[i] = materialIndex;

                vertices.AddRange(primitiveVertices);
                indices.AddRange(primitiveIndices);
                faceMaterials.AddRange(primitiveFaceMaterials);
            }

            var meshIndex = Meshes.Count;
            var vimMesh = new DocumentBuilder.Mesh(
                vertices,
                indices,
                faceMaterials
            );
            Meshes.Add(vimMesh);

            return meshIndex;
        }

        /// <summary>
        /// Store the GLTF material. Note: many GLTF models are styled using textures, however these textures are not stored in the VIM file in this example.
        /// </summary>
        private int StoreGltfMaterial(
            SharpGLTF.Schema2.Material gltfMaterial,
            BimDocument bimDocument,
            Category category)
        {
            var getOrAddResult = ObjectModelBuilder.GetOrAdd(
                gltfMaterial.LogicalIndex,
            _ =>
                {
                    var maybeChannel = gltfMaterial.FindChannel("BaseColor") ?? gltfMaterial.FindChannel("Diffuse");
                    if (maybeChannel == null)
                        return new Format.ObjectModel.Material { Color_X = 0.5d, Color_Y = 0.5d, Color_Z = 0.5d }; // default material

                    var color = maybeChannel.Value.Color;

                    return new Format.ObjectModel.Material { Color_X = color.X, Color_Y = color.Y, Color_Z = color.Z, Transparency = 1 - color.W };
                });

            var material = getOrAddResult.Entity;
            var materialIndex = material.Index;
            
            if (!getOrAddResult.Added)
                return materialIndex; // If the GetOrAdd operation already contained the material, then return early.

            // The material entity was just added so apply an Element to it.
            var materialElement = ObjectModelBuilder.Add(new Element()
            {
                Id = VimConstants.SyntheticElementId,
                Name = gltfMaterial.Name,
                _BimDocument = { Index = bimDocument.Index },
                _Category = { Index = category.Index },
            });

            // Connect the material entity to the element we created.
            material._Element.Index = materialElement.Index;

            // Store any relevant parameters about the material.
            StoreParameter(materialElement, nameof(gltfMaterial.Alpha), $"{gltfMaterial.Alpha:G}");
            StoreParameter(materialElement, nameof(gltfMaterial.AlphaCutoff), gltfMaterial.AlphaCutoff.ToString(CultureInfo.InvariantCulture));
            StoreParameter(materialElement, nameof(gltfMaterial.DoubleSided), gltfMaterial.DoubleSided.ToString());
            StoreParameter(materialElement, nameof(gltfMaterial.IndexOfRefraction), gltfMaterial.IndexOfRefraction.ToString(CultureInfo.InvariantCulture));
            StoreParameter(materialElement, nameof(gltfMaterial.Unlit), gltfMaterial.Unlit.ToString());

            return materialIndex;
        }

        private void WriteVim(string vimFilePath)
        {
            var vimDocumentBuilder = ToDocumentBuilder(GeneratorString, VersionString);
            vimDocumentBuilder.Write(vimFilePath);
        }
    }
}
