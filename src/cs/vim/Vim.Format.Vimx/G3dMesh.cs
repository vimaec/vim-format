using System.Collections.Generic;
using System.IO;
using Vim.Math3d;
using Vim.G3dNext.Attributes;
using Vim.G3dNext;

namespace Vim.Format.Vimx
{
    public enum MeshSection
    {
        Opaque,
        Transparent
    }

    public class G3dMesh
    {
        public const int POSITION_SIZE = 3;

        public G3DNext<MeshAttributeCollection> source;

        // instances
        public int[] instanceNodes; // This field does not get written on purpose.
        public Matrix4x4[] instanceTransforms;

        //mesh
        public int[] opaqueSubmeshCount;

        //submesh
        public int[] submeshIndexOffsets;
        public int[] submeshVertexOffsets;
        public int[] submeshMaterials;

        public int[] indices;
        public Vector3[] positions;

        // materials
        public Vector4[] materialColors;
        public float[] materialGlossiness;
        public float[] materialSmoothness;

        public static G3dMesh FromArrays(
            int[] instanceNode,
            Matrix4x4[] instanceTransforms,
            int meshOpaqueCount,
            int[] submeshIndexOffsets,
            int[] submeshVertexOffsets,
            int[] submeshMaterials,
            int[] indices,
            Vector3[] vertices)
        {
            var collection = new MeshAttributeCollection();
            
            // instances
            collection.MeshInstanceTransformAttribute.TypedData = instanceTransforms ?? new Matrix4x4[0];

            //mesh
            collection.MeshOpaqueSubmeshCountAttribute.TypedData = new int[] { meshOpaqueCount };

            // submeshes
            collection.MeshSubmeshIndexOffsetAttribute.TypedData = submeshIndexOffsets ?? new int[0];
            collection.MeshSubmeshVertexOffsetAttribute.TypedData = submeshVertexOffsets ?? new int[0];
            collection.MeshSubmeshMaterialAttribute.TypedData = submeshMaterials ?? new int[0];
            
            // vertices
            collection.MeshIndexAttribute.TypedData = indices ?? new int[0];
            collection.MeshVertexAttribute.TypedData = vertices ?? new Vector3[0];
            
            var g3d = new G3DNext<MeshAttributeCollection>(collection);
            var mesh = new G3dMesh(g3d);
            mesh.instanceNodes = instanceNode;
            return mesh;
        }

        public bool Equals(G3dVim other)
        {
            // instances
            return
            MemberwiseEquals(instanceNodes, other.instanceMeshes) &&
            MemberwiseEquals(instanceTransforms, other.instanceTransforms) &&

            //submesh
            MemberwiseEquals(submeshIndexOffsets, other.submeshIndexOffsets) &&
            MemberwiseEquals(submeshMaterials, other.submeshMaterials) &&

            MemberwiseEquals(indices, other.indices) &&
            MemberwiseEquals(positions, other.vertices) &&

            // materials
            MemberwiseEquals(materialColors, other.materialColors) &&
            MemberwiseEquals(materialGlossiness, other.materialGlossiness) &&
            MemberwiseEquals(materialSmoothness, other.materialSmoothness);

        }

        public static bool MemberwiseEquals<T>(T[] array1, T[] array2)
        {
            if (array1 == null || array2 == null) return array1 == array2;

            if (array1.Length != array2.Length) return false;

            for (var i = 0; i < array1.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(array1[i], array2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public G3dMesh(G3DNext<MeshAttributeCollection> g3d)
        {
            this.source = g3d;

            // instances
            instanceTransforms = g3d.AttributeCollection.MeshInstanceTransformAttribute.TypedData;

            // submeshes
            opaqueSubmeshCount = g3d.AttributeCollection.MeshOpaqueSubmeshCountAttribute.TypedData;
            submeshIndexOffsets = g3d.AttributeCollection.MeshSubmeshIndexOffsetAttribute.TypedData;
            submeshVertexOffsets  = g3d.AttributeCollection.MeshSubmeshVertexOffsetAttribute.TypedData; 
            submeshMaterials = g3d.AttributeCollection.MeshSubmeshMaterialAttribute.TypedData;

            // vertices
            indices = g3d.AttributeCollection.MeshIndexAttribute.TypedData;
            positions = g3d.AttributeCollection.MeshVertexAttribute.TypedData;
        }

        public static G3dMesh FromBFast(BFastNext.BFastNext bfast)
        {
            var g3d = G3DNext<MeshAttributeCollection>.ReadBFast(bfast);
            return new G3dMesh(g3d);
        }

        public int GetTriangleCount()
        {
            return GetIndexCount() / 3;
        }

        /// <summary>
        /// The total number of instances.
        /// </summary>
        public int GetInstanceCount() => instanceTransforms?.Length ?? 0;

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount() => submeshIndexOffsets?.Length ?? 0;

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount(MeshSection section)
        {
            var count = GetSubmeshCount();
            if (opaqueSubmeshCount == null) return count;
            var opaque = opaqueSubmeshCount[0];

            return section == MeshSection.Opaque
                ? opaque
                : count - opaque;
        }

        public int GetIndexStart(MeshSection section)
        {
            if (opaqueSubmeshCount == null) return 0;
            if (section == MeshSection.Opaque) return 0;
            var opaque = opaqueSubmeshCount[0];
            return GetSubmeshIndexStart(opaque);
        }

        public int GetIndexEnd(MeshSection section)
        {
            if (opaqueSubmeshCount == null) return indices.Length;
            if (section == MeshSection.Transparent) return indices.Length;
            var opaque = opaqueSubmeshCount[0];
            return GetSubmeshIndexEnd(opaque - 1);
        }

        public int GetIndexCount(MeshSection section)
        {
            return GetIndexEnd(section) - GetIndexStart(section);
        }

        /// <summary>
        /// The total number of indices.
        /// </summary>
        public int GetIndexCount() => indices?.Length ?? 0;

        public int GetVertexStart(MeshSection section)
        {
            if (opaqueSubmeshCount == null) return 0;
            if (submeshVertexOffsets == null) return 0;
            if (section == MeshSection.Opaque) return 0;
            var opaque = opaqueSubmeshCount[0];
            return GetSubmeshVertexStart(opaque);
        }

        public int GetVertexEnd(MeshSection section)
        {
            if (opaqueSubmeshCount == null) return positions.Length;
            if (submeshVertexOffsets == null) return positions.Length;
            if (section == MeshSection.Transparent) return positions.Length;
            var opaque = opaqueSubmeshCount[0];
            return GetSubmeshVertexEnd(opaque - 1);
        }

        public int GetVertexCount(MeshSection section)
        {
            return GetVertexEnd(section) - GetVertexStart(section);
        }

        /// <summary>
        /// The total number of vertices.
        /// </summary>
        public int GetVertexCount() => (positions?.Length ?? 0);


        /// <summary>
        /// The total number of materials.
        /// </summary>
        public int GetMaterialCount() => materialColors?.Length ?? 0;

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

        public int GetSubmeshVertexStart(int submesh)
        {
            return submeshVertexOffsets[submesh];
        }

        public int GetSubmeshVertexEnd(int submesh)
        {
            return submesh + 1 < GetSubmeshCount() ? submeshVertexOffsets[submesh + 1] : GetVertexCount();
        }

        public int GetSubmeshVertexCount(int submesh)
        {
            return GetSubmeshVertexEnd(submesh) - GetSubmeshVertexStart(submesh);
        }
    }
}
