using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Exercises.Queries.GetAllQuestionExercisesByTestId;

public record GetAllQuestionExercisesByTestIdQuery(int TestId) : IRequest<QuestionExercise[]>;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllQuestionExercisesByTestIdQuery, QuestionExercise[]>
{
    public async Task<QuestionExercise[]> Handle(GetAllQuestionExercisesByTestIdQuery request, CancellationToken cancellationToken)
    {
        var questionExercises = await context.QuestionExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(request.TestId))
            .ToArrayAsync(cancellationToken);

        return questionExercises.Length == 0 ? [] : questionExercises;
    }
}