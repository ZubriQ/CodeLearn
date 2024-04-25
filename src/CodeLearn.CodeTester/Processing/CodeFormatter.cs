using System.Text;

namespace CodeLearn.CodeTester.Processing;

public class CodeFormatter
{
    private readonly IEnumerable<string> _defaultNamespaces = CodeInitializer.GetDefaultNamespaces();
    private string _header = "";
    private readonly string _footer = CodeInitializer.GetFooterCode();

    /// <returns>Formatted code (a complete .cs file).</returns>
    public string Format(string code, string className)
    {
        _header = CodeInitializer.GetHeaderCode(className);
        var namespaces = FormatNamespaces();
        
        return string.Concat(namespaces, _header, code, _footer);
    }

    private string FormatNamespaces()
    {
        StringBuilder sb = new();
        foreach (var namespaceString in _defaultNamespaces)
            sb.Append($"using {namespaceString};{Environment.NewLine}");
        
        return sb.ToString();
    }
}