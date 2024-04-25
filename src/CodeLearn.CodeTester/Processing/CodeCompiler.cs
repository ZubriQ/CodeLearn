using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeLearn.CodeTester.Processing;

public class CodeCompiler
{
    private static readonly string RuntimePath = 
        Path.GetDirectoryName(typeof(object).GetTypeInfo().Assembly.Location) + @"\{0}.dll";

    private static readonly IEnumerable<MetadataReference> DefaultReferences = new[]
    {
        MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "mscorlib")),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "System")),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "System.Console")),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "System.Runtime")),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "System.Core")),
        MetadataReference.CreateFromFile(string.Format(RuntimePath, "System.Core"))
    };

    private static string _dllName = string.Empty;
    public static string AssemblyPath => Path.GetDirectoryName(GetPath()) + "\\" + _dllName;

    private static string GetPath()
    {
        var codeBase = Assembly.GetExecutingAssembly().Location;
        var uri = new UriBuilder(codeBase);
            
        return Uri.UnescapeDataString(uri.Path);
    }
    
    /// <returns>Returns true if .cs file code compiles successfully.</returns>
    public bool Compile(string formattedCode)
    {
        try
        {
            var tree = CSharpSyntaxTree.ParseText(formattedCode);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("TestingCompilation",
                syntaxTrees: new[] { tree },
                references: DefaultReferences,
                options: options);
            
            _dllName = Guid.NewGuid() + ".dll";
            var result = compilation.Emit(_dllName);
                
            return result.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}