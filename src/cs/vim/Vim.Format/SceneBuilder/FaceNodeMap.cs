using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Format.Geometry;
using Vim.LinqArray;

namespace Vim.Format.SceneBuilder
{
    // TODO: Is this still required now that the Face table and Face Group Id have been removed?  

    /// <summary>
    /// This class helps when a mesh is created from multiple nodes (e.g. a scene), and can be used 
    /// to figure out the mapping from faces back to nodes, and even the specific face in the source geometry.
    /// </summary>
    public class FaceNodeMap
    {
        public FaceNodeMap(IArray<ISceneNode> nodes)
        {
            Nodes = nodes;

            NodeIndexToVertexOffset.Add(0);
            NodeIndexToIndexOffset.Add(0);

            // Written long-hand for efficiency
            var prevVertexOffset = 0;
            var prevIndexOffset = 0;
            for (var i = 0; i < Nodes.Count; ++i)
            {
                var g = Nodes[i].GetMesh();
                var nVertices = g?.NumVertices ?? 0;
                var nFaces = g?.NumFaces ?? 0;
                NodeIndexToVertexOffset.Add(prevVertexOffset += nVertices);
                NodeIndexToIndexOffset.Add(prevIndexOffset += nFaces * 3);
                for (var j = 0; j < nFaces; ++j)
                    FaceIndexToNodeIndex.Add(i);
            }
            Debug.Assert(NodeIndexToIndexOffset.Count == nodes.Count + 1);
            Debug.Assert(NodeIndexToVertexOffset.Count == nodes.Count + 1);
            Debug.Assert(Nodes.Count == nodes.Count);
        }

        public IArray<ISceneNode> Nodes { get; }
        public List<int> FaceIndexToNodeIndex { get; } = new List<int>();
        public List<int> NodeIndexToVertexOffset { get; } = new List<int>();
        public List<int> NodeIndexToIndexOffset { get; } = new List<int>();

        public int NumFaces
            => NodeIndexToIndexOffset.Last() / 3;

        public int NumVertices
            => NodeIndexToVertexOffset.Last();

        public int GetNodeIndex(int face)
            => FaceIndexToNodeIndex[face];

        public ISceneNode GetNode(int face)
            => Nodes[GetNodeIndex(face)];

        public IMesh GetGeometry(int face)
            => GetNode(face).GetMesh() ?? throw new Exception("Internal error: could not find node");

        public int GetFaceOffset(int face)
            => face - (NodeIndexToIndexOffset[GetNodeIndex(face)] * 3);

        public int GetNumVertices(int nodeIndex)
            => NodeIndexToVertexOffset[nodeIndex + 1] - NodeIndexToVertexOffset[nodeIndex];

        public int GetNumIndices(int nodeIndex)
            => NodeIndexToIndexOffset[nodeIndex + 1] - NodeIndexToIndexOffset[nodeIndex];
    }
}