namespace CodeLearn.Domain.Students.Entities;

public sealed class StudentGroup : BaseEntity<StudentGroupId> // Can be an aggregate root.
{
    public string Name { get; set; } = null!;
    public int Year { get; set; }

    private StudentGroup() { }

    private StudentGroup(StudentGroupId id, string name, int year) 
        : base(id)
    {
        Name = name;
        Year = year;
    }

    public static StudentGroup Create(string name, int year)
    {
        return new StudentGroup(
            StudentGroupId.CreateUnique(),
            name,
            year);
    }
}