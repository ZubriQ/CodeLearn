using CodeLearn.CodeEngine.Analyzers;
using CodeLearn.CodeEngine.Errors;
using CodeLearn.Domain.Common.Result;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;

namespace CodeLearn.CodeEngine.Processing;

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
    public Result Compile(string formattedCode)
    {
        try
        {
            var tree = CSharpSyntaxTree.ParseText(formattedCode);
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var compilation = CSharpCompilation.Create("TestingCompilation",
                syntaxTrees: [tree],
                references: DefaultReferences,
                options: options);

            var codeAnalyzer = new CodeAnalyzer(tree, compilation);
            var (hasInfiniteLoop, hasRecursion) = codeAnalyzer.AnalyzeForInfiniteLoopsOrRecursion();

            if (hasInfiniteLoop)
            {
                return Result.Failure(CodeEngineErrors.Compilation.InfiniteLoopDetected);
            }

            if (hasRecursion)
            {
                return Result.Failure(CodeEngineErrors.Compilation.RecursionDetected);
            }

            _assemblyDirectory = Path.GetDirectoryName(GetPath())!;
            _dllFileName = Guid.NewGuid() + ".dll";

            var buildConfigurationDirectory = Path.Combine("bin", "Debug", "net8.0"); // TODO: or "Release"
            var dllPath = Path.Combine(buildConfigurationDirectory, _dllFileName);
            var result = compilation.Emit(dllPath);

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(CodeEngineErrors.Compilation.MethodCompilationFailed);
        }
    }
}