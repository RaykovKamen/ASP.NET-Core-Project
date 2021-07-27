namespace Project.Services.Creators
{
    public interface ICreatorService
    {
        public bool IsCreator(string userId);

        public int IdByUser(string userId);
    }
}
 