using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Court
    {
        public Guid ID { get; set; }
        public Guid AccountID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Indoor { get; set; }
        public bool ScoreBoard { get; set; }
        public string Size { get; set; }
        public string Ilumination { get; set; }
        public int Status { get; set; }
    }
}
