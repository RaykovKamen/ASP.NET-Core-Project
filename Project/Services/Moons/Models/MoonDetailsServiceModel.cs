namespace Project.Services.Moons.Models
{
    public class MoonDetailsServiceModel : MoonServiceModel
    {
        public int CreatorId { get; init; }

        public int PlanetId { get; init; }

        public string CreatorName { get; init; }

        public string UserId { get; init; }
    }
}
