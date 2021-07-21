using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Models.Moons;
using Project.Models.Planets;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class MoonsController : Controller
    {
        private readonly ProjectDbContext data;

        public MoonsController(ProjectDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Add() => View(new AddMoonFormModel
        {
            Planets = this.GetPlanets()
        });

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddMoonFormModel moon)
        { 
            if (!ModelState.IsValid)
            {
                moon.Planets = this.GetPlanets();

                return View(moon);
            }

            var moonData = new Moon
            {
                Name = moon.Name,
                OrbitalDistance = (double)moon.OrbitalDistance,
                OrbitalPeriod = (double)moon.OrbitalPeriod,
                Radius = (int)moon.Radius,
                AtmosphericPressure = (double)moon.AtmosphericPressure,
                SurfaceTemperature = (int)moon.SurfaceTemperature,
                Analysis = moon.Analysis,
                ImageUrl = moon.ImageUrl,
                PlanetId = moon.PlanetId
            };

            this.data.Moons.Add(moonData);
            this.data.SaveChanges();

            return Redirect("/Home");
        }

        private IEnumerable<PlanetViewModel> GetPlanets() => this.data
            .Planets
            .Select(p => new PlanetViewModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToList();
    }
}
