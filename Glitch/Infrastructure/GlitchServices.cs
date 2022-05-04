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
            //services.AddIdentity<IdentityAuthUser, IdentityAuthRole>(opt =>
            //{
            //    opt.SignIn.RequireConfirmedEmail = true;
            //})
            //.AddEntityFrameworkStores<TrackerContext>();
            services.AddAuthentication("MyCookie")
            .AddCookie("MyCookie", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });


            // Import Repositories
            services.AddGlitchRepos();

            // Import All Services
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
