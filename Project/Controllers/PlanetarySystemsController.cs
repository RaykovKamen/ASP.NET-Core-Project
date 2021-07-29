using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure;
using Project.Models.PlanetarySystems;
using Project.Services.Creators;
using Project.Services.PlanetarySystems;

namespace Project.Controllers
{
    public class PlanetarySystemsController : Controller
    {
        private readonly IPlanetarySystemService planetarySystems;
        private readonly ICreatorService creators;
        public PlanetarySystemsController(
            IPlanetarySystemService planetarySystems,
            ICreatorService creators)
        {
            this.planetarySystems = planetarySystems;
            this.creators = creators;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.creators.IsCreator(this.User.Id()))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PlanetarySystemFormModel planetarySystem)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (this.planetarySystems.PlanetarySystemExists(planetarySystem.Name))
            {
                this.ModelState.AddModelError(nameof(planetarySystem.Name), "Planetary System already exist.");

                return View(planetarySystem);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            this.planetarySystems.Create(planetarySystem.Name);

            return Redirect("/Planets/Add");
        }
    }
}
