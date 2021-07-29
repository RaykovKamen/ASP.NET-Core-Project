using Microsoft.AspNetCore.Mvc;

namespace Project.Areas.Admin.Controllers
{
    public class CarsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
