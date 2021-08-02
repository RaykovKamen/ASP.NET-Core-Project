using Microsoft.AspNetCore.Mvc;
using Project.Models.Home;
using Project.Services.PlanetarySystems;
using Project.Services.Planets;
using Project.Services.Statistics;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlanetService planets;
        private readonly IPlanetarySystemService planetarySystems;
        private readonly IStatisticsService statistics;

        public HomeController(
            IStatisticsService statistics,
            IPlanetService planets,
            IPlanetarySystemService planetarySystems)
        {
            this.statistics = statistics;
            this.planets = planets;
            this.planetarySystems = planetarySystems;
        }

        public IActionResult Index()
        {
            var latestPlanetarySystems = this.planetarySystems
                .Latest()
                .ToList();

            var latestPlanets = this.planets
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {            
                TotalPlanets = totalStatistics.TotalPlanets,
                TotalUsers = totalStatistics.TotalUsers,
                Planets = latestPlanets,
                PlanetarySystems = latestPlanetarySystems
            });
        }

        public IActionResult Error() => View();
    }
}
