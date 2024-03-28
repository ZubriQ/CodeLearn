using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Tests.Queries.GetTestByIdWithExercises;

public record GetTestByIdWithExercisesQuery(int TestId) : IRequest<OneOf<TestWithExercisesDto, NotFound>>;

public class GetTestByIdWithExercisesQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetTestByIdWithExercisesQuery, OneOf<TestWithExercisesDto, NotFound>>
{
    public async Task<OneOf<TestWithExercisesDto, NotFound>> Handle(GetTestByIdWithExercisesQuery request, CancellationToken cancellationToken)
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
            .ToArrayAsync(cancellationToken);

        var questionExercises = await _context.QuestionExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(test.Id.Value))
            .ToArrayAsync(cancellationToken);

        return new TestWithExercisesDto(test, methodCodingExercises, questionExercises);
    }
}