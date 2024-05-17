using CodeLearn.Domain.TestingSessions.Enums;

namespace CodeLearn.Application.TestingSessions.Queries.GetAllMyTestingSessions;

public class StudentTestingSessionDto
{
    public int Id { get; set; }

    public int TestingId { get; set; }

    public int TestId { get; set; }

    public string TestTitle { get; set; } = string.Empty;

    public TestingSessionStatus Status { get; set; }

    public DateTimeOffset StartDateTime { get; set; }

    public DateTimeOffset FinishDateTime { get; set; }

    public int CorrectQuestionsCount { get; set; }

    public int SolvedExercisesCount { get; set; }
}