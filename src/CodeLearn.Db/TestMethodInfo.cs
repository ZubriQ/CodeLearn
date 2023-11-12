using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class TestMethodInfo
    {
        public TestMethodInfo()
        {
            TestCases = new HashSet<TestCase>();
            TestMethodParameters = new HashSet<TestMethodParameter>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ReturnTypeId { get; set; }
        public int ExerciseId { get; set; }

        public virtual Exercise Exercise { get; set; } = null!;
        public virtual DataType ReturnType { get; set; } = null!;

        public virtual ICollection<TestCase> TestCases { get; set; }
        public virtual ICollection<TestMethodParameter> TestMethodParameters { get; set; }
    }
}
