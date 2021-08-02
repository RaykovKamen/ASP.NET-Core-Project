using Microsoft.EntityFrameworkCore;
using Project.Data;
using System;

namespace Project.Test.Mocks
{
    public static class DatabaseMock
    {
        public static ProjectDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ProjectDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ProjectDbContext(dbContextOptions);
            }
        }
    }
}
