using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserLog> UserLogs { get; set; }
        public bool Enabled { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
