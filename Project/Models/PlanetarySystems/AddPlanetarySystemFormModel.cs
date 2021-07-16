using Project.Data;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.PlanetarySystems
{
    using static DataConstants;

    public class AddPlanetarySystemFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }
    }
}
