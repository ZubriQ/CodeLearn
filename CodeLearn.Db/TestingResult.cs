using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class TestingResult
    {
        public TestingResult()
        {
            TestingAnswers = new HashSet<TestingAnswer>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public DateTime PassingDate { get; set; }

        public virtual Testing Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual ICollection<TestingAnswer> TestingAnswers { get; set; }
    }
}
