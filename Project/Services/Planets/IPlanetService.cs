using Project.Services.Planets.Models;
using System.Collections.Generic;

namespace Project.Services.Planets
{
    public interface IPlanetService
    {
        PlanetQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int planetsPerPage);

        IEnumerable<LatestPlanetServiceModel> Latest();

        PlanetDetailsServiceModel Details(int planetId);

        int Create(
            string name,
            double orbitalDistance,
            double orbitalPeriod,
            int radius,
            double atmosphericPressure,
            int? surfaceTemperature,
            string analysis,
            string imageUrl,
            int planetarySystemId,
            int creatorId);

        bool Edit(
            int planetId,
            string name,
            double orbitalDistance,
            double orbitalPeriod,
            int radius,
            double atmosphericPressure,
            int? surfaceTemperature,
            string analysis,
            string imageUrl,
            int planetarySystemId);

        IEnumerable<PlanetServiceModel> ByUser(string userId);

        bool IsByCreator(int planetId, int creatorId);

        IEnumerable<PlanetarySystemServiceModel> AllPlanetarySystems();

        bool PlanetarySystemExists(int planetartySystemId);
    }
}
