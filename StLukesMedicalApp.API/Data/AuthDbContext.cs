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

            // Retrieve JWT configuration from env
            var readerId = Environment.GetEnvironmentVariable("readerRoleId");
            var writerId = Environment.GetEnvironmentVariable("writerRoleId");

            var readerRoleId = readerId;
            var writerRoleId = writerId;

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
            var adminUserId = Environment.GetEnvironmentVariable("adminUserId");
            var adminUserEmail = Environment.GetEnvironmentVariable("adminEmail");
            var adminUserPass = Environment.GetEnvironmentVariable("adminPass");


            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = adminUserEmail,
                Email = adminUserEmail,
                NormalizedEmail = adminUserEmail.ToUpper(),
                NormalizedUserName = adminUserEmail.ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, adminUserPass);

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
