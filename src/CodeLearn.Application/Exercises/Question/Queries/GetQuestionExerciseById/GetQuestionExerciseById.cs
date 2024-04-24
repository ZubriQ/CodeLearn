using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Exercises.ValueObjects;

namespace CodeLearn.Application.Exercises.QuestionExercises.Queries.GetQuestionExerciseById;

public record GetQuestionExerciseByIdQuery(int Id) : IRequest<OneOf<QuestionExercise, NotFound>>;

public class GetQuestionExerciseByIdQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetQuestionExerciseByIdQuery, OneOf<QuestionExercise, NotFound>>
{
    public async Task<OneOf<QuestionExercise, NotFound>> Handle(GetQuestionExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _context.QuestionExercises
            .Include(x => x.ExerciseTopics)
            .Include(x => x.QuestionChoices)
            .FirstOrDefaultAsync(x => x.Id == ExerciseId.Create(request.Id), cancellationToken);

        if (question is null)
        {
            return new NotFound();
        }

        return question;
    }
}