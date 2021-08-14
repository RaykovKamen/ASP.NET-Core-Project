using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Moons;
using Xunit;

namespace Project.Test.Routing
{
    public class MoonsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Moons/All"))
            .To<MoonsController>(c => c.All(With.Any<AllMoonsQueryModel>()));

        [Fact]
        public void MineShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Moons/Mine")
            .To<MoonsController>(c => c.Mine());

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Moons/Details/1/Luna"))
            .To<MoonsController>(c => c.Details(1, "Luna"));

        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Moons/Add")
            .To<MoonsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Moons/Add")
            .WithMethod(HttpMethod.Post))
            .To<MoonsController>(c => c.Add(With.Any<MoonFormModel>()));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Moons/Edit/0")
            .To<MoonsController>(c => c.Edit(0));

        [Fact]
        public void PostEdditShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Moons/Edit/0")
            .WithMethod(HttpMethod.Post))
            .To<MoonsController>(c => c.Edit(0, With.Any<MoonFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Moons/Delete/0")
            .To<MoonsController>(c => c.Delete(0));
    }
}
