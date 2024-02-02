namespace CodeLearn.Domain.StudentGroups;

public sealed class StudentGroup : BaseAuditableEntity<StudentGroupId>, IAggregateRoot
{
    public string Name { get; private set; }
    public int EnrolmentYear { get; private set; }
    
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