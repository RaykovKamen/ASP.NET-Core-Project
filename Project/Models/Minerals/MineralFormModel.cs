using Project.Services.Minerals.Models;
using System.Collections.Generic;

namespace Project.Models.Minerals
{
    public class MineralFormModel
    {
        public int? Aluminum { get; init; }
                  
        public int? Beryllium { get; init; }
                  
        public int? Cadmium { get; init; }
                  
        public int? Copper { get; init; }
                  
        public int? Fluorite { get; init; }
                  
        public int? Graphite { get; init; }
                  
        public int? Iridium { get; init; }
                  
        public int? Iron { get; init; }
                  
        public int? Lithium { get; init; }
                  
        public int? Magnesium { get; init; }
                  
        public int? Nickel { get; init; }
                  
        public int? Platinum { get; init; }
                  
        public int? Silicon { get; init; }
                  
        public int? Titanium { get; init; }
                  
        public int? Uranium { get; init; }
                  
        public int? Vanadium { get; init; }

        public int PlanetId { get; init; }

        public IEnumerable<MineralServiceModel> Planets { get; set; }
    }
}
