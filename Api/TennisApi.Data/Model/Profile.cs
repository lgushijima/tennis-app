using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Profile
    {
        public Guid ID { get; set; }
        public Guid LoginID { get; set; }
        public Guid PersonID { get; set; }
        public Guid RoleID { get; set; }
    }
}
