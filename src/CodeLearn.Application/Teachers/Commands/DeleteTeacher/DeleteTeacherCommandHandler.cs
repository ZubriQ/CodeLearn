using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Commands.DeleteTeacher;

public class DeleteTeacherCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTeacherCommand, bool>
{
    public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .Where(t => t.Id == TeacherId.Create(request.Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (teacher is null)
        {
            return false; // TODO: custom Result class
        }

        context.Teachers.Remove(teacher!);

        await context.SaveChangesAsync(cancellationToken);

        return true; 
    }
}