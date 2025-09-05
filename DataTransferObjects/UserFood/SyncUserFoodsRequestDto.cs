using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.UserFood
{
    public class SyncUserFoodsRequestDto : ISyncRequestDto<AddUserFoodRequestDto>
    {
        public DateTime LastSyncedAt { get; set; } = DateTime.MinValue;

        public List<AddUserFoodRequestDto> UserFoods { get; set; } = [];

        IEnumerable<AddUserFoodRequestDto> ISyncRequestDto<AddUserFoodRequestDto>.AddEntityRequestDtos => UserFoods;
    }
}
