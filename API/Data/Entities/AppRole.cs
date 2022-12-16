using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() :base()
        {

        }
        public AppRole(string role) :base(role)
        {

        }

        public IEnumerable<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
    }
}