using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Data;
using Project.Data.Models;
using Project.Services.PlanetarySystems.Models;
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

        public IEnumerable<LatestPlanetarySystemServiceModel> Latest()
        => this.data
            .Planets
            .OrderByDescending(p => p.Id)
            .ProjectTo<LatestPlanetarySystemServiceModel>(this.mapper)
            .ToList();

        public bool PlanetarySystemExists(string planetartySystemName)
            => this.data
            .PlanetarySystems
            .Any(p => p.Name == planetartySystemName);
    }
}
