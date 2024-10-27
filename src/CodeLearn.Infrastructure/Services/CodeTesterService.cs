using CodeLearn.Application.Common.Interfaces;
using CodeLearn.CodeEngine.Interfaces;
using CodeLearn.CodeEngine.Models;
using CodeLearn.Domain.Common.Result;
using CodeLearn.Domain.Exercises;

namespace CodeLearn.Infrastructure.Services;

public class CodeTesterService(ICodeFormatter formatter, ICodeCompiler compiler, ICodeTester tester)
    : ICodeTesterService
{
    public async Task<Result> TestMethodAsync(MethodCodingExercise methodCodingExercise, string studentCode)
    {
        var exercise = ConvertToExercise(methodCodingExercise, studentCode);

        var formattedCode = formatter.Format(exercise.StudentCode, exercise.ClassName);

        var compilationResult = compiler.Compile(formattedCode!);

        if (compilationResult.IsFailure)
        {
            return compilationResult;
        }

        var testingResult = tester.Test(exercise);

        return testingResult;
    }

    private static CodeExercise ConvertToExercise(MethodCodingExercise exercise, string studentCode)
    {
        var mappedExercise = new CodeExercise()
        {
            ClassName = "MethodCodingClass",
            MethodToExecute = exercise.MethodToExecute,
            MethodReturnTypeSystemName = exercise.MethodReturnDataType.SystemName,
            StudentCode = studentCode,
        };

        foreach (var parameter in exercise.MethodParameters)
        {
            mappedExercise.MethodParameters.Add(new MethodParameter()
            {
                Position = parameter.Position,
                SystemName = parameter.DataType.SystemName,
            });
        }

        foreach (var testCase in exercise.TestCases)
        {
            var mappedTestCase = new TestCase()
            {
                CorrectOutputValue = testCase.CorrectOutputValue,
                TestCaseParameters = [],
            };

            foreach (var parameter in testCase.TestCaseParameters)
            {
                mappedTestCase.TestCaseParameters.Add(new TestCaseParameter()
                {
                    Position = parameter.Position,
                    Value = parameter.Value
                });
            }

            mappedExercise.TestCases.Add(mappedTestCase);
        }

        return mappedExercise;
    }
}