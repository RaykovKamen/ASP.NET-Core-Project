using Project.Models.Planets;
using System.Collections.Generic;

namespace Project.Models.Home
{
    public class IndexViewModel
    {
        public int TotalPlanets { get; init; }

        public int TotalUsers { get; init; }

        public int TotalMisions { get; init; }

        public List<PlanetIndexViewModel> Planets { get; init; }

        public List<PlanetarySystemIndexViewModel> PlanetarySystems { get; init; }
    }
}
