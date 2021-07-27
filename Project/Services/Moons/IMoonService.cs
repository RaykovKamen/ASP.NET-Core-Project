using System.Collections.Generic;

namespace Project.Services.Moons
{
    public interface IMoonService
    {
        MoonQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int moonsPerPage);

        MoonDetailsServiceModel Details(int moonId);

        int Create(
            string name,
            double orbitalDistance,
            double orbitalPeriod,
            int radius,
            double atmosphericPressure,
            int surfaceTemperature,
            string analysis,
            string imageUrl,
            int planetId);

        bool Edit(
            int moonId,
            string name,
            double orbitalDistance,
            double orbitalPeriod,
            int radius,
            double atmosphericPressure,
            int surfaceTemperature,
            string analysis,
            string imageUrl,
            int planetId);

        IEnumerable<MoonPlanetServiceModel> AllPlanets();

        bool PlanetExists(int planetId);
    }
}
