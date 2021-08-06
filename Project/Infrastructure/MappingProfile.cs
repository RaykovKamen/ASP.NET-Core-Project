using AutoMapper;
using Project.Data.Models;
using Project.Models.Moons;
using Project.Models.Planets;
using Project.Services.Moons.Models;
using Project.Services.PlanetarySystems.Models;
using Project.Services.Planets.Models;

namespace Project.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<PlanetarySystem, PlanetarySystemServiceModel>();
            this.CreateMap<Planet, MoonPlanetServiceModel>();

            this.CreateMap<PlanetarySystem, LatestPlanetarySystemServiceModel>();
            this.CreateMap<Planet, LatestPlanetServiceModel>();
            this.CreateMap<Planet, LatestPlanetarySystemServiceModel>();
            this.CreateMap<PlanetDetailsServiceModel, PlanetFormModel>();
            this.CreateMap<MoonDetailsServiceModel, MoonFormModel>();

            this.CreateMap<Planet, PlanetServiceModel>()
                .ForMember(p => p.PlanetarySystemName, cfg => cfg.MapFrom(p => p.PlanetarySystem.Name));

            this.CreateMap<Moon, MoonServiceModel>()
                .ForMember(p => p.PlanetName, cfg => cfg.MapFrom(p => p.Planet.Name));

            this.CreateMap<Planet, PlanetDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Creator.UserId));

            this.CreateMap<Moon, MoonDetailsServiceModel>()
                .ForMember(m => m.UserId, cfg => cfg.MapFrom(m => m.Creator.UserId));
        }
    }
}
