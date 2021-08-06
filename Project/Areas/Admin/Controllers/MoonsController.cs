using Microsoft.AspNetCore.Mvc;
using Project.Services.Moons;

namespace Project.Areas.Admin.Controllers
{
    public class MoonsController : AdminController
    {
        private readonly IMoonService moons;

        public MoonsController(IMoonService moons)
            => this.moons = moons;

        public IActionResult All() => View(this.moons.All().Moons);
    }
}
