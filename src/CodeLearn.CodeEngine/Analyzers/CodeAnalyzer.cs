using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeLearn.CodeEngine.Analyzers;

public class CodeAnalyzer(SyntaxTree tree, CSharpCompilation compilation)
{
    private readonly SemanticModel _model = compilation.GetSemanticModel(tree);
    private readonly SyntaxNode _root = tree.GetRoot();

    public (bool HasInfiniteLoop, bool HasRecursion) AnalyzeForInfiniteLoopsOrRecursion()
    {
        var visitor = new InfiniteLoopAnalyzer(_model);
        visitor.Visit(_root);

        return (visitor.HasInfiniteLoop, visitor.HasRecursion);
    }
}