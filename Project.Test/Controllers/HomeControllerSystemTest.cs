using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Project.Test.Controllers
{
    public class HomeControllerSystemTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Startup> factory)
            => this.factory = factory;

        [Fact]
        public async Task IndexShouldReturnCorrectResult()
        {
            var client = this.factory.CreateClient();

            var result = await client.GetAsync("/");

            Assert.True(result.IsSuccessStatusCode);    
        }
    }
}
