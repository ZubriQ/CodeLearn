using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Teacher
    {
        public Teacher()
        {
            Testings = new HashSet<Testing>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Testing> Testings { get; set; }
    }
}
