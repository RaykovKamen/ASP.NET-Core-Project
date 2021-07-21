using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.Models.Home;
using Project.Services.Statistics;
using System.Diagnostics;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly ProjectDbContext data;

        public HomeController(
            IStatisticsService statistics,
            ProjectDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
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

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {            
                TotalPlanets = totalStatistics.TotalPlanets,
                TotalUsers = totalStatistics.TotalUsers,
                Planets = planets,
                PlanetarySystems = planetarySystems,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
