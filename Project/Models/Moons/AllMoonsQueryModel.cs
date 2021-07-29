using Project.Services.Moons.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Moons
{
    public class AllMoonsQueryModel
    {
        public const int MoonsPerPage = 6;

        [Display(Name = "Search by name:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalMoons{ get; set; }

        public IEnumerable<MoonServiceModel> Moons { get; set; }
    }
}
