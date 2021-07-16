using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;

    public class Satellite
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; init; }

        public int PlanetId { get; set; }

        public int MoonId { get; set; }

        public Planet Planet { get; init; }

        public Moon Moon { get; init; }
    }
}
