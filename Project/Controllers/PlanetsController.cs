using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure.Extensions;
using Project.Models.Planets;
using Project.Services.Creators;
using Project.Services.Planets;
using static Project.WebConstants;

namespace Project.Controllers
{
    public class PlanetsController : Controller
    {

        private readonly IPlanetService planets;
        private readonly ICreatorService creators;
        private readonly IMapper mapper;
        public PlanetsController(
            IPlanetService planets,
            ICreatorService creators,
            IMapper mapper)
        {
            this.planets = planets;
            this.creators = creators;
            this.mapper = mapper;
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

        public IActionResult Details(int id, string information)
        {
            var planet = this.planets.Details(id);

            if (information != planet.GetInformation())
            {
                return BadRequest();
            }

            return View(planet);
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

            var planetId = this.planets.Create(
                planet.Name,
                (double)planet.OrbitalDistance,
                (double)planet.OrbitalPeriod,
                (int)planet.Radius,
                (double)planet.AtmosphericPressure,
                planet.SurfaceTemperature,
                planet.Analysis,
                planet.ImageUrl,
                planet.PlanetarySystemId,
                creatorId);

            TempData[GlobalMessageKey] = "Your planet was created!";

            return RedirectToAction(nameof(Details), new { id = planetId, information = planet.GetInformation() });
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.creators.IsCreator(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var planet = this.planets.Details(id);

            if (planet.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var planetForm = this.mapper.Map<PlanetFormModel>(planet);

            planetForm.PlanetarySystems = this.planets.AllPlanetarySystems();

            return View(planetForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PlanetFormModel planet)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0 && !User.IsAdmin())
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

            if (!this.planets.IsByCreator(id, creatorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.planets.Edit(
                id,
                planet.Name,
                (double)planet.OrbitalDistance,
                (double)planet.OrbitalPeriod,
                (int)planet.Radius,
                (double)planet.AtmosphericPressure,
                planet.SurfaceTemperature,
                planet.Analysis,
                planet.ImageUrl,
                planet.PlanetarySystemId);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"Your planet was edited!";

            return RedirectToAction(nameof(Details), new { id, information = planet.GetInformation() });
        }
    }
}
