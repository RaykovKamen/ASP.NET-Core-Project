using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Moons;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    using static WebConstants;

    public class MoonsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
           => MyController<MoonsController>
           .Instance()
           .Calling(c => c.All(new AllMoonsQueryModel()))
           .ShouldReturn()
           .View();

        [Fact]
        public void MineShouldReturnView()
           => MyController<MoonsController>
           .Instance()
           .WithUser()
           .Calling(c => c.Mine())
           .ShouldHave()
           .ActionAttributes()
           .AndAlso()
           .ShouldReturn()
           .View();

        [Fact]
        public void DetailsShoulReturnView()
         => MyController<MoonsController>
             .Instance(controller => controller
             .WithUser(user => user.WithIdentifier("1"))
             .WithData(new Creator { UserId = "1" })
             .WithData(new Moon
             {
                 Name = "Test",
                 OrbitalDistance = 1,
                 OrbitalPeriod = 1,
                 Radius = 1,
                 AtmosphericPressure = 1,
                 SurfaceTemperature = 1,
                 Analysis = "TestTestTest",
                 ImageUrl = "https://picsum.photos/200/300",
                 PlanetId = 1,
                 CreatorId = 1,
             })
             .WithData(new Planet { Id = 1 }))
         .Calling(c => c.Details(1, "Test"))
         .ShouldReturn()
         .View();

        [Fact]
        public void AddShouldBeForCretorsAndReturnView()
        => MyController<MoonsController>
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
        public void PostAddShouldBeForCretorsAndReturnView()
            => MyController<MoonsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Planet { Id = 1 }))
            .Calling(c => c.Add(new MoonFormModel
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetId = 1
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Moon>(moon => moon
                .Any(c =>
                c.Name == "Test" &&
                c.OrbitalDistance == 1 &&
                c.OrbitalPeriod == 1 &&
                c.Radius == 1 &&
                c.AtmosphericPressure == 1 &&
                c.SurfaceTemperature == 1 &&
                c.Analysis == "TestTestTest" &&
                c.ImageUrl == "https://picsum.photos/200/300" &&
                c.PlanetId == 1)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<MoonsController>(c => c.Details(1, "Test")));

        [Fact]
        public void EditShouldBeForCretorsAndReturnView()
        => MyController<MoonsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Moon
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetId = 1,
                CreatorId = 1,
            })
            .WithData(new Planet { Id = 1 }))
        .Calling(c => c.Edit(1))
        .ShouldHave()
        .ActionAttributes(attributes => attributes
        .RestrictingForAuthorizedRequests())
        .AndAlso()
        .ShouldReturn()
        .View();

        [Fact]
        public void PostEditShouldBeForCretorsAndReturnView()
            => MyController<MoonsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Moon { CreatorId = 1, Id = 1 })
            .WithData(new Planet { Id = 1 }))
            .Calling(c => c.Edit(1, new MoonFormModel
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetId = 1
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Moon>(moon => moon
                .Any(c =>
                c.Name == "Test" &&
                c.OrbitalDistance == 1 &&
                c.OrbitalPeriod == 1 &&
                c.Radius == 1 &&
                c.AtmosphericPressure == 1 &&
                c.SurfaceTemperature == 1 &&
                c.Analysis == "TestTestTest" &&
                c.ImageUrl == "https://picsum.photos/200/300" &&
                c.PlanetId == 1)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<MoonsController>(c => c.Details(1, "Test")));

        [Fact]
        public void DeleteShouldBeForCretorsAndReturnView()
           => MyController<MoonsController>
           .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Moon
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetId = 1,
                CreatorId = 1,
            })
            .WithData(new Planet { Id = 1 }))
           .Calling(c => c.Delete(1))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .Redirect("/Planets/All");
    }
}
