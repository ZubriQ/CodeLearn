using CodeLearn.Application.Testings.Queries.GetAllTestings;
using CodeLearn.Domain.Testings.Enums;

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
            .Where(x => (x.StudentGroupId == studentGroup.Id
                         && x.Status == TestingStatus.Open
                         || x.Status == TestingStatus.Completed) &&
                         !_context.TestingSessions.Any(es => es.TestingId == x.Id && es.CreatedBy == request.Username))
            .OrderByDescending(x => x.Status)
            .ThenBy(x => x.DeadlineDate)
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
                testing.DurationInMinutes,
                testing.Status.ToString()));
        }

        // TODO: Optimize with Dapper?

        return testingDetails.Count == 0 ? [] : testingDetails.ToArray();
    }
}