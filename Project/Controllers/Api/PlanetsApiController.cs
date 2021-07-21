using Microsoft.AspNetCore.Mvc;
using Project.Models.Api.Planets;
using Project.Services.Planets;

namespace Project.Controllers.Api
{
    [ApiController]
    [Route("api/planets")]
    public class PlanetsApiController : ControllerBase
    {
        private readonly IPlanetService planets;

        public PlanetsApiController(IPlanetService planets)
            => this.planets = planets;

        [HttpGet]
        public PlanetQueryServiceModel All([FromQuery] AllPlanetsApiRequestModel query)
           => this.planets.All(
                query.SearchTerm,
                query.CurrentPage,
                query.PlanetsPerPage);
    }
}
