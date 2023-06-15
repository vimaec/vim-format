namespace Vim.ObjectModel.CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var file = args[0];
            var efFile = args[1];
            var tsFile = args[2];
            var hFile = args[3];

            ObjectModelGenerator.WriteDocument(file);
            ObjectModelDbContextGenerator.WriteDbContext(efFile);
            ObjectModelTypeScriptGenerator.WriteDocument(tsFile);
            ObjectModelCppGenerator.WriteDocument(hFile);
        }
    }
}
