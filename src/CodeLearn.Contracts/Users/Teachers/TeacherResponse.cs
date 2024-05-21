namespace CodeLearn.Contracts.Users.Teachers;

public record TeacherResponse(
    string Id,
    string UserName,
    string FullName,
    string TemporaryPassword);