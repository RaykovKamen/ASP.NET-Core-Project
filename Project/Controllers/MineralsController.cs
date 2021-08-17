using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Extensions;
using Project.Models.Minerals;
using Project.Services.Creators;
using Project.Services.Minerals;
using static Project.WebConstants;

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
                mineral.Aluminum,
                mineral.Beryllium,
                mineral.Cadmium,
                mineral.Copper,
                mineral.Fluorite,
                mineral.Graphite,
                mineral.Iridium,
                mineral.Iron,
                mineral.Lithium,
                mineral.Magnesium,
                mineral.Nickel,
                mineral.Platinum,
                mineral.Silicon,
                mineral.Titanium,
                mineral.Uranium,
                mineral.Vanadium,
                mineral.PlanetId);

            TempData[GlobalMessageKey] = "Minerals Added!";
            return Redirect("/Planets/All");       
        }

        [Authorize]
        public IActionResult All() => View(this.minerals.All().Minerals);


        [Authorize]
        public IActionResult Delete(int id)
        {
            this.minerals.Delete(id);
            TempData[GlobalMessageKey] = $"Mining was successful!";
            return this.Redirect("/Minerals/All");
        }
    }
}