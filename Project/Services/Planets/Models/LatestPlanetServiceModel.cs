namespace Project.Services.Planets.Models
{
    public class LatestPlanetServiceModel : IPlanetModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }
    }
}
