using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommandHandler(
    IApplicationDbContext context,
    IValidator<CreateTeacherCommand> validator)
    : IRequestHandler<CreateTeacherCommand, OneOf<Guid, ValidationFailed>>
{
    public async Task<OneOf<Guid, ValidationFailed>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        var teacher = Teacher.Create(request.FirstName, request.LastName, request.Patronymic);

        context.Teachers.Add(teacher);

        await context.SaveChangesAsync(cancellationToken);

        return teacher.Id.Value;
    }
}