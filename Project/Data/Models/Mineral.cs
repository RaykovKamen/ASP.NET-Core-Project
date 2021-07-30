namespace Project.Data.Models
{
    public class Mineral
    {
        public int Id { get; init; }
                  
        public int? Aluminum { get; set; }
                  
        public int? Beryllium { get; set; }
                  
        public int? Cadmium { get; set; }
                  
        public int? Copper { get; set; }
                  
        public int? Fluorite { get; set; }
                  
        public int? Graphite { get; set; }
                  
        public int? Iridium { get; set; }
                  
        public int? Iron { get; set; }
                  
        public int? Lithium { get; set; }
                  
        public int? Magnesium { get; set; }
                  
        public int? Nickel { get; set; }
                  
        public int? Platinum { get; set; }
                  
        public int? Silicon { get; set; }
                  
        public int? Titanium { get; set; }
                  
        public int? Uranium { get; set; }
                  
        public int? Vanadium { get; set; }

        public int PlanetId { get; set; }

        public Planet Planet { get; init; }
    }
}
