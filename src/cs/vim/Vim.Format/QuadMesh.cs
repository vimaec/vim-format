using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.G3d;

namespace Vim.Format
{
    /// <summary>
    /// This is a quadrilateral mesh. Note that it does not implement the IMesh interface,
    /// but does implement IGeometryAttributes and inherits from a G3D.
    /// </summary>
    public class QuadMesh : G3D, IGeometryAttributes
    {
        public QuadMesh(IEnumerable<GeometryAttribute> attributes)
            : base(attributes.Append(new[] { 4 }.ToObjectFaceSizeAttribute()))
            => Debug.Assert(NumCornersPerFace == 4);

        public IMesh ToTriMesh()
            => this.TriangulateQuadMesh().ToIMesh();
    }
}
