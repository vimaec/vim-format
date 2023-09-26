using System.IO;
using Vim.G3dNext.Attributes;
using Vim.Math3d;
using Vim.G3dNext;
using Vim.BFast;
/*
namespace Vim.Format.Vimx
{
    public class G3dScene
    {
        public G3DNext<SceneAttributeCollection> source;

        // instances
        public int[] instanceFiles;
        public int[] instanceIndices;
        public int[] instanceNodes;
        public int[] instanceGroups;
        public long[] instanceTags;
        public ushort[] instanceFlags;
        public Vector3[] instanceMins;
        public Vector3[] instanceMaxs;

        //mesh
        public int[] meshInstanceCounts;
        public int[] meshVertexCounts;
        public int[] meshIndexCounts;

        // mesh opaque
        public int[] meshOpaqueVertexCounts;
        public int[] meshOpaqueIndexCounts;

        public G3dScene()
        {
            this.source = new G3DNext<SceneAttributeCollection>();
        }

        public G3dScene(G3DNext<SceneAttributeCollection> g3d)
        {
            this.source = g3d;

            // instances
            instanceFiles = g3d.AttributeCollection.InstanceFilesAttribute.TypedData;
            instanceIndices = g3d.AttributeCollection.InstanceIndicesAttribute.TypedData;
            instanceNodes = g3d.AttributeCollection.InstanceNodesAttribute.TypedData;
            instanceGroups = g3d.AttributeCollection.InstanceGroupsAttribute.TypedData;
            instanceTags = g3d.AttributeCollection.InstanceTagsAttribute.TypedData;
            instanceFlags = g3d.AttributeCollection.InstanceFlagsAttribute.TypedData;
            instanceMins = g3d.AttributeCollection.InstanceMinsAttribute.TypedData;
            instanceMaxs = g3d.AttributeCollection.InstanceMaxsAttribute.TypedData;

            // meshes
            meshInstanceCounts = g3d.AttributeCollection.MeshInstanceCountsAttribute.TypedData;
            meshIndexCounts = g3d.AttributeCollection.MeshIndexCountsAttribute.TypedData;
            meshVertexCounts = g3d.AttributeCollection.MeshVertexCountsAttribute.TypedData;

            // meshes opaque
            meshOpaqueIndexCounts = g3d.AttributeCollection.MeshOpaqueIndexCountsAttribute.TypedData;
            meshOpaqueVertexCounts = g3d.AttributeCollection.MeshOpaqueVertexCountsAttribute.TypedData;
        }

        public void Write(string path, string name)
        {
            var p = Path.Combine(path, $"{name}_index.g3d");
            source.ToBFast().Write(p);
        }

        public int MeshCount => this.meshInstanceCounts.Length;
    }
}
*/