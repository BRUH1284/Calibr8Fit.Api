using Calibr8Fit.Api.Interfaces.Model;

namespace Calibr8Fit.Api.Models.Abstract
{
    public abstract class FoodBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        // Basic Nutritional Information (per 100g)
        public required float CaloricValue { get; set; } // Total energy in kcal per 100g

        // Macronutrients (in grams per 100g)
        public required float Fat { get; set; } // Total fats in g per 100g
        public required float SaturatedFats { get; set; } // Saturated fats in g per 100g
        public required float MonounsaturatedFats { get; set; } // Monounsaturated fats in g per 100g
        public required float PolyunsaturatedFats { get; set; } // Polyunsaturated fats in g per 100g
        public required float Carbohydrates { get; set; } // Total carbohydrates in g per 100g
        public required float Sugars { get; set; } // Total sugars in g per 100g
        public required float Protein { get; set; } // Total proteins in g per 100g
        public required float DietaryFiber { get; set; } // Fiber content in g per 100g
        public required float Water { get; set; } // Water content in g per 100g

        // Other Important Nutrients
        public required float Cholesterol { get; set; } // Cholesterol content in mg per 100g
        public required float Sodium { get; set; } // Sodium content in mg per 100g

        // Vitamins
        public required float VitaminA { get; set; } // Vitamin A in µg per 100g
        public required float VitaminB1Thiamine { get; set; } // Vitamin B1 (Thiamine) in mg per 100g
        public required float VitaminB11FolicAcid { get; set; } // Vitamin B11 (Folic Acid) in mg per 100g
        public required float VitaminB12 { get; set; } // Vitamin B12 in mg per 100g
        public required float VitaminB2Riboflavin { get; set; } // Vitamin B2 (Riboflavin) in mg per 100g
        public required float VitaminB3Niacin { get; set; } // Vitamin B3 (Niacin) in mg per 100g
        public required float VitaminB5PantothenicAcid { get; set; } // Vitamin B5 (Pantothenic Acid) in mg per 100g
        public required float VitaminB6 { get; set; } // Vitamin B6 in mg per 100g
        public required float VitaminC { get; set; } // Vitamin C in mg per 100g
        public required float VitaminD { get; set; } // Vitamin D in mg per 100g
        public required float VitaminE { get; set; } // Vitamin E in mg per 100g
        public required float VitaminK { get; set; } // Vitamin K in mg per 100g

        // Minerals
        public required float Calcium { get; set; } // Calcium in mg per 100g
        public required float Copper { get; set; } // Copper in mg per 100g
        public required float Iron { get; set; } // Iron in mg per 100g
        public required float Magnesium { get; set; } // Magnesium in mg per 100g
        public required float Manganese { get; set; } // Manganese in mg per 100g
        public required float Phosphorus { get; set; } // Phosphorus in mg per 100g
        public required float Potassium { get; set; } // Potassium in mg per 100g
        public required float Selenium { get; set; } // Selenium in mg per 100g
        public required float Zinc { get; set; } // Zinc in mg per 100g

        // Nutritional Quality Metric
        public required float NutritionDensity { get; set; } // Nutrient richness per calorie
    }
}