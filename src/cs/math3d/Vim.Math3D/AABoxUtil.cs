using System.Collections.Generic;

namespace Vim.Math3d
{
    public static class AABoxUtil
    {
        public static AABox ToAABox(this IEnumerable<Vector3> self)
            => AABox.Create(self);
    }
}
