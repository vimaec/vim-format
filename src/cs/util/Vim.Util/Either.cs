using System;

namespace Vim.Util
{
    /// <summary>
    /// Contains either a Left object or a Right object.
    /// </summary>
    public class Either<TL, TR>
    {
        public readonly TL Left;
        public readonly TR Right;
        public readonly bool IsLeft;

        public Either(TL left)
        {
            Left = left;
            IsLeft = true;
        }

        public Either(TR right)
        {
            Right = right;
            IsLeft = false;
        }

        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc)
            => IsLeft ? leftFunc(Left) : rightFunc(Right);

        public static implicit operator Either<TL, TR>(TL left) => new Either<TL, TR>(left);

        public static implicit operator Either<TL, TR>(TR right) => new Either<TL, TR>(right);
    }
}
