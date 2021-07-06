using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class PersonAddress
    {
        public Guid PersonID { get; set; }
        public Guid AddressID { get; set; }
    }
}
