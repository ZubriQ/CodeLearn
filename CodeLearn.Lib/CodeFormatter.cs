using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLearn.Lib
{
    internal class CodeFormatter
    {
        private IEnumerable<string> DefaultNamespaces { get; set; }

        private string? Header { get; set; }

        private string Footer { get; set; }

        public CodeFormatter()
        {
            Footer = CodeInitializer.InitializeFooter();
            DefaultNamespaces = CodeInitializer.InitializeDefaultNamespaces();
        }

        /// <returns>Formatted code (a complete .cs file).</returns>
        public string FormatSources(string code, string className)
        {
            Header = CodeInitializer.InitializeHeader(className);
            string namespaces = FormatNamespaces();
            return string.Concat(namespaces, Header, code, Footer);
        }

        private string FormatNamespaces()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string namespaceString in DefaultNamespaces)
                sb.AppendFormat("using {0};{1}", namespaceString, Environment.NewLine);
            return sb.ToString();
        }
    }
}
