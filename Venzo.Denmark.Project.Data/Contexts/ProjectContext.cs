using Microsoft.EntityFrameworkCore;
using Venzo.Denmark.Project.Data.Entities;

namespace Venzo.Denmark.Project.Data.Contexts
{
    public class ProjectContext : DbContext
    {
        DbSet<RoomEntity> Rooms { get; set; }
        DbSet<ReservationEntity> Reservations { get; set; } 

        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomEntity>(config =>
            {
                config.HasMany(room => room.Reservations)
                .WithOne(reservation => reservation.Room)
                .HasForeignKey(reservation => reservation.ResourceId);
            });
        }
    }
}
