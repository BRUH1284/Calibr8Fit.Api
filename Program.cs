using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository;
using Calibr8Fit.Api.Services;
using Calibr8Fit.Api.Validators;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Load environment variables from .env file
Env.Load();

// Configure DbContext with PostgreSQL connection
builder.Services.AddDbContext<ApplicationDbContext>(options => options
    .UseSnakeCaseNamingConvention()
    .UseLazyLoadingProxies()
    .UseNpgsql(Environment.GetEnvironmentVariable("DefaultConnection")));

// Custom validator
builder.Services.AddScoped<IUserValidator<User>>(provider =>
    new UserNameLengthValidator<User>(5, 32));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddJwtAuthentication(builder.Configuration);

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ISyncService<UserActivity, Guid>, SyncService<UserActivity, Guid>>();
builder.Services.AddScoped<ISyncService<ActivityRecord, Guid>, SyncService<ActivityRecord, Guid>>();
builder.Services.AddScoped<IActivityValidationService, ActivityValidationService>();
builder.Services.AddScoped<ISyncService<WaterIntakeRecord, Guid>, SyncService<WaterIntakeRecord, Guid>>();
builder.Services.AddScoped<ISyncService<WeightRecord, Guid>, SyncService<WeightRecord, Guid>>();

// Repositories
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IDataVersionRepository, DataVersionRepository>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddScoped<IUserSyncRepositoryBase<UserActivity, Guid>, UserActivityRepository>();
builder.Services.AddScoped<IActivityRecordRepository, ActivityRecordRepository>();
builder.Services.AddScoped<IUserSyncRepositoryBase<ActivityRecord, Guid>, ActivityRecordRepository>();
builder.Services.AddScoped<IWaterIntakeRecordRepository, WaterIntakeRecordRepository>();
builder.Services.AddScoped<IUserSyncRepositoryBase<WaterIntakeRecord, Guid>, WaterIntakeRecordRepository>();
builder.Services.AddScoped<IWeightRecordRepository, WeightRecordRepository>();
builder.Services.AddScoped<IUserSyncRepositoryBase<WeightRecord, Guid>, WeightRecordRepository>();



builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();