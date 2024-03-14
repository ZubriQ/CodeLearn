using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Queries.GetTestById;

public class GetTestByIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTestByIdQuery, OneOf<Test, NotFound>>
{
    public async Task<OneOf<Test, NotFound>> Handle(GetTestByIdQuery query, CancellationToken cancellationToken)
    {
        // TODO:
        // var test = await context.Tests
        //     .AsNoTracking()
        //     .Where(t => t.Id == TestId.Create(query.TestId))
        //     .FirstOrDefaultAsync(cancellationToken);
        Test test = null;

        if (test is null)
        {
            return new NotFound();
        }

        return test;
    }
}