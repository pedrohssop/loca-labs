using LocaLabs.Domain.Entities;
using LocaLabs.Infra.Data.Entities.Cars;
using LocaLabs.Infra.Data.Entities.Clients;
using LocaLabs.Infra.Data.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LocaLabs.Infra.Data
{
    internal class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new CarBrandMapping());
        }
    }
}