using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CodeLearn.Lib
{
    internal class CodeFormatter
    {
        private readonly IEnumerable<string> _defaultNamespaces;
        private string _header;
        private string _footer;

        public CodeFormatter()
        {
            _header = "";
            _footer = CodeInitializer.InitializeFooter();
            _defaultNamespaces = CodeInitializer.InitializeDefaultNamespaces();
        }

        /// <returns>Formatted code (a complete .cs file).</returns>
        public string FormatSources(string code, string className)
        {
            _header = CodeInitializer.InitializeHeader(className);
            string namespaces = FormatNamespaces();
            return string.Concat(namespaces, _header, code, _footer);
        }

        private string FormatNamespaces()
        {
            StringBuilder sb = new();
            foreach (string namespaceString in _defaultNamespaces)
                sb.AppendFormat("using {0};{1}", namespaceString, Environment.NewLine);
            return sb.ToString();
        }
    }
}
