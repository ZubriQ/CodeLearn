namespace CodeLearn.Domain.Common.Errors;

public static partial class DomainErrors
{
    public static class Test
    {
        private static string Prefix => "Test.";

        public static readonly Error InvalidTitleLength = new(
            $"{Prefix}{nameof(InvalidTitleLength)}",
            "Title can't be longer than 100 characters or be empty.");

        public static readonly Error InvalidDescriptionLength = new(
            $"{Prefix}{nameof(InvalidDescriptionLength)}",
            "Description can't be longer than 1000 characters or be empty.");
    }
}
