using CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateMethodCodingExerciseSubmission;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CodeLearn.Application.ExerciseSubmissions.MethodCoding.Commands.CreateExerciseSubmission;

public sealed class ExerciseSubmissionEventConsumer(ILogger<ExerciseSubmissionEventConsumer> logger)
    : IConsumer<CodeExeciseSubmissionCreatedEvent>
{
    private readonly ILogger<ExerciseSubmissionEventConsumer> _logger = logger;

    public Task Consume(ConsumeContext<CodeExeciseSubmissionCreatedEvent> context)
    {
        _logger.LogInformation("ExerciseSubmission created: {@ExerciseSubmission}", context.Message);

        return Task.CompletedTask;
    }
}