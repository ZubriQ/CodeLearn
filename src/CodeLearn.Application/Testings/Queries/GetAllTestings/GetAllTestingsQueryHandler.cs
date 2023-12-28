using CodeLearn.Domain.Testings;

namespace CodeLearn.Application.Testings.Queries.GetAllTestings;

public class GetAllTestingsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllTestingsQuery, Testing[]>
{
    public async Task<Testing[]> Handle(GetAllTestingsQuery query, CancellationToken cancellationToken)
    {
        var testings = await context.Testings
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return testings.Length == 0 ? [] : testings;
    }
}