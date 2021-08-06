using Project.Services.Planets.Models;

namespace Project.Infrastructure.Extensions
{
    public static class PlanetModelExtensions
    {
        public static string GetInformation(this IPlanetModel planet)
            => planet.Name;
    }
}
