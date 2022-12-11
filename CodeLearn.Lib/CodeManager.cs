using CodeLearn.Db;
using System.Text;
using System.Xml.Linq;

namespace CodeLearn.Lib
{
    public class CodeManager
    {
        private CodeFormatter Formatter { get; set; } = new();
        private CodeTester Tester { get; set; } = new();

        public TestingResult TestingResult { get; private set; }
        public Exercise[] Exercises { get; private set; }
        public string[] ExerciseAnswers { get; private set; }

        public CodeManager()
        {
            ExerciseAnswers = new string[0];
            Exercises = new Exercise[0];
        }

        public bool CompileAndTestMethod(string methodCode, Exercise exercise)
        {
            bool success = false;
            string formattedCode = Formatter.FormatSources(methodCode, exercise.ClassName);
            bool isCompiled = CodeCompiler.Compile(formattedCode);
            if (isCompiled)
            {
                Tester.LoadExerciseData(exercise);
                success = Tester.TestLoadedExercise();
            }
            return success;
        }

        public TestingResult CompileAndTest(string[] exerciseAnswers, Exercise[] exercises)
        {
            this.Exercises = exercises;
            this.ExerciseAnswers = exerciseAnswers;
            FormatAnswers();
            return CompileAndTestAnswers();
        }

        private void FormatAnswers()
        {
            for (int i = 0; i < ExerciseAnswers.Length; i++)
            {
                ExerciseAnswers[i] = Formatter.FormatSources(ExerciseAnswers[i], Exercises[i].ClassName);
            }
        }

        private TestingResult CompileAndTestAnswers()
        {
            TestingAnswer[] testingAnswers = TestingResult.TestingAnswers.ToArray();
            int scoreSum = 0;
            for (int i = 0; i < Exercises.Length; i++)
            {
                if (CodeCompiler.Compile(ExerciseAnswers[i]))
                {
                    Tester.LoadExerciseData(Exercises[i]);
                    bool isPassed = Tester.TestLoadedExercise();
                    AssignAnswerData(testingAnswers[i], i, isPassed);
                    scoreSum += GetScore(i, isPassed);
                }
            }
            TestingResult.Score = scoreSum;
            return TestingResult;
        }

        private void AssignAnswerData(TestingAnswer testingAnswer, int index, bool isPassed)
        {
            testingAnswer.IsCorrect = isPassed;
            testingAnswer.Exercise = Exercises[index];
            testingAnswer.Answer = Exercises[index].CodingArea;
        }

        private int GetScore(int index, bool isPassed)
        {
            if (isPassed)
            {
                return Exercises[index].Score;
            }
            else
            {
                return 0;
            }
        }

        public void SetTestingResultData(TestingResult testingResult)
        {
            TestingResult = testingResult;
        }
    }
}