using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeLearn.CodeEngine.Analyzers;

public class InfiniteLoopAnalyzer : CSharpSyntaxWalker
{
    private readonly SemanticModel _semanticModel;
    private readonly HashSet<SyntaxNode> _visitedNodes = new HashSet<SyntaxNode>();
    private readonly HashSet<string> _visitedMethods = new HashSet<string>();

    public bool HasInfiniteLoop { get; private set; } = false;
    public bool HasRecursion { get; private set; } = false;

    public InfiniteLoopAnalyzer(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    private static bool IsAlwaysTrue(ExpressionSyntax condition)
    {
        if (condition is LiteralExpressionSyntax literal && literal.Kind() == SyntaxKind.TrueLiteralExpression)
        {
            return true;
        }

        return false;
    }

    public override void VisitWhileStatement(WhileStatementSyntax node)
    {
        if (!_visitedNodes.Contains(node))
        {
            _visitedNodes.Add(node);
            var condition = node.Condition;
            if (IsAlwaysTrue(condition))
            {
                HasInfiniteLoop = true;
                return;
            }
        }
        base.VisitWhileStatement(node);
    }

    public override void VisitForStatement(ForStatementSyntax node)
    {
        if (!_visitedNodes.Contains(node))
        {
            _visitedNodes.Add(node);
            if (node.Condition == null) // Handle for(;;) loop
            {
                HasInfiniteLoop = true;
                return;
            }
            var condition = node.Condition;
            if (IsAlwaysTrue(condition))
            {
                HasInfiniteLoop = true;
                return;
            }
        }
        base.VisitForStatement(node);
    }

    public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        if (!_visitedNodes.Contains(node))
        {
            _visitedNodes.Add(node);
            var methodName = node.Identifier.Text;
            _visitedMethods.Add(methodName);
            base.VisitMethodDeclaration(node);
            _visitedMethods.Remove(methodName);
        }
    }

    public override void VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        var methodName = GetInvokedMethodName(node);
        if (methodName != null && _visitedMethods.Contains(methodName))
        {
            HasRecursion = true;
            return;
        }
        base.VisitInvocationExpression(node);
    }

    private static string? GetInvokedMethodName(InvocationExpressionSyntax node)
    {
        if (node.Expression is IdentifierNameSyntax identifierName)
        {
            return identifierName.Identifier.Text;
        }
        return null;
    }
}