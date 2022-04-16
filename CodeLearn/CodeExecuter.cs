using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using TypeLoader;

namespace CodeLearn
{
    // WPF's Output.
    public delegate void ExecuteLogHandler(object message);

    public class CodeExecuter
    {
        // User's entered code.
        public string FormattedCode { get; private set; }

        // Declared static so that it can be called from a compiled program.
        public static ExecuteLogHandler OnExecute;

        // Needed DLLs & Usings.
        public List<string> Refferences { get; set; } = new List<string>();
        public List<string> Usings { get; set; } = new List<string>();

        // Method ***ScriptMethod*** is used from the main application.
        // Method ***Log*** calls OnExecute delegate in the external assembly.
        readonly string header = @"
            namespace CScript
            {
                [Serializable]
                public class Script
                {
                    public static void ScriptMethod()
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();  
            ";
        //              Entered program text by a user ...
        readonly string footer = @"
                        sw.Stop();
                        Log(sw.Elapsed.ToString());
                    }
                    static void Log(object message)
                    {
                        if(CodeLearn.CodeExecuter.OnExecute != null)
                            CodeLearn.CodeExecuter.OnExecute(message);
                    }
                }
            }";

        // Delegate, DLLs, Usings initialization. 
        // TODO: Determine which DLLs and Usings the program needs
        public CodeExecuter(ExecuteLogHandler onExecute)
        {
            OnExecute += onExecute;

            Refferences.AddRange(new string[]
                 {
                    "System.dll",
                    "System.Core.dll",
                    "System.Net.dll",
                    "System.Data.dll",
                    //"System.Drawing.dll",

                    // It's necessary to add our Assembly to be able to call OnExecute.
                    Assembly.GetAssembly(typeof(CodeExecuter)).Location
                 });

            Usings.AddRange(new string[]
             {
                    "System",
                    "System.IO",
                    "System.Net",
                    "System.Threading",
                    "System.Collections.Generic",
                    "System.Text",
                    "System.Text.RegularExpressions",
                    "System.ComponentModel",
                    "System.Data",
                    "System.Drawing",
                    "System.Diagnostics",
                    "System.Linq"
             });
        }

        public void Execute()
        {
            Compile(FormattedCode);
        }

        public void Compile(string formattedCode)
        {
            // Declaring parameters; generating an Assembly into memory.
            CompilerParameters compilerParams = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = false
            };
            compilerParams.ReferencedAssemblies.AddRange(Refferences.ToArray());

            // Compilation.
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            var compilerResult = codeDomProvider.CompileAssemblyFromSource(compilerParams, formattedCode);


            byte[] bytedAssembly = File.ReadAllBytes(compilerResult.PathToAssembly);

            string pathToDll = Assembly.GetExecutingAssembly().CodeBase;
            AppDomainSetup domainSetup = new AppDomainSetup { PrivateBinPath = pathToDll };
            var newDomain = AppDomain.CreateDomain("Another Domain", null, domainSetup);
            var loaderInstance = newDomain.CreateInstanceAndUnwrap("TypeLoader", typeof(Loader).FullName) as Loader;
            loaderInstance.Load(bytedAssembly);
            var exported = loaderInstance.GetExportedTypes();

            var local = new Loader();
            var exp2 = local.GetExportedTypes();

            foreach (var name in exported)
            {
                Console.WriteLine(name);
            }
            //  ProxyClass c = (ProxyClass)newDomain.CreateInstanceFromAndUnwrap(pathToDll, typeof(ProxyClass).FullName);


            //   newDomain.Load(bytedAssembly);
            //  var obj = newDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.Name);

            // Run Script.ScriptMethod().
            RunCompilerResults(compilerResult);
        }

        private void RunCompilerResults(CompilerResults compilerResult)
        {
            if (compilerResult.Errors.Count == 0)
            {
                OnExecute(string.Concat(GetTempFilename(compilerResult), Environment.NewLine));
                try
                {
                    // Call ScriptMethod in the compiled Assembly (to execute the user's entered code).
                    compilerResult.CompiledAssembly.GetType("CScript.Script").GetMethod("ScriptMethod").Invoke(null, null);


                    // some new test code 
                    //Assembly a = Assembly.LoadFile(compilerResult.PathToAssembly);
                    //MethodInfo[] mi = a.GetType("CScript.Script").GetMethods();
                    //

                    OnExecute(string.Empty);
                    OnExecute("The code has been successfully compiled.");
                }
                catch (Exception e)
                {
                    OnExecute(e.InnerException.Message + "rn" + e.InnerException.StackTrace);
                }
            }
            else
            {
                foreach (var oline in compilerResult.Output)
                    OnExecute(oline);
            }
        }

        // Concatenation of code parts.
        public void FormatSources(string text)
        {
            string usings = FormatUsings();
            FormattedCode = string.Concat(usings, header, text, footer);
        }

        private string FormatUsings()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string using_str in Usings)
                sb.AppendFormat("using {0};{1}", using_str, Environment.NewLine);
            return sb.ToString();
        }

        private string GetTempFilename(CompilerResults assembly)
        {
            string result = "", path;
            path = assembly.TempFiles.BasePath;
            for (int i = path.Length; i > path.Length - 8; i--)
            {
                result += path[i - 1];
            }
            return  "// " + result + ".tmp";
        }
    }
}
