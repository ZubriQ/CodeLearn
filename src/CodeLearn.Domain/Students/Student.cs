namespace CodeLearn.Domain.Students;

public sealed class Student : BaseEntity<StudentId>, IAggregateRoot
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? Patronymic { get; private set; }
    public StudentGroupId StudentGroupId { get; private set; } = null!;
    public StudentGroup StudentGroup { get; private set; } = null!;

    private Student() { }

    private Student(
        StudentId id,
        string firstName,
        string lastName,
        string? patronymic)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }

    public static Student Create(
        StudentGroup studentGroup,
        string firstName,
        string lastName,
        string? patronymic = null)
    {
        var student = new Student(
            StudentId.CreateUnique(),
            firstName,
            lastName,
            patronymic)
        {
            StudentGroup = studentGroup,
            StudentGroupId = studentGroup.Id,
        };

        return student;
    }
}