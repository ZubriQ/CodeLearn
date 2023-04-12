using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class Teacher : ApplicationUser
    {
        public Teacher()
        {
            Testings = new HashSet<Testing>();
        }

        public virtual ICollection<Testing> Testings { get; set; }
    }
}
