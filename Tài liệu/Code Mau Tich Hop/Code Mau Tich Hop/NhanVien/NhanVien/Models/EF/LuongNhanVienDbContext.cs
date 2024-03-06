namespace NhanVien.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LuongNhanVienDbContext : DbContext
    {
        public LuongNhanVienDbContext()
            : base("name=LuongNhanVien")
        {
        }

        public virtual DbSet<LUONG> LUONGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LUONG>()
                .Property(e => e.LUONGCOBAN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<LUONG>()
                .Property(e => e.LUONGTHUCLINH)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.LUONGs)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);
        }
    }
}
