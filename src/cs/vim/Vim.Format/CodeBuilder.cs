using System.Linq;
using System.Text;

namespace Vim.DotNetUtilities
{
    public class CodeBuilder
    {
        private int indentCount;
        private StringBuilder sb = new StringBuilder();

        public CodeBuilder AppendRaw(string line)
        {
            sb.Append(new string(' ', indentCount * 4));
            sb.AppendLine(line);
            return this;
        }

        public CodeBuilder AppendLine(string line = "")
        {
            var openBraces = line.Count(c => c == '{');
            var closeBraces = line.Count(c => c == '}');

            // Sometimes we have {} on the same line
            if (openBraces == closeBraces)
            {
                openBraces = 0;
                closeBraces = 0;
            }

            indentCount -= closeBraces;
            sb.Append(new string(' ', indentCount * 4));
            sb.AppendLine(line);
            indentCount += openBraces;
            return this;
        }

        public void Indent()
        {
            ++indentCount;
        }
        
        public void Unindent()
        {
            indentCount = System.Math.Max(0, --indentCount);
        }

        public void IndentOneLine(string line)
        {
            Indent();
            AppendLine(line);
            Unindent();
        }

        public void UnindentOneLine(string line)
        {
            Unindent();
            AppendLine(line);
            Indent();
        }

        public override string ToString()
            => sb.ToString();
    }
}
