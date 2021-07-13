using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data
{
    public class ProjectDbContext : IdentityDbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        public DbSet<Planet> Planets { get; init; }

        public DbSet<PlanetarySystem> PlanetarySystems { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Planet>()
                .HasOne(p => p.PlanetarySystem)
                .WithMany(p => p.Planets)
                .HasForeignKey(p => p.PlanetarySystemId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
