using CodeLearn.Domain.Tests;
using CodeLearn.Domain.Tests.ValueObjects;

namespace CodeLearn.Application.Tests.Queries.GetTestById;

public record GetTestByIdQuery(int TestId) : IRequest<OneOf<Test, NotFound>>;

public class GetTestByIdQueryHandler(IApplicationDbContext _context)
    : IRequestHandler<GetTestByIdQuery, OneOf<Test, NotFound>>
{
    public async Task<OneOf<Test, NotFound>> Handle(GetTestByIdQuery query, CancellationToken cancellationToken)
    {
        var test = await _context.Tests
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == TestId.Create(query.TestId), cancellationToken);

        if (test is null)
        {
            return new NotFound();
        }

        return test;
    }
}