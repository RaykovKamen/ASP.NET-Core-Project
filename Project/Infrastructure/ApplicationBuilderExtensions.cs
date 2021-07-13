using Microsoft.AspNetCore.Builder;
using Project.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Project.Data.Models;

namespace Project.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ProjectDbContext>();

            data.Database.Migrate();

            SeedPlanetarySystems(data);

            return app;
        }

        private static void SeedPlanetarySystems(ProjectDbContext data)
        {
            if (data.PlanetarySystems.Any())
            {
                return;
            }

            data.PlanetarySystems.Add(new PlanetarySystem { Name = "Sol" });

            data.SaveChanges();
        }
    }
}
