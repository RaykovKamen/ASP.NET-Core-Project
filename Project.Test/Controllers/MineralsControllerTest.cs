using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Minerals;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    using static WebConstants;

    public class MineralsControllerTest
    {
        [Fact]
        public void AddShouldBeForCretorsAndReturnView()
        => MyController<MineralsController>
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
        public void PostAddShouldBeForCretorsAndRedirect()
            => MyController<MineralsController>
            .Instance(controller => controller
            .WithUser(user => user.WithIdentifier("1"))
            .WithData(new Creator { UserId = "1" })
            .WithData(new Planet()))
            .Calling(c => c.Add(new MineralFormModel
            {
                Aluminum = 1,
                Beryllium = 1,
                Cadmium = 1,
                Copper = 1,
                Fluorite = 1,
                Graphite = 1,
                Iridium = 1,
                Iron = 1,
                Lithium = 1,
                Magnesium = 1,
                Nickel = 1,
                Platinum = 1,
                Silicon = 1,
                Titanium = 1,
                Uranium = 1,
                Vanadium = 1,
                PlanetId = 1
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Mineral>(mineral => mineral
                .Any(c =>
                c.Aluminum == 1 &&
                c.Aluminum == 1 &&
                c.Beryllium == 1 &&
                c.Cadmium == 1 &&
                c.Copper == 1 &&
                c.Fluorite == 1 &&
                c.Graphite == 1 &&
                c.Iridium == 1 &&
                c.Iron == 1 &&
                c.Lithium == 1 &&
                c.Magnesium == 1 &&
                c.Nickel == 1 &&
                c.Platinum == 1 &&
                c.Silicon == 1 &&
                c.Titanium == 1 &&
                c.Uranium == 1 &&
                c.Vanadium == 1 &&
                c.PlanetId == 1)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect("/Planets/All");

        [Fact]
        public void AllShouldReturnView()
           => MyController<MineralsController>
           .Instance()
           .Calling(c => c.All())
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .View();

        [Fact]
        public void DeleteShouldBeForCretorsAndRedirect()
           => MyController<MineralsController>
           .Instance(controller => controller
           .WithUser(user => user.WithIdentifier("1"))
           .WithData(new Creator { UserId = "1" })
           .WithData(new Mineral()))
           .Calling(c => c.Delete(1))
           .ShouldHave()
           .ActionAttributes(attributes => attributes
           .RestrictingForAuthorizedRequests())
           .AndAlso()
           .ShouldReturn()
           .Redirect("/Minerals/All");
    }
}
