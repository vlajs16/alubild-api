using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Role : IdentityRole<long>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
