using System.ComponentModel.DataAnnotations;

namespace Calibr8Fit.Api.DataTransferObjects.UserFood
{
    public class AddUserFoodRequestDto
    {
        public Guid Id { get; set; } // Optional, will be generated if not provided
        [Required]
        public required string Name { get; set; }
        [Required]
        public required float CaloricValue { get; set; }
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
        [Required]
        public required float Cholesterol { get; set; }
        [Required]
        public required float Sodium { get; set; }
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
        [Required]
        public required float NutritionDensity { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow; // Default to current time if not specified
        public bool Deleted { get; set; } = false; // Default to false if not specified
    }
}
