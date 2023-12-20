namespace CodeLearn.Domain.Students.Entities;

public sealed class StudentGroup : BaseEntity<StudentGroupId> // Can be an aggregate root.
{
    public string Name { get; set; } = null!;
    public int EnrolmentYear { get; set; }

    private StudentGroup() { }

    private StudentGroup(StudentGroupId id, string name, int enrolmentYear)
        : base(id)
    {
        Name = name;
        EnrolmentYear = enrolmentYear;
    }

    public static StudentGroup Create(string name, int enrolmentYear)
    {
        return new StudentGroup(
            StudentGroupId.CreateUnique(),
            name,
            enrolmentYear);
    }
}