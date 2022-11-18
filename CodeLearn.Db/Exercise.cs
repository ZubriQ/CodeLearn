using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Exercise
    {
        public Exercise()
        {
            TestMethodInfos = new HashSet<TestMethodInfo>();
            TestingAnswers = new HashSet<TestingAnswer>();
            Courses = new HashSet<Testing>();
        }

        public int Id { get; set; }
        public string ExerciseDescription { get; set; } = null!;
        public string Context { get; set; } = null!;
        public string? CodingArea { get; set; }
        public int ExerciseTypeId { get; set; }
        public string? OptionalUsings { get; set; }
        public string? OptionalDlls { get; set; }
        public string? ClassName { get; set; }
        public int Score { get; set; }

        public virtual ExerciseType ExerciseType { get; set; } = null!;
        public virtual ICollection<TestMethodInfo> TestMethodInfos { get; set; }
        public virtual ICollection<TestingAnswer> TestingAnswers { get; set; }

        public virtual ICollection<Testing> Courses { get; set; }
    }
}
