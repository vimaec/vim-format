namespace Vim.Format.CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var legacyObjectModelFilePath = args[0];
            var v2ObjectModelFilePath = args[1];
            var tsFilePath = args[2];
            var hFilePath = args[3];

            ObjectModelGenerator.WriteDocument(legacyObjectModelFilePath);
            VimEntityCodeGen.WriteDocument(v2ObjectModelFilePath);
            ObjectModelTypeScriptGenerator.WriteDocument(tsFilePath);
            ObjectModelCppGenerator.WriteDocument(hFilePath);
        }
    }
}
