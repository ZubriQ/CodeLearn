namespace CodeLearn.Domain.StudentGroups;

public sealed class StudentGroup : BaseEntity<StudentGroupId>, IAggregateRoot // TODO: Aggregate root, Auditable
{
    public string Name { get; private set; } = null!;
    public int EnrolmentYear { get; private set; }

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