namespace CodeLearn.Domain.StudentGroups;

public sealed class StudentGroup : BaseAuditableEntity<StudentGroupId>, IAggregateRoot
{
    public string Name { get; private set; }
    public int EnrolmentYear { get; private set; }
    
    private StudentGroup(string name, int enrolmentYear)
        : base(default!)
    {
        Name = name;
        EnrolmentYear = enrolmentYear;
    }

    public static StudentGroup Create(string name, int enrolmentYear)
    {
        return new StudentGroup(
            name,
            enrolmentYear);
    }
}