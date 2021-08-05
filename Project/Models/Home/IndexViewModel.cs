using Project.Services.PlanetarySystems.Models;
using Project.Services.Planets.Models;
using System.Collections.Generic;

namespace Project.Models.Home
{
    public class IndexViewModel
    {
        public IList<LatestPlanetServiceModel> Planets { get; init; }

        public IList<LatestPlanetarySystemServiceModel> PlanetarySystems { get; init; }
    }
}
