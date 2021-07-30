using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class PlanetesController : AdminController
    {
        public IActionResult Index() => View();
    }
}
