using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class ExerciseType
    {
        public ExerciseType()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
