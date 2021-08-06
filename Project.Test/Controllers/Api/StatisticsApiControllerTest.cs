using Project.Controllers.Api;
using Project.Test.Mocks;
using Xunit;

namespace Project.Test.Controllers.Api
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalPlanets);
            Assert.Equal(10, result.TotalUsers);
            Assert.Equal(15, result.TotalMissions);
        }
    }
}
