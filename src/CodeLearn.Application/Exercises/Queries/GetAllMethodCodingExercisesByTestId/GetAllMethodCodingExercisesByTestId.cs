using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Exercises.Queries.GetAllMethodCodingExercisesByTestId;

public record GetAllMethodCodingExercisesByTestIdQuery(int TestId) : IRequest<OneOf<MethodCodingExercise[], NotFound, BadRequest>>;

public class GetAllMethodCodingExercisesByTestIdQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetAllMethodCodingExercisesByTestIdQuery, OneOf<MethodCodingExercise[], NotFound, BadRequest>>
{
    public async Task<OneOf<MethodCodingExercise[], NotFound, BadRequest>> Handle(GetAllMethodCodingExercisesByTestIdQuery request, CancellationToken cancellationToken)
    {
        if (request.TestId <= 0)
        {
            return new BadRequest();
        }

        var testExists = await _context.Tests.AnyAsync(x => x.Id == TestId.Create(request.TestId), cancellationToken);

        if (!testExists)
        {
            return new NotFound();
        }

        var methodCodingExercises = await _context.MethodCodingExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(request.TestId))
            .Include(x => x.InputOutputExamples)
            .Include(x => x.ExerciseTopics)
            .Include(x => x.MethodParameters)
            .ThenInclude(y => y.DataType)
            .Include(x => x.TestCases)
            .ThenInclude(y => y.TestCaseParameters)
            .ToArrayAsync(cancellationToken);

        return methodCodingExercises.Length == 0 ? [] : methodCodingExercises;
    }
}