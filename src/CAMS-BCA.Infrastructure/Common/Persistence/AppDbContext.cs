using CAMS_BCA.Domain.Auctions;
using CAMS_BCA.Domain.Bids;
using CAMS_BCA.Domain.Vehicles;

using Microsoft.EntityFrameworkCore;

namespace CAMS_BCA.Infrastructure.Common
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<HatchbackVehicle> HatchbackVehicles { get; set; }
        public DbSet<SedanVehicle> SedanVehicles { get; set; }
        public DbSet<SUVVehicle> SUVVehicles { get; set; }
        public DbSet<TruckVehicle> TruckVehicles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}