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
            var testUser = await userManager.FindByEmailAsync("johndoe1337@mailinator.com");

            if (testUser != null)
            {
                return;
            }

            var teacher1 = new ApplicationUser
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "johndoe1337@mailinator.com",
                UserName = "johndoe1337@mailinator.com"
            };

            var teacher2 = new ApplicationUser
            {
                Firstname = "Mary",
                Lastname = "Doe",
                Email = "marydoe1337@mailinator.com",
                UserName = "marydoe1337@mailinator.com"
            };

            await userManager.CreateAsync(teacher1, "JohnDoe1337!");
            await userManager.CreateAsync(teacher2, "MaryDoe1337!");

            await userManager.AddToRoleAsync(teacher1, Roles.Tutor);
            await userManager.AddToRoleAsync(teacher2, Roles.Tutor);
        }
    }
}
