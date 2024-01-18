using CodeLearn.Domain.Common.Errors;
using CodeLearn.Domain.Common.Result;
using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(new Error("Identity.JWT", "Failure message is empty."));
    }
}