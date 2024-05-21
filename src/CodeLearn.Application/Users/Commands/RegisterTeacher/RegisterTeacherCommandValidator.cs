namespace CodeLearn.Application.Users.Commands.RegisterTeacher;

public class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.Patronymic)
            .MaximumLength(50);
    }
}