using Calibr8Fit.Api.DataTransferObjects.UserMeal;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class UserMealMapper
    {
        public static UserMealDto ToUserMealDto(this UserMeal userMeal)
        {
            return new UserMealDto
            {
                Id = userMeal.Id,
                Name = userMeal.Name,
                Notes = userMeal.Notes,
                MealItems = userMeal.MealItems?.Select(mi => mi.ToUserMealItemDto()).ToList() ?? [],
                ModifiedAt = userMeal.ModifiedAt,
                Deleted = userMeal.Deleted
            };
        }

        public static UserMeal ToUserMeal(this AddUserMealRequestDto requestDto, string userId)
        {
            return new UserMeal
            {
                Id = requestDto.Id,
                UserId = userId,
                Name = requestDto.Name,
                Notes = requestDto.Notes,
                MealItems = requestDto.MealItems?.Select(mi => mi.ToUserMealItem(requestDto.Id)).ToList() ?? [],
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static UserMeal ToUserMeal(this UserMealDto userMealDto, string userId)
        {
            return new UserMeal
            {
                UserId = userId,
                Id = userMealDto.Id,
                Name = userMealDto.Name,
                Notes = userMealDto.Notes,
                MealItems = userMealDto.MealItems?.Select(mi => mi.ToUserMealItem(userMealDto.Id)).ToList() ?? [],
                ModifiedAt = userMealDto.ModifiedAt,
                Deleted = userMealDto.Deleted
            };
        }

        public static UserMeal ToUserMeal(this UpdateUserMealRequestDto requestDto, string userId)
        {
            return new UserMeal
            {
                UserId = userId,
                Id = requestDto.Id,
                Name = requestDto.Name,
                Notes = requestDto.Notes,
                MealItems = requestDto.MealItems?.Select(mi => mi.ToUserMealItem(requestDto.Id)).ToList() ?? [],
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncUserMealResponseDto ToSyncUserMealResponseDto(
            this IEnumerable<UserMeal> userMeals,
            DateTime syncedAt
        )
        {
            return new SyncUserMealResponseDto
            {
                LastSyncedAt = syncedAt,
                UserMeals = userMeals.Select(um => um.ToUserMealDto()).ToList()
            };
        }
    }
}
