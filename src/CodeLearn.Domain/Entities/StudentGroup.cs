using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class StudentGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}
