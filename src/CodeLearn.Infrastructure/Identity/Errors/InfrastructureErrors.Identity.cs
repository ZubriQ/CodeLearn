using CodeLearn.Domain.Common.Errors;

namespace CodeLearn.Infrastructure.Identity.Errors;

public static partial class InfrastructureErrors
{
    public static class Identity
    {
        private static string Prefix => "Identity.";

        public static readonly Error EmailAlreadyInUse = new(
            $"{Prefix}{nameof(EmailAlreadyInUse)}",
            "Email is already in use.");

        public static readonly Error UserNotFoundByEmail = new(
            $"{Prefix}{nameof(UserNotFoundByEmail)}",
            "User does not exist.");

        public static readonly Error InvalidCredentials = new(
            $"{Prefix}{nameof(InvalidCredentials)}",
            "Invalid credentials.");
    }
}