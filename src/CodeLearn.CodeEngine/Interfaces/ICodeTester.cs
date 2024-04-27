using CodeLearn.CodeEngine.Models;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.CodeEngine.Interfaces;

public interface ICodeTester
{
    Result Test(CodeExercise exercise);
}