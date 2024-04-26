namespace CodeLearn.CodeEngine.Processing;

/// <summary>
/// Initializes initial data.
/// </summary>
public static class CodeInitializer
{
    private static readonly string[] DefaultNamespacesCollection = new[]
    {
        "System",
        "System.IO",
        "System.Net",
        "System.Linq",
        "System.Text",
        "System.Text.RegularExpressions",
        "System.Collections.Generic"
    };

    public static IEnumerable<string> GetDefaultNamespaces()
    {
        return new List<string>(DefaultNamespacesCollection);
    }

    public static string GetHeaderCode(string className)
    {
        if (string.IsNullOrEmpty(className))
        {
            throw new ArgumentException("Class name cannot be null or empty.");
        }

        var header = @"
            namespace CScript
            {
                [Serializable]
                public class " + className + @"
                {
            ";
        return header;
    }

    public static string GetFooterCode()
    {
        var footer = @"
            }
        }";
        return footer;
    }
}