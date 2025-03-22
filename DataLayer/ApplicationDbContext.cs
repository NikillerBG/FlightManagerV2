using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ASUS-KIKO\SQLEXPRESS;Database=FlightManagerDatabase;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.Plane)
                .WithMany()
                .HasForeignKey("PlaneId")
                .IsRequired();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany()
                .HasForeignKey(r => r.FlightId)
                .IsRequired();

            modelBuilder.Entity<Passenger>()
                .HasOne(p => p.Reservation)
                .WithOne()
                .HasForeignKey<Passenger>(p => p.ReservationId);
        }

    }
}
