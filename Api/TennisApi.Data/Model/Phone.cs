using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Phone
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
    }
}
