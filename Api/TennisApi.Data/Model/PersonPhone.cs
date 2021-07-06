using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class PersonPhone
    {
        public Guid PersonID { get; set; }
        public Guid PhoneID { get; set; }
    }
}
