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
                    await userStore.AddToRoleAsync(admin, "Admin");
                    var listOfUsers = CreateUsers();
                    foreach (var user in listOfUsers)
                    {
                        user.PasswordHash = password.HashPassword(user, "User111");
                        await userStore.CreateAsync(user);
                        await userStore.AddToRoleAsync(user, "PlaceOwner");
                    
                    }

                }
                if (!context.Places.Any())
                {
                    context.CreatePlaces();
                }

                await context.SaveChangesAsync();
            });
        }
        public static List<User> CreateUsers()
        {
            return new List<User>
            {
                new User { FirstName = "Vitaliy", LastName = "Kim", Email = "korea@gmail.com", PhoneNumber = "+3890656560", UserName = "korea@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")},
                new User { FirstName = "Dmytro", LastName = "Voitkiv", Email = "prisno@gmail.com", PhoneNumber = "+303058939", UserName = "prisno@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")},
                new User { FirstName = "Max", LastName = "Kuk", Email = "gorpyn@ukr.net", PhoneNumber = "+338535735", UserName = "gorpyn@ukr.net", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")},
                new User { FirstName = "Nadiya", LastName = "Khasyshyn", Email = "ruby@gmail.com", PhoneNumber = "+3093053933", UserName = "ruby@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")},
                new User { FirstName = "Sanya", LastName = "Orlovskii", Email = "zakarpattya@gmail.com", UserName = "zakarpattya@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")},
                new User { FirstName = "Yarik", LastName = "Sobko", Email= "poshta@gmail.com", UserName = "poshta@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D") },
                new User { FirstName = "Mykola", LastName = "Pavlyk", Email = "grandbityyy67@gmail.com", UserName = "grandbityyy67@gmail.com", EmailConfirmed = true, PhoneNumberConfirmed = true, SecurityStamp = Guid.NewGuid().ToString("D")}
            };
        }
        public static GlitchContext CreatePlaces(this GlitchContext context)
        {
            var listOfPlaces = new List<Place>();
            using (var streamReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Helpers\Seed\Places.json"), Encoding.GetEncoding(1251)))
            {
                string jsonString = streamReader.ReadToEnd();
                listOfPlaces =
                    JsonConvert.DeserializeObject<List<Place>>(jsonString);
            }
            context.Places.AddRange(listOfPlaces);
            return context;
        }
        
    }
}
