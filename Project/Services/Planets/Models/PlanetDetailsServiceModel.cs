using Project.Services.Minerals.Models;
using Project.Services.Moons.Models;
using System.Collections.Generic;

namespace Project.Services.Planets.Models
{
    public class PlanetDetailsServiceModel : PlanetServiceModel
    {
        public int CreatorId { get; init; }

        public int PlanetarySystemId { get; init; }

        public string CreatorName { get; init; }

        public string UserId { get; init; }

        public IEnumerable<MoonServiceModel> Moons { get; init; }

        public IEnumerable<MineralServiceModel> Minerals { get; init; }
    }
}
