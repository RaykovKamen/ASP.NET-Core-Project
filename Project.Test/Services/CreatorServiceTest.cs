using Project.Data.Models;
using Project.Services.Creators;
using Project.Test.Mocks;
using Xunit;

namespace Project.Test.Services
{
    public class CreatorServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsCreatorShouldReturnTrueWhenUserIsCreator()
        {
            var creatorService = GetCreatorService();

            var result = creatorService.IsCreator(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsCreatorShouldReturnFalseWhenUserIsNotCreator()
        {
            var creatorService = GetCreatorService();

            var result = creatorService.IsCreator("AnotherUserId");

            Assert.False(result);
        }

        private static ICreatorService GetCreatorService()
        {
            var data = DatabaseMock.Instance;

            data.Creators.Add(new Creator { UserId = UserId });
            data.SaveChanges();

            return new CreatorService(data);
        }
    }
}
