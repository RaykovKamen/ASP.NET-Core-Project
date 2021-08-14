using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Data;
using Project.Data.Models;
using Project.Services.Minerals.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Services.Minerals
{
    public class MineralService : IMineralService
    {
        private readonly ProjectDbContext data;
        private readonly IConfigurationProvider mapper;

        public MineralService(ProjectDbContext data, 
            IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public MineralQueryServiceModel All()
        {
            var mineralQuery = this.data.Minerals.AsQueryable();

            var totalMinerals = mineralQuery.Count();

            var mineral = GetMinerals(mineralQuery
               .OrderBy(m => m.Id));

            return new MineralQueryServiceModel
            {
                TotalMinerals = totalMinerals,
                Minerals = mineral
            };
        }

        public int Create(int? aluminum, int? beryllium, int? cadmium, int? copper, int? fluorite, int? graphite, int? iridium, int? iron, int? lithium, int? magnesium, int? nickel, int? platinum, int? silicon, int? titanium, int? uranium, int? vanadium, int planetId)
        {
            var mineralData = new Mineral
            {
                Aluminum = aluminum,
                Beryllium = beryllium,
                Cadmium = cadmium,
                Copper = copper,
                Fluorite = fluorite,
                Graphite = graphite,
                Iridium = iridium,
                Iron = iron,
                Lithium = lithium,
                Magnesium = magnesium,
                Nickel = nickel,
                Platinum = platinum,
                Silicon = silicon,
                Titanium = titanium,
                Uranium = uranium,
                Vanadium = vanadium,
                PlanetId = planetId
            };

            this.data.Minerals.Add(mineralData);
            this.data.SaveChanges();

            return mineralData.Id;
        }

        public void Delete(int id)
        {
            var submission = this.data.Minerals.Find(id);
            this.data.Minerals.Remove(submission);
            this.data.SaveChanges();
        }

        public IEnumerable<MineralServiceModel> AllPlanets()
           => this.data
           .Planets
           .Select(p => new MineralServiceModel
           {
               Id = p.Id,
               PlanetName = p.Name
           })
           .ToList();

        public bool PlanetExists(int? planetId)
           => this.data
           .Planets
           .Any(p => p.Id == planetId);

        private IEnumerable<MineralServiceModel> GetMinerals(IQueryable<Mineral> mineralQuery)
            => mineralQuery
                .ProjectTo<MineralServiceModel>(this.mapper)
                .ToList();
    }
}
