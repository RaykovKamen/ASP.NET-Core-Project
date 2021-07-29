using Project.Data;
using Project.Services.Planets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Planets
{
    using static DataConstants;

    public class PlanetFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        [Display(Name = "Orbital Distance")]
        public double? OrbitalDistance { get; init; }

        [Required]
        [Display(Name = "Orbital Period")]
        public double? OrbitalPeriod { get; init; }

        [Required]
        public int? Radius { get; init; }

        [Required]
        [Display(Name = "Atmospheric Pressure")]
        public double? AtmosphericPressure { get; init; }

        [Display(Name = "Surface Temperature if there is a surface")]
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

        public IEnumerable<PlanetarySystemServiceModel> PlanetarySystems { get; set; }
    }
}
