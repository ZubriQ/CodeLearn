namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public class CreateTestingCommandValidator : AbstractValidator<CreateTestingCommand>
{
    public CreateTestingCommandValidator()
    {
        RuleFor(x => x.TestId)
            .GreaterThan(0);

        RuleFor(x => x.StudentGroupId)
            .GreaterThan(0);

        RuleFor(x => x.DeadlineDate)
            .Must(BeAValidDate);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(300);
    }

    private bool BeAValidDate(DateTimeOffset date)
    {
        return date > DateTimeOffset.Now;
    }
}