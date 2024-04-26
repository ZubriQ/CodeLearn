using CodeLearn.CodeEngine.Models;

namespace CodeLearn.CodeEngine.Services;

public interface ICodeExecutionManager
{
    Task<bool> ExecuteAsync(CodeExercise exercise);
}