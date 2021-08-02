using Project.Services.PlanetarySystems.Models;
using System.Collections.Generic;

namespace Project.Services.PlanetarySystems
{
    public interface IPlanetarySystemService
    {
        int Create(string name);

        bool PlanetarySystemExists(string planetartySystemName);

        IEnumerable<LatestPlanetarySystemServiceModel> Latest();
    }
}
