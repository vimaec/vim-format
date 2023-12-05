namespace Vim.G3dNext.Attributes
{
    public partial class G3dScene
    {
        public int GetChunksCount() => ChunkCount[0];
        public int GetInstanceCount() => InstanceMeshes.Length;
        void ISetup.Setup()
        {
            // empty
        }
    }
}
