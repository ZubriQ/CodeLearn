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

    public Result UpdateDetails(string name, int enrolmentYear)
    {
        if (string.IsNullOrEmpty(name) || name.Length > 50)
        {
            return Result.Failure(DomainErrors.StudentGroup.InvalidNameLength);
        }

        if (enrolmentYear < 2020 || enrolmentYear > 2100)
        {
            return Result.Failure(DomainErrors.StudentGroup.InvalidEnrolmentYear);
        }

        Name = name;
        EnrolmentYear = enrolmentYear;

        return Result.Success();
    }
}