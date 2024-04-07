using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

public partial class MydbContext : DbContext
{
    public MydbContext()
    {
    }

    public MydbContext(DbContextOptions<MydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Birthday> Birthdays { get; set; }

    public virtual DbSet<DetailVacation> DetailVacations { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PayRate> PayRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;port=3306;database=mydb;user=root;password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Birthday>(entity =>
        {
            entity.HasKey(e => e.EmployeeNumber).HasName("PRIMARY");

            entity.HasOne(d => d.EmployeeNumberNavigation).WithOne(p => p.Birthday)
                .HasPrincipalKey<Employee>(p => p.EmployeeNumber)
                .HasForeignKey<Birthday>(d => d.EmployeeNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("birthday_ibfk_1");
        });

        modelBuilder.Entity<DetailVacation>(entity =>
        {
            entity.HasKey(e => e.VacationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeNumber, e.PayRatesIdPayRates }).HasName("PRIMARY");

            entity.HasOne(d => d.PayRatesIdPayRatesNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Employee_Pay Rates");
        });

        modelBuilder.Entity<PayRate>(entity =>
        {
            entity.HasKey(e => e.IdPayRates).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
