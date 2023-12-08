using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class TestingResult
{
    public int Id { get; set; }

    public int TestingId { get; set; }

    public int StudentId { get; set; }

    public int Score { get; set; }

    public DateTime CompletionDate { get; set; }

    public virtual ICollection<ExerciseAnswer> ExerciseAnswers { get; set; } = new List<ExerciseAnswer>();

    public virtual Student Student { get; set; } = null!;

    public virtual Testing Testing { get; set; } = null!;
}
