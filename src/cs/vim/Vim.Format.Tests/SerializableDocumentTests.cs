using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Util.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Util.Tests;

namespace Vim.Format.Tests;



[TestFixture]
public static class SerializableDocumentTests
{
    [Test]
    public static void TestEmpty()
    {
        var doc = new SerializableDocument();
        Assert.DoesNotThrow(() => doc.ToBFast());
    }
}

