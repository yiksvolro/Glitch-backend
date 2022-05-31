using Glitch.Repositories;
using Glitch.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Glitch.Infrastructure
{
    public static class GlitchRepos
    {
        public static IServiceCollection AddGlitchRepos(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IFileRepository, FileRepository>();

            return services;
        }
    }
}
