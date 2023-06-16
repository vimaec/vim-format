using System.Collections.Generic;
using System.Linq;

namespace Vim.Format.SceneBuilder
{
    /// <summary>
    /// This class exists to group nodes. 
    /// TODO: add this to the VimScene and see how long it takes to create for large files.
    /// </summary>
    public class VimSceneNodeGroups
    {
        public VimScene Vim;

        public DictionaryOfLists<int, VimSceneNode> Categories { get; } = new DictionaryOfLists<int, VimSceneNode>();
        public DictionaryOfLists<int, VimSceneNode> Families { get; } = new DictionaryOfLists<int, VimSceneNode>();
        public DictionaryOfLists<int, VimSceneNode> Levels { get; } = new DictionaryOfLists<int, VimSceneNode>();
        public DictionaryOfLists<int, VimSceneNode> BimDocuments { get; } = new DictionaryOfLists<int, VimSceneNode>();
        public DictionaryOfLists<int, VimSceneNode> Worksets { get; } = new DictionaryOfLists<int, VimSceneNode>();

        public VimSceneNodeGroups(VimScene vim)
        {
            Vim = vim;
            foreach (var node in vim.VimNodesWithGeometry())
            {
                Categories.Add(node.CategoryIndex, node);
                Families.Add(node.FamilyIndex, node);
                Levels.Add(node.LevelIndex, node);
                BimDocuments.Add(node.BimDocumentIndex, node);
                Worksets.Add(node.WorksetIndex, node);
            }
        }

        public IEnumerable<(string, int, List<VimSceneNode>)> GetCategoryGroups()
            => Categories.Select(kv => (Vim.GetCategoryName(kv.Key), kv.Key, kv.Value)).OrderBy(x => x.Item1);

        public IEnumerable<(string, int, List<VimSceneNode>)> GetFamilyGroups()
            => Families.Select(kv => (Vim.GetFamilyName(kv.Key), kv.Key, kv.Value)).OrderBy(x => x.Item1);

        public IEnumerable<(string, int, List<VimSceneNode>)> GetLevelGroups()
            => Levels.Select(kv => (Vim.GetLevelName(kv.Key), kv.Key, kv.Value)).OrderBy(x => x.Item1);

        public IEnumerable<(string, int, List<VimSceneNode>)> GetWorksetGroups()
            => Worksets.Select(kv => (Vim.GetWorksetName(kv.Key), kv.Key, kv.Value)).OrderBy(x => x.Item1);

        public IEnumerable<(string, int, List<VimSceneNode>)> GetBimDocumentGroups()
            => BimDocuments.Select(kv => (Vim.GetBimDocumentFileName(kv.Key), kv.Key, kv.Value)).OrderBy(x => x.Item1);
    }
}
