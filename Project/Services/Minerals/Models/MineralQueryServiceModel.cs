using System.Collections.Generic;

namespace Project.Services.Minerals.Models
{
    public class MineralQueryServiceModel 
    {
        public int TotalMinerals { get; init; }

        public IEnumerable<MineralServiceModel> Minerals { get; init; }
    }
}
