using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class TestMethodParameter
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public int DataTypeId { get; set; }

    public int Position { get; set; }

    public virtual DataType DataType { get; set; } = null!;

    public virtual Exercise Exercise { get; set; } = null!;
}
