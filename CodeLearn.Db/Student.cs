using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Student : ApplicationUser
    {
        public Student()
        {
            TestingResults = new HashSet<TestingResult>();
        }

        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<TestingResult> TestingResults { get; set; }
    }
}
