using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class AccountPhone
    {
        public Guid AccountID { get; set; }
        public Guid PhoneID { get; set; }
    }
}
