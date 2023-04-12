using Microsoft.AspNetCore.Identity;

namespace CodeLearn.Db
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }

        public int? GroupId { get; set; }
        public bool IsTeacher { get; set; }
    }
}
