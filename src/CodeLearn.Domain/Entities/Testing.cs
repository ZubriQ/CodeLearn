using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class Testing
{
    public int Id { get; set; }

    public int CreatorId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int DurationInMinutes { get; set; }

    public bool IsPublic { get; set; }

    public string? Feedback { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Teacher Creator { get; set; } = null!;

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<TestingResult> TestingResults { get; set; } = new List<TestingResult>();

    public virtual ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
}
