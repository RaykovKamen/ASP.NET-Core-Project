using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.Models.Home;
using System.Diagnostics;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectDbContext data;

        public HomeController(ProjectDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalPlanets = this.data.Planets.Count();

            var totalPlanetarySystems = this.data.PlanetarySystems.Count();

            var planetarySystems = this.data
                .PlanetarySystems
                .OrderByDescending(p => p.Id)
                .Select(p => new PlanetarySystemIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();

            var planets = this.data
                .Planets
                .OrderByDescending(p => p.Id)
                .Select(p => new PlanetIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {            
                TotalPlanets = totalPlanets,
                Planets = planets,
                PlanetarySystems = planetarySystems,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
