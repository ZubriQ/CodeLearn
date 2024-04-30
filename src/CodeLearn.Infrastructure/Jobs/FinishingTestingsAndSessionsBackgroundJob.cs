using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.Testings.Enums;
using CodeLearn.Domain.TestingSessions.Enums;
using Microsoft.Extensions.Logging;
using Quartz;

namespace CodeLearn.Infrastructure.Jobs;

/// <summary>
/// Makes sure that completed testings have Completed status
/// and Testing sessions have Finished status
/// </summary>
[DisallowConcurrentExecution]
public class FinishingTestingsAndSessionsBackgroundJob(
    IApplicationDbContext context,
    ILogger<FinishingTestingsAndSessionsBackgroundJob> logger) : IJob
{
    private readonly IApplicationDbContext _context = context;
    private readonly ILogger<FinishingTestingsAndSessionsBackgroundJob> _logger = logger;

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{UtcNow}", DateTime.UtcNow);

        int affectedTestingsCount = await _context.Testings
            .Where(x => x.Status != TestingStatus.Completed
                        && x.DeadlineDate < DateTimeOffset.UtcNow)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Status, TestingStatus.Completed));

        _logger.LogInformation($"Number of Testings completed: {affectedTestingsCount}");

        int affectedTestingSessionsCount = await _context.TestingSessions
            .Where(x => x.Status != TestingSessionStatus.Finished
                        && x.FinishDateTime < DateTimeOffset.UtcNow)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Status, TestingSessionStatus.Finished));

        _logger.LogInformation($"Number of Testing sessions finished: {affectedTestingSessionsCount}");
    }
}