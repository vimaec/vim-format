namespace Vim.Format.CodeGen
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var vimEntityCodeGenFilePath = args[0];
            var tsFilePath = args[1];
            var hFilePath = args[2];

            VimEntityCodeGen.WriteDocument(vimEntityCodeGenFilePath);
            ObjectModelTypeScriptGenerator.WriteDocument(tsFilePath);
            ObjectModelCppGenerator.WriteDocument(hFilePath);
        }
    }
}
