using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure;
using Project.Models.Minerals;
using Project.Services.Creators;
using Project.Services.Minerals;

namespace Project.Controllers
{
    public class MineralsController : Controller
    {
        private readonly IMineralService minerals;
        private readonly ICreatorService creators;

        public MineralsController(
            IMineralService minerals,
            ICreatorService creators)
        {
            this.minerals = minerals;
            this.creators = creators;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.creators.IsCreator(this.User.Id()))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new MineralFormModel
            {
                Planets = this.minerals.AllPlanets()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MineralFormModel mineral)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.minerals.PlanetExists(mineral.PlanetId))
            {
                this.ModelState.AddModelError(nameof(mineral.PlanetId), "Planet does not exist.");
            }

            if (!ModelState.IsValid)
            {
                mineral.Planets = this.minerals.AllPlanets();

                return View(mineral);
            }

            this.minerals.Create(
                (int)mineral.Aluminum,
                (int)mineral.Beryllium,
                (int)mineral.Cadmium,
                (int)mineral.Copper,
                (int)mineral.Fluorite,
                (int)mineral.Graphite,
                (int)mineral.Iridium,
                (int)mineral.Iron,
                (int)mineral.Lithium,
                (int)mineral.Magnesium,
                (int)mineral.Nickel,
                (int)mineral.Platinum,
                (int)mineral.Silicon,
                (int)mineral.Titanium,
                (int)mineral.Uranium,
                (int)mineral.Vanadium,
                mineral.PlanetId
                );

            return Redirect("/Planets/All");       
        }
    }
}