using Moq;
using Project.Services.Statistics;

namespace Project.Test.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalPlanets = 5,
                        TotalUsers = 10,
                        TotalMisions = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
