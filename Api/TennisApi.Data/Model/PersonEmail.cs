using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class PersonEmail
    {
        public Guid PersonID { get; set; }
        public Guid EmailID { get; set; }
    }
}
