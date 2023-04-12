using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Testing
    {
        public Testing()
        {
            TestingResults = new HashSet<TestingResult>();
            Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationInMinutes { get; set; }

        public string? TestCreatorId { get; set; }
        public virtual Teacher? TestCreator { get; set; }

        public virtual ICollection<TestingResult> TestingResults { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
