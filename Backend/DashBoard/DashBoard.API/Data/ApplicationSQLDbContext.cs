using DashBoard.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Data
{
    public class ApplicationSQLDbContext : DbContext
    {
        public ApplicationSQLDbContext(DbContextOptions options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("Server=localhost;port=3306;database=mydb;user=root;password=12345");
        //}
        public DbSet<BenefitPlan> BenefitPlans { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<JobHistory> JobHistory { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayRate> PayRates { get; set; }
    }
}
