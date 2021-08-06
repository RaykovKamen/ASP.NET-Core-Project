using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Home;
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

        private static IEnumerable<Planet> GetPlanets()
            => Enumerable.Range(0, 10).Select(i => new Planet());
    }
}
