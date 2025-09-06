using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class SyncUserMealsRequestDto : ISyncRequestDto<AddUserMealRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;

        public List<AddUserMealRequestDto> UserMeals { get; set; } = [];

        IEnumerable<AddUserMealRequestDto> ISyncRequestDto<AddUserMealRequestDto>.AddEntityRequestDtos => UserMeals;
    }
}
