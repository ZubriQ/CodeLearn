using FluentValidation.Results;

namespace CodeLearn.Application.Common.Errors;

public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
{
    public ValidationFailed(ValidationFailure error) : this(new[] { error })
    {
        
    }
}