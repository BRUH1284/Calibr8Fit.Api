using Calibr8Fit.Api.DataTransferObjects.UserMeal;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class UserMealItemMapper
    {
        public static UserMealItemDto ToUserMealItemDto(this UserMealItem userMealItem)
        {
            return new UserMealItemDto
            {
                FoodId = userMealItem.FoodId,
                Quantity = userMealItem.Quantity
            };
        }

        public static UserMealItem ToUserMealItem(this UserMealItemDto userMealItemDto, Guid userMealId)
        {
            return new UserMealItem
            {
                UserMealId = userMealId,
                FoodId = userMealItemDto.FoodId,
                Quantity = userMealItemDto.Quantity
            };
        }

        public static UserMealItem ToUserMealItem(this AddUserMealItemDto userMealItemDto, Guid userMealId)
        {
            return new UserMealItem
            {
                UserMealId = userMealId,
                FoodId = userMealItemDto.FoodId,
                Quantity = userMealItemDto.Quantity
            };
        }
    }
}
