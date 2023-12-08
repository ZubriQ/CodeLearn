using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class ExerciseNote
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public string? Entry { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;
}
