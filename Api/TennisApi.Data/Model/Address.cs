using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Address
    {
        public Guid ID { get; set; }
        public string Address1 { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
