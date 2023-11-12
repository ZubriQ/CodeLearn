using CodeLearn.Db;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeLearn.Lib
{
    public class CodeTester
    {
        private HostAssemblyLoadContext? _assemblyLoader;
        private Assembly? _methodDllAssembly;
        private Type? _type;
        private object? _classInstance;
        private MethodInfo? _method;

        private int ParametersLength
        {
            get
            {
                if (_method != null)
                {
                    return _method.GetParameters().Length;
                }
                else throw new NullReferenceException("_method variable equals null.");
            }
        }

        public ExerciseData? Data { get; private set; }
        public string? ClassName => Data?.Exercise.ClassName;
        public string? MethodName => Data?.TestMethodInfo.Name;

        public CodeTester()
        {

        }

        public void LoadExerciseData(Exercise exercise)
        {
            Data = new ExerciseData(exercise);
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
        public bool TestLoadedExercise()
        {
            try
            {
                GetMethodFromAssembly();
                bool passedTestCases = TestMethodTestCases();
                UnloadAndFinalize();
                return passedTestCases;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void GetMethodFromAssembly()
        {
            _assemblyLoader = new HostAssemblyLoadContext(CodeCompiler.AssemblyPath);
            _methodDllAssembly = _assemblyLoader.LoadFromAssemblyPath(CodeCompiler.AssemblyPath);
            _type = _methodDllAssembly.GetTypes().FirstOrDefault(t => t.Name == ClassName);
            if (_type != null && !string.IsNullOrEmpty(MethodName))
            {
                _classInstance = Activator.CreateInstance(_type, null);
                _method = _type.GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static);
            }
        }
        // TODO: optimize
        private bool TestMethodTestCases()
        {
            bool success = false;
            
            if (_method != null)
            {
                object[] parametersArray = new object[ParametersLength];

                var methodParameters = Data.TestMethodInfo.TestMethodParameters.ToArray();

                foreach (var testCase in Data.TestCases)
                {
                    var testCaseParameters = testCase.TestCaseParameters.ToArray();

                    for (int p = 0; p < methodParameters.Length; p++)
                    {
                        Type? paramType = Type.GetType(methodParameters[p].DataType.Name);
                        var convertedType = Convert.ChangeType(testCaseParameters[p].Value, paramType);
                        parametersArray[p] = convertedType;
                    }
                    dynamic? methodResult = _method.Invoke(_classInstance,
                                            ParametersLength == 0 ? null : parametersArray);

                    Type? testResultType = Type.GetType(Data.TestMethodInfo.ReturnType.Name);
                    dynamic testResult = Convert.ChangeType(testCase.Result, testResultType);

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
}
