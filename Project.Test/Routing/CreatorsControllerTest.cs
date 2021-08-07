using MyTested.AspNetCore.Mvc;
using Project.Controllers;
using Project.Models.Creators;
using Xunit;

namespace Project.Test.Routing
{
    public class CreatorsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Creators/Become")
            .To<CreatorsController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Creators/Become")
            .WithMethod(HttpMethod.Post))
            .To<CreatorsController>(c => c.Become(With.Any<BecomeCreatorFormModel>()));
    }
}
