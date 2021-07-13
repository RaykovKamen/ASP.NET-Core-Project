using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Models.Planets;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class PlanetsController : Controller
    {
        private readonly ProjectDbContext data;

        public PlanetsController(ProjectDbContext data) 
            => this.data = data;

        public IActionResult Add() => View(new AddPlanetFormModel 
        {
            PlanetarySystems = this.GetPlanetarySystems()
        });

        [HttpPost]
        public IActionResult Add(AddPlanetFormModel planet)
        {
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
                OrbitalDistance = (int)planet.OrbitalDistance,
                OrbitalPeriod = (int)planet.OrbitalPeriod,
                Radius = (int)planet.Radius,
                AtmosphericPressure = (int)planet.AtmosphericPressure,
                SurfaceTemperature = (int)planet.SurfaceTemperature,
                Analysis = planet.Analysis,
                ImageUrl = planet.ImageUrl,
                PlanetarySystemId = planet.PlanetarySystemId
            };

            this.data.Planets.Add(planetData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<PlanetarySystemViewModel> GetPlanetarySystems() => this.data
            .PlanetarySystems
            .Select(p => new PlanetarySystemViewModel
            {
                Id= p.Id,
                Name = p.Name
            })
            .ToList();
    }
}
