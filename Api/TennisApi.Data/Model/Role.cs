using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Role
    {
        public Guid ID { get; set; }
        public Guid? AccountID { get; set; }
        public string Name { get; set; }
        public bool System { get; set; }
    }
}
