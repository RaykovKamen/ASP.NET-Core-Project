using Project.Services.Planets.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.Planets
{
    public class AllPlanetsQueryModel
    {
        public const int PlanetsPerPage = 6;

        [Display(Name = "Search by name:")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPlanets { get; set; } 

        public IEnumerable<PlanetServiceModel> Planets { get; set; }
    }
}
