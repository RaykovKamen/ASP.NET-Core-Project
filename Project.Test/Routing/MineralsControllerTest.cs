using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Minerals;
using Xunit;

namespace Project.Test.Routing
{
    public class MineralsControllerTest
    {
        [Fact]
        public void AddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Minerals/Add")
            .To<MineralsController>(c => c.Add());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Minerals/Add")
            .WithMethod(HttpMethod.Post))
            .To<MineralsController>(c => c.Add(With.Any<MineralFormModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Minerals/Delete/0")
            .To<MineralsController>(c => c.Delete(0));

        [Fact]
        public void AllShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
           .WithPath("/Minerals/All"))
           .To<MineralsController>(c => c.All());
    }
}
