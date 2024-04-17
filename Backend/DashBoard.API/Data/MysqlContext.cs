using DashBoard.API.Models;
using DashBoard.API.Models.Domain;

//using DashBoard.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Configuration;
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
                .HasKey(m => m.EmployeeNumber); // Sử dụng EmployeeNumber làm khóa chính cho Employee

            //modelBuilder.Entity<Birthday>()
            //    .HasKey(b => b.EmployeeNumber);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Birthday)
            //    .WithOne(b => b.EmployeeNumberNavigation)
            //    .HasForeignKey<Birthday>(b => b.EmployeeNumber); // Sử dụng EmployeeNumber làm khóa ngoại trong Birthday

            //bỏ
            //modelBuilder.Entity<Employee>()
            //    .HasMany(e => e.DetailVacations)
            //    .WithOne()
            //    .HasForeignKey(d => d.EmployeeNumber); // Đảm bảo DetailVacation có EmployeeNumber là khóa ngoại


            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.DetailVacation)
            //    .WithOne(d => d.EmployeeNumberNavigation)
            //    .HasForeignKey<DetailVacation>(d => d.EmployeeNumber)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            //optionsbuilder.UseMySQL("server=localhost;uid=root;password=12345;database=mydb");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayRate> Payrates { get; set; }
    }
}
