using CodeLearn.CodeEngine.Errors;
using CodeLearn.CodeEngine.Interfaces;
using CodeLearn.CodeEngine.Models;
using CodeLearn.Domain.Common.Errors;
using CodeLearn.Domain.Common.Result;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeLearn.CodeEngine.Processing;

public class CodeTester : ICodeTester
{
    private HostAssemblyLoadContext? _assemblyLoader;
    private Assembly? _methodDllAssembly;
    private Type? _type;
    private object? _classInstance;
    private MethodInfo? _method;

    public CodeExercise Exercise { get; private set; } = null!;

    private int ParametersLength
    {
        get
        {
            if (_method != null)
            {
                return _method.GetParameters().Length;
            }

            throw new NullReferenceException("_method variable equals null.");
        }
    }

    // It is important to mark this method as NoInlining, otherwise the JIT could decide
    // to inline it into the Main method. That could then prevent successful unloading
    // of the plugin because some of the MethodInfo / Type / Plugin.Interface / HostAssemblyLoadContext
    // instances may get lifetime extended beyond the point when the plugin is expected to be
    // unloaded.
    /// <summary>
    /// Gets the desired method and tests it, then unloads the assembly.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Result Test(CodeExercise exercise)
    {
        Exercise = exercise;

        try
        {
            GetMethodFromAssembly();
            var result = TestMethodWithTestCases();

            UnloadAndFinalize();
            return result;
        }
        catch (BadImageFormatException)
        {
            return Result.Failure(CodeEngineErrors.Compilation.MethodCompilationFailed);
        }
        catch (Exception ex)
        {
            return Result.Failure(new Error("CodeEngine.CodeTester.Test", $"Unexpected exception: {ex.Message}"));
        }
    }

    private void GetMethodFromAssembly()
    {
        _assemblyLoader = new HostAssemblyLoadContext(CodeCompiler.AssemblyPath);
        _methodDllAssembly = _assemblyLoader.LoadFromAssemblyPath(CodeCompiler.AssemblyPath);
        _type = _methodDllAssembly.GetTypes().FirstOrDefault(t => t.Name == Exercise.ClassName);

        if (_type == null || string.IsNullOrEmpty(Exercise.MethodToExecute))
        {
            return;
        }

        _classInstance = Activator.CreateInstance(_type, null);
        _method = _type.GetMethod(Exercise.MethodToExecute, BindingFlags.Public | BindingFlags.Static);
    }

    // TODO: optimize
    private Result TestMethodWithTestCases()
    {
        if (_method == null)
        {
            return Result.Failure(CodeEngineErrors.CodeTesting.MethodNotFound);
        }

        var methodParameters = Exercise.MethodParameters.ToArray();
        var testResultType = Type.GetType(Exercise.MethodReturnTypeSystemName);

        foreach (var testCase in Exercise.TestCases)
        {
            var testCaseParameters = testCase.TestCaseParameters.ToArray();
            var parametersArray = new object[methodParameters.Length];

            for (var i = 0; i < methodParameters.Length; i++)
            {
                var paramType = Type.GetType(methodParameters[i].SystemName);
                var convertedType = Convert.ChangeType(testCaseParameters[i].Value, paramType!);
                parametersArray[i] = convertedType;
            }

            var methodResult = _method.Invoke(_classInstance, ParametersLength == 0 ? null : parametersArray);

            var testResult = Convert.ChangeType(testCase.CorrectOutputValue, testResultType!);
            if (!Equals(methodResult, testResult))
            {
                return Result.Failure(CodeEngineErrors.CodeTesting.TestCasesFailed);
            }
        }

        return Result.Success();
    }

    private void UnloadAndFinalize()
    {
        _assemblyLoader?.Unload();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}