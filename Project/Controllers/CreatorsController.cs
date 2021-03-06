using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.Models;
using Project.Infrastructure.Extensions;
using Project.Models.Creators;
using System.Linq;

namespace Project.Controllers
{
    using static Project.WebConstants;

    public class CreatorsController : Controller
    {
        private readonly ProjectDbContext data;

        public CreatorsController(ProjectDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeCreatorFormModel creator)
        {
            var userId = this.User.Id();

            var userIdAlreadyCreator = this.data
                .Creators
                .Any(c => c.UserId == userId);

            if (userIdAlreadyCreator)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(creator);
            }

            var cratorData = new Creator
            {
                Name = creator.Name,
                UserId = userId
            };

            this.data.Creators.Add(cratorData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a creator!";

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
