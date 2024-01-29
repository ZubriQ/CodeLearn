using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? Organization { get; set; }
    public string? StudentGroupName { get; set; }
    public int? EnrolmentYear { get; set; }
}