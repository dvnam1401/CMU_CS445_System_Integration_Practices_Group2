using DashBoard.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DashBoard.API.Data
{
    public class MysqlContext : DbContext
    {
        public MysqlContext()
        {
        }
        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                  .HasKey(m => new { m.EmployeeNumber, m.PayRatesIdPayRates });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;User ID=root;Password=12345;Database=mydb");
        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<PayRate> payrates { get; set; }
    }
}
