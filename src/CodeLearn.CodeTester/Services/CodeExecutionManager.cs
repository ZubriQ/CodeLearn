using CodeLearn.CodeTester.Models;
using CodeLearn.CodeTester.Processing;

namespace CodeLearn.CodeTester.Services;

public class CodeExecutionManager(CodeFormatter formatter, CodeCompiler compiler, Processing.CodeTester tester) : ICodeExecutionManager
{
    public async Task<bool> ExecuteAsync(CodeExercise exercise)
    {
        var formattedCode = formatter.Format(exercise.StudentCode, exercise.ClassName);
        var isCompiled = compiler.Compile(formattedCode);

        if (!isCompiled)
        {
            return false;
        }

        var isSuccess = tester.Test(exercise);

        return isSuccess;
    }
}