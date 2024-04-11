using Microsoft.AspNetCore.Http;

namespace CodeLearn.Contracts.Users;

public record ImportStudentListRequest(IFormFile File, string StudentGroupName);