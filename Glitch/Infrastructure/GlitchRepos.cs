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

            return services;
        }
    }
}
