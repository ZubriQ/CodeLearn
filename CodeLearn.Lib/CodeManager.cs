using CodeLearn.Db;
using System.Text;
using System.Xml.Linq;

namespace CodeLearn.Lib
{
    public class CodeManager
    {
        private CodeFormatter Formatter { get; set; } = new();

        private CodeTester Tester { get; set; } = new();

        public Exercise[] Exercises { get; private set; }

        public string[] ExerciseAnswers { get; private set; }

        public CodeManager()
        {
            ExerciseAnswers = new string[0];
            Exercises = new Exercise[0];
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
            this.Exercises = exercises;
            this.ExerciseAnswers = exerciseAnswers;
            FormatAnswers();
            CompileAndTestAnswers();
        }

        private void FormatAnswers()
        {
            for (int i = 0; i < ExerciseAnswers.Length; i++)
            {
                ExerciseAnswers[i] = Formatter.FormatSources(ExerciseAnswers[i], Exercises[i].ClassName);
            }
        }

        private void CompileAndTestAnswers()
        {
            for (int i = 0; i < Exercises.Length; i++)
            {
                bool success = CodeCompiler.Compile(ExerciseAnswers[i]);
                if (success)
                {
                    Tester.LoadExerciseData(Exercises[i]);
                    Tester.Test();
                }
            }
        }
    }
}