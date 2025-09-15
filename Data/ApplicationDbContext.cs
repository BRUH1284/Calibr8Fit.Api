using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Models.Abstract;
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
        public DbSet<ActivityBase> BaseActivities { get; set; }
        public DbSet<Food> BaseFood { get; set; }
        public DbSet<ActivityRecord> ActivityRecords { get; set; }
        public DbSet<ConsumptionRecord> ConsumptionRecords { get; set; }
        public DbSet<WaterIntakeRecord> WaterIntakeRecords { get; set; }
        public DbSet<WeightRecord> WeightRecords { get; set; }
        public IQueryable<Activity> Activities => Set<ActivityBase>().OfType<Activity>();
        public IQueryable<UserActivity> UserActivities => Set<ActivityBase>().OfType<UserActivity>();
        public IQueryable<Food> Foods => Set<FoodBase>().OfType<Food>();
        public IQueryable<UserFood> UserFoods => Set<FoodBase>().OfType<UserFood>();
        public DbSet<UserMeal> UserMeals { get; set; }
        public DbSet<UserMealItem> UserMealItems { get; set; }
        public DbSet<DailyBurnTarget> DailyBurnTargets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Create roles
            var roles = new List<IdentityRole>{
                new() {
                    Id = "Admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new() {
                    Id = "User",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            // Configure Data Versions
            builder.Entity<DataVersion>()
                .HasKey(dv => dv.DataResource); // DataResource is the primary key in DataVersion

            // Configure User Profile
            builder.Entity<UserProfile>()
                .HasOne<User>()
                .WithOne(u => u.Profile) // User has one UserProfile
                .HasForeignKey<UserProfile>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserProfile

            // Configure RefreshToken
            builder.Entity<RefreshToken>()
                .HasKey(rt => new { rt.UserId, rt.DeviceId }); // Composite key

            builder.Entity<RefreshToken>()
                .HasOne<User>()
                .WithMany() // User can have many RefreshTokens
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> RefreshToken

            // Configure BaseActivities
            builder.Entity<ActivityBase>()
                .Property(a => a.Id) // Id is the primary key in BaseActivity
                .HasDefaultValueSql("uuid_generate_v4()");

            // Configure Activities
            builder.Entity<ActivityBase>()
                .HasDiscriminator<bool>("IsUserActivity")
                .HasValue<Activity>(false)
                .HasValue<UserActivity>(true);

            // Configure UserActivity
            builder.Entity<UserActivity>()
                .HasIndex(ua => new { ua.UserId, ua.Id }); // Composite index for UserId and and Id

            builder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities) // User can have many UserActivities
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserActivity

            // Configure BaseFood
            builder.Entity<FoodBase>()
                .Property(f => f.Id) // Id is the primary key in BaseFood
                .HasDefaultValueSql("uuid_generate_v4()");

            // Configure Foods
            builder.Entity<FoodBase>()
                .HasDiscriminator<bool>("IsUserFood")
                .HasValue<Food>(false)
                .HasValue<UserFood>(true);

            // Configure UserFood
            builder.Entity<UserFood>()
                .HasIndex(uf => new { uf.UserId, uf.Id }); // Composite index for UserId and and Id

            builder.Entity<UserFood>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFoods) // User can have many UserFoods
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserFood

            // Configure ActivityRecord
            builder.Entity<ActivityRecord>()
                .HasOne(ar => ar.User)
                .WithMany(u => u.ActivityRecords) // User can have many ActivityRecords
                .HasForeignKey(ar => ar.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> ActivityRecord

            // Configure ActivityRecord -> Activity relationship
            builder.Entity<ActivityRecord>()
                .HasOne(ar => ar.Activity)
                .WithMany() // ActivityBase can have many ActivityRecords
                .HasForeignKey(ar => ar.ActivityId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Activity -> ActivityRecord

            // Configure ConsumptionRecord
            builder.Entity<ConsumptionRecord>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.ConsumptionRecords) // User can have many ConsumptionRecords
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> ConsumptionRecord

            // Configure ConsumptionRecord -> Food relationship
            builder.Entity<ConsumptionRecord>()
                .HasOne(cr => cr.Food)
                .WithMany() // FoodBase can have many ConsumptionRecords
                .HasForeignKey(cr => cr.FoodId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Food -> ConsumptionRecord

            // Configure ConsumptionRecord -> UserMeal relationship
            builder.Entity<ConsumptionRecord>()
                .HasOne(cr => cr.UserMeal)
                .WithMany() // UserMeal can have many ConsumptionRecords
                .HasForeignKey(cr => cr.UserMealId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete UserMeal -> ConsumptionRecord

            // Configure WaterIntakeRecord
            builder.Entity<WaterIntakeRecord>()
                .HasOne(wir => wir.User)
                .WithMany(u => u.WaterIntakeRecords) // User can have many WaterIntakeRecords
                .HasForeignKey(wir => wir.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> WaterIntakeRecord

            // Configure WeightRecord
            builder.Entity<WeightRecord>()
                .HasOne(wr => wr.User)
                .WithMany(u => u.WeightRecords) // User can have many WeightRecords
                .HasForeignKey(wr => wr.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> WeightRecord

            // Configure UserMeal
            builder.Entity<UserMeal>()
                .HasOne(um => um.User)
                .WithMany(u => u.UserMeals) // User can have many UserMeals
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> UserMeal

            // Configure UserMealItem
            builder.Entity<UserMealItem>()
                .HasKey(umi => new { umi.UserMealId, umi.FoodId }); // Composite key

            builder.Entity<UserMealItem>()
                .HasOne(umi => umi.UserMeal)
                .WithMany(um => um.MealItems) // UserMeal can have many UserMealItems
                .HasForeignKey(umi => umi.UserMealId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for UserMeal -> UserMealItem

            builder.Entity<UserMealItem>()
                .HasOne(umi => umi.Food)
                .WithMany() // FoodBase can have many UserMealItems
                .HasForeignKey(umi => umi.FoodId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Food -> UserMealItem

            // Insert check to ensure either FoodId or UserMealId is set, but not both
            builder.Entity<ConsumptionRecord>()
                .ToTable(tb => tb.HasCheckConstraint("ck_consumption_record_food_id_user_meal_id",
                    "(food_id IS NOT NULL) != (user_meal_id IS NOT NULL)"));

            // Configure DailyBurnTarget
            builder.Entity<DailyBurnTarget>()
                .HasOne(dbt => dbt.User)
                .WithMany(u => u.DailyBurnTargets) // User can have many DailyBurnTargets
                .HasForeignKey(dbt => dbt.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for User -> DailyBurnTarget

            builder.Entity<DailyBurnTarget>()
                .HasOne(dbt => dbt.Activity)
                .WithMany() // ActivityBase can have many DailyBurnTargets
                .HasForeignKey(dbt => dbt.ActivityId)
                .OnDelete(DeleteBehavior.Restrict); // Don't cascade delete Activity -> DailyBurnTarget

        }
    }
}