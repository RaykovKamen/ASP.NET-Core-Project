using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.PlanetarySystems;
using Xunit;

namespace Project.Test.Controllers
{
    using static Data.Planets;

    public class PlanetarySystemsControllerTest
    {
        [Fact]
        public void GetAddShouldBeForAuthorizrdUsersAndReturnRedirectWithValidModel()
            => MyController<PlanetarySystemsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Add())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<CreatorsController>(c => c.Become()));

        [Theory]
        [InlineData("planetarySystemName")]
        public void PostAddShouldBeForAuthorizrdUsersAndReturnRedirectWithValidModel(
            string planetarySystemName)
            => MyController<PlanetarySystemsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Add(new PlanetarySystemFormModel
            {
                Name = planetarySystemName
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<CreatorsController>(c => c.Become()));

        [Fact]
        public void GetDeleteShouldBeForAuthorizrdUsersAndReturnRedirectWithValidModel()
            => MyController<PlanetarySystemsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Delete(0))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<CreatorsController>(c => c.Become()));
    }
}
