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

        public IActionResult All([FromQuery] AllPlanetsQueryModel query)
        {
            var planetsQuery = this.data.Planets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                planetsQuery = planetsQuery.Where(p =>
                p.Name.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            var totalPlanets = planetsQuery.Count();

            var planets = planetsQuery
                .Skip((query.CurrentPage - 1) * AllPlanetsQueryModel.PlanetsPerPage)
                .Take(AllPlanetsQueryModel.PlanetsPerPage)
                .OrderByDescending(p => p.Id)
                .Select(p => new PlanetListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    OrbitalDistance = (double)p.OrbitalDistance,
                    OrbitalPeriod = (double)p.OrbitalPeriod,
                    Radius = p.Radius,
                    AtmosphericPressure = (double)p.AtmosphericPressure,
                    SurfaceTemperature = p.SurfaceTemperature,
                    Analysis = p.Analysis,
                    ImageUrl = p.ImageUrl,
                    PlanetarySystem = p.PlanetarySystem.Name
                })
                .ToList();

            query.TotalPlanets = totalPlanets;
            query.Planets = planets;

            return View(query);
        }

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
                OrbitalDistance = (double)planet.OrbitalDistance,
                OrbitalPeriod = (double)planet.OrbitalPeriod,
                Radius = (int)planet.Radius,
                AtmosphericPressure = (double)planet.AtmosphericPressure,
                SurfaceTemperature = planet.SurfaceTemperature,
                Analysis = planet.Analysis,
                ImageUrl = planet.ImageUrl,
                PlanetarySystemId = planet.PlanetarySystemId
            };

            this.data.Planets.Add(planetData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

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
