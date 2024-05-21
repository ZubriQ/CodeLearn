using Microsoft.AspNetCore.Http;

namespace CodeLearn.Contracts.Users.Students;

public record ImportStudentListRequest(IFormFile File, string StudentGroupName);