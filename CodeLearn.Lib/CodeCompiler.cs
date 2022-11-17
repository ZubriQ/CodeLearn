using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace CodeLearn.Lib
{
    internal static class CodeCompiler
    {
        private static readonly string runtimePath = 
            Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location) + @"\{0}.dll";

        private static readonly IEnumerable<MetadataReference> defaultReferences = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "mscorlib")),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "System")),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Console")),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Runtime")),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Core")),
            MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Core"))
        };

        public static string? DllName;

        public static string AssemblyPath => Path.GetDirectoryName(GetPath()) + "\\" + DllName;

        private static string GetPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().Location;
            UriBuilder uri = new UriBuilder(codeBase);
            return Uri.UnescapeDataString(uri.Path);
        }

        /// <returns>Returns true if .cs file code compiles successfully.</returns>
        public static bool Compile(string formattedCode)
        {
            try
            {
                DllName = Guid.NewGuid().ToString() + ".dll";
                SyntaxTree tree = CSharpSyntaxTree.ParseText(formattedCode);
                var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
                var compilation = CSharpCompilation.Create("TestingCompilation",
                                                           syntaxTrees: new[] { tree },
                                                           references: defaultReferences,
                                                           options: options);
                var result = compilation.Emit(DllName);
                return result.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
