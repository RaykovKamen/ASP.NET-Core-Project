using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Data.Models;
using Project.Models.Creators;
using System.Linq;
using Xunit;

namespace Project.Test.Pipeline
{
    using static WebConstants;

    public class CreatorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Creators/Become")
            .WithUser())
            .To<CreatorsController>(c => c.Become())
            .Which()
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
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Creators/Become")
            .WithMethod(HttpMethod.Post)
            .WithFormFields(new
            { 
                Name = creatorName
            })
            .WithUser()
            .WithAntiForgeryToken())
            .To<CreatorsController>(c => c.Become(new BecomeCreatorFormModel
            {
                Name = creatorName
            }))
            .Which()
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
