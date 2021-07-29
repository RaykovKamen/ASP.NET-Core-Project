using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;

    public class Moon
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public double OrbitalDistance { get; set; }

        [Required]
        public double OrbitalPeriod { get; set; }

        [Required]
        public int Radius { get; set; }

        [Required]
        public double AtmosphericPressure { get; set; }

        [Required]
        public int SurfaceTemperature { get; set; }

        [Required]
        [MaxLength(AnalysisMaxLength)]
        public string Analysis { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PlanetId { get; set; }

        public int CreatorId { get; init; }

        public Planet Planet { get; init; }

        public Creator Creator { get; init; }

        public IEnumerable<Satellite> Satellites { get; init; } = new List<Satellite>();

        public IEnumerable<Mineral> Mineral { get; init; } = new List<Mineral>();
    }
}
