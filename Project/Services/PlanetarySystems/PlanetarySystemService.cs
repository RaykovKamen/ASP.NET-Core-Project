using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Data;
using Project.Data.Models;
using Project.Services.PlanetarySystems.Models;
using Project.Services.Planets.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Services.PlanetarySystems
{
    public class PlanetarySystemService : IPlanetarySystemService
    {
        private readonly ProjectDbContext data;
        private readonly IConfigurationProvider mapper;

        public PlanetarySystemService(ProjectDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public PlanetarySystemQueryServiceModel All()
        {
            var planetarySystemQuery = this.data.PlanetarySystems.AsQueryable();

            var totalPlanetarySystems = planetarySystemQuery.Count();

            var planetarySystem = GetPlanetarySystems(planetarySystemQuery
               .OrderBy(p => p.Name));

            return new PlanetarySystemQueryServiceModel
            {
                TotalPlanetarySystems = totalPlanetarySystems,
                PlanetarySystems = planetarySystem
            };
        }
        public int Create(string name)
        {
            var planetarySystemData = new PlanetarySystem
            {
                Name = name,
            };

            this.data.PlanetarySystems.Add(planetarySystemData);
            this.data.SaveChanges();

            return planetarySystemData.Id;
        }

        public void Delete(int id)
        {
            var submission = this.data.PlanetarySystems.Find(id);
            this.data.PlanetarySystems.Remove(submission);
            this.data.SaveChanges();
        }

        public IEnumerable<LatestPlanetarySystemServiceModel> Latest()
        => this.data
            .PlanetarySystems
            .OrderByDescending(p => p.Id)
            .ProjectTo<LatestPlanetarySystemServiceModel>(this.mapper)
            .ToList();

        public bool PlanetarySystemExists(string planetartySystemName)
            => this.data
            .PlanetarySystems
            .Any(p => p.Name == planetartySystemName);

        private IEnumerable<PlanetarySystemServiceModel> GetPlanetarySystems(IQueryable<PlanetarySystem> planetarySystemQuery)
            => planetarySystemQuery
                .ProjectTo<PlanetarySystemServiceModel>(this.mapper)
                .ToList();
    }
}
