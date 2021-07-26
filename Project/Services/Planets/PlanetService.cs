using Project.Data;
using Project.Data.Models;
using System.Collections.Generic;
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

            var planets = GetPlanets(planetsQuery
                .Skip((currentPage - 1) * planetsPerPage)
                .Take(planetsPerPage));

            return new PlanetQueryServiceModel
            {
                TotalPlanets = totalPlanets,
                CurrentPage = currentPage,
                PlanetsPerPage = planetsPerPage,
                Planets = planets
            };
        }

        public PlanetDetailsServiceModel Details(int id)
            => this.data
            .Planets
            .Where(p => p.Id == id)
            .Select(p => new PlanetDetailsServiceModel
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
                PlanetarySystemName = p.PlanetarySystem.Name,
                CreatorId = p.CreatorId,
                CreatorName = p.Creator.Name,
                UserId = p.Creator.UserId
            })
            .FirstOrDefault();

        public int Create(string name, double orbitalDistance, double orbitalPeriod, int radius, double atmosphericPressure, int surfaceTemperature, string analysis, string imageUrl, int planetarySystemId, int creatorId)
        {
            var planetData = new Planet
            {
                Name = name,
                OrbitalDistance = orbitalDistance,
                OrbitalPeriod = orbitalPeriod,
                Radius = radius,
                AtmosphericPressure = atmosphericPressure,
                SurfaceTemperature = surfaceTemperature,
                Analysis = analysis,
                ImageUrl = imageUrl,
                PlanetarySystemId = planetarySystemId,
                CreatorId = creatorId
            };

            this.data.Planets.Add(planetData);
            this.data.SaveChanges();

            return planetData.Id;
        }

        public bool Edit(int id, string name, double orbitalDistance, double orbitalPeriod, int radius, double atmosphericPressure, int surfaceTemperature, string analysis, string imageUrl, int planetarySystemId)
        {
            var planetData = this.data.Planets.Find(id);

            if (planetData == null)
            {
                return false;
            }

            planetData.Name = name;
            planetData.OrbitalDistance = orbitalDistance;
            planetData.OrbitalPeriod = orbitalPeriod;
            planetData.Radius = radius;
            planetData.AtmosphericPressure = atmosphericPressure;
            planetData.SurfaceTemperature = surfaceTemperature;
            planetData.Analysis = analysis;
            planetData.ImageUrl = imageUrl;
            planetData.PlanetarySystemId = planetarySystemId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<PlanetServiceModel> ByUser(string userId)
            => GetPlanets(this.data
            .Planets
            .Where(p => p.Creator.UserId == userId));

        public bool IsByCreator(int planetId, int creatorId)
        => this.data
            .Planets
            .Any(p => p.Id == planetId && p.CreatorId == creatorId);

        public IEnumerable<PlanetarySystemServiceModel> AllPlanetarySystems()
            => this.data
            .PlanetarySystems
            .Select(p => new PlanetarySystemServiceModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();

        public bool PlanetarySystemExists(int planetartySystemId)
            => this.data
            .PlanetarySystems
            .Any(p => p.Id == planetartySystemId);

        private static IEnumerable<PlanetServiceModel> GetPlanets(IQueryable<Planet> planetQuery)
            => planetQuery
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
                PlanetarySystemName = p.PlanetarySystem.Name
            })
                .ToList();
    }
}
