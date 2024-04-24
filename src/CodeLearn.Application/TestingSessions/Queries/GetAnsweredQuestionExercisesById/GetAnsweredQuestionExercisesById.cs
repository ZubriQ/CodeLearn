using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.TestingSessions.Queries.GetAnsweredQuestionExercisesById;

public record GetAnsweredQuestionExercisesByIdQuery(int Id) : IRequest<OneOf<int[], NotFound>>;

public class GetAnsweredQuestionExercisesByIdQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetAnsweredQuestionExercisesByIdQuery, OneOf<int[], NotFound>>
{
    public async Task<OneOf<int[], NotFound>> Handle(GetAnsweredQuestionExercisesByIdQuery request, CancellationToken cancellationToken)
    {
        var testingSessionExists = await _context.TestingSessions
            .AnyAsync(t => t.Id == TestingSessionId.Create(request.Id), cancellationToken);

        if (!testingSessionExists)
        {
            return new NotFound();
        }

        var exerciseIds = await _context.ChoiceExerciseSubmissions
            .Where(x => x.TestingSessionId == TestingSessionId.Create(request.Id))
            .Select(x => x.ExerciseId.Value)
            .ToArrayAsync(cancellationToken);

        return exerciseIds;
    }
}