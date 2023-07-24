using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Vim.Util
{
    public static class Reflection
    {
        public static IEnumerable<Type> GetAllSubclassesOf(this Assembly asm, Type t)
            => asm.GetTypes().Where(x => x.IsSubclassOf(t));
    }
}
