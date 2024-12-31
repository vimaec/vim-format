using System.IO;
using System.Linq;
using Vim.BFastLib;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format
{
    public class VimInstance : IVimInstance
    {
        public int Index => Node.Index;
        public InstanceFlags InstanceFlags { get; }
        public Matrix4x4 WorldTransform { get; }
        public IVimMesh Mesh { get; }
        public Node Node { get; }
        public AABox BoundingBox { get; }

        public VimInstance(G3dVim g3d, Node node)
        {
            InstanceFlags = (InstanceFlags) g3d.InstanceFlags.ElementAtOrDefault(node.Index);
            WorldTransform = node.Index >= 0 ? g3d.InstanceTransforms[node.Index] : Matrix4x4.Identity;
            Mesh = VimMesh2.FromG3d(g3d, g3d.InstanceMeshes[node.Index]);
            Node = node;
            BoundingBox = AABox.Create(Mesh.Submeshes.SelectMany(s => s.Vertices));
        }
    }

    public class VimModel : IVimModel
    {
        public static IVimModel Load(
            Stream stream,
            LoadOptions loadOptions = null)
        {
            var doc = SerializableDocument.FromBFast(new BFast(stream), loadOptions);
            return new VimModel(doc);
        }

        public static IVimModel Load(
            string filePath,
            LoadOptions loadOptions = null)
        {
            using (var stream = File.OpenRead(filePath))
            {
                return Load(stream, loadOptions);
            }
        }

        public SerializableVersion SchemaVersion => SerializableDocument.Header.FileFormatVersion;
        public SerializableHeader Header => SerializableDocument.Header;
        public SerializableDocument SerializableDocument { get; }
        public string FilePath => SerializableDocument.FileName;
        public string[] StringBuffer => SerializableDocument.StringTable;
        public EntityTableSet Entities { get; }
        public IVimInstance[] Instances { get; }
        public AABox BoundingBox { get; }

        private VimModel(SerializableDocument serializableDocument)
        {
            SerializableDocument = serializableDocument;
            Entities = new EntityTableSet(SerializableDocument.EntityTables.ToArray(), StringBuffer);
            Instances = CreateVimSceneNodes(SerializableDocument.GeometryNext);
            BoundingBox = Instances
                .Select(i => i.BoundingBox)
                .Aggregate(Instances[0].BoundingBox, (b1, b2) => b1.Merge(b2));
        }

        private IVimInstance[] CreateVimSceneNodes(G3dVim g3d)
        {
            return g3d.InstanceTransforms.Select((_, i) =>
                new VimInstance(g3d, Entities.GetNode(i))).ToArray<IVimInstance>();
        }

        public IVimModel Merge(IVimModel other)
        {
            return MergeService2.MergeVimModels(new[] { this, other });
        }

        public void Write(Stream stream)
        {
            SerializableDocument.ToBFast().Write(stream);
        }

        public void Write(string filePath)
        {
            using (var stream = File.Create(filePath))
            {
                Write(stream);
            }
        }
    }
}
