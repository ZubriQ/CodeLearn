using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeLearn.CodeEngine.Analyzers;

/// <summary>
/// Checks code for infinite loops and recursions
/// But does not cover all loop cases
/// </summary>
public class InfiniteLoopAnalyzer : CSharpSyntaxWalker
{
    private readonly SemanticModel _semanticModel;
    private readonly HashSet<SyntaxNode> _visitedNodes = [];
    private readonly HashSet<string> _visitedMethods = [];

    public bool HasInfiniteLoop { get; private set; } = false;
    public bool HasRecursion { get; private set; } = false;

    public InfiniteLoopAnalyzer(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    /// <summary>
    /// Checks if a 'while' loop is infinite
    /// Currently does not cover all cases
    /// </summary>
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

    /// <summary>
    /// Checks if a 'for' loop is infinite
    /// Currently does not cover all cases
    /// </summary>
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

    /// <summary>
    /// For for-while loops
    /// </summary>
    private static bool IsAlwaysTrue(ExpressionSyntax condition)
    {
        return condition is LiteralExpressionSyntax literal && literal.Kind() == SyntaxKind.TrueLiteralExpression;
    }

    /// <summary>
    /// For recursion check
    /// </summary>
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

    /// <summary>
    /// For recursion check
    /// </summary>
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

    /// <summary>
    /// For recursion check
    /// </summary>
    private static string? GetInvokedMethodName(InvocationExpressionSyntax node)
    {
        if (node.Expression is IdentifierNameSyntax identifierName)
        {
            return identifierName.Identifier.Text;
        }
        return null;
    }
}