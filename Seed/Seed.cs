using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData
{
    public class Seed
    {
        public static void SeedRoles(RoleManager<Role> rolesManager)
        {
            if (!rolesManager.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Client"},
                    new Role { Name = "Administrator"},
                    new Role { Name = "Developper"}
                };

                foreach (var role in roles)
                {
                    rolesManager.CreateAsync(role).Wait();
                }
            }


        }
    }
}
