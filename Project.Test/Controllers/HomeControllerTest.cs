using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Home;
using Project.Services.PlanetarySystems;
using Project.Services.Planets;
using Project.Services.Statistics;
using Project.Test.Mocks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index())
            .Which(controller => controller
            .WithData(GetPlanets()))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<IndexViewModel>()
            .Passing(m => m.Planets.Should().HaveCount(3)));

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var planets = Enumerable
                .Range(0, 10)
                .Select(i => new Planet());

            data.Planets.AddRange(planets);
            data.Users.Add(new User());
            data.SaveChanges();

            var planetService = new PlanetService(data, mapper);
            var planetarySystemService = new PlanetarySystemService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, planetService, planetarySystemService);

            var result = homeController.Index();

            result
                .Should()
                .NotBeNull()
                .And
                .BeAssignableTo<ViewResult>()
                .Which
                .Model
                .As<IndexViewModel>()
                .Invoking(model =>
                {
                    model.Planets.Should().HaveCount(3);
                    model.TotalPlanets.Should().Be(10);
                    model.TotalUsers.Should().Be(1);
                })
                .Invoke();
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(
                null,
                null,
                null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<Planet> GetPlanets()
            => Enumerable.Range(0, 10).Select(i => new Planet());
    }
}
