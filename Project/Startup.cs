using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Controllers;
using Project.Data;
using Project.Data.Models;
using Project.Infrastructure.Extensions;
using Project.Services.Creators;
using Project.Services.Minerals;
using Project.Services.Moons;
using Project.Services.PlanetarySystems;
using Project.Services.Planets;
using Project.Services.Statistics;

namespace Project
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ProjectDbContext>(options => options
                .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ProjectDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add <AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IMoonService, MoonService>();
            services.AddTransient<IPlanetarySystemService, PlanetarySystemService>();
            services.AddTransient<IMineralService, MineralService>();
            services.AddTransient<ICreatorService, CreatorService>();
            services.AddTransient<IPlanetService, PlanetService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultAreaRoute();

                    endpoints.MapControllerRoute(
                        name: "Planet Details",
                        pattern: "/Planets/Details/{id}/{information}",
                        defaults: new
                        {
                            controller = typeof(PlanetsController).GetControllerName(),
                            action = nameof(PlanetsController.Details)
                        });

                    endpoints.MapControllerRoute(
                        name: "Moon Details",
                        pattern: "/Moons/Details/{id}/{information}",
                        defaults: new
                        {
                            controller = typeof(MoonsController).GetControllerName(),
                            action = nameof(MoonsController.Details)
                        });

                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                });     
        }
    }
}
