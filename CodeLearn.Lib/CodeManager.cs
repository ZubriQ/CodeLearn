using CodeLearn.Db;
using System.Text;
using System.Xml.Linq;

namespace CodeLearn.Lib
{
    public class CodeManager
    {
        private CodeFormatter Formatter { get; set; } = new();

        private CodeTester Tester { get; set; } = new();

        public CodeManager()
        {

        }

        public void CompileAndTestMethod(string methodCode, Exercise exercise)
        {
            string formattedCode = Formatter.FormatSources(methodCode, exercise.ClassName);
            bool success = CodeCompiler.Compile(formattedCode);
            if (success)
            {
                Tester.LoadExerciseData(exercise);
                Tester.Test();
            }
        }
    }
}