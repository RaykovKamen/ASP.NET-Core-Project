using Project.Services.Moons.Models;
using System.Collections.Generic;

namespace Project.Services.Moons
{
    public interface IMoonService
    {
        MoonQueryServiceModel All(
            string searchTerm = null,
            int currentPage = 1,
            int moonsPerPage = int.MaxValue);

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
            int planetId,
            int creatorId);

        void Delete(int id);

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

        IEnumerable<MoonServiceModel> ByUser(string userId);

        bool IsByCreator(int planetId, int creatorId);

        IEnumerable<MoonPlanetServiceModel> AllPlanets();

        bool PlanetExists(int planetId);
    }
}
