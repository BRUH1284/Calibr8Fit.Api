using Calibr8Fit.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Calibr8Fit.Api.Data
{
    public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<DataVersion> DataVersions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

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

            // Configure Data Versions
            builder.Entity<DataVersion>()
                .HasKey(dv => dv.DataResource); // DataResource is the primary key in DataVersion

            // Configure Activity
            builder.Entity<Activity>()
                .HasKey(a => a.Code); // Code is the primary key in Activity

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

            // Configure UserActivity
            builder.Entity<UserActivity>()
                .Property(ua => ua.Id) // Id is the primary key in UserActivity
                .HasDefaultValueSql("uuid_generate_v4()");

            builder.Entity<UserActivity>()
            .HasIndex(ua => new { ua.UserId, ua.Id }); // Composite index for UserId and and Id

            builder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities) // User can have many UserActivities
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserActivity
        }
    }
}