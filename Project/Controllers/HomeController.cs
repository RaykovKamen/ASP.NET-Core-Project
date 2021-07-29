using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IConfigurationProvider mapper;
        private readonly ProjectDbContext data;

        public HomeController(
            IStatisticsService statistics,
            IMapper mapper,
            ProjectDbContext data)
        {
            this.statistics = statistics;
            this.mapper = mapper.ConfigurationProvider;
            this.data = data;
        }

        public IActionResult Index()
        {
            var planetarySystems = this.data
                .PlanetarySystems
                .OrderByDescending(p => p.Id)
                .ProjectTo<PlanetarySystemIndexViewModel>(this.mapper)
                .ToList();

            var planets = this.data
                .Planets
                .OrderByDescending(p => p.Id)
                .ProjectTo<PlanetIndexViewModel>(this.mapper)
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
