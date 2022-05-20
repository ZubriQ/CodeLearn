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

        public string ClassName { get; set; }

        public string MethodName { get; set; } // TODO: or methodS in the future?

        // Method ***ScriptMethod*** is used from the main application.
        // Method ***Log*** calls OnExecute delegate in the external assembly.
        private string _header;
        // Then entered program text by a user ...
        private string _footer;

        // Delegate, DLLs, Usings initialization. 
        // TODO: Determine which DLLs and Usings the program needs
        public CodeExecuter(ExecuteLogHandler onExecute, string className, string methodName)
        {
            OnExecute += onExecute;
            ClassName = className;
            MethodName = methodName;
            InitializeCodeParts();
            InitializeDlls();
            InitializeUsings();
        }

        void InitializeDlls()
        {
            Refferences.AddRange(new string[]
                 {
                    "System.dll",
                    "System.Core.dll",
                    "System.Net.dll",
                    "System.Data.dll",
                    // It's necessary to add our Assembly to be able to call OnExecute.
                    Assembly.GetAssembly(typeof(CodeExecuter)).Location
                 });
        }
        void InitializeUsings()
        {
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
        void InitializeCodeParts()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append()
            _header = @"
            namespace CScript
            {
                [Serializable]
                public class " + ClassName + @"
                {
            ";
            // User's code.
            _footer = @"
                static void Log(object message)
                {
                    if (CodeLearn.CodeExecuter.OnExecute != null)
                     CodeLearn.CodeExecuter.OnExecute(message);
                }
            }
        }";
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

            // TODO: Fix error
            //var local = new Loader();
            //var exp2 = local.GetExportedTypes(); 

            foreach (var name in exported)
            {
                Console.WriteLine(name);
            }
            //ProxyClass c = (ProxyClass)newDomain.CreateInstanceFromAndUnwrap(pathToDll, typeof(ProxyClass).FullName);

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
                    MethodInfo method = compilerResult.CompiledAssembly.GetType("CScript." + ClassName).GetMethod(MethodName);
                    object d = method.Invoke(null, new object[] { 5, 5 });
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
            FormattedCode = string.Concat(usings, _header, text, _footer);
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
            return "// " + result + ".tmp";
        }
    }
}
