﻿namespace CodeLearn.Domain.ExerciseSubmissions;

public abstract class ExerciseSubmission(
    ExerciseSubmissionId id,
    ExerciseId exerciseId,
    TestingSessionId testingSessionId,
    DateTime sentDateTime,
    SubmissionTestStatus status)
    : BaseEntity<ExerciseSubmissionId>(id), IAggregateRoot
{
    public ExerciseId ExerciseId { get; private set; } = exerciseId;
    public TestingSessionId TestingSessionId { get; private set; } = testingSessionId;
    public DateTime SentDateTime { get; private set; } = sentDateTime;
    public SubmissionTestStatus Status { get; private set; } = status;
}