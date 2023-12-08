using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class DataType
{
    public int Id { get; set; }

    public string SystemName { get; set; } = null!;

    public string Alias { get; set; } = null!;

    public virtual ICollection<TestMethodParameter> TestMethodParameters { get; set; } = new List<TestMethodParameter>();
}
