using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}
