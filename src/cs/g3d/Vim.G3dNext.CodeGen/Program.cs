namespace Vim.G3dNext.CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var file = args[0];
            G3dCodeGen.WriteDocument(file);
        }
    }
}
