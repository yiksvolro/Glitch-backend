using Glitch.Helpers.Seed;
using Glitch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Helpers
{
    public static class SeedData
    {
        private static readonly string simplePass = "!Admin11";
        public static List<Place> listOfPlaces;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Task.Run(async () =>
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<GlitchContext>();
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

                string[] roles = new string[] { "Admin", "PlaceOwner", "User" };

                foreach (string role in roles)
                {
                    var roleStore = new AppRoleStore(context);

                    if (!context.Roles.Any(r => r.Name == role))
                    {
                        await roleStore.CreateAsync(new AppRole(role));
                    }
                }


                var admin = new User
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


                if (!context.Users.Any())
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(admin, simplePass);
                    admin.PasswordHash = hashed;

                    var userStore = new AppUserStore(context);
                    await userStore.CreateAsync(admin);
                    await AssignRoles(userManager, admin.Email, roles);


                    var listOfUsers = CreateUsers();
                    foreach (var user in listOfUsers)
                    {
                        user.PasswordHash = password.HashPassword(user, "User111");
                        await userStore.CreateAsync(user);
                        await AssignRoles(userManager, user.Email, new string[] { "PlaceOwner" });
                    
                    }

                }
                if (!context.Places.Any())
                {
                    context.CreatePlaces();
                }

                if (!context.Files.Any())
                {
                    context.CreateFiles();
                }

                await context.SaveChangesAsync();
            });
        }
        public static List<User> CreateUsers()
        {
            return new List<User>
            {
                new User { FirstName = "Vitaliy", LastName = "Kim", Email = "korea@gmail.com", PhoneNumber = "+3890656560", UserName = "korea@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "korea@gmail.com".ToUpper()},
                new User { FirstName = "Dmytro", LastName = "Voitkiv", Email = "prisno@gmail.com", PhoneNumber = "+303058939", UserName = "prisno@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "prisno@gmail.com".ToUpper()},
                new User { FirstName = "Max", LastName = "Kuk", Email = "gorpyn@ukr.net", PhoneNumber = "+338535735", UserName = "gorpyn@ukr.net", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "gorpyn@ukr.net".ToUpper()},
                new User { FirstName = "Nadiya", LastName = "Khasyshyn", Email = "ruby@gmail.com", PhoneNumber = "+3093053933", UserName = "ruby@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "ruby@gmail.com".ToUpper()},
                new User { FirstName = "Sanya", LastName = "Orlovskii", Email = "zakarpattya@gmail.com", UserName = "zakarpattya@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "zakarpattya@gmail.com".ToUpper()},
                new User { FirstName = "Yarik", LastName = "Sobko", Email= "poshta@gmail.com", UserName = "poshta@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "poshta@gmail.com".ToUpper()},
                new User { FirstName = "Mykola", LastName = "Pavlyk", Email = "grandbityyy67@gmail.com", UserName = "grandbityyy67@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D"), NormalizedEmail = "grandbityyy67@gmail.com".ToUpper()}
            };
        }
        public static async Task<IdentityResult> AssignRoles(UserManager<User> _userManager, string email, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
        public static GlitchContext CreatePlaces(this GlitchContext context)
        {
            using (var streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Helpers\Seed\Places.json"), Encoding.GetEncoding(1251)))
            {
                string jsonString = streamReader.ReadToEnd();
                listOfPlaces =
                    JsonConvert.DeserializeObject<List<Place>>(jsonString);
            }
            context.Places.AddRange(listOfPlaces);
            return context;
        }
        public static GlitchContext CreateFiles(this GlitchContext context)
        {
            var listOfFiles = new List<Glitch.Models.File>();
            using (var streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Helpers\Seed\Files.json"), Encoding.GetEncoding(1251)))
            {
                string jsonString = streamReader.ReadToEnd();
                listOfFiles =
                    JsonConvert.DeserializeObject<List<Glitch.Models.File>>(jsonString);
            }
            listOfFiles.ForEach(file => file.Place = listOfPlaces[file.PlaceId - 1]);
            context.Files.AddRange(listOfFiles);
            return context;
        }
        
    }
}
