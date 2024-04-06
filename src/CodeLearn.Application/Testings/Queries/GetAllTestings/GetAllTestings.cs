namespace CodeLearn.Application.Testings.Queries.GetAllTestings;

public record GetAllTestingsQuery : IRequest<TestingDto[]>;

public class GetAllTestingsQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetAllTestingsQuery, TestingDto[]>
{
    public async Task<TestingDto[]> Handle(GetAllTestingsQuery request, CancellationToken cancellationToken)
    {
        var testings = await _context.Testings
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        var testingDetails = new List<TestingDto>();

        foreach (var testing in testings)
        {
            var test = await _context.Tests.FindAsync([testing.TestId, cancellationToken], cancellationToken);
            var studentGroup = await _context.StudentGroups.FindAsync([testing.StudentGroupId, cancellationToken], cancellationToken);

            testingDetails.Add(new TestingDto(
                testing.Id.Value,
                testing.TestId.Value,
                test?.Title,
                testing.StudentGroupId.Value,
                studentGroup?.Name,
                testing.DeadlineDate,
                testing.DurationInMinutes));
        }

        // TODO: Optimize with Dapper?

        return [.. testingDetails];
    }
}