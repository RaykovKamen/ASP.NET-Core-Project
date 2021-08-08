using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class SatellitesController : Controller
    {
        [Authorize]
        public IActionResult Sent(int id)
        {
            return null;
        }
    }
}
