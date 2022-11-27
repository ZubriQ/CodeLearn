using CodeLearn.Db;
using System.Text;
using System.Xml.Linq;

namespace CodeLearn.Lib
{
    public class CodeManager
    {
        private CodeFormatter Formatter { get; set; } = new();

        private CodeTester Tester { get; set; } = new();

        private Exercise[] exercises;

        private string[] exerciseAnswers;

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

        public void CompileAndTest(string[] exerciseAnswers, Exercise[] exercises)
        {
            this.exercises = exercises;
            this.exerciseAnswers = exerciseAnswers;
            FormatAnswers();
            CompileAndTestAnswers();
        }

        private void FormatAnswers()
        {
            for (int i = 0; i < exerciseAnswers.Length; i++)
            {
                exerciseAnswers[i] = Formatter.FormatSources(exerciseAnswers[i], exercises[i].ClassName);
            }
        }

        private void CompileAndTestAnswers()
        {
            for (int i = 0; i < exercises.Length; i++)
            {
                bool success = CodeCompiler.Compile(exerciseAnswers[i]);
                if (success)
                {
                    Tester.LoadExerciseData(exercises[i]);
                    Tester.Test();
                }
            }
        }
    }
}