using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MitigatingCircumstances.Models;
using MitigatingCircumstances.Models.Static;
using System;
using System.Threading.Tasks;

namespace MitigatingCircumstances.Seeder
{
    public static class UserSeeder
    {
        public static void Initialize(IServiceProvider provider)
        {
            CreateRoles(provider).Wait();
            CreateTeachers(provider).Wait();
        }

        private static async Task CreateRoles(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[] { Roles.Student, Roles.Tutor };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task CreateTeachers(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = provider.GetRequiredService<ApplicationDbContext>();
            var testUser = await userManager.FindByEmailAsync("jakepaul@test.com");

            if (testUser != null)
            {
                return;
            }

            var teacher1 = new ApplicationUser
            {
                Firstname = "Jake",
                Lastname = "Paul",
                Email = "jakepaul@test.com",
                UserName = "jakepaul@test.com"
            };

            var teacher2 = new ApplicationUser
            {
                Firstname = "Logan",
                Lastname = "Paul",
                Email = "loganpaul@test.com",
                UserName = "loganpaul@test.com"
            };

            var teacher3 = new ApplicationUser
            {
                Firstname = "Mike",
                Lastname = "Pound",
                Email = "mikepound@test.com",
                UserName = "mikepound@test.com"
            };

            await userManager.CreateAsync(teacher1, "LoveTheBest282aaassxx!");
            await userManager.CreateAsync(teacher2, "LoveTheBest282JBHFGH!{}");
            await userManager.CreateAsync(teacher3, "LoveTheBest282x'#]][");

            await userManager.AddToRoleAsync(teacher1, Roles.Tutor);
            await userManager.AddToRoleAsync(teacher2, Roles.Tutor);
            await userManager.AddToRoleAsync(teacher3, Roles.Tutor);
        }
    }
}
