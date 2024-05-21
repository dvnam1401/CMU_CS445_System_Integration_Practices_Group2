using DashBoard.API.Models.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DashBoard.API.Data
{
    public class AdminContext : IdentityDbContext
    {
        public AdminContext()
        {
        }

        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
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
                var connectionString = configuration.GetConnectionString("ConnectionAuth");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AdminContext>()
                .AddDefaultTokenProviders(); // Đảm bảo thêm Token Provider

            // Các cài đặt khác...
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Group>().ToTable("Groups");
        }

        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
    }
}
