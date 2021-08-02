using Project.Services.PlanetarySystems.Models;
using Project.Services.Planets.Models;
using System.Collections.Generic;

namespace Project.Models.Home
{
    public class IndexViewModel
    {
        public int TotalPlanets { get; init; }

        public int TotalUsers { get; init; }

        public int TotalMisions { get; init; }

        public IList<LatestPlanetServiceModel> Planets { get; init; }

        public IList<LatestPlanetarySystemServiceModel> PlanetarySystems { get; init; }
    }
}
