using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure;
using Project.Models.Planets;
using Project.Services.Creators;
using Project.Services.Planets;

namespace Project.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly IPlanetService planets;
        private readonly ICreatorService creators;
        public PlanetsController(
            IPlanetService planets, 
            ICreatorService creators)
        {
            this.planets = planets;
            this.creators = creators;
        }

        public IActionResult All([FromQuery] AllPlanetsQueryModel query)
        {
            var queryResult = this.planets.All(
                query.SearchTerm,
                query.CurrentPage,
                AllPlanetsQueryModel.PlanetsPerPage);

            query.TotalPlanets = queryResult.TotalPlanets;
            query.Planets = queryResult.Planets;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myPlanets = this.planets.ByUser(this.User.Id());

            return View(myPlanets);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.creators.IsCreator(this.User.Id()))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new PlanetFormModel
            {
                PlanetarySystems = this.planets.AllPlanetarySystems()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PlanetFormModel planet)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.planets.PlanetarySystemExists(planet.PlanetarySystemId))
            {
                this.ModelState.AddModelError(nameof(planet.PlanetarySystemId), "Planetary System does not exist.");
            }

            if (!ModelState.IsValid)
            {
                planet.PlanetarySystems = this.planets.AllPlanetarySystems();

                return View(planet);
            }

            this.planets.Create(
                planet.Name,
                (double)planet.OrbitalDistance,
                (double)planet.OrbitalPeriod,
                (int)planet.Radius,
                (double)planet.AtmosphericPressure,
                (int)planet.SurfaceTemperature,
                planet.Analysis,
                planet.ImageUrl,
                planet.PlanetarySystemId,
                creatorId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.creators.IsCreator(userId))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var planet = this.planets.Details(id);

            if (planet.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new PlanetFormModel
            {
                Name = planet.Name,
                OrbitalDistance = planet.OrbitalDistance,
                OrbitalPeriod = planet.OrbitalPeriod,
                Radius = planet.Radius,
                AtmosphericPressure = planet.AtmosphericPressure,
                SurfaceTemperature = planet.SurfaceTemperature,
                Analysis = planet.Analysis,
                ImageUrl = planet.ImageUrl,
                PlanetarySystemId = planet.PlanetarySystemId,
                PlanetarySystems = this.planets.AllPlanetarySystems()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PlanetFormModel planet)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.planets.PlanetarySystemExists(planet.PlanetarySystemId))
            {
                this.ModelState.AddModelError(nameof(planet.PlanetarySystemId), "Planetary System does not exist.");
            }

            if (!ModelState.IsValid)
            {
                planet.PlanetarySystems = this.planets.AllPlanetarySystems();

                return View(planet);
            }

            if (!this.planets.IsByCreator(id, creatorId))
            {
                return BadRequest();
            }

            this.planets.Edit(
                id,
                planet.Name,
                (double)planet.OrbitalDistance,
                (double)planet.OrbitalPeriod,
                (int)planet.Radius,
                (double)planet.AtmosphericPressure,
                (int)planet.SurfaceTemperature,
                planet.Analysis,
                planet.ImageUrl,
                planet.PlanetarySystemId);

            return RedirectToAction(nameof(All));
        }
    }
}
