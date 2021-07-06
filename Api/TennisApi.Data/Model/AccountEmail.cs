using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class AccountEmail
    {
        public Guid AccountID { get; set; }
        public Guid EmailID { get; set; }
    }
}
