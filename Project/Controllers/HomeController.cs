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
    using static WebConstants.Cache;

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
            var latestPlanets = this.cache.Get<List<LatestPlanetServiceModel>>(LatestPlanetsCacheKey);
            var latestPlanetarySystems = this.cache.Get<List<LatestPlanetarySystemServiceModel>>(LatestPlanetarySystemsCacheKey);

            if (latestPlanets == null)
            {
                latestPlanets = this.planets
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMilliseconds(15));

                this.cache.Set(LatestPlanetsCacheKey, latestPlanets, cacheOptions);
            }

            if (latestPlanetarySystems == null)
            {
                latestPlanetarySystems = this.planetarySystems
                   .Latest()
                   .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMilliseconds(15));

                this.cache.Set(LatestPlanetarySystemsCacheKey, latestPlanetarySystems, cacheOptions);
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
