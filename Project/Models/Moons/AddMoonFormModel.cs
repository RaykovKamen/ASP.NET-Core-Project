using Project.Data;
using Project.Models.Planets;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Moons
{
    using static DataConstants;

    public class AddMoonFormModel
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

        [Display(Name = "Planet")]
        public int PlanetId { get; init; }

        public IEnumerable<PlanetViewModel> Planets { get; set; }
    }
}
