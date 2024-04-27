using Error = CodeLearn.Domain.Common.Errors.Error;

namespace CodeLearn.Application.Common.Errors;

public static partial class ApplicationErrors
{
    public static class ExerciseSubmissions
    {
        private static string Prefix => "ExerciseSubmissions.";

        public static readonly Error UnexpectedCodeTesterServiceException = new(
            $"{Prefix}{nameof(UnexpectedCodeTesterServiceException)}",
            "Test method async error.");

        public static readonly Error TestingTimeout = new(
            $"{Prefix}{nameof(TestingTimeout)}",
            "Timeout. It took more than 2 seconds to complete the task.");
    }
}