using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserFood
{
    public class SyncUserFoodResponseDto : ISyncResponseDto<UserFoodDto>
    {
        public required DateTime LastSyncedAt { get; set; }
        public required List<UserFoodDto> UserFoods { get; set; }

        IEnumerable<UserFoodDto> ISyncResponseDto<UserFoodDto>.Entities => UserFoods;
    }
}
