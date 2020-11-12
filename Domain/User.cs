﻿using Microsoft.AspNetCore.Identity;
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
        public bool Enabled { get; set; }
    }
}
