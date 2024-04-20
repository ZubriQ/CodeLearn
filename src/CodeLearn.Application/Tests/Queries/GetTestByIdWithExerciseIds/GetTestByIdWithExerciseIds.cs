using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Tests.Queries.GetTestByIdWithExerciseIds;

public record GetTestByIdWithExerciseIdsQuery(int TestId) : IRequest<OneOf<TestWithExerciseIdsDto, NotFound>>;

public class GetTestByIdWithExerciseIdsQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetTestByIdWithExerciseIdsQuery, OneOf<TestWithExerciseIdsDto, NotFound>>
{
    public async Task<OneOf<TestWithExerciseIdsDto, NotFound>> Handle(GetTestByIdWithExerciseIdsQuery request, CancellationToken cancellationToken)
    {
        var test = await _context.Tests
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == TestId.Create(request.TestId), cancellationToken);

        if (test is null)
        {
            return new NotFound();
        }

        var methodCodingExercises = await _context.MethodCodingExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(test.Id.Value))
            .Select(x => x.Id.Value)
            .ToArrayAsync(cancellationToken);

        var questionExercises = await _context.QuestionExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(test.Id.Value))
            .Select(x => x.Id.Value)
            .ToArrayAsync(cancellationToken);

        var dto = new TestWithExerciseIdsDto(
            test.Id.Value,
            test.Title,
            test.Description,
            methodCodingExercises,
            questionExercises);

        return dto;
    }
}