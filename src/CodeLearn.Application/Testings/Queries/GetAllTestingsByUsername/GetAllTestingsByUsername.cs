using CodeLearn.Application.Testings.Queries.GetAllTestings;

namespace CodeLearn.Application.Testings.Queries.GetAllMyTestings;

public record GetAllTestingsByUsernameQuery(string Username) : IRequest<OneOf<TestingDto[], NotFound>>;

public class GetAllTestingsByUsernameQueryHandler(
    IApplicationDbContext _context,
    IIdentityService _identityService)
    : IRequestHandler<GetAllTestingsByUsernameQuery, OneOf<TestingDto[], NotFound>>
{
    public async Task<OneOf<TestingDto[], NotFound>> Handle(GetAllTestingsByUsernameQuery request, CancellationToken cancellationToken)
    {
        var studentGroupName = await _identityService.GetStudentGroupNameByUsernameAsync(request.Username);

        var studentGroup = await _context.StudentGroups
            .FirstOrDefaultAsync(x => x.Name == studentGroupName, cancellationToken);

        if (studentGroup is null)
        {
            return new NotFound();
        }

        var testings = await _context.Testings
            .AsNoTracking()
            .Where(x => x.StudentGroupId == studentGroup.Id)
            .ToArrayAsync(cancellationToken);

        var testingDetails = new List<TestingDto>();

        foreach (var testing in testings)
        {
            var test = await _context.Tests.FindAsync([testing.TestId, cancellationToken], cancellationToken);

            testingDetails.Add(new TestingDto(
                testing.Id.Value,
                testing.TestId.Value,
                test?.Title,
                testing.StudentGroupId.Value,
                studentGroupName,
                testing.DeadlineDate,
                testing.DurationInMinutes));
        }

        // TODO: Optimize with Dapper?

        return testingDetails.Count == 0 ? [] : testingDetails.ToArray();
    }
}