using CodeLearn.CodeEngine.Models;
using CodeLearn.CodeEngine.Processing;
using CodeLearn.Domain.Common.Result;

namespace CodeLearn.CodeEngine.Services;

public class CodeExecutionManager(CodeFormatter formatter, CodeCompiler compiler, CodeTester tester) : ICodeExecutionManager
{
    public async Task<Result> ExecuteAsync(CodeExercise exercise)
    {
        var formattedCode = formatter.Format(exercise.StudentCode, exercise.ClassName);

        var compilationResult = compiler.Compile(formattedCode!);

        if (compilationResult.IsFailure)
        {
            return compilationResult;
        }

        var testingResult = tester.Test(exercise);

        return testingResult;
    }
}