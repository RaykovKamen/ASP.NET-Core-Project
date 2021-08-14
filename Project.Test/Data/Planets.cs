using Project.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Test.Data
{
    public static class Planets
    {
        public static IEnumerable<Planet> TenPublicPlanets()
            => Enumerable.Range(0, 10).Select(i => new Planet());

        public static IEnumerable<User> TenPublicUsers()
            => Enumerable.Range(0, 10).Select(i => new User());
    }
}
