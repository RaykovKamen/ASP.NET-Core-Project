using Microsoft.AspNetCore.Mvc;
using Project.Services.PlanetarySystems;
using System.Linq;

namespace Project.Areas.Admin.Controllers
{
    public class PlanetarySystemsController : AdminController
    {
        private readonly IPlanetarySystemService planetarySystems;

        public PlanetarySystemsController(IPlanetarySystemService planetarySystems)
            => this.planetarySystems = planetarySystems;

        public IActionResult All() => View(this.planetarySystems.All().PlanetarySystems);
    }
}
