using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Domain;

public partial class HrmContext : DbContext
{
    public HrmContext()
    {
    }

    public HrmContext(DbContextOptions<HrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BenefitPlan> BenefitPlans { get; set; }

    public virtual DbSet<Employment> Employments { get; set; }

    public virtual DbSet<EmploymentWorkingTime> EmploymentWorkingTimes { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VAN-NAM;Initial Catalog=HRM;User ID=sa;Password=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BenefitPlan>(entity =>
        {
            entity.Property(e => e.PlanName).IsFixedLength();
        });

        modelBuilder.Entity<Employment>(entity =>
        {
            entity.Property(e => e.EmploymentStatus).IsFixedLength();
            entity.Property(e => e.WorkersCompCode)
                .IsFixedLength()
                .HasComment("MÃ CÔNG VIỆC");

            entity.HasOne(d => d.Personal).WithMany(p => p.Employments).HasConstraintName("FK_EMPLOYMENT_PERSONAL");
        });

        modelBuilder.Entity<EmploymentWorkingTime>(entity =>
        {
            entity.HasOne(d => d.Employment).WithMany(p => p.EmploymentWorkingTimes).HasConstraintName("FK_EMPLOYMENT_WORKING_TIME_EMPLOYMENT");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.HasOne(d => d.Employment).WithMany(p => p.JobHistories).HasConstraintName("FK_JOB_HISTORY_EMPLOYMENT");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.Property(e => e.Ethnicity).IsFixedLength();

            entity.HasOne(d => d.BenefitPlan).WithMany(p => p.Personals).HasConstraintName("FK_PERSONAL_BENEFIT_PLANS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
