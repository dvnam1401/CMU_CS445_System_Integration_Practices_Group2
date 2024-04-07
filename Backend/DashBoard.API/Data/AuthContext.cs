using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.API.Data
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var roleHR = "22d92cd6-759a-430e-a077-acd48ad908b7";
            var rolePayRoll = "261de13d-3f76-409d-8ce8-c5bc1540c213";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = roleHR,
                    Name = "HR",
                    NormalizedName = "HR".ToUpper(),
                    ConcurrencyStamp = roleHR,
                },
                new IdentityRole
                {
                    Id = rolePayRoll,
                    Name = "PayRoll",
                    NormalizedName = "PayRoll".ToUpper(),
                    ConcurrencyStamp = rolePayRoll,
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //create account admin
            var adminUser = "c7779e2e-3942-4633-a3dd-8bd1051497ab";
            var admin = new IdentityUser
            {
                Id = adminUser,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);
            //Give Roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUser,
                    RoleId = roleHR,
                },
                new()
                {
                    UserId = adminUser,
                    RoleId= rolePayRoll,
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

            //create account admin hr
            var adminHR = "3da10d72-5354-44fa-9d52-f7f157e55e37";
            var HR = new IdentityUser
            {
                Id = adminHR,
                UserName = "adminhr@gmail.com",
                Email = "adminhr@gmail.com",
                NormalizedEmail = "adminhr@gmail.com".ToUpper(),
                NormalizedUserName = "adminhr@gmail.com".ToUpper(),
            };
            HR.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(HR, "Admin@123");
            builder.Entity<IdentityUser>().HasData(HR);            
            var userHR = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminHR,
                    RoleId = roleHR,
                }              
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userHR);

            //create account admin payroll
            var adminPayRoll = "5d462fc9-497d-4001-9f19-5b7b160cd0cb";
            var PayRoll = new IdentityUser
            {
                Id = adminPayRoll,
                UserName = "adminpayroll@gmail.com",
                Email = "adminpayroll@gmail.com",
                NormalizedEmail = "adminpayroll@gmail.com".ToUpper(),
                NormalizedUserName = "adminpayroll@gmail.com".ToUpper(),
            };
            PayRoll.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(PayRoll, "Admin@123");
            builder.Entity<IdentityUser>().HasData(PayRoll);
            var userPayRoll = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminPayRoll,
                    RoleId = rolePayRoll,
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userPayRoll);
        }
    }
}
