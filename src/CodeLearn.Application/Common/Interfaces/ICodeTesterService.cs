using CodeLearn.Domain.Exercises;

namespace CodeLearn.Application.Common.Interfaces;

public interface ICodeTesterService
{
    Task<bool> TestMethodAsync(MethodCodingExercise methodCodingExercise, string studentCode);
}