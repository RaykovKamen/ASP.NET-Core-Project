using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project.Models.Home;
using Project.Services.PlanetarySystems;
using Project.Services.PlanetarySystems.Models;
using Project.Services.Planets;
using Project.Services.Planets.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlanetService planets;
        private readonly IPlanetarySystemService planetarySystems;
        private readonly IMemoryCache cache;

        public HomeController(
            IPlanetService planets,
            IPlanetarySystemService planetarySystems, 
            IMemoryCache cache)
        {
            this.planets = planets;
            this.planetarySystems = planetarySystems;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestPlanetsCacheKey = "LatestPlanetsCacheKey";
            const string latestPlanetarySystemsCacheKey = "LatestPlanetarySystemsCacheKey";

            var latestPlanets = this.cache.Get<List<LatestPlanetServiceModel>>(latestPlanetsCacheKey);
            var latestPlanetarySystems = this.cache.Get<List<LatestPlanetarySystemServiceModel>>(latestPlanetarySystemsCacheKey);

            if (latestPlanets == null)
            {
                latestPlanets = this.planets
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestPlanetsCacheKey, latestPlanets, cacheOptions);
            }

            if (latestPlanetarySystems == null)
            {
                latestPlanetarySystems = this.planetarySystems
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestPlanetarySystemsCacheKey, latestPlanetarySystems, cacheOptions);
            }

            return View(new IndexViewModel
            {            
                Planets = latestPlanets,
                PlanetarySystems = latestPlanetarySystems
            });
        }

        public IActionResult Error() => View();
    }
}
