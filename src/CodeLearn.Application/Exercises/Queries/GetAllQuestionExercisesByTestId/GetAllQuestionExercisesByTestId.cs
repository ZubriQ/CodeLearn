using CodeLearn.Domain.Exercises;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Exercises.Queries.GetAllQuestionExercisesByTestId;

public record GetAllQuestionExercisesByTestIdQuery(int TestId) : IRequest<OneOf<QuestionExercise[], NotFound, BadRequest>>;

public class GetAllStudentGroupsQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetAllQuestionExercisesByTestIdQuery, OneOf<QuestionExercise[], NotFound, BadRequest>>
{
    public async Task<OneOf<QuestionExercise[], NotFound, BadRequest>> Handle(GetAllQuestionExercisesByTestIdQuery request, CancellationToken cancellationToken)
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

        var questionExercises = await _context.QuestionExercises
            .AsNoTracking()
            .Where(x => x.TestId == TestId.Create(request.TestId))
            .Include(x => x.QuestionChoices)
            .ToArrayAsync(cancellationToken);

        return questionExercises.Length == 0 ? [] : questionExercises;
    }
}