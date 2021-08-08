using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;

namespace Project.Data
{
    public class ProjectDbContext : IdentityDbContext<User>
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

        public DbSet<Creator> Creators { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Planet>()
                .HasOne(p => p.PlanetarySystem)
                .WithMany(p => p.Planets)
                .HasForeignKey(p => p.PlanetarySystemId);

            builder
                .Entity<Planet>()
                .HasOne(p => p.Creator)
                .WithMany(c => c.Planets)
                .HasForeignKey(p => p.CreatorId);

            builder
                .Entity<Moon>()
                .HasOne(m => m.Planet)
                .WithMany(p => p.Moons)
                .HasForeignKey(m => m.PlanetId);

            builder
                .Entity<Moon>()
                .HasOne(m => m.Creator)
                .WithMany(c => c.Moons)
                .HasForeignKey(m => m.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Mineral>()
                .HasOne(m => m.Planet)
                .WithMany(p => p.Minerals)
                .HasForeignKey(m => m.PlanetId);

            builder
                .Entity<Satellite>()
                .HasOne(s => s.Planet)
                .WithMany(p => p.Satellites)
                .HasForeignKey(s => s.PlanetId);

            builder
                .Entity<Creator>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Creator>(c => c.UserId);

            base.OnModelCreating(builder);
        }
    }
}
