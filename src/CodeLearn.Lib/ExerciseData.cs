using CodeLearn.Db;

namespace CodeLearn.Lib
{
    public class ExerciseData
    {
        public Exercise Exercise { get; set; }
        public TestMethodInfo TestMethodInfo { get; set; }
        public List<TestCase> TestCases { get; set; }

        public ExerciseData(Exercise exercise)
        {
            Exercise = exercise;
            TestMethodInfo = exercise.TestMethodInfos.First();
            TestCases = TestMethodInfo.TestCases.ToList();
        }
    }
}
