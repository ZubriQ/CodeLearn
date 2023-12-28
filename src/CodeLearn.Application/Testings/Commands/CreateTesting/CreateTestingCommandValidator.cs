namespace CodeLearn.Application.Testings.Commands.CreateTesting;

public class CreateTestingCommandValidator : AbstractValidator<CreateTestingCommand>
{
    public CreateTestingCommandValidator()
    {
        RuleFor(x => x.TeacherId).NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.DurationInMinutes)
            .NotEmpty()
            .GreaterThanOrEqualTo(5)
            .LessThanOrEqualTo(180);
    }
}