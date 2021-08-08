using Project.Services.PlanetarySystems.Models;
using System.Collections.Generic;

namespace Project.Services.PlanetarySystems
{
    public interface IPlanetarySystemService
    {
        PlanetarySystemQueryServiceModel All();

        int Create(string name);

        void Delete(int id);

        bool PlanetarySystemExists(string planetartySystemName);

        IEnumerable<LatestPlanetarySystemServiceModel> Latest();
    }
}
