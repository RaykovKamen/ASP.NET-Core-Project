using Project.Services.Minerals.Models;
using System.Collections.Generic;

namespace Project.Services.Moons.Models
{
    public class MoonDetailsServiceModel : MoonServiceModel
    {
        public string UserId { get; init; }
        
        public IEnumerable<MineralServiceModel> Minerals { get; init; }
    }
}
