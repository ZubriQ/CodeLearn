using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class TestCase
    {
        public TestCase()
        {
            TestCaseParameters = new HashSet<TestCaseParameter>();
        }

        public int Id { get; set; }
        public string Result { get; set; } = null!;
        public int TestMethodId { get; set; }

        public virtual TestMethodInfo TestMethod { get; set; } = null!;
        public virtual ICollection<TestCaseParameter> TestCaseParameters { get; set; }
    }
}
