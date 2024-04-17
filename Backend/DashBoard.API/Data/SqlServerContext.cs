using DashBoard.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Data
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext()
        {
        }
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ConnectionSQLServer");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<BenefitPlan> BenefitPlans { get; set; }
        public DbSet<EmploymentWorkingTime> EmergencyContacts { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<JobHistory> JobHistory { get; set; }
    }
}
