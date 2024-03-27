namespace CodeLearn.Application.Testings.Queries.GetAllTestings;

public record TestingModel(
    int Id,
    int TestId,
    string? TestTitle,
    int StudentGroupId,
    string? StudentGroupName,
    DateTime StartDateTime,
    DateTime EndDateTime,
    int DurationInMinutes);

public record GetAllTestingsQuery : IRequest<TestingModel[]>;

public class GetAllTestingsQueryHandler(IApplicationDbContext _context) : IRequestHandler<GetAllTestingsQuery, TestingModel[]>
{
    public async Task<TestingModel[]> Handle(GetAllTestingsQuery request, CancellationToken cancellationToken)
    {
        var testings = await _context.Testings
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        var testingDetails = new List<TestingModel>();

        foreach (var testing in testings)
        {
            var test = await _context.Tests.FindAsync([testing.TestId, cancellationToken], cancellationToken);
            var studentGroup = await _context.StudentGroups.FindAsync([testing.StudentGroupId, cancellationToken], cancellationToken);

            testingDetails.Add(new TestingModel(
                testing.Id.Value,
                testing.TestId.Value,
                test?.Title,
                testing.StudentGroupId.Value,
                studentGroup?.Name,
                testing.StartDateTime,
                testing.EndDateTime,
                testing.DurationInMinutes));
        }

        // TODO: or use Dapper?

        return [.. testingDetails];
    }
}