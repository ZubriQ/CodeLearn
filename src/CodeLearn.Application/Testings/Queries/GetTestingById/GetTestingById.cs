using CodeLearn.Domain.Testings;
using CodeLearn.Domain.Testings.ValueObjects;

namespace CodeLearn.Application.Testings.Queries.GetTestingById;

public record GetTestingByIdQuery(int TestingId) : IRequest<OneOf<Testing, NotFound>>;

public class GetTestingByIdQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetTestingByIdQuery, OneOf<Testing, NotFound>>
{
    public async Task<OneOf<Testing, NotFound>> Handle(GetTestingByIdQuery request, CancellationToken cancellationToken)
    {
        var testing = await _context.Testings
            .FirstOrDefaultAsync(x => x.Id == TestingId.Create(request.TestingId), cancellationToken);

        if (testing is null)
        {
            return new NotFound();
        }

        return testing;
    }
}