using Project.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Planets
{
    using static DataConstants;

    public class AddPlanetFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Orbital Distance")]
        public int? OrbitalDistance { get; init; }

        [Required]
        [Display(Name = "Orbital Period")]
        public int? OrbitalPeriod { get; init; }

        [Required]
        public int? Radius { get; init; }

        [Required]
        [Display(Name = "Atmospheric Pressure")]
        public int? AtmosphericPressure { get; init; }

        [Required]
        [Display(Name = "Surface Temperature")]
        public int? SurfaceTemperature { get; init; }

        [Required]
        [StringLength(AnalysisMaxLength, MinimumLength = AnalysisMinLength)]
        public string Analysis { get; init; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; init; }

        [Display(Name = "Planetary System")]
        public int PlanetarySystemId { get; init; }

        public IEnumerable<PlanetarySystemViewModel> PlanetarySystems { get; set; }
    }
}
