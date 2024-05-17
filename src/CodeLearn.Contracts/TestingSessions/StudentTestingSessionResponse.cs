namespace CodeLearn.Contracts.TestingSessions;

public record StudentTestingSessionResponse(
    int Id,
    int TestingId,
    int TestId,
    string TestTitle,
    string Status,
    string StartDateTime,
    string FinishDateTime,
    int CorrectQuestionsCount,
    int SolvedExercisesCount);