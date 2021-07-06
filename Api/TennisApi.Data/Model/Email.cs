using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Email
    {
        public Guid ID { get; set; }
        public string Email1 { get; set; }
        public string Type { get; set; }
    }
}
