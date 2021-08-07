using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Creators;
using System.Linq;
using Xunit;

namespace Project.Test.Controllers
{
    using static WebConstants;

    public class CreatorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<CreatorsController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Creator")]
        public void PostBecomeShouldBeForAuthorizrdUsersAndReturnRedirectWithValidModel(
            string creatorName)
            => MyController<CreatorsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Become(new BecomeCreatorFormModel
            {
                Name = creatorName
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Creator>(creators => creators
                .Any(c =>
                c.Name == creatorName &&
                c.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect
            .To<HomeController>(c => c.Index()));
    }
}
