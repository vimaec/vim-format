using System.Collections.Generic;
using Vim.Math3d;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Represents the geometric information of an element.
    /// </summary>
    public class VimElementGeometryInfo
    {
        public int ElementIndex { get; }

        public int VertexCount { get; set; }

        public int FaceCount { get; set; }

        public AABox WorldSpaceBoundingBox { get; set; } = AABox.Empty;

        public List<(int NodeIndex, int GeometryIndex)> NodeAndGeometryIndices { get; } = new List<(int NodeIndex, int GeometryIndex)>();

        public int NodeCount
            => NodeAndGeometryIndices.Count;

        public bool HasGeometry
            => FaceCount > 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public VimElementGeometryInfo(int elementIndex)
        {
            ElementIndex = elementIndex;
        }
    }
}