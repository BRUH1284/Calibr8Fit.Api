using Calibr8Fit.Api.Interfaces.Model;
using Calibr8Fit.Api.Models.Abstract;

namespace Calibr8Fit.Api.Models
{
    public class UserMealItem : IEntity<string[]>
    {
        public required Guid UserMealId { get; set; }
        public virtual UserMeal? UserMeal { get; set; }

        public required Guid FoodId { get; set; }
        public virtual FoodBase? Food { get; set; }

        public required float Quantity { get; set; } // Amount consumed in grams

        string[] IEntity<string[]>.Id => [UserMealId.ToString(), FoodId.ToString()];
    }
}