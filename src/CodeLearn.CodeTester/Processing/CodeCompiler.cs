using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeLearn.CodeTester.Processing;

public class CodeCompiler
{
    private static string _assemblyDirectory = "";
    private static string _dllFileName = "";

    public static string AssemblyPath => _assemblyDirectory + "\\" + _dllFileName;

    private static string GetPath()
    {
        var codeBase = Assembly.GetExecutingAssembly().Location;
        var uri = new UriBuilder(codeBase);

        return Uri.UnescapeDataString(uri.Path);
    }

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

    /// <returns>Returns true if .cs file code compiles successfully.</returns>
    public bool Compile(string formattedCode)
    {
        try
        {
            var tree = CSharpSyntaxTree.ParseText(formattedCode);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("TestingCompilation",
                syntaxTrees: [tree],
                references: DefaultReferences,
                options: options);

            _assemblyDirectory = Path.GetDirectoryName(GetPath())!;

            _dllFileName = Guid.NewGuid() + ".dll";

            var buildConfigurationDirectory = Path.Combine("bin", "Debug", "net8.0"); // TODO: or "Release"
            
            var dllPath = Path.Combine(buildConfigurationDirectory, _dllFileName);

            var result = compilation.Emit(dllPath);

            return result.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }
}