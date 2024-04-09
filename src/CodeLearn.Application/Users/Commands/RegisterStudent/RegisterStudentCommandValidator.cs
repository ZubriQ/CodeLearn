namespace CodeLearn.Application.Users.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotNull();

        RuleFor(x => x.FullName.FirstName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.FullName.FirstName)
            .NotNull()
            .MaximumLength(50);

        RuleFor(x => x.FullName.Patronymic)
            .MaximumLength(50);

        RuleFor(x => x.StudentDetails)
            .NotNull();

        RuleFor(x => x.StudentDetails.StudentGroupName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.StudentDetails.UserCode)
            .NotEmpty()
            .MaximumLength(50);
    }
}