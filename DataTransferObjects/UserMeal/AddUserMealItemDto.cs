namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class AddUserMealItemDto
    {
        public required Guid FoodId { get; set; }
        public required float Quantity { get; set; }
    }
}