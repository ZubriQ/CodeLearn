using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class ExerciseTopic
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
