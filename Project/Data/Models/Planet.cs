using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;
    public class Planet
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        [Required]
        public double OrbitalDistance { get; set; }

        [Required]
        public double OrbitalPeriod { get; set; }

        [Required]
        public int Radius { get; set; }

        [Required]
        public double AtmosphericPressure { get; set; }

        public int? SurfaceTemperature { get; set; }

        [Required]
        [MaxLength(AnalysisMaxLength)]
        public string Analysis { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PlanetarySystemId { get; set; }

        public PlanetarySystem PlanetarySystem { get; init; }
    }
}
