using CodeLearn.Domain.ExerciseTopics;

namespace CodeLearn.Application.ExerciseTopics.Queries.GetAllExerciseTopics;

public record GetAllExerciseTopicsQuery() : IRequest<ExerciseTopic[]>;

public class GetAllExerciseTopicsQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetAllExerciseTopicsQuery, ExerciseTopic[]>
{
    public async Task<ExerciseTopic[]> Handle(GetAllExerciseTopicsQuery request, CancellationToken cancellationToken)
    {
        var topics = await _context.ExerciseTopics
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return topics.Length == 0 ? [] : topics;
    }
}