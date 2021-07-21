using Project.Data;
using System.Linq;

namespace Project.Services.Planets
{
    public class PlanetService : IPlanetService
    {
        private readonly ProjectDbContext data;

        public PlanetService(ProjectDbContext data)
            => this.data = data;

        public PlanetQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int planetsPerPage)
        {
            var planetsQuery = this.data.Planets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                planetsQuery = planetsQuery.Where(p =>
                p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalPlanets = planetsQuery.Count();

            var planets = planetsQuery
                .Skip((currentPage - 1) * planetsPerPage)
                .Take(planetsPerPage)
                .Select(p => new PlanetServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    OrbitalDistance = (double)p.OrbitalDistance,
                    OrbitalPeriod = (double)p.OrbitalPeriod,
                    Radius = p.Radius,
                    AtmosphericPressure = (double)p.AtmosphericPressure,
                    SurfaceTemperature = p.SurfaceTemperature,
                    Analysis = p.Analysis,
                    ImageUrl = p.ImageUrl,
                    PlanetarySystem = p.PlanetarySystem.Name
                })
                .ToList();

            return new PlanetQueryServiceModel
            {
                TotalPlanets = totalPlanets,
                CurrentPage = currentPage,
                PlanetsPerPage = planetsPerPage,
                Planets = planets
            };
        }
    }
}
