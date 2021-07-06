using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TennisApi.Data.Model;
using System.Linq;

namespace TennisApi.Data.Extensions
{
    public static class PersonExtension
    {
        public static List<Address> FindByProfileId(this DbSet<PersonAddress> dbset, TennisDBContext context, Guid personID)
        {
            return (from p in context.PersonAddress
                    join a in context.Address on p.AddressID equals a.ID
                    where p.PersonID == personID
                    select a).ToList();

        }
    }
}
