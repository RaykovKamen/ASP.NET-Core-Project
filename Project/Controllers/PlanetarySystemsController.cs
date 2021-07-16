using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Models.PlanetarySystems;
using System.Linq;

namespace Project.Controllers
{
    public class PlanetarySystemsController : Controller
    {
        private readonly ProjectDbContext data;

        public PlanetarySystemsController(ProjectDbContext data)
            => this.data = data;

        public IActionResult Add() 
        {
           return View();
        }

        [HttpPost]
        public IActionResult Add(AddPlanetarySystemFormModel planetarySystem)
        {
            if (this.data.PlanetarySystems.Any(p => p.Name == planetarySystem.Name))
            {
                this.ModelState.AddModelError(nameof(planetarySystem.Name), "Planetary System exist.");
            }

            var planearySystemtData = new PlanetarySystem
            {
                Name = planetarySystem.Name,
            };

            this.data.PlanetarySystems.Add(planearySystemtData);
            this.data.SaveChanges();

            return Redirect("/Planets/Add");
        }
    }
}
