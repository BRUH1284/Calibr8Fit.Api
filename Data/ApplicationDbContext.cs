using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Create roles
            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Id = "Admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Id = "User",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Configure User Profile
            builder.Entity<UserProfile>()
                .HasKey(up => up.UserId); // UserId is the primary key in UserProfile

            builder.Entity<UserProfile>()
                .HasOne<User>()
                .WithOne(u => u.Profile) // User has one UserProfile
                .HasForeignKey<UserProfile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserProfile

            // Configure RefreshToken
            builder.Entity<RefreshToken>()
                .HasKey(rt => new { rt.UserId, rt.DeviceId }); // Composite key

            builder.Entity<RefreshToken>()
                .HasOne<User>()
                .WithMany() // User can have many RefreshTokens
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> RefreshToken
        }
    }
}