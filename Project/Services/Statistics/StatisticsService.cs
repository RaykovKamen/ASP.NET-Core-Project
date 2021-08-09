using Project.Data;
using System.Linq;

namespace Project.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ProjectDbContext data;

        public StatisticsService(ProjectDbContext data)
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalPlanets = this.data.Planets.Count();
            var totalUsers = this.data.Users.Count();
            var totalMinerals = this.data.Minerals.Count();

            return new StatisticsServiceModel
            {
                TotalPlanets = totalPlanets,
                TotalUsers = totalUsers,
                TotalMinerals = totalMinerals
            };
        }
    }
}
