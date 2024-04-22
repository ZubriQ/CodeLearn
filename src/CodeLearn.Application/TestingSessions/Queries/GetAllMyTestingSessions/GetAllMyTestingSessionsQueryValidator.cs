namespace CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;

public class GetAllMyTestingSessionsQueryValidator : AbstractValidator<GetAllMyTestingSessionsQuery>
{
    public GetAllMyTestingSessionsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("User ID is required and cannot be null.");
    }
}