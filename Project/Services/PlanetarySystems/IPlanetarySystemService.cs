namespace Project.Services.PlanetarySystems
{
    public interface IPlanetarySystemService
    {
        int Create(string name);

        bool PlanetarySystemExists(string planetartySystemName);
    }
}
