namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public class CreateTestingCommandValidator : AbstractValidator<CreateTestingCommand>
{
    public CreateTestingCommandValidator()
    {
        RuleFor(x => x.TestId)
            .GreaterThan(0);

        RuleFor(x => x.StudentGroupId)
            .GreaterThan(0);

        RuleFor(x => x.StartDateTime)
            .Must(BeAValidDate);

        RuleFor(x => x.DurationInMinutes)
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(300);
    }

    private bool BeAValidDate(DateTime date)
    {
        return date > DateTime.Now;
    }
}