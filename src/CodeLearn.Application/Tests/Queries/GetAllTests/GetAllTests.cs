using CodeLearn.Domain.Tests;

namespace CodeLearn.Application.Tests.Queries.GetAllTests;

public record GetAllTestsQuery : IRequest<Test[]>;

public class GetAllTestsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllTestsQuery, Test[]>
{
    public async Task<Test[]> Handle(GetAllTestsQuery query, CancellationToken cancellationToken)
    {
        var tests = await context.Tests
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return tests.Length == 0 ? [] : tests;
    }
}