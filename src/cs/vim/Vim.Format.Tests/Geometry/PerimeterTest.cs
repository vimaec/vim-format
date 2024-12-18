using NUnit.Framework;
using Vim.Format.Geometry;
using Vim.Math3d;

namespace Vim.Format.Tests.Geometry
{
    public static class PerimeterTest
    {

        [Test]
        public static void Test()
        {
            var torus = Primitives.QuadMesh(uv => TorusFunction(uv, 10, 0.2f), 10, 24);

            var perimeter = torus.GeneratePerimeter(Vector3.UnitX);
        }

        public static Vector3 TorusFunction(Vector2 uv, float radius, float tube)
        {
            uv = uv * Constants.TwoPi;
            return new Vector3(
                (radius + tube * uv.Y.Cos()) * uv.X.Cos(),
                (radius + tube * uv.Y.Cos()) * uv.X.Sin(),
                tube * uv.X.Sin());
        }
    }
}
