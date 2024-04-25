using CodeLearn.CodeTester.Models;
using CodeLearn.CodeTester.Processing;

namespace CodeLearn.CodeTester;

public class CodeExecutionManager(
    CodeFormatter formatter,
    CodeCompiler compiler,
    Processing.CodeTester tester)
{
    public bool Execute(Exercise exercise)
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