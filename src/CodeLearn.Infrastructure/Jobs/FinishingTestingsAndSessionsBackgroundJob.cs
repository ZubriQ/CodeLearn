using CodeLearn.Application.Common.Interfaces;
using CodeLearn.Domain.ExerciseSubmissions.Enums;
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

        await FinishTestings();
        await FinishTestingSessions();
    }
    
    private async Task FinishTestingSessions()
    {
        // Testing sessions
        var sessions = _context.TestingSessions
            .Where(x => x.Status != TestingSessionStatus.Finished && DateTimeOffset.UtcNow > x.FinishDateTime);

        foreach (var session in sessions)
        {
            var correctCodingExerciseSubmissions = _context.CodeExerciseSubmissions
                .Where(x => x.TestingSessionId == session.Id && x.Status == SubmissionTestStatus.Solved);

            // TODO: this
            //var correctQuestionExerciseSubmissions = _context.QuestionExercises
            //    .Where(x => x.TestingSessionId == session.Id && x.Status == SubmissionTestStatus.Solved);
        }
    }

    /// <summary>
    ///     Make testings unavailable if deadline is expired
    /// </summary>
    private async Task FinishTestings()
    {
        // Testings
        int affectedTestingsCount = await _context.Testings
            .Where(x => x.Status != TestingStatus.Completed
                        && x.DeadlineDate < DateTimeOffset.UtcNow)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Status, TestingStatus.Completed));
        
        _logger.LogInformation($"Number of Testings completed: {affectedTestingsCount}");
    }
}