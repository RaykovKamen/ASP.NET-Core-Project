using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Home;
using System;
using Xunit;

namespace Project.Test.Controllers
{
    using static Data.Planets;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
            .Instance(controller => controller
            .WithData(TenPublicPlanets()))
            .Calling(c => c.Index())
            .ShouldHave()
            .MemoryCache(cache => cache
            .ContainingEntry(entrie => entrie
            .WithKey(LatestPlanetsCacheKey)
            .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMilliseconds(15))))
            .AndAlso()
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<IndexViewModel>());

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();
    }
}
