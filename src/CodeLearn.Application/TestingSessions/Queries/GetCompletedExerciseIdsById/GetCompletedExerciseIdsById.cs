using CodeLearn.Domain.ExerciseSubmissions.Enums;
using CodeLearn.Domain.TestingSessions.ValueObjects;

namespace CodeLearn.Application.TestingSessions.Queries.GetCompletedExerciseIdsById;

public record GetCompletedExerciseIdsByIdQuery(int Id) : IRequest<OneOf<int[], NotFound>>;

public class GetCompletedExerciseIdsByIdQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetCompletedExerciseIdsByIdQuery, OneOf<int[], NotFound>>
{
    public async Task<OneOf<int[], NotFound>> Handle(GetCompletedExerciseIdsByIdQuery request, CancellationToken cancellationToken)
    {
        var testingSessionExists = await _context.TestingSessions
            .AnyAsync(t => t.Id == TestingSessionId.Create(request.Id), cancellationToken);

        if (!testingSessionExists)
        {
            return new NotFound();
        }

        var questionExerciseIds = await _context.ChoiceExerciseSubmissions
            .Where(x => x.TestingSessionId == TestingSessionId.Create(request.Id))
            .Select(x => x.ExerciseId.Value)
            .ToArrayAsync(cancellationToken);

        var methodCodingExerciseIds = await _context.CodeExerciseSubmissions
            .Where(x => x.TestingSessionId == TestingSessionId.Create(request.Id)
                        && x.Status == SubmissionTestStatus.Solved)
            .Select(x => x.ExerciseId.Value)
            .ToArrayAsync(cancellationToken);

        var combinedExerciseIds = questionExerciseIds.Concat(methodCodingExerciseIds).Distinct().ToArray();

        return combinedExerciseIds;
    }
}