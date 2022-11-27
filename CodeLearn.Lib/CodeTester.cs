using CodeLearn.Db;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeLearn.Lib
{
    public class CodeTester
    {
        private HostAssemblyLoadContext? assemblyLoader { get; set; }

        private Assembly? methodDllAssembly { get; set; }

        private Type? type { get; set; }

        private object? classInstance { get; set; }

        private MethodInfo? method { get; set; }

        public ExerciseData? data { get; set; }

        private string? className => data?.Exercise.ClassName;

        private string? methodName => data?.TestMethodInfo.Name;

        private int parametersLength => method.GetParameters().Count();

        public CodeTester()
        {

        }

        public void LoadExerciseData(Exercise exercise)
        {
            data = new ExerciseData(exercise);
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
        public bool Test()
        {
            try
            {
                GetMethodFromAssembly();
                TestMethodTestCases();
                UnloadAndFinilize();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void GetMethodFromAssembly()
        {
            assemblyLoader = new HostAssemblyLoadContext(CodeCompiler.AssemblyPath);
            methodDllAssembly = assemblyLoader.LoadFromAssemblyPath(CodeCompiler.AssemblyPath);
            type = methodDllAssembly.GetTypes().FirstOrDefault(t => t.Name == className);
            if (type != null && !string.IsNullOrEmpty(methodName))
            {
                classInstance = Activator.CreateInstance(type, null);
                method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            }
        }
        // TODO: optimize
        private bool TestMethodTestCases()
        {
            bool success = false;
            
            if (method != null)
            {
                object[] parametersArray = new object[parametersLength];

                var methodParameters = data.TestMethodInfo.TestMethodParameters.ToArray();

                foreach (var testCase in data.TestCases)
                {
                    var testCaseParameters = testCase.TestCaseParameters.ToArray();

                    for (int p = 0; p < methodParameters.Length; p++)
                    {
                        Type? paramType = Type.GetType(methodParameters[p].DataType.Name);
                        var convertedType = Convert.ChangeType(testCaseParameters[p].Value, paramType);
                        parametersArray[p] = convertedType;
                    }
                    dynamic? methodResult = method.Invoke(classInstance,
                                            parametersLength == 0 ? null : parametersArray);

                    Type? testResultType = Type.GetType(data.TestMethodInfo.ReturnType.Name);
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

        private void UnloadAndFinilize()
        {
            assemblyLoader?.Unload();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
