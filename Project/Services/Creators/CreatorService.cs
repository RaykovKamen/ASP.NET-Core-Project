using Project.Data;
using System.Linq;

namespace Project.Services.Creators
{
    public class CreatorService : ICreatorService
    {
        private readonly ProjectDbContext data;

        public CreatorService(ProjectDbContext data)
            => this.data = data;
       
        public bool IsCreator(string userId)
        => this.data
            .Creators
            .Any(c => c.UserId == userId);

        public int IdByUser(string userId)
        => this.data
                .Creators
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefault();
    }
}
