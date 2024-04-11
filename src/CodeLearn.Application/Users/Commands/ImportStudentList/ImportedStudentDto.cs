namespace CodeLearn.Application.Users.Commands.ImportStudentList;

public class ImportedStudentDto
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string StudentGroup { get; set; } = null!;

    public string UserCode { get; set; } = null!;
}