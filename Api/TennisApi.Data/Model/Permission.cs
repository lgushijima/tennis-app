using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Permission
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
