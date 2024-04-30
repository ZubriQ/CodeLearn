using FluentValidation.Results;

namespace CodeLearn.Application.Common.Errors;

/// <summary>
/// Indicates 400 Bad request.
/// </summary>
/// <param name="Errors">ValidationFailure errors.</param>
public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this([error])
    {

    }

    /// <summary>
    /// For custom errors.
    /// </summary>
    /// <param name="error">Application layer errors.</param>
    public ValidationFailed(Domain.Common.Errors.Error error) : this([new ValidationFailure(error.Code, error.Message)])
    {

    }
}