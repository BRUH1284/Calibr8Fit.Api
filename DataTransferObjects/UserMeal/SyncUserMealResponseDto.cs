using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class SyncUserMealResponseDto : ISyncResponseDto<UserMealDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<UserMealDto> UserMeals { get; set; }

        IEnumerable<UserMealDto> ISyncResponseDto<UserMealDto>.Entities => UserMeals;
    }
}
