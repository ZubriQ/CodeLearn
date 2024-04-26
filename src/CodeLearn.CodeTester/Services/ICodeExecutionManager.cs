using CodeLearn.CodeTester.Models;

namespace CodeLearn.CodeTester.Services;

public interface ICodeExecutionManager
{
    Task<bool> ExecuteAsync(CodeExercise exercise);
}