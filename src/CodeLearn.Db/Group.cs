using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Year { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
