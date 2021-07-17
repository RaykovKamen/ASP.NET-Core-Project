using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    using static DataConstants;

    public class Creator
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Planet> Planets { get; init; } = new List<Planet>(); 
    }
}
