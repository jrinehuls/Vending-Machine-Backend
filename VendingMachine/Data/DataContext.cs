using Microsoft.EntityFrameworkCore;
using VendingMachine.Data.Config;
using VendingMachine.Models.Entities;

namespace VendingMachine.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Snack> Snacks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SnackConfig());
        }
    }
}
