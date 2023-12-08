using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class TestCase
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public string CorrectOutputValue { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ICollection<TestCaseParameter> TestCaseParameters { get; set; } = new List<TestCaseParameter>();
}
