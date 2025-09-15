using Calibr8Fit.Api.Data;
using Calibr8Fit.Api.Enums;
using Calibr8Fit.Api.Extensions;
using Calibr8Fit.Api.Interfaces.Repository;
using Calibr8Fit.Api.Interfaces.Repository.Base;
using Calibr8Fit.Api.Interfaces.Service;
using Calibr8Fit.Api.Models;
using Calibr8Fit.Api.Repository;
using Calibr8Fit.Api.Repository.Base;
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
builder.Services.AddScoped<ITPHValidationService<Guid, Activity, UserActivity>, TPHValidationService<Guid, Activity, UserActivity>>();
builder.Services.AddScoped<ITPHValidationService<Guid, Food, UserFood>, TPHValidationService<Guid, Food, UserFood>>();

// Repositories
builder.Services.AddScoped<IDataVersionRepository, DataVersionRepository>();
builder.Services.AddScoped<IUserRepositoryBase<RefreshToken, string[]>, RefreshTokenRepository>();
builder.Services.AddScoped<IRepositoryBase<UserProfile, string>, RepositoryBase<UserProfile, string>>();
builder.Services.AddScoped<IRepositoryBase<Activity, Guid>, RepositoryBase<Activity, Guid>>();
builder.Services.AddScoped<IRepositoryBase<Food, Guid>, RepositoryBase<Food, Guid>>();

// Data Version Repositories
builder.Services.AddDataVersionRepo<Food, Guid>(DataResource.Foods);
builder.Services.AddDataVersionRepo<Activity, Guid>(DataResource.Activities);

// Sync Repositories
builder.Services.AddUserSyncRepo<UserActivity, Guid>();
builder.Services.AddUserSyncRepo<ActivityRecord, Guid>();
builder.Services.AddUserSyncRepo<ConsumptionRecord, Guid>();
builder.Services.AddUserSyncRepo<UserFood, Guid>();
builder.Services.AddUserSyncRepo<WaterIntakeRecord, Guid>();
builder.Services.AddUserSyncRepo<WeightRecord, Guid>();
builder.Services.AddUserSyncRepo<UserMeal, Guid>();
builder.Services.AddUserSyncRepo<DailyBurnTarget, Guid>();

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