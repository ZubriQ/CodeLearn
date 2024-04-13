using CodeLearn.Domain.Common.Errors;

namespace CodeLearn.Infrastructure.Identity.Errors;

public static partial class InfrastructureErrors
{
    public static class Identity
    {
        private static string Prefix => "Identity.";

        public static readonly Error InvalidUserFields = new(
            $"{Prefix}{nameof(InvalidUserFields)}",
            "Invalid user fields.");

        public static readonly Error UserNotFoundByUserName = new(
            $"{Prefix}{nameof(UserNotFoundByUserName)}",
            "User does not exist.");

        public static readonly Error InvalidCredentials = new(
            $"{Prefix}{nameof(InvalidCredentials)}",
            "Invalid credentials.");

        public static readonly Error InvalidTokenName = new(
            $"{Prefix}{nameof(InvalidTokenName)}",
            "Invalid token name.");

        public static readonly Error InvalidRefreshTokenOrUser = new(
            $"{Prefix}{nameof(InvalidRefreshTokenOrUser)}",
            "Invalid refresh token or user.");
    }
}