using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Models.Admin;

public partial class AdminDashBoardContext : DbContext
{
    public AdminDashBoardContext()
    {
    }

    public AdminDashBoardContext(DbContextOptions<AdminDashBoardContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VAN-NAM;Initial Catalog=AdminDashBoard;User ID=sa;Password=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_AspNetUsers");
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasOne(d => d.Group).WithMany(p => p.GroupPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupPermissions_Groups");

            entity.HasOne(d => d.Permission).WithMany(p => p.GroupPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupPermissions_Permissions");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasOne(d => d.Group).WithMany(p => p.UserGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroups_Groups");

            entity.HasOne(d => d.User).WithMany(p => p.UserGroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroups_AccountUser");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_AccountUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
