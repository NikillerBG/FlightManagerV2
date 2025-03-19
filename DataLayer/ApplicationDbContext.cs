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

            optionsBuilder.UseSqlServer(@"...");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passenger { get; set; }

        public DbSet<Plane> Planes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

    }
}
