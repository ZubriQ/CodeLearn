namespace CodeLearn.Lib
{
    /// <summary>
    /// Initializes initial data.
    /// </summary>
    public static class CodeInitializer
    {
        public static IEnumerable<string> InitializeDefaultNamespaces()
        {
            var namespaces = new List<string>();
            namespaces.AddRange(new string[]
             {
                "System",
                "System.IO",
                "System.Net",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections.Generic"
             });
            return namespaces;
        }

        public static string InitializeHeader(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new Exception("Class name cannot be null or empty.");
            }

            string header = @"
            namespace CScript
            {
                [Serializable]
                public class " + className + @"
                {
            ";
            return header;
        }

        public static string InitializeFooter()
        {
            string footer = @"
            }
        }";
            return footer;
        }
    }
}
