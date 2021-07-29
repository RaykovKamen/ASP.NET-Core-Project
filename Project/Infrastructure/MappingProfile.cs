using AutoMapper;
using Project.Data.Models;
using Project.Models.Home;
using Project.Models.Moons;
using Project.Models.Planets;
using Project.Services.Moons.Models;
using Project.Services.Planets.Models;

namespace Project.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<PlanetarySystem, PlanetarySystemIndexViewModel>();
            this.CreateMap<Planet, PlanetIndexViewModel>();
            this.CreateMap<PlanetDetailsServiceModel, PlanetFormModel>();
            this.CreateMap<MoonDetailsServiceModel, MoonFormModel>();

            this.CreateMap<Planet, PlanetDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Creator.UserId));

            this.CreateMap<Moon, MoonDetailsServiceModel>()
                .ForMember(m => m.UserId, cfg => cfg.MapFrom(m => m.Creator.UserId));
        }
    }
}
