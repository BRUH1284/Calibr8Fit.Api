namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class UserMealItemDto
    {
        public required Guid FoodId { get; set; }
        public required float Quantity { get; set; } // Amount consumed in grams
    }
}
