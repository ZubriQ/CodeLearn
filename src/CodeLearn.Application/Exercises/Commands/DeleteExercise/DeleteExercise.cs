using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(int Id) : IRequest<OneOf<Success, NotFound, BadRequest>>;

public class DeleteExerciseCommandHandler(IApplicationDbContext _context)
    : IRequestHandler<DeleteExerciseCommand, OneOf<Success, NotFound, BadRequest>>
{
    public async Task<OneOf<Success, NotFound, BadRequest>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            return new BadRequest();
        }

        var exercise = await _context.Exercises
            .FirstOrDefaultAsync(x => x.Id == ExerciseId.Create(request.Id), cancellationToken);

        if (exercise is null)
        {
            return new NotFound();
        }

        _context.Exercises.Remove(exercise);

        await _context.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}