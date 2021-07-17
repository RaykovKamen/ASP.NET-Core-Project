using Project.Data;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Creators
{
    using static DataConstants;

    public class BecomeCreatorFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }
    }
}
