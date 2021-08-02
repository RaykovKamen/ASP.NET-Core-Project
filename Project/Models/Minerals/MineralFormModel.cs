using Project.Services.Minerals.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Minerals
{
    public class MineralFormModel
    {
        [Range(0, 100)]
        public int? Aluminum { get; init; }

        [Range(0, 100)]
        public int? Beryllium { get; init; }

        [Range(0, 100)]
        public int? Cadmium { get; init; }

        [Range(0, 100)]
        public int? Copper { get; init; }

        [Range(0, 100)]
        public int? Fluorite { get; init; }

        [Range(0, 100)]
        public int? Graphite { get; init; }

        [Range(0, 100)]
        public int? Iridium { get; init; }

        [Range(0, 100)]
        public int? Iron { get; init; }

        [Range(0, 100)]
        public int? Lithium { get; init; }

        [Range(0, 100)]
        public int? Magnesium { get; init; }

        [Range(0, 100)]
        public int? Nickel { get; init; }

        [Range(0, 100)]
        public int? Platinum { get; init; }

        [Range(0, 100)]
        public int? Silicon { get; init; }

        [Range(0, 100)]
        public int? Titanium { get; init; }

        [Range(0, 100)]
        public int? Uranium { get; init; }

        [Range(0, 100)]
        public int? Vanadium { get; init; }

        [Display(Name = "Planet Name")]
        public int PlanetId { get; init; }

        public IEnumerable<MineralServiceModel> Planets { get; set; }
    }
}
