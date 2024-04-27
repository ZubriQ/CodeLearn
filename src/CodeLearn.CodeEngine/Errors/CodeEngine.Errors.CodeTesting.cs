using CodeLearn.Domain.Common.Errors;

namespace CodeLearn.CodeEngine.Errors;

public static partial class CodeEngineErrors
{
    public static class CodeTesting
    {
        private static string Prefix => "CodeTesting.";

        public static readonly Error TestCasesFailed = new(
            $"{Prefix}{nameof(TestCasesFailed)}",
            "The test cases did not pass.");

        public static readonly Error MethodNotFound = new(
            $"{Prefix}{nameof(MethodNotFound)}",
            "Method not found error.");
    }
}