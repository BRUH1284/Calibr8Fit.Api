using System.ComponentModel.DataAnnotations;
using Calibr8Fit.Api.Interfaces.DataTransferObjects;

namespace Calibr8Fit.Api.DataTransferObjects.Food
{
    public class UpdateFoodRequestDto : IUpdateRequestDto<Guid>
    {
        [Required]
        public required Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }

        // Basic Nutritional Information (per 100g)
        [Required]
        public required float CaloricValue { get; set; }

        // Macronutrients (in grams per 100g)
        [Required]
        public required float Fat { get; set; }
        [Required]
        public required float SaturatedFats { get; set; }
        [Required]
        public required float MonounsaturatedFats { get; set; }
        [Required]
        public required float PolyunsaturatedFats { get; set; }
        [Required]
        public required float Carbohydrates { get; set; }
        [Required]
        public required float Sugars { get; set; }
        [Required]
        public required float Protein { get; set; }
        [Required]
        public required float DietaryFiber { get; set; }
        [Required]
        public required float Water { get; set; }

        // Other Important Nutrients
        [Required]
        public required float Cholesterol { get; set; }
        [Required]
        public required float Sodium { get; set; }

        // Vitamins
        [Required]
        public required float VitaminA { get; set; }
        [Required]
        public required float VitaminB1Thiamine { get; set; }
        [Required]
        public required float VitaminB11FolicAcid { get; set; }
        [Required]
        public required float VitaminB12 { get; set; }
        [Required]
        public required float VitaminB2Riboflavin { get; set; }
        [Required]
        public required float VitaminB3Niacin { get; set; }
        [Required]
        public required float VitaminB5PantothenicAcid { get; set; }
        [Required]
        public required float VitaminB6 { get; set; }
        [Required]
        public required float VitaminC { get; set; }
        [Required]
        public required float VitaminD { get; set; }
        [Required]
        public required float VitaminE { get; set; }
        [Required]
        public required float VitaminK { get; set; }

        // Minerals
        [Required]
        public required float Calcium { get; set; }
        [Required]
        public required float Copper { get; set; }
        [Required]
        public required float Iron { get; set; }
        [Required]
        public required float Magnesium { get; set; }
        [Required]
        public required float Manganese { get; set; }
        [Required]
        public required float Phosphorus { get; set; }
        [Required]
        public required float Potassium { get; set; }
        [Required]
        public required float Selenium { get; set; }
        [Required]
        public required float Zinc { get; set; }

        // Nutritional Quality Metric
        [Required]
        public required float NutritionDensity { get; set; }
    }
}
