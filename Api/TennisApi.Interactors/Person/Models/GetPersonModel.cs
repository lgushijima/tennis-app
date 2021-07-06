using System;
using System.Collections.Generic;
using System.Text;

namespace TennisApi.Interactors.Person.Models
{
    public class GetPersonModel
    {
        public Guid ProfileID { get; set; }
        public Data.Model.Person Person { get; set; }
        public Data.Model.Address[] Addresses { get; set; }
        public Data.Model.Phone[] Phones { get; set; }
        public Data.Model.Email[] Emails { get; set; }
    }
}
