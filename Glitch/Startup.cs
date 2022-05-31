﻿
using Glitch.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Glitch.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Glitch.Helpers.Seed;
using AutoMapper;
using Glitch.Infrastructure;

namespace Glitch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            MapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfiles()); });
        }
        private MapperConfiguration MapperConfiguration { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<GlitchContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GlitchSqlDb")));

            services.AddSingleton(c => MapperConfiguration.CreateMapper());

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader());
                // .AllowAnyOrigin());
            });

            services.AddMvc();

            services.AddIdentity<User, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
                options.Password.RequireNonAlphanumeric = false;
            })  
            .AddEntityFrameworkStores<GlitchContext>()
            .AddDefaultTokenProviders();

            services.AddGlitchServices(Configuration);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            SeedData.Initialize(app.ApplicationServices);
        }
    }
}