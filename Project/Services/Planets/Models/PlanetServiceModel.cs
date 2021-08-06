namespace Project.Services.Planets.Models
{
    public class PlanetServiceModel : IPlanetModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public double OrbitalDistance { get; init; }

        public double OrbitalPeriod { get; init; }

        public int Radius { get; init; }

        public double AtmosphericPressure { get; init; }

        public int? SurfaceTemperature { get; init; }

        public string Analysis { get; init; }

        public string ImageUrl { get; init; }

        public string PlanetarySystemName { get; init; }
    }
}
