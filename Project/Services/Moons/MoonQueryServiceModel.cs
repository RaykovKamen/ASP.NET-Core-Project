using System.Collections.Generic;

namespace Project.Services.Moons
{
    public class MoonQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int MoonsPerPage { get; init; }

        public int TotalMoons { get; init; }

        public IEnumerable<MoonServiceModel> Moons { get; init; }
    }
}
