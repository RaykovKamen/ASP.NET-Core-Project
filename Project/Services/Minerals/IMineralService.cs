using Project.Services.Minerals.Models;
using System.Collections.Generic;

namespace Project.Services.Minerals
{
    public interface IMineralService
    {
        MineralQueryServiceModel All();

        int Create(
           int? aluminum,
           int? beryllium,
           int? cadmium,
           int? copper,
           int? fluorite,
           int? graphite,
           int? iridium,
           int? iron,
           int? lithium,
           int? magnesium,
           int? nickel,
           int? platinum,
           int? silicon,
           int? titanium,
           int? uranium,
           int? vanadium,
           int? planetId,
           int? moonId);

        int CreateToMoon(
          int? aluminum,
          int? beryllium,
          int? cadmium,
          int? copper,
          int? fluorite,
          int? graphite,
          int? iridium,
          int? iron,
          int? lithium,
          int? magnesium,
          int? nickel,
          int? platinum,
          int? silicon,
          int? titanium,
          int? uranium,
          int? vanadium,
          int? planetId,
          int? moonId);

        void Delete(int id);

        IEnumerable<MineralServiceModel> AllPlanets();

        IEnumerable<MineralServiceModel> AllMoons();

        bool PlanetExists(int? planetId);

        bool MoonExists(int? moonId);
    }
}
