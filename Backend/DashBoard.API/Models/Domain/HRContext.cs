using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DashBoard.API.Models.Domain
{
    public partial class HRContext : DbContext
    {
        public HRContext()
        {
        }

        public HRContext(DbContextOptions<HRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BenefitPlan> BenefitPlans { get; set; }
        public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<JobHistory> JobHistories { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Personal> Personals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=VAN-NAM;Initial Catalog=HR;User ID=sa;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BenefitPlan>(entity =>
            {
                entity.Property(e => e.BenefitPlanId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<EmergencyContact>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_dbo.Emergency_Contacts");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmergencyContact)
                    .HasForeignKey<EmergencyContact>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Emergency_Contacts_dbo.Personal_Employee_ID");
            });

            modelBuilder.Entity<Employment>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_dbo.Employment");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Employment)
                    .HasForeignKey<Employment>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Employment_dbo.Personal_Employee_ID");
            });

            modelBuilder.Entity<JobHistory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.JobHistories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Job_History_dbo.Personal_Employee_ID");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");
            });

            modelBuilder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_dbo.Personal");

                entity.HasOne(d => d.BenefitPlansNavigation)
                    .WithMany(p => p.Personals)
                    .HasForeignKey(d => d.BenefitPlans)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.Personal_dbo.Benefit_Plans_Benefit_Plans");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
