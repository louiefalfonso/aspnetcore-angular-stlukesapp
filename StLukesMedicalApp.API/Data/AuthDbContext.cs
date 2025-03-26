using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StLukesMedicalApp.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        // create contructor
        public AuthDbContext(DbContextOptions <AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Load .env file
            DotNetEnv.Env.Load();

            // Retrieve JWT configuration from environment variables
            var readerRoleId_env = Environment.GetEnvironmentVariable("readerRoleId_env");
            var writerRoleId_env = Environment.GetEnvironmentVariable("readerRoleId_env");
            var adminUserId_env = Environment.GetEnvironmentVariable("adminUserId_env");

            var adminEmail_env = Environment.GetEnvironmentVariable("adminEmail_env");
            var adminPass_env = Environment.GetEnvironmentVariable("adminPass_env");

            var readerRoleId = "readerRoleId_env";
            var writerRoleId = "writerRoleId_env ";

            // create reader & writter role
            var roles = new List<IdentityRole>
            {
                 new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                 new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writter",
                    NormalizedName = "Writter".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                },
            };

            // seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            // create admin user
            var adminUserId = "adminUserId_env";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "adminEmail_env",
                Email = "adminEmail_env",
                NormalizedEmail = "adminEmail_env".ToUpper(),
                NormalizedUserName = "adminEmail_env".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "adminPass_env");

            builder.Entity<IdentityUser>().HasData(admin);

            // give toles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
