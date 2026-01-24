# Calibr8Fit - Backend API

Backend for a mobile fitness and lifestyle tracking application.
This service provides RESTful endpoints for nutrition tracking, water intake, weight logging, exercise logging, and social features such as profiles, friendships, subscriptions, posts, and messaging (in progress).

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Firebase Cloud Messaging (FCM)
- Scalar for API documentation

## Features

- **Nutrition Tracking**: Log meals, track calories
- **Water Intake**: Monitor daily water consumption
- **Weight Logging**: Record weight changes over time
- **Exercise Logging**: Track workouts and physical activities
- **Custom Exercises and Meals**: Create custom exercises and meals
- **Daily Targets**: Set and monitor daily fitness goals
- **User Profiles**: Manage user information and settings
- **Friendships**: Add and manage friends
- **Subscriptions**: Follow other users
- **Posts**: Create and view posts
- **Messaging**: Real-time chat functionality (in progress)

## Project Structure

```
├── Controllers/             # API endpoints
|   ├── Abstract/            # Abstract controller classes
├── Data/                    # EF Core DbContext
├── DataTransferObjects/     # DTOs for API requests and responses
├── Enums/                   # Enum types
├── Extensions/              # Extension methods
├── Interfaces/
|   ├── DataTransferObjects/ # DTO interfaces
|   ├── Model/               # Model interfaces
|   ├── Repository/          # Repository interfaces
|   ├── Service/             # Service interfaces
├── Mappers/                 # Object mappers
├── Migrations/              # EF Core migrations
├── Models/                  # Data models
|   ├── Abstract/            # Abstract model classes
├── Options/                 # Configuration options
├── Repository/              # Data access layer
|   ├── Base/                # Base repository classes
├── Services/                # Business logic layer
├── Validators/              # Input validators
├── Program.cs               # Application entry point
└── README.md
└── .env
```

Example .env
```
DefaultConnection="Host=localhost;Port=5432;Database=calibr8fitdb;Username=calibr8fit;Password=***"
JWT_ISSUER=http://localhost:5246
JWT_AUDIENCE=http://localhost:5246
JWT_SIGNING_KEY=LONGRANDOMSIGNINGKEY

FirebaseCredentialPath=credentials/***.json
```
Example run command:
```
dotnet run --urls "http://127.0.0.1:5054"
```

## Database Schema
<img width="1921" height="2368" alt="Untitled" src="https://github.com/user-attachments/assets/ad449d51-298b-49cb-8403-075ba39fa7d0" />

## Related Repositories

- [React Native Frontend](https://github.com/BRUH1284/Calibr8Fit)

## Status

Backend: ~90% complete <br>
Frontend: ~70% complete
