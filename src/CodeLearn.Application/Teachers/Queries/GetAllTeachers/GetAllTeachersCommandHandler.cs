namespace CodeLearn.Application.Teachers.Queries.GetAllTeachers;

public class GetAllTeachersCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<GetAllTeachersCommand, TeacherModel[]>
{
    public async Task<TeacherModel[]> Handle(GetAllTeachersCommand request, CancellationToken cancellationToken)
    {
        return await context.Teachers
            .AsNoTracking()
            .Select(t => new TeacherModel(t.Id.Value, t.FirstName, t.LastName, t.Patronymic))
            .ToArrayAsync(cancellationToken); // TODO: Contracts.
    }
}