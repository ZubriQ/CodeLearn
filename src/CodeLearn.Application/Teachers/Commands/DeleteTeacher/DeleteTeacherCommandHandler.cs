using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Commands.DeleteTeacher;

public class DeleteTeacherCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteTeacherCommand, OneOf<Success, NotFound>>
{
    public async Task<OneOf<Success, NotFound>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .Where(t => t.Id == TeacherId.Create(request.Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (teacher is null)
        {
            return new NotFound();
        }

        context.Teachers.Remove(teacher!);

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}