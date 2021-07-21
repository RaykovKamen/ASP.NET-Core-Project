namespace Project.Services.Planets
{
    public interface IPlanetService
    {
        PlanetQueryServiceModel All(
            string searchTerm,
            int currentPage,
            int planetsPerPage);
    }
}
