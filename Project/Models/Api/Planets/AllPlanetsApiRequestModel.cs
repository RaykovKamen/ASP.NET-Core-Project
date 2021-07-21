namespace Project.Models.Api.Planets
{
    public class AllPlanetsApiRequestModel
    {
        public string SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int PlanetsPerPage { get; init; } = 10;
    }
}
