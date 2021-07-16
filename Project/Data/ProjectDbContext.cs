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

        public DbSet<Moon> Moons { get; init; }

        public DbSet<PlanetarySystem> PlanetarySystems { get; init; }

        public DbSet<Mineral> Minerals { get; init; }

        public DbSet<Satellite> Satellites { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Planet>()
                .HasOne(p => p.PlanetarySystem)
                .WithMany(p => p.Planets)
                .HasForeignKey(p => p.PlanetarySystemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Moon>()
                .HasOne(p => p.Planet)
                .WithMany(p => p.Moons)
                .HasForeignKey(p => p.PlanetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Mineral>()
                .HasOne(p => p.Planet)
                .WithMany(p => p.Minerals)
                .HasForeignKey(p => p.PlanetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Mineral>()
                .HasOne(p => p.Moon)
                .WithMany(p => p.Mineral)
                .HasForeignKey(p => p.MoonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Satellite>()
                .HasOne(p => p.Planet)
                .WithMany(p => p.Satellites)
                .HasForeignKey(p => p.PlanetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Satellite>()
                .HasOne(p => p.Moon)
                .WithMany(p => p.Satellites)
                .HasForeignKey(p => p.MoonId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
