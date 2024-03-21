namespace CodeLearn.Domain.Exercises;

public sealed class ClassCodingExercise : Exercise
{
    public string ClassSolutionCode { get; private set; }
    public string ClassTesterCode { get; private set; }

    private ClassCodingExercise(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string classSolutionCode,
        string classTesterCode)
        : base(testId, title, description, difficulty)
    {
        ClassSolutionCode = classSolutionCode;
        ClassTesterCode = classTesterCode;
    }

    public static ClassCodingExercise Create(
        TestId testId,
        string title,
        string description,
        ExerciseDifficulty difficulty,
        string classSolutionCode,
        string classTesterCode)
    {
        return new ClassCodingExercise(
            testId,
            title,
            description,
            difficulty,
            classSolutionCode,
            classTesterCode);
    }
}