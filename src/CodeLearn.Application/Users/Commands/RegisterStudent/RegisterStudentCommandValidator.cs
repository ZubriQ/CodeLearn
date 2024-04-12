namespace CodeLearn.Application.Users.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.Student)
            .NotNull();

        RuleFor(x => x.Student.FirstName)
            .NotNull();

        RuleFor(x => x.Student.FirstName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.Student.FirstName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.Student.Patronymic)
            .MaximumLength(50);

        RuleFor(x => x.Student.StudentGroupName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Student.UserCode)
            .NotEmpty()
            .MaximumLength(50);
    }
}