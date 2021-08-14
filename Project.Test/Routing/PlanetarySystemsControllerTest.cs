using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.PlanetarySystems;
using Xunit;

namespace Project.Test.Routing
{
    public class PlanetarySystemsControllerTest
    {
        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/PlanetarySystems/Add")
            .To<PlanetarySystemsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/PlanetarySystems/Add")
            .WithMethod(HttpMethod.Post))
            .To<PlanetarySystemsController>(c => c.Add(With.Any<PlanetarySystemFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/PlanetarySystems/Delete/0")
            .To<PlanetarySystemsController>(c => c.Delete(0));
    }
}
