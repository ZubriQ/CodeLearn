using CodeLearn.Domain.Teachers.ValueObjects;

namespace CodeLearn.Application.Teachers.Commands.UpdateTeacherName;

public class UpdateTeacherNameCommandHandler(IApplicationDbContext context)
    : IRequestHandler<UpdateTeacherNameCommand, OneOf<Success, NotFound, BadRequest>>
{
    public async Task<OneOf<Success, NotFound, BadRequest>> Handle(UpdateTeacherNameCommand request, CancellationToken cancellationToken)
    {
        var teacher = await context.Teachers
            .Where(t => t.Id == TeacherId.Create(request.TeacherId))
            .FirstOrDefaultAsync(cancellationToken);

        if (teacher is null)
        {
            return new NotFound();
        }

        var result = teacher.UpdateName(request.FirstName, request.LastName, request.Patronymic);

        if (result.IsFailure)
        {
            return new BadRequest();
        }

        await context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}