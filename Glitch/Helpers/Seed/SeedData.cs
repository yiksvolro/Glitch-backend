using Glitch.Helpers.Seed;
using Glitch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Glitch.Helpers
{
    public static class SeedData
    {
        private static readonly string simplePass = "!Admin11";
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Task.Run(async () =>
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<GlitchContext>();

                string[] roles = new string[] { "Admin", "PlaceOwner", "User" };

                foreach (string role in roles)
                {
                    var roleStore = new AppRoleStore(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new AppRole(role));
                    }
                }


                var user = new User
                {
                    FirstName = "SuperAdmin",
                    LastName = "Admin",
                    Email = "xxxx@example.com",
                    NormalizedEmail = "XXXX@EXAMPLE.COM",
                    UserName = "xxxx@example.com",
                    NormalizedUserName = "OWNER",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };


                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user, simplePass);
                    user.PasswordHash = hashed;

                    var userStore = new AppUserStore(context);
                    var result = userStore.CreateAsync(user);

                }
                await AssignRoles(scope.ServiceProvider, user.Email, roles);

                await context.SaveChangesAsync();
            });
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<User> _userManager = services.GetService<UserManager<User>>();
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

        
    }
}
