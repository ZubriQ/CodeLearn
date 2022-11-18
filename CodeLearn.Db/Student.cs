using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Student
    {
        public Student()
        {
            TestingResults = new HashSet<TestingResult>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Patronymic { get; set; }
        public int GroupId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<TestingResult> TestingResults { get; set; }
    }
}
