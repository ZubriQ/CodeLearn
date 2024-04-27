using CodeLearn.Domain.Common.Errors;

namespace CodeLearn.CodeEngine.Errors;

public static partial class CodeEngineErrors
{
    public static class Compilation
    {
        private static string Prefix => "Compilation.";

        public static readonly Error MethodCompilationFailed = new(
            $"{Prefix}{nameof(MethodCompilationFailed)}",
            "Could not compile the method.");

        public static readonly Error RecursionDetected = new(
            $"{Prefix}{nameof(RecursionDetected)}",
            "Recursion detected.");

        public static readonly Error InfiniteLoopDetected = new(
            $"{Prefix}{nameof(InfiniteLoopDetected)}",
            "Infinite loop detected.");
    }
}