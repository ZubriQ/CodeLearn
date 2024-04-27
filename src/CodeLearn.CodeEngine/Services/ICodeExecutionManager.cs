using CodeLearn.CodeEngine.Models;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.CodeEngine.Services;

public interface ICodeExecutionManager
{
    Task<Result> ExecuteAsync(CodeExercise exercise);
}