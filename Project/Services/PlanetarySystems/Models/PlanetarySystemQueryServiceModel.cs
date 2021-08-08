using Project.Services.Planets.Models;
using System.Collections.Generic;

namespace Project.Services.PlanetarySystems.Models
{
    public class PlanetarySystemQueryServiceModel
    {
        public int TotalPlanetarySystems { get; init; }

        public IEnumerable<PlanetarySystemServiceModel> PlanetarySystems { get; init; }
    }
}
