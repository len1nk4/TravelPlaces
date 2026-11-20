using Microsoft.EntityFrameworkCore;
using TravelPLaces11.Data.Entities;

namespace TravelPLaces11.Data
{
    public class TravelPlaces11DbContext : DbContext
    {
        public TravelPlaces11DbContext(DbContextOptions<TravelPlaces11DbContext> options) : base(options)
        {
        }

        public DbSet<TravelPlace> TravelPlaces => Set<TravelPlace>();
        public DbSet<Area> Areas => Set<Area>();
        public DbSet<PlaceType> PlaceTypes => Set<PlaceType>();
        public DbSet<Visitor> Visitors => Set<Visitor>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(a => a.Name).IsUnique();
            });

            modelBuilder.Entity<PlaceType>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<TravelPlace>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Name);

                entity.Property(e => e.Fee)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.ToTable(t => t.HasCheckConstraint("CK_TravelPlaces_Fee", "[Fee] >= 0"));

                entity.HasOne(e => e.Area)
                    .WithMany(a => a.TravelPlaces)
                    .HasForeignKey(e => e.AreaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.PlaceType)
                    .WithMany(t => t.TravelPlaces)
                    .HasForeignKey(e => e.PlaceTypeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Visitor>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(v => v.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(v => v.Email).IsUnique();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Comment)
                    .HasMaxLength(500);

                entity.Property(r => r.VisitDate)
                    .HasColumnType("date");

                entity.ToTable(t => t.HasCheckConstraint("CK_Reviews_Rating", "[Rating] BETWEEN 1 AND 5"));

                entity.HasOne(r => r.TravelPlace)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(r => r.TravelPlaceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Visitor)
                    .WithMany(v => v.Reviews)
                    .HasForeignKey(r => r.VisitorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
