using System;
using System.Collections.Generic;

namespace CodeLearn.Domain.Entities;

public partial class TestCaseParameter
{
    public int Id { get; set; }

    public int TestCaseId { get; set; }

    public string Value { get; set; } = null!;

    public int Position { get; set; }

    public virtual TestCase TestCase { get; set; } = null!;
}
