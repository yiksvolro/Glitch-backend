using Glitch.Services;
using Glitch.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Glitch.Infrastructure
{
    public static class GlitchServices
    {
        public static IServiceCollection AddGlitchServices(this IServiceCollection services)
        {
            
            services.AddAuthentication("MyCookie")
            .AddCookie("MyCookie", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });


            // Import Repositories
            services.AddGlitchRepos();

            // Import All Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ITableService, TableService>();

            return services;
        }
    }
}
