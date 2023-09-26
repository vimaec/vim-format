using System;
using System.IO;
using System.Linq;
using Vim.Math3d;
using Vim.G3dNext.Attributes;
using Vim.G3dNext;

namespace Vim.Format.Vimx
{
    public class G3dVim
    {
        public G3DNext<VimAttributeCollection> source;

        // instances
        public int[] instanceMeshes;
        public Matrix4x4[] instanceTransforms;
        public int[] instanceParents;
        public ushort[] instanceFlags;

        //mesh
        public int[] meshSubmeshOffsets;

        //submesh
        public int[] submeshIndexOffsets;
        public int[] submeshMaterials;

        public int[] indices;
        public Vector3[] vertices;

        // materials
        public Vector4[] materialColors;
        public float[] materialGlossiness;
        public float[] materialSmoothness;

        // shapes
        public Vector3[] shapeVertices;
        public int[] shapeVertexOffsets;
        public Vector4[] shapeColors;
        public float[] shapeWidth;

        // Computed fields
        public int[] meshVertexOffsets;


        public G3dVim()
        {
            this.source = new G3DNext<VimAttributeCollection>();
        }
        public G3dVim(G3DNext<VimAttributeCollection> g3d)
        {
            this.source = g3d;

            // instances
            instanceMeshes = g3d.AttributeCollection.InstanceMeshAttribute.TypedData;
            instanceTransforms = g3d.AttributeCollection.InstanceTransformAttribute.TypedData;
            instanceParents = g3d.AttributeCollection.InstanceParentAttribute.TypedData;
            instanceFlags = g3d.AttributeCollection.InstanceFlagsAttribute.TypedData;
            if (instanceFlags.Length < instanceMeshes.Length)
            {
                instanceFlags = new ushort[instanceMeshes.Length];
            }

            // meshes
            meshSubmeshOffsets = g3d.AttributeCollection.MeshSubmeshOffsetAttribute.TypedData;

            // submeshes
            submeshIndexOffsets = g3d.AttributeCollection.SubmeshIndexOffsetAttribute.TypedData;
            submeshMaterials = g3d.AttributeCollection.SubmeshMaterialAttribute.TypedData;

            // vertices
            indices = g3d.AttributeCollection.IndexAttribute.TypedData;
            vertices = g3d.AttributeCollection.VertexAttribute.TypedData;

            // materials
            materialColors = g3d.AttributeCollection.MaterialColorAttribute.TypedData;
            materialGlossiness = g3d.AttributeCollection.MaterialGlossinessAttribute.TypedData;
            materialSmoothness = g3d.AttributeCollection.MaterialSmoothnessAttribute.TypedData;

            // shapes
            shapeVertexOffsets = g3d.AttributeCollection.ShapeVertexOffsetAttribute.TypedData;
            shapeVertices = g3d.AttributeCollection.ShapeVertexAttribute.TypedData;
            shapeColors = g3d.AttributeCollection.ShapeColorAttribute.TypedData;
            shapeWidth = g3d.AttributeCollection.ShapeWidthAttribute.TypedData;

            // computed fields
            meshVertexOffsets = ComputeMeshVertexOffsets();
        }

        public bool Equals(G3dVim other)
        {
            // instances
            return
            instanceMeshes.SequenceEqual(other.instanceMeshes) &&
            instanceTransforms.SequenceEqual(other.instanceTransforms) &&
            instanceParents.SequenceEqual(other.instanceParents) &&
            instanceFlags.SequenceEqual(other.instanceFlags) &&

            //mesh
            meshSubmeshOffsets.SequenceEqual(other.meshSubmeshOffsets) &&

            //submesh
            submeshIndexOffsets.SequenceEqual(other.submeshIndexOffsets) &&
            submeshMaterials.SequenceEqual(other.submeshMaterials) &&

            indices.SequenceEqual(other.indices) &&
            vertices.SequenceEqual(other.vertices) &&

            // materials
            materialColors.SequenceEqual(other.materialColors) &&
            materialGlossiness.SequenceEqual(other.materialGlossiness) &&
            materialSmoothness.SequenceEqual(other.materialSmoothness) &&

            // shapes
            shapeVertices.SequenceEqual(other.shapeVertices) &&
            shapeVertexOffsets.SequenceEqual(other.shapeVertexOffsets) &&
            shapeColors.SequenceEqual(shapeColors) &&
            shapeWidth.SequenceEqual(other.shapeWidth);
        }

        public G3dMaterials ToG3dMaterials()
        {
            return G3dMaterials.FromArrays(
                materialColors,
                materialGlossiness,
                materialSmoothness
            );
        }

         public static G3dVim FromBFast2(BFastNext.BFastNext bfast)
        {
            var next = G3DNext<VimAttributeCollection>.ReadBFast(bfast);
            return new G3dVim(next);
        }

        public static G3dVim FromPath2(string path)
        {
            var next = G3DNext<VimAttributeCollection>.ReadBFast(path);
            return new G3dVim(next);
        }

        public static G3dVim ReadFromVim(string vimPath)
        {
            var file = new FileStream(vimPath, FileMode.Open);
            var bfast = new BFastNext.BFastNext(file);
            var geometry = bfast.GetBFast("geometry");
            return FromBFast(geometry);
        }
        
        public static G3dVim FromBFast(BFastNext.BFastNext bfast)
        {
            var g3d = G3DNext<VimAttributeCollection>.ReadBFast(bfast);
            return new G3dVim(g3d);
        }

        private int[] ComputeMeshVertexOffsets()
        {
            var result = new int[GetMeshCount()];
            for (var m = 0; m < result.Length; m++)
            {
                var min = int.MaxValue;
                var start = GetMeshIndexStart(m);
                var end = GetMeshIndexEnd(m);
                for (var i = start; i < end; i++)
                {
                    min = Math.Min(min, indices[i]);
                }
                result[m] = min;
            }
            return result;
        }

        public int GetTriangleCount()
        {
            return GetIndexCount() / 3;
        }

        /// <summary>
        /// The total number of instances.
        /// </summary>
        public int GetInstanceCount() => instanceTransforms?.Length ?? 0;

        #region meshes
        /// <summary>
        /// The total number of meshes.
        /// </summary>
        public int GetMeshCount() => meshSubmeshOffsets?.Length ?? 0;

        public int GetMeshIndexStart(int mesh)
        {
            var submesh = GetMeshSubmeshStart(mesh);
            return GetSubmeshIndexStart(submesh);
        }

        public int GetMeshIndexEnd(int mesh)
        {
            var submesh = GetMeshSubmeshEnd(mesh) - 1;
            return GetSubmeshIndexEnd(submesh);
        }

        public int GetMeshIndexCount(int mesh)
        {
            return GetMeshIndexEnd(mesh) - GetMeshIndexStart(mesh);
        }

        public int GetMeshVertexStart(int mesh)
        {
            return meshVertexOffsets[mesh];
        }

        public int GetMeshVertexEnd(int mesh)
        {
            return mesh + 1 < GetMeshCount() ? meshVertexOffsets[mesh + 1] : vertices.Length;
        }

        public int GetMeshVertexCount(int mesh)
        {
            return GetMeshVertexEnd(mesh) - GetMeshVertexStart(mesh);
        }

        public int GetMeshSubmeshStart(int mesh)
        {
            return meshSubmeshOffsets[mesh];
        }

        public int GetMeshSubmeshEnd(int mesh)
        {
            return mesh + 1 < GetMeshCount()
                ? meshSubmeshOffsets[mesh + 1]
                : GetSubmeshCount();
        }

        public int GetMeshSubmeshCount(int mesh)
        {
            return GetMeshSubmeshEnd(mesh) - GetMeshSubmeshStart(mesh);
        }

        #endregion

        #region submesh

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount() => submeshIndexOffsets?.Length ?? 0;

        public int GetSubmeshIndexStart(int submesh)
        {
            return submeshIndexOffsets[submesh];
        }

        public int GetSubmeshIndexEnd(int submesh)
        {
            return submesh + 1 < GetSubmeshCount() ? submeshIndexOffsets[submesh + 1] : GetIndexCount();
        }

        public int GetSubmeshIndexCount(int submesh)
        {
            return GetSubmeshIndexEnd(submesh) - GetSubmeshIndexStart(submesh);
        }

        #endregion

        /// <summary>
        /// The total number of indices.
        /// </summary>
        public int GetIndexCount() => indices?.Length ?? 0;

        /// <summary>
        /// The total number of vertices.
        /// </summary>
        public int GetVertexCount() => vertices?.Length ?? 0;

        /// <summary>
        /// The total number of materials.
        /// </summary>
        public int GetMaterialCount() => materialColors?.Length ?? 0;

        /// <summary>
        /// The total number of shapes.
        /// </summary>
        public int GetShapeCount() => shapeVertexOffsets?.Length ?? 0;

        /// <summary>
        /// The total number of shape vertices.
        /// </summary>
        public int GetShapeVertexCount() => shapeVertices?.Length ?? 0;
    }
}
