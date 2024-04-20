using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Application.Exercises.MethodCodingExercises.Queries.GetMethodCodingExerciseById;

public record GetMethodCodingExerciseByIdQuery(int Id) : IRequest<OneOf<MethodCodingExercise, NotFound>>;

public class GetMethodCodingExerciseByIdQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetMethodCodingExerciseByIdQuery, OneOf<MethodCodingExercise, NotFound>>
{
    public async Task<OneOf<MethodCodingExercise, NotFound>> Handle(GetMethodCodingExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var methodCodingExercise = await _context.MethodCodingExercises
            .Include(x => x.ExerciseTopics)
            .FirstOrDefaultAsync(x => x.Id == ExerciseId.Create(request.Id), cancellationToken);

        if (methodCodingExercise is null)
        {
            return new NotFound();
        }

        return methodCodingExercise;
    }
}