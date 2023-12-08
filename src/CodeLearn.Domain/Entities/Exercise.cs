using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class Exercise
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Difficulty { get; set; } = null!;

    public virtual ICollection<ExerciseAnswer> ExerciseAnswers { get; set; } = new List<ExerciseAnswer>();

    public virtual ICollection<ExerciseNote> ExerciseNotes { get; set; } = new List<ExerciseNote>();

    public virtual Testing Test { get; set; } = null!;

    public virtual ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();

    public virtual ICollection<TestMethodParameter> TestMethodParameters { get; set; } = new List<TestMethodParameter>();

    public virtual ICollection<ExerciseTopic> Topics { get; set; } = new List<ExerciseTopic>();
}
