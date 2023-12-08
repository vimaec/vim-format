using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vim.Format;
using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.Util;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;
using IVimSceneProgress = System.IProgress<(string, double)>;
using Vim.BFastNS;

namespace Vim
{
    // TODO: add property cache lookup

    /// <summary>
    /// This is the top-level class of a loaded VIM file.
    /// </summary>
    public class VimScene : IScene
    {
        /// <summary>
        /// Returns the VIM file's header schema version. Returns null if the header schema is not found.
        /// </summary>
        public static string GetSchemaVersion(string path)
            => string.IsNullOrEmpty(path) ? null : GetHeader(path)?.Schema?.ToString();

        public static SerializableHeader GetHeader(string path)
        {
            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                return GetHeader(file);
            }
        }

        public static SerializableHeader GetHeader(Stream stream)
        {
            var bfast = new BFastNS.BFast(stream);
            var bytes = bfast.GetArray<byte>(BufferNames.Header);
            if (bytes == null) return null;
            return SerializableHeader.FromBytes(bytes);
        }

        public static VimScene LoadVim(string f, LoadOptions loadOptions, IVimSceneProgress progress = null, bool inParallel = false, int vimIndex = 0)
            => new VimScene(SerializableDocument.FromPath(f, loadOptions), progress, inParallel, vimIndex);

        public static VimScene LoadVim(string f, IVimSceneProgress progress = null, bool skipGeometry = false, bool skipAssets = false, bool skipNodes = false, bool inParallel = false)
            => LoadVim(f, new LoadOptions { SkipGeometry = skipGeometry, SkipAssets = skipAssets}, progress, inParallel);

        public static VimScene LoadVim(Stream stream, LoadOptions loadOptions, IVimSceneProgress progress = null, bool inParallel = false)
            => new VimScene(SerializableDocument.FromBFast(new BFastNS.BFast(stream), loadOptions), progress, inParallel);

        public static VimScene LoadVim2(Stream stream, LoadOptions loadOptions = null, IVimSceneProgress progress = null, bool inParallel = false)
        {
            var bfast = new BFastNS.BFast(stream);
            var doc = SerializableDocument.FromBFast(bfast, loadOptions);
            return new VimScene(doc, progress, inParallel);
        }

        public static VimScene LoadVim(Stream stream, IVimSceneProgress progress = null, bool skipGeometry = false, bool skipAssets = false, bool skipNodes = false, bool inParallel = false)
            => LoadVim(stream, new LoadOptions { SkipGeometry = skipGeometry, SkipAssets = skipAssets}, progress, inParallel);

        public int VimIndex { get; set; }
        public IArray<IMesh> Meshes { get; private set; }
        public IArray<ISceneNode> Nodes { get; private set; }
        public IArray<VimSceneNode> VimNodes { get; private set; }
        public IArray<VimShape> VimShapes { get; private set; }
        public IArray<IMaterial> Materials { get; private set; }

        public SerializableDocument _SerializableDocument { get; }
        public Document Document { get; private set; }
        public DocumentModel DocumentModel { get; private set; }

        public string PersistingId
            => Document.Header.PersistingId;

        public Material GetMaterial(int materialIndex)
            => DocumentModel.MaterialList.ElementAtOrDefault(materialIndex);

        public Vector4 GetMaterialColor(int materialIndex)
            => _SerializableDocument.Geometry.MaterialColors[materialIndex];

        public static IMesh ToIMesh(G3dMesh g3d)
            => Primitives.TriMesh(
                g3d.Vertices.ToPositionAttribute(),
                g3d.Indices.ToIndexAttribute(),
                g3d.VertexUvs?.ToVertexUvAttribute(),
                g3d.SubmeshIndexOffsets?.ToSubmeshIndexOffsetAttribute(),
                g3d.SubmeshMaterials?.ToSubmeshMaterialAttribute(),
                g3d.MeshSubmeshOffset?.ToMeshSubmeshOffsetAttribute()
            );

        private VimScene(SerializableDocument doc)
            => _SerializableDocument = doc;

        public VimScene(SerializableDocument doc, IVimSceneProgress progress = null, bool inParallel = false, int vimIndex = 0) : this(doc)
        {
            VimIndex = vimIndex;
            progress?.Report(($"Creating scene from {doc.FileName}", 0.0));


            var actions = GetInitStepsWithProgress(inParallel, progress);

            if (inParallel)
            {
                Parallel.Invoke(actions);
            }
            else
            {
                foreach (var action in actions)
                    action();
            }

            progress?.Report(("Completed creating scene", 1.0));
        }

        private Action[] GetInitStepsWithProgress(bool inParallel, IVimSceneProgress progress = null)
        {
            var steps = GetInitSteps(inParallel);
            var total = steps.Sum(s => s.Effort);
            var cumulProgress = CumulativeProgressDecorator.Decorate(progress, total);
            var actions = steps.Select(s => new Action(() => s.Run(cumulProgress))).ToArray();
            return actions;
        }

        private IStep[] GetInitSteps(bool inParallel)
        {
            var createDocument = new Step(
                () => Document = _SerializableDocument.ToDocument(),
                "Creating Document"
            );

            var createModel = new Step(
                () => DocumentModel = new DocumentModel(Document, inParallel),
                "Creating Model"
            );

            //Requires model to be ready.
            var createScene = new Step(
                () => CreateScene(inParallel),
                "Creating Scene",
                3f
            );

            var createMeshes = new Step(
                () => CreateMeshes(inParallel),
                "Unpacking Meshes",
                3f
            );

            var createMaterials = new Step(
                () => CreateMaterials(inParallel),
                "Creating Materials",
                1f
            );

            var createShapes = new Step(
                () => CreateShapes(inParallel),
                "Creating Shapes"
            );

            if (inParallel)
            {
                var dataSequence = new StepSequence(
                    createDocument,
                    createModel,
                    createScene,
                    createShapes
                );

                return new IStep[]
                {
                    dataSequence,
                    createMeshes,
                    createMaterials
                };
            }
            else
            {
                return new IStep[]
                {
                    createDocument,
                    createModel,
                    createMeshes,
                    createScene,
                    createShapes,
                    createMaterials,
                };
            }
        }

        private void CreateMeshes(bool inParallel)
        {
            if (_SerializableDocument.Geometry == null)
            {
                return;
            }

            var srcGeo = _SerializableDocument.Geometry;
            var tmp = srcGeo?.Meshes.Select(ToIMesh);
            Meshes = (tmp == null)
                ? LinqArray.LinqArray.Empty<IMesh>()
                : inParallel 
                    ? tmp.EvaluateInParallel() 
                    : tmp.Evaluate();
        }

        private void CreateShapes(bool inParallel)
        {
            if (_SerializableDocument.Geometry == null)
            {
                return;
            }

            var r = _SerializableDocument.Geometry.Shapes.Select((s, i) => new VimShape(this, i));
            VimShapes = inParallel ? r.EvaluateInParallel() : r.Evaluate();
        }

        private void CreateScene(bool inParallel)
        {
            if (_SerializableDocument.Geometry == null)
            {
                return;
            }

            VimNodes = CreateVimSceneNodes(this, _SerializableDocument.Geometry, inParallel);
            Nodes = VimNodes.Select(n => n as ISceneNode);
        }

        private void CreateMaterials(bool inParallel)
        {
            if (_SerializableDocument.Geometry == null)
            {
                return;
            }

            var query = _SerializableDocument.Geometry.Materials.Select(m => new VimMaterial(m) as IMaterial);
            Materials = inParallel ? query.EvaluateInParallel() : query.Evaluate();
        }

        public static IArray<VimSceneNode> CreateVimSceneNodes(VimScene scene, G3D g3d, bool inParallel)
        {
            Matrix4x4 GetMatrix(int i) => i >= 0 ? g3d.InstanceTransforms[i] : Matrix4x4.Identity;
            
            var r = g3d.InstanceTransforms.Select((_, i) =>
                new VimSceneNode(scene, i, g3d.InstanceMeshes[i], GetMatrix(i)));

            return inParallel ? r.EvaluateInParallel() : r.Evaluate();
        }

        public void Save(string filePath)
            => _SerializableDocument.ToBFast().Write(filePath);

        public string FileName => _SerializableDocument.FileName;

        public void TransformSceneInPlace(Func<IMesh, IMesh> meshTransform = null, Func<VimSceneNode, VimSceneNode> nodeTransform = null)
        {
            if (meshTransform != null)
                Meshes = Meshes.Select(meshTransform).EvaluateInParallel();
            if (nodeTransform != null)
                VimNodes = VimNodes.Select(nodeTransform).EvaluateInParallel();
        }

        public string GetElementName(int elementIndex, string missing = "")
            => DocumentModel.GetElementName(elementIndex, missing);

        public string GetBimDocumentFileName(int index = 0, string missing = "")
        {
            var bimDocumentPathName = DocumentModel.GetBimDocumentPathName(index, null);
            return bimDocumentPathName == null
                ? missing
                : Path.GetFileName(bimDocumentPathName);
        }

        public BimDocument GetBimDocument(int index = 0)
            => DocumentModel.GetBimDocument(index);

        private class Step : IStep
        {
            private readonly Action _action;
            private readonly string _name;
            public float Effort { get; }

            public Step(Action action, string name, float effort = 1f)
            {
                _action = action;
                _name = name;
                Effort = effort;
            }

            public void Run(IVimSceneProgress progress)
            {
                progress?.Report((_name, Effort));
                _action();
            }
        }

        private class StepSequence : IStep
        {
            private readonly Step[] _steps;
            public float Effort { get; }

            public StepSequence(params Step[] steps)
            {
                _steps = steps;
                Effort = _steps.Sum(s => s.Effort);
            }

            public void Run(IVimSceneProgress progress)
            {
                foreach (var step in _steps)
                    step.Run(progress);
            }
        }

        public interface IStep
        {
            void Run(IVimSceneProgress progress);
            float Effort { get; }
        }

        private class CumulativeProgressDecorator : IVimSceneProgress
        {
            private readonly double _total;
            private double _current;

            private readonly IVimSceneProgress _progress;

            private CumulativeProgressDecorator(IVimSceneProgress progress, float total)
                => (_progress, _total) = (progress, total);

            public static CumulativeProgressDecorator Decorate(IVimSceneProgress logger, float total)
                => logger != null ? new CumulativeProgressDecorator(logger, total) : null;

            public void Report((string, double) value)
            {
                _current += value.Item2;
                _progress.Report((value.Item1, _current / _total));
            }
        }
    }
}
