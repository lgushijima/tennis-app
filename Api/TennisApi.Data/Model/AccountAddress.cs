using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class AccountAddress
    {
        public Guid AccountID { get; set; }
        public Guid AddressID { get; set; }
    }
}
