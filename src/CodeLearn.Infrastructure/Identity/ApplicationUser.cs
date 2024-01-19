using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Organization { get; set; } = null!;
    public string StudentGroupName { get; set; } = null!;
    public int EnrolmentYear { get; set; }
}