using Microsoft.AspNetCore.Mvc;
using Project.Services.Planets;

namespace Project.Areas.Admin.Controllers
{
    public class PlanetsController : AdminController
    {
        private readonly IPlanetService planets;

        public PlanetsController(IPlanetService planets)
            => this.planets = planets;

        public IActionResult All() => View(this.planets.All().Planets);
    }
}
