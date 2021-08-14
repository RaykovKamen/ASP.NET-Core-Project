using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Planets;
using Xunit;

namespace Project.Test.Routing
{
    public class PlanetsControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Planets/All"))
            .To<PlanetsController>(c => c.All(With.Any<AllPlanetsQueryModel>()));

        [Fact]
        public void MineShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Planets/Mine")
            .To<PlanetsController>(c => c.Mine());

        [Fact]
        public void DetailsShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Planets/Details/1/Earth"))
            .To<PlanetsController>(c => c.Details(1, "Earth"));

        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Planets/Add")
            .To<PlanetsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Planets/Add")
            .WithMethod(HttpMethod.Post))
            .To<PlanetsController>(c => c.Add(With.Any<PlanetFormModel>()));

        [Fact]
        public void EditShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Planets/Edit/0")
            .To<PlanetsController>(c => c.Edit(0));

        [Fact]
        public void PostEdditShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Planets/Edit/0")
            .WithMethod(HttpMethod.Post))
            .To<PlanetsController>(c => c.Edit(0, With.Any<PlanetFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Planets/Delete/0")
            .To<PlanetsController>(c => c.Delete(0));
    }
}
