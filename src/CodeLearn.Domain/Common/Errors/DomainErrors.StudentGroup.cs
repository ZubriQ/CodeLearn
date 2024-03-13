namespace CodeLearn.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class StudentGroup
    {
        private static string Prefix => "StudentGroup.";

        public static readonly Error InvalidNameLength = new(
            $"{Prefix}{nameof(InvalidNameLength)}",
            "Name can't be longer than 50 characters");

        public static readonly Error InvalidEnrolmentYear = new(
            $"{Prefix}{nameof(InvalidEnrolmentYear)}",
            "Invalid year input.");
    }
}