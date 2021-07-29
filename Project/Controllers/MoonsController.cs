using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Infrastructure;
using Project.Models.Moons;
using Project.Services.Creators;
using Project.Services.Moons;

namespace Project.Controllers
{
    public class MoonsController : Controller
    {
        private readonly IMoonService moons;
        private readonly ICreatorService creators;
        private readonly IMapper mapper;

        public MoonsController(
            IMoonService moons,
            ICreatorService creators, 
            IMapper mapper)
        {
            this.moons = moons;
            this.creators = creators;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllMoonsQueryModel query)
        {
            var queryResult = this.moons.All(
                query.SearchTerm,
                query.CurrentPage,
                AllMoonsQueryModel.MoonsPerPage);

            query.TotalMoons = queryResult.TotalMoons;
            query.Moons = queryResult.Moons;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.creators.IsCreator(this.User.Id()))
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            return View(new MoonFormModel
            {
                Planets = this.moons.AllPlanets()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MoonFormModel moon)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0)
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.moons.PlanetExists(moon.PlanetId))
            {
                this.ModelState.AddModelError(nameof(moon.PlanetId), "Planet does not exist.");
            }

            if (!ModelState.IsValid)
            {
                moon.Planets = this.moons.AllPlanets();

                return View(moon);
            }

            this.moons.Create(
                moon.Name,
                (double)moon.OrbitalDistance,
                (double)moon.OrbitalPeriod,
                (int)moon.Radius,
                (double)moon.AtmosphericPressure,
                (int)moon.SurfaceTemperature,
                moon.Analysis,
                moon.ImageUrl,
                moon.PlanetId,
                creatorId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.creators.IsCreator(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            var moon = this.moons.Details(id);

            if (moon.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var moonForm = this.mapper.Map<MoonFormModel>(moon);

            moonForm.Planets = this.moons.AllPlanets();

            return View(moonForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MoonFormModel moon)
        {
            var creatorId = this.creators.IdByUser(this.User.Id());

            if (creatorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(CreatorsController.Become), "Creators");
            }

            if (!this.moons.PlanetExists(moon.PlanetId))
            {
                this.ModelState.AddModelError(nameof(moon.PlanetId), "Planet does not exist.");
            }

            if (!ModelState.IsValid)
            {
                moon.Planets = this.moons.AllPlanets();

                return View(moon);
            }

            if (!this.moons.IsByCreator(id, creatorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.moons.Edit(
                id,
                moon.Name,
                (double)moon.OrbitalDistance,
                (double)moon.OrbitalPeriod,
                (int)moon.Radius,
                (double)moon.AtmosphericPressure,
                (int)moon.SurfaceTemperature,
                moon.Analysis,
                moon.ImageUrl,
                moon.PlanetId);

            return RedirectToAction(nameof(All));
        }
    }
}