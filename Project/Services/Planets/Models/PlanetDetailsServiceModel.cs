namespace Project.Services.Planets.Models
{
    public class PlanetDetailsServiceModel : PlanetServiceModel
    {
        public int CreatorId { get; init; }

        public int PlanetarySystemId { get; init; }

        public string CreatorName { get; init; }

        public string UserId { get; init; }
    }
}
