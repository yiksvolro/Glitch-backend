using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Glitch.Services;
using Glitch.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Glitch.Infrastructure
{
    public static class GlitchServices
    {
        public static IServiceCollection AddGlitchServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAuthentication("MyCookie")
            .AddCookie("MyCookie", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });

            services.ConfigureApplicationCookie(options => {
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddAWSService<IAmazonS3>(new AWSOptions
            {
                Credentials = new BasicAWSCredentials(configuration.GetSection("AWS").GetSection("AccessKeyId").Value, configuration.GetSection("AWS").GetSection("SecretAccessKey").Value),
                Region = RegionEndpoint.GetBySystemName(configuration.GetSection("AWS").GetSection("RegionEndpoint").Value)
            });
            // Import Repositories
            services.AddGlitchRepos();

            // Import All Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}
