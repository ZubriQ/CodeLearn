using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace CodeLearn
{
    // Определения делегата для организации вывода результатов выполнения частей кода 
    public delegate void ExecuteLogHandler(object message);

    public class CodeExecuter
    {
        // Код готовый к выполнению
        string formatedProgramText;

        public string LastProgramText
        {
            get { return formatedProgramText; }
        }

        // Поле делегата объявлено статическим для того, чтобы можно было
        // вызывать из программы, которая будет компилироваться
        public static ExecuteLogHandler OnExecute;

        // Список сборок, которые будут подключаться при компиляции
        private List<string> refferences = new List<string>();

        public List<string> Refferences
        {
            get { return refferences; }
            set { refferences = value; }
        }

        // Список using определений, которые будут добавлены начало кода
        private List<string> usings = new List<string>();

        public List<string> Usings
        {
            get { return usings; }
            set { usings = value; }
        }

        // Предопределенные части программы. Добавляется публичный статический
        // метод ScriptMethod, который будет вызываться из основного приложения
        // внутри используется Stopwatch для вычисления времени выполнения программы
        // Также объявлен метод Log, который вызывает OnExecute во внешней сборке (см. ниже)
        readonly string header = @"
            namespace CScript
            {
                public class Script
                {
                    public static void ScriptMethod()
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();  
            ";

        readonly string footer =
                    @" sw.Stop();Log(sw.Elapsed.ToString());
                    }
                    static void Log(object message)
                    {
                        if(CodeLearn.CodeExecuter.OnExecute != null)
                            CodeLearn.CodeExecuter.OnExecute(message);
                    }
                }
            }";

        public CodeExecuter(ExecuteLogHandler onExecute)
        {
            OnExecute += onExecute;

            // Инициализация сборок, которые будут добавлены по умолчанию
            refferences.AddRange(new string[]
                 {
                    "System.dll",
                    "System.Core.dll",
                    "System.Net.dll",
                    "System.Data.dll",
                    "System.Drawing.dll",
                    "System.Windows.Forms.dll",

                    // Необходимо добавить свою сборку, чтобы можно было вызывать OnExecute
                    Assembly.GetAssembly(typeof(CodeExecuter)).Location,
                 });

            // Инициализация using которые будут добавлены по умолчанию
            usings.AddRange(new string[]
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
                    "System.Linq",
                    "System.Windows.Forms"
             });
        }

        // Выполнение кода
        public void Execute(List<string> code)
        {
            // Форматирование сырого кода (добавление предопределенный частей)
            FormatSources(code);
            // Выполнение
            Execute();
        }

        public void Execute()
        {
            Execute(formatedProgramText);
        }

        public void Execute(string program)
        {
            // Создание класса CSHarpProvider с указанием того, что сборка генерируется в памяти
            var CSHarpProvider = CSharpCodeProvider.CreateProvider("CSharp");
            CompilerParameters compilerParams = new CompilerParameters()
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
            };

            // Добавление сборок для компиляции
            compilerParams.ReferencedAssemblies.AddRange(refferences.ToArray());

            // Компиляция
            CompilerResults compilerResult = CSHarpProvider.CompileAssemblyFromSource(compilerParams, program);
            if (compilerResult.Errors.Count == 0)
            {
                OnExecute(string.Concat(compilerResult.PathToAssembly, Environment.NewLine));
                try
                {
                    // Вызов метода ScriptMethod в сборке которая скомпилировалась
                    compilerResult.CompiledAssembly.GetType("CScript.Script").GetMethod("ScriptMethod").Invoke(null, null);
                    OnExecute(string.Empty);
                    OnExecute("Компиляция кода прошла успешно.");
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

        // Форматирование кода (добавление предопределенных частей)
        public string FormatSources(string text)
        {
            string usings = FormatUsings();
            formatedProgramText = string.Concat(usings, header, text, footer);
            return formatedProgramText;
        }

        public string FormatSources(List<string> code)
        {
            StringBuilder sb = new StringBuilder(header);
            foreach (var sc in code)
            {
                sb.AppendLine(sc);
            }
            sb.AppendLine(footer);
            formatedProgramText = sb.ToString();
            return formatedProgramText;
        }

        // Форматирование определений using
        private string FormatUsings()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string using_str in usings)
                sb.AppendFormat("using {0};{1}", using_str, Environment.NewLine);
            return sb.ToString();
        }
    }
}
