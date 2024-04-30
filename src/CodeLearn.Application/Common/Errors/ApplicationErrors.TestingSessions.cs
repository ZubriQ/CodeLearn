namespace CodeLearn.Application.Common.Errors;

public static partial class ApplicationErrors
{
    public static class TestingSessions
    {
        private static string Prefix => "TestingSessions.";

        public static readonly Domain.Common.Errors.Error SessionAlreadyFinished = new(
            $"{Prefix}{nameof(SessionAlreadyFinished)}",
            "The testing session has already been finished.");

        public static readonly Domain.Common.Errors.Error SessionFinishDateTimeInFuture = new(
            $"{Prefix}{nameof(SessionFinishDateTimeInFuture)}",
            "The finish date and time for the testing session is in the future.");

        public static readonly Domain.Common.Errors.Error SessionAlreadyHasFeedback = new(
            $"{Prefix}{nameof(SessionAlreadyHasFeedback)}",
            "The testing session already has student's feedback.");
    }
}