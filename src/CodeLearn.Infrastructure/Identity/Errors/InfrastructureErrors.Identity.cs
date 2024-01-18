using CodeLearn.Domain.Common.Errors;

namespace CodeLearn.Infrastructure.Identity.Errors;

public static partial class InfrastructureErrors
{
    public static class Identity
    {
        private static string Prefix => "Identity.";

        public static readonly Error InvalidRoleName = new(
            $"{Prefix}{nameof(InvalidRoleName)}",
            "Role can be only Teacher or Student.");

        public static readonly Error UserNotFoundByEmail = new(
            $"{Prefix}{nameof(UserNotFoundByEmail)}",
            "User does not exist.");

        public static readonly Error InvalidCredentials = new(
            $"{Prefix}{nameof(InvalidCredentials)}",
            "Invalid credentials.");
    }
}