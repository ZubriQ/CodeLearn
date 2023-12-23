using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Teachers;

namespace CodeLearn.Application.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTeacherCommand, Guid>
{
    public async Task<Guid> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        // TODO: Validate

        var teacher = Teacher.Create(request.FirstName, request.LastName, request.Patronymic);

        context.Teachers.Add(teacher);

        await context.SaveChangesAsync(cancellationToken);

        return teacher.Id.Value;
    }
}