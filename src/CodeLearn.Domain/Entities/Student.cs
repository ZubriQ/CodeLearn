using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public int GroupId { get; set; }

    public string Username { get; set; } = null!;

    public virtual StudentGroup Group { get; set; } = null!;

    public virtual ICollection<TestingResult> TestingResults { get; set; } = new List<TestingResult>();
}
