using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.PlanetarySystems;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    using static WebConstants;

    public class PlanetarySystemsControllerTest
    {
        [Fact]
        public void AddShouldBeForCretorsAndReturnView()
        => MyController<PlanetarySystemsController>
        .Instance(controller => controller
        .WithUser(user => user.WithIdentifier("1"))
        .WithData(new Creator { UserId = "1" }))
        .Calling(c => c.Add())
        .ShouldHave()
        .ActionAttributes(attributes => attributes
        .RestrictingForAuthorizedRequests())
        .AndAlso()
        .ShouldReturn()
        .View();

        [Fact]
        public void PostAddShouldBeForCretorsAndRedirect()
            => MyController<PlanetarySystemsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" }))
            .Calling(c => c.Add(new PlanetarySystemFormModel
            {
                Name = "Test"
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<PlanetarySystem>(planetarySystem => planetarySystem
                .Any(c =>
                c.Name == "Test")))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Home");

        [Fact]
        public void DeleteShouldBeForCretorsAndRedirect()
           => MyController<PlanetarySystemsController>
           .Instance(controller => controller
           .WithUser(user => user.WithIdentifier("1"))
           .WithData(new Creator { UserId = "1" })
           .WithData(new PlanetarySystem()))
           .Calling(c => c.Delete(1))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .Redirect("/");
    }
}