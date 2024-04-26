using System.Reflection;
using System.Runtime.CompilerServices;
using CodeLearn.CodeTester.Models;

namespace CodeLearn.CodeTester.Processing;

public class CodeTester
{
    private HostAssemblyLoadContext? _assemblyLoader;
    private Assembly? _methodDllAssembly;
    private Type? _type;
    private object? _classInstance;
    private MethodInfo? _method;

    public CodeExercise Exercise { get; private set; } = null!;
    public string ClassName => Exercise.ClassName;
    public string MethodName => Exercise.MethodToExecute;

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
    /// <returns>Returns true if there were no exceptions.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool Test(CodeExercise exercise)
    {
        Exercise = exercise;
        
        try
        {
            GetMethodFromAssembly();
            var isSuccess = TestMethodTestCases();
            
            UnloadAndFinalize();
            return isSuccess;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void GetMethodFromAssembly()
    {
        _assemblyLoader = new HostAssemblyLoadContext(CodeCompiler.AssemblyPath);
        _methodDllAssembly = _assemblyLoader.LoadFromAssemblyPath(CodeCompiler.AssemblyPath);
        _type = _methodDllAssembly.GetTypes().FirstOrDefault(t => t.Name == ClassName);
            
        if (_type == null || string.IsNullOrEmpty(MethodName))
        {
            return;
        }
            
        _classInstance = Activator.CreateInstance(_type, null);
        _method = _type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static);
    }
    
    // TODO: optimize
    private bool TestMethodTestCases()
    {
        var success = false;
            
        if (_method != null)
        {
            var parametersArray = new object[ParametersLength];

            var methodParameters = Exercise.MethodParameters.ToArray();

            foreach (var testCase in Exercise.TestCases)
            {
                var testCaseParameters = testCase.TestCaseParameters.ToArray();

                for (var p = 0; p < methodParameters.Length; p++)
                {
                    var paramType = Type.GetType(methodParameters[p].SystemName);
                    var convertedType = Convert.ChangeType(testCaseParameters[p].Value, paramType!);
                    parametersArray[p] = convertedType;
                }
                dynamic? methodResult = _method.Invoke(_classInstance,
                    ParametersLength == 0 ? null : parametersArray);

                var testResultType = Type.GetType(Exercise.MethodReturnTypeSystemName);
                dynamic testResult = Convert.ChangeType(testCase.CorrectOutputValue, testResultType!);

                if (methodResult == testResult)
                {
                    success = true;
                }
                else
                {
                    return false;
                }
            }
        }
        return success;
    }

    private void UnloadAndFinalize()
    {
        _assemblyLoader?.Unload();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}