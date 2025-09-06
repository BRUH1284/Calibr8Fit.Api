namespace Calibr8Fit.Api.DataTransferObjects.UserMeal
{
    public class UserMealDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Notes { get; set; }
        public List<UserMealItemDto>? MealItems { get; set; } = [];
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
