using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Commands.UpdateTeacherName;

public class UpdateTeacherNameCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<UpdateTeacherNameCommand, OneOf<Success, NotFound>>
{
    public async Task<OneOf<Success, NotFound>> Handle(UpdateTeacherNameCommand request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .Where(t => t.Id == TeacherId.Create(request.Id))
            .FirstOrDefaultAsync(cancellationToken);

        if (teacher is null)
        {
            return new NotFound();
        }

        teacher.UpdateName(request.FirstName, request.LastName, request.Patronymic);

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}