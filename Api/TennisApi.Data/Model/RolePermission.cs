using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class RolePermission
    {
        public Guid RoleID { get; set; }
        public Guid PermissionID { get; set; }
        public string Value { get; set; }
    }
}
