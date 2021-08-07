using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Home;
using Xunit;

namespace Project.Test.Pipeline
{
    using static Data.Planets;

    public class HomeControllerTest
    {

        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
            .Pipeline()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index())
            .Which(controller => controller
            .WithData(TenPublicPlanets()))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<IndexViewModel>()
            .Passing(m => m.Planets.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error())
            .Which()
            .ShouldReturn()
            .View();
    }
}
