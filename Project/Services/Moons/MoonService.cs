using Project.Data;
using Project.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Services.Moons
{
    public class MoonService : IMoonService
    {
        private readonly ProjectDbContext data;

        public MoonService(ProjectDbContext data)
            => this.data = data;

        public MoonQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int moonsPerPage)
        {
            var moonsQuery = this.data.Moons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moonsQuery = moonsQuery.Where(m =>
                m.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var totalMoons = moonsQuery.Count();

            var moons = GetMoons(moonsQuery
                .Skip((currentPage - 1) * moonsPerPage)
                .Take(moonsPerPage));

            return new MoonQueryServiceModel
            {
                TotalMoons = totalMoons,
                CurrentPage = currentPage,
                MoonsPerPage = moonsPerPage,
                Moons = moons
            };
        }

        public MoonDetailsServiceModel Details(int id)
            => this.data
            .Moons
            .Where(m => m.Id == id)
            .Select(m => new MoonDetailsServiceModel
            {
                Id = m.Id,
                Name = m.Name,
                OrbitalDistance = (double)m.OrbitalDistance,
                OrbitalPeriod = (double)m.OrbitalPeriod,
                Radius = m.Radius,
                AtmosphericPressure = (double)m.AtmosphericPressure,
                SurfaceTemperature = m.SurfaceTemperature,
                Analysis = m.Analysis,
                ImageUrl = m.ImageUrl,
                PlanetName = m.Planet.Name
            })
            .FirstOrDefault();

        public int Create(string name, double orbitalDistance, double orbitalPeriod, int radius, double atmosphericPressure, int surfaceTemperature, string analysis, string imageUrl, int planetId)
        {
            var moonData = new Moon
            {
                Name = name,
                OrbitalDistance = orbitalDistance,
                OrbitalPeriod = orbitalPeriod,
                Radius = radius,
                AtmosphericPressure = atmosphericPressure,
                SurfaceTemperature = surfaceTemperature,
                Analysis = analysis,
                ImageUrl = imageUrl,
                PlanetId = planetId
            };

            this.data.Moons.Add(moonData);
            this.data.SaveChanges();

            return moonData.Id;
        }

        public bool Edit(int id, string name, double orbitalDistance, double orbitalPeriod, int radius, double atmosphericPressure, int surfaceTemperature, string analysis, string imageUrl, int planetId)
        {
            var moonData = this.data.Moons.Find(id);

            if (moonData == null)
            {
                return false;
            }

            moonData.Name = name;
            moonData.OrbitalDistance = orbitalDistance;
            moonData.OrbitalPeriod = orbitalPeriod;
            moonData.Radius = radius;
            moonData.AtmosphericPressure = atmosphericPressure;
            moonData.SurfaceTemperature = surfaceTemperature;
            moonData.Analysis = analysis;
            moonData.ImageUrl = imageUrl;
            moonData.PlanetId = planetId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<MoonPlanetServiceModel> AllPlanets()
            => this.data
            .Planets
            .Select(p => new MoonPlanetServiceModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();

        public bool PlanetExists(int planetId)
            => this.data
            .Planets
            .Any(p => p.Id == planetId);

        private static IEnumerable<MoonServiceModel> GetMoons(IQueryable<Moon> moonQuery)
            => moonQuery
            .Select(p => new MoonServiceModel
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
                PlanetName = p.Planet.Name
            })
                .ToList();
    }
}
