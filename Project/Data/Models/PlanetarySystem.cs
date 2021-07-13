using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;

    public class PlanetarySystem
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Planet> Planets { get; init; } = new List<Planet>();
    }
}
