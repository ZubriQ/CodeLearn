namespace CodeLearn.Application.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
{
    public CreateTeacherCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(v => v.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(v => v.Patronymic).MaximumLength(50);
    }
}