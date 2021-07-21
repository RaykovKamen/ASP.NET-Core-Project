using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Infrastructure;
using Project.Models.Planets;
using Project.Services.Planets;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly IPlanetService planets;
        private readonly ProjectDbContext data;

        public PlanetsController(IPlanetService planets, ProjectDbContext data)
        {
            this.planets = planets;
            this.data = data;
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
        public IActionResult Add()
        {
            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new AddPlanetFormModel
            {
                PlanetarySystems = this.GetPlanetarySystems()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPlanetFormModel planet)
        {
            var creatorId = this.data
                .Creators
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.data.PlanetarySystems.Any(p => p.Id == planet.PlanetarySystemId))
            {
                this.ModelState.AddModelError(nameof(planet.PlanetarySystemId), "Planetary System does not exist.");
            }

            if (!ModelState.IsValid)
            {
                planet.PlanetarySystems = this.GetPlanetarySystems();

                return View(planet);
            }

            var planetData = new Planet
            {
                Name = planet.Name,
                OrbitalDistance = (double)planet.OrbitalDistance,
                OrbitalPeriod = (double)planet.OrbitalPeriod,
                Radius = (int)planet.Radius,
                AtmosphericPressure = (double)planet.AtmosphericPressure,
                SurfaceTemperature = planet.SurfaceTemperature,
                Analysis = planet.Analysis,
                ImageUrl = planet.ImageUrl,
                PlanetarySystemId = planet.PlanetarySystemId,
                CreatorId = creatorId
            };

            this.data.Planets.Add(planetData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsCreator()
            => this.data
                .Creators
                .Any(c => c.UserId == this.User.GetId());


        private IEnumerable<PlanetarySystemViewModel> GetPlanetarySystems() => this.data
            .PlanetarySystems
            .Select(p => new PlanetarySystemViewModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();
    }
}
