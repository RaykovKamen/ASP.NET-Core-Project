using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class Mineral
    {
        public int Id { get; init; }

        [Range(0, 100)]
        public int? Aluminum { get; set; }

        [Range(0, 100)]
        public int? Beryllium { get; set; }

        [Range(0, 100)]
        public int? Cadmium { get; set; }

        [Range(0, 100)]
        public int? Copper { get; set; }

        [Range(0, 100)]
        public int? Fluorite { get; set; }

        [Range(0, 100)]
        public int? Graphite { get; set; }

        [Range(0, 100)]
        public int? Iridium { get; set; }

        [Range(0, 100)]
        public int? Iron { get; set; }

        [Range(0, 100)]
        public int? Lithium { get; set; }

        [Range(0, 100)]
        public int? Magnesium { get; set; }

        [Range(0, 100)]
        public int? Nickel { get; set; }

        [Range(0, 100)]
        public int? Platinum { get; set; }

        [Range(0, 100)]
        public int? Silicon { get; set; }

        [Range(0, 100)]
        public int? Titanium { get; set; }

        [Range(0, 100)]
        public int? Uranium { get; set; }

        [Range(0, 100)]
        public int? Vanadium { get; set; }

        public int PlanetId { get; set; }

        public Planet Planet { get; init; }
    }
}
