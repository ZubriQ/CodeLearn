using System;
using System.Collections.Generic;

namespace CodeLearn.Db
{
    public partial class TestMethodParameter
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; }
        public int TestMethodInfoId { get; set; }
        public int Position { get; set; }

        public virtual DataType DataType { get; set; } = null!;
        public virtual TestMethodInfo TestMethodInfo { get; set; } = null!;
    }
}
