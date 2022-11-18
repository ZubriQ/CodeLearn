using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class DataType
    {
        public DataType()
        {
            TestMethodInfos = new HashSet<TestMethodInfo>();
            TestMethodParameters = new HashSet<TestMethodParameter>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShortName { get; set; } = null!;

        public virtual ICollection<TestMethodInfo> TestMethodInfos { get; set; }
        public virtual ICollection<TestMethodParameter> TestMethodParameters { get; set; }
    }
}
