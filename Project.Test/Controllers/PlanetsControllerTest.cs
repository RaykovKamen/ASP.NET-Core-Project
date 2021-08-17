using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Planets;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    using static WebConstants;

    public class PlanetsControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
           => MyController<PlanetsController>
           .Instance()
           .Calling(c => c.All(new AllPlanetsQueryModel()))
           .ShouldReturn()
           .View();

        [Fact]
        public void MineShouldReturnView()
           => MyController<PlanetsController>
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
         => MyController<PlanetsController>
             .Instance(controller => controller
             .WithUser(user => user.WithIdentifier("1"))
             .WithData(new Creator { UserId = "1" })
             .WithData(new Planet
             {
                 Name = "Test",
                 OrbitalDistance = 1,
                 OrbitalPeriod = 1,
                 Radius = 1,
                 AtmosphericPressure = 1,
                 SurfaceTemperature = 1,
                 Analysis = "TestTestTest",
                 ImageUrl = "https://picsum.photos/200/300",
                 PlanetarySystemId = 1,
                 CreatorId = 1,
             })
             .WithData(new PlanetarySystem { Id = 1 }))
         .Calling(c => c.Details(1, "Test"))
         .ShouldReturn()
         .View();

        [Fact]
        public void AddShouldBeForCretorsAndReturnView()
        => MyController<PlanetsController>
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
            => MyController<PlanetsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new PlanetarySystem { Id = 1}))
            .Calling(c => c.Add(new PlanetFormModel
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetarySystemId = 1             
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Planet>(planet => planet
                .Any(c =>
                c.Name == "Test" &&
                c.OrbitalDistance == 1 &&
                c.OrbitalPeriod == 1 &&
                c.Radius == 1 &&
                c.AtmosphericPressure == 1 &&
                c.SurfaceTemperature == 1 &&
                c.Analysis == "TestTestTest" &&
                c.ImageUrl == "https://picsum.photos/200/300" &&
                c.PlanetarySystemId == 1)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<PlanetsController>(c => c.Details( 1, "Test")));

        [Fact]
        public void EditShouldBeForCretorsAndReturnView()
        => MyController<PlanetsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Planet
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetarySystemId = 1,
                CreatorId = 1,
            })
            .WithData(new PlanetarySystem { Id = 1 }))
        .Calling(c => c.Edit(1))
        .ShouldHave()
        .ActionAttributes(attributes => attributes
        .RestrictingForAuthorizedRequests())
        .AndAlso()
        .ShouldReturn()
        .View();

        [Fact]
        public void PostEditShouldBeForCretorsAndReturnView()
            => MyController<PlanetsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Planet { CreatorId = 1, Id = 1 })
            .WithData(new PlanetarySystem { Id = 1 }))
            .Calling(c => c.Edit(1, new PlanetFormModel
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetarySystemId = 1
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Planet>(planet => planet
                .Any(c =>
                c.Name == "Test" &&
                c.OrbitalDistance == 1 &&
                c.OrbitalPeriod == 1 &&
                c.Radius == 1 &&
                c.AtmosphericPressure == 1 &&
                c.SurfaceTemperature == 1 &&
                c.Analysis == "TestTestTest" &&
                c.ImageUrl == "https://picsum.photos/200/300" &&
                c.PlanetarySystemId == 1)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<PlanetsController>(c => c.Details(1, "Test")));

        [Fact]
        public void DeleteShouldBeForCretorsAndReturnView()
           => MyController<PlanetsController>
           .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Planet
            {
                Name = "Test",
                OrbitalDistance = 1,
                OrbitalPeriod = 1,
                Radius = 1,
                AtmosphericPressure = 1,
                SurfaceTemperature = 1,
                Analysis = "TestTestTest",
                ImageUrl = "https://picsum.photos/200/300",
                PlanetarySystemId = 1,
                CreatorId = 1,
            })
            .WithData(new PlanetarySystem { Id = 1 }))
           .Calling(c => c.Delete(1))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .Redirect("/Planets/All");
    }
}
