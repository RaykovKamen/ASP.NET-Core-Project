using Project.Services.Moons.Models;

namespace Project.Infrastructure.Extensions
{
    public static class MoonModelExtensions
    {
        public static string GetInformation(this IMoonModel moon)
            => moon.Name;
    }
}
