using Calibr8Fit.Api.DataTransferObjects.Food;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class FoodMapper
    {
        public static FoodDto ToFoodDto(this Food food)
        {
            return new FoodDto
            {
                Id = food.Id,
                Name = food.Name,
                CaloricValue = food.CaloricValue,
                Fat = food.Fat,
                SaturatedFats = food.SaturatedFats,
                MonounsaturatedFats = food.MonounsaturatedFats,
                PolyunsaturatedFats = food.PolyunsaturatedFats,
                Carbohydrates = food.Carbohydrates,
                Sugars = food.Sugars,
                Protein = food.Protein,
                DietaryFiber = food.DietaryFiber,
                Water = food.Water,
                Cholesterol = food.Cholesterol,
                Sodium = food.Sodium,
                VitaminA = food.VitaminA,
                VitaminB1Thiamine = food.VitaminB1Thiamine,
                VitaminB11FolicAcid = food.VitaminB11FolicAcid,
                VitaminB12 = food.VitaminB12,
                VitaminB2Riboflavin = food.VitaminB2Riboflavin,
                VitaminB3Niacin = food.VitaminB3Niacin,
                VitaminB5PantothenicAcid = food.VitaminB5PantothenicAcid,
                VitaminB6 = food.VitaminB6,
                VitaminC = food.VitaminC,
                VitaminD = food.VitaminD,
                VitaminE = food.VitaminE,
                VitaminK = food.VitaminK,
                Calcium = food.Calcium,
                Copper = food.Copper,
                Iron = food.Iron,
                Magnesium = food.Magnesium,
                Manganese = food.Manganese,
                Phosphorus = food.Phosphorus,
                Potassium = food.Potassium,
                Selenium = food.Selenium,
                Zinc = food.Zinc,
                NutritionDensity = food.NutritionDensity
            };
        }

        public static Food ToFood(this FoodDto foodDto)
        {
            return new Food
            {
                Id = foodDto.Id,
                Name = foodDto.Name,
                CaloricValue = foodDto.CaloricValue,
                Fat = foodDto.Fat,
                SaturatedFats = foodDto.SaturatedFats,
                MonounsaturatedFats = foodDto.MonounsaturatedFats,
                PolyunsaturatedFats = foodDto.PolyunsaturatedFats,
                Carbohydrates = foodDto.Carbohydrates,
                Sugars = foodDto.Sugars,
                Protein = foodDto.Protein,
                DietaryFiber = foodDto.DietaryFiber,
                Water = foodDto.Water,
                Cholesterol = foodDto.Cholesterol,
                Sodium = foodDto.Sodium,
                VitaminA = foodDto.VitaminA,
                VitaminB1Thiamine = foodDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = foodDto.VitaminB11FolicAcid,
                VitaminB12 = foodDto.VitaminB12,
                VitaminB2Riboflavin = foodDto.VitaminB2Riboflavin,
                VitaminB3Niacin = foodDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = foodDto.VitaminB5PantothenicAcid,
                VitaminB6 = foodDto.VitaminB6,
                VitaminC = foodDto.VitaminC,
                VitaminD = foodDto.VitaminD,
                VitaminE = foodDto.VitaminE,
                VitaminK = foodDto.VitaminK,
                Calcium = foodDto.Calcium,
                Copper = foodDto.Copper,
                Iron = foodDto.Iron,
                Magnesium = foodDto.Magnesium,
                Manganese = foodDto.Manganese,
                Phosphorus = foodDto.Phosphorus,
                Potassium = foodDto.Potassium,
                Selenium = foodDto.Selenium,
                Zinc = foodDto.Zinc,
                NutritionDensity = foodDto.NutritionDensity
            };
        }

        public static Food ToFood(this AddFoodRequestDto foodDto)
        {
            return new Food
            {
                Id = foodDto.Id,
                Name = foodDto.Name,
                CaloricValue = foodDto.CaloricValue,
                Fat = foodDto.Fat,
                SaturatedFats = foodDto.SaturatedFats,
                MonounsaturatedFats = foodDto.MonounsaturatedFats,
                PolyunsaturatedFats = foodDto.PolyunsaturatedFats,
                Carbohydrates = foodDto.Carbohydrates,
                Sugars = foodDto.Sugars,
                Protein = foodDto.Protein,
                DietaryFiber = foodDto.DietaryFiber,
                Water = foodDto.Water,
                Cholesterol = foodDto.Cholesterol,
                Sodium = foodDto.Sodium,
                VitaminA = foodDto.VitaminA,
                VitaminB1Thiamine = foodDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = foodDto.VitaminB11FolicAcid,
                VitaminB12 = foodDto.VitaminB12,
                VitaminB2Riboflavin = foodDto.VitaminB2Riboflavin,
                VitaminB3Niacin = foodDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = foodDto.VitaminB5PantothenicAcid,
                VitaminB6 = foodDto.VitaminB6,
                VitaminC = foodDto.VitaminC,
                VitaminD = foodDto.VitaminD,
                VitaminE = foodDto.VitaminE,
                VitaminK = foodDto.VitaminK,
                Calcium = foodDto.Calcium,
                Copper = foodDto.Copper,
                Iron = foodDto.Iron,
                Magnesium = foodDto.Magnesium,
                Manganese = foodDto.Manganese,
                Phosphorus = foodDto.Phosphorus,
                Potassium = foodDto.Potassium,
                Selenium = foodDto.Selenium,
                Zinc = foodDto.Zinc,
                NutritionDensity = foodDto.NutritionDensity
            };
        }

        public static Food ToFood(this UpdateFoodRequestDto foodDto)
        {
            return new Food
            {
                Id = foodDto.Id,
                Name = foodDto.Name,
                CaloricValue = foodDto.CaloricValue,
                Fat = foodDto.Fat,
                SaturatedFats = foodDto.SaturatedFats,
                MonounsaturatedFats = foodDto.MonounsaturatedFats,
                PolyunsaturatedFats = foodDto.PolyunsaturatedFats,
                Carbohydrates = foodDto.Carbohydrates,
                Sugars = foodDto.Sugars,
                Protein = foodDto.Protein,
                DietaryFiber = foodDto.DietaryFiber,
                Water = foodDto.Water,
                Cholesterol = foodDto.Cholesterol,
                Sodium = foodDto.Sodium,
                VitaminA = foodDto.VitaminA,
                VitaminB1Thiamine = foodDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = foodDto.VitaminB11FolicAcid,
                VitaminB12 = foodDto.VitaminB12,
                VitaminB2Riboflavin = foodDto.VitaminB2Riboflavin,
                VitaminB3Niacin = foodDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = foodDto.VitaminB5PantothenicAcid,
                VitaminB6 = foodDto.VitaminB6,
                VitaminC = foodDto.VitaminC,
                VitaminD = foodDto.VitaminD,
                VitaminE = foodDto.VitaminE,
                VitaminK = foodDto.VitaminK,
                Calcium = foodDto.Calcium,
                Copper = foodDto.Copper,
                Iron = foodDto.Iron,
                Magnesium = foodDto.Magnesium,
                Manganese = foodDto.Manganese,
                Phosphorus = foodDto.Phosphorus,
                Potassium = foodDto.Potassium,
                Selenium = foodDto.Selenium,
                Zinc = foodDto.Zinc,
                NutritionDensity = foodDto.NutritionDensity
            };
        }
    }
}
