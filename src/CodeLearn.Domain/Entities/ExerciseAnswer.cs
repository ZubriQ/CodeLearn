using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class ExerciseAnswer
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public int TestingResultId { get; set; }

    public string? Answer { get; set; }

    public bool IsCorrect { get; set; }

    public string? FailureInfo { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual TestingResult TestingResult { get; set; } = null!;
}
