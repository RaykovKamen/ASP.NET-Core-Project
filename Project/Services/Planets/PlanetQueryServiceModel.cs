using System.Collections.Generic;

namespace Project.Services.Planets
{
    public class PlanetQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int PlanetsPerPage { get; init; }

        public int TotalPlanets { get; init; }

        public IEnumerable<PlanetServiceModel> Planets { get; init; }
    }
}
