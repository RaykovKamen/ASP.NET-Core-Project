using Project.Data;
using Project.Data.Models;
using System.Linq;

namespace Project.Services.PlanetarySystems
{
    public class PlanetarySystemService : IPlanetarySystemService
    {
        private readonly ProjectDbContext data;

        public PlanetarySystemService(ProjectDbContext data)
            => this.data = data;

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

        public bool PlanetarySystemExists(string planetartySystemName)
            => this.data
            .PlanetarySystems
            .Any(p => p.Name == planetartySystemName);
    }
}
