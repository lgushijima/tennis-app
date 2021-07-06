using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Person
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
