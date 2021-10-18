using HeadHunter.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter
{
    public class AdminInitializer
    {
        public static async System.Threading.Tasks.Task SeedAdminUser(
            RoleManager<IdentityRole> _roleManager)
        {
            var roles = new[] { "applicant", "boss" };
            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) is null)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
