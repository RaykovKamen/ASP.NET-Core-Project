using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Infrastructure;
using Project.Models.PlanetarySystems;
using System.Linq;

namespace Project.Controllers
{
    public class PlanetarySystemsController : Controller
    {
        private readonly ProjectDbContext data;

        public PlanetarySystemsController(ProjectDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Add() 
        {
            if (!this.UserIsCreator())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPlanetarySystemFormModel planetarySystem)
        {          
            if (this.data.PlanetarySystems.Any(p => p.Name == planetarySystem.Name))
            {
                this.ModelState.AddModelError(nameof(planetarySystem.Name), "Planetary System already exist.");

                return View(planetarySystem);
            }

            var planearySystemtData = new PlanetarySystem
            {
                Name = planetarySystem.Name,
            };

            this.data.PlanetarySystems.Add(planearySystemtData);
            this.data.SaveChanges();

            return Redirect("/Planets/Add");
        }

        private bool UserIsCreator()
            => this.data
                .Creators
                .Any(c => c.UserId == this.User.Id());
    }
}
