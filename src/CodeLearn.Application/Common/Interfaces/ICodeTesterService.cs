using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Exercises;

namespace CodeLearn.Application.Common.Interfaces;

public interface ICodeTesterService
{
    Task<Result> TestMethodAsync(MethodCodingExercise methodCodingExercise, string studentCode);
}