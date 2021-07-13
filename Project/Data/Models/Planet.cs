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
        public int OrbitalDistance { get; set; }

        [Required]
        public int OrbitalPeriod { get; set; }

        [Required]
        public int Radius { get; set; }

        [Required]
        public int AtmosphericPressure { get; set; }

        [Required]
        public int SurfaceTemperature { get; set; }

        [Required]
        [MaxLength(AnalysisMaxLength)]
        public string Analysis { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PlanetarySystemId { get; set; }

        public PlanetarySystem PlanetarySystem { get; init; }
    }
}
