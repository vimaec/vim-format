namespace Vim.Format.CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var file = args[0];
            var tsFile = args[1];
            var hFile = args[2];

            ObjectModelGenerator.WriteDocument(file);
            ObjectModelTypeScriptGenerator.WriteDocument(tsFile);
            ObjectModelCppGenerator.WriteDocument(hFile);
        }
    }
}
