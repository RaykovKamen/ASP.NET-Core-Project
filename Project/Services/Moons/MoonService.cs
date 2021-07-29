using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Data;
using Project.Data.Models;
using Project.Services.Moons.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Services.Moons
{
    public class MoonService : IMoonService
    {
        private readonly ProjectDbContext data;
        private readonly IConfigurationProvider mapper;

        public MoonService(ProjectDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

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
                .OrderBy(p => p.Name)
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
            .ProjectTo<MoonDetailsServiceModel>(this.mapper)
            .FirstOrDefault();

        public int Create(string name, double orbitalDistance, double orbitalPeriod, int radius, double atmosphericPressure, int surfaceTemperature, string analysis, string imageUrl, int planetId, int creatorId)
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
                PlanetId = planetId,
                CreatorId = creatorId
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

        public IEnumerable<MoonServiceModel> ByUser(string userId)
            => GetMoons(this.data
            .Moons
            .Where(m => m.Creator.UserId == userId));

        public bool IsByCreator(int moonId, int creatorId)
        => this.data
            .Moons
            .Any(p => p.Id == moonId && p.CreatorId == creatorId);

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
            .Select(m => new MoonServiceModel
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
                .ToList();
    }
}
