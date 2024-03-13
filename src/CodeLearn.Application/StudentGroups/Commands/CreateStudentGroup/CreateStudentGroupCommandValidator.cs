namespace CodeLearn.Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupCommandValidator : AbstractValidator<CreateStudentGroupCommand>
{
    public CreateStudentGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.EnrolmentYear)
            .GreaterThanOrEqualTo(2020)
            .LessThanOrEqualTo(2100);
    }
}