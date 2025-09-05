using Calibr8Fit.Api.DataTransferObjects.UserFood;
using Calibr8Fit.Api.Models;

namespace Calibr8Fit.Api.Mappers
{
    public static class UserFoodMapper
    {
        public static UserFoodDto ToUserFoodDto(this UserFood userFood)
        {
            return new UserFoodDto
            {
                Id = userFood.Id,
                Name = userFood.Name,
                CaloricValue = userFood.CaloricValue,
                Fat = userFood.Fat,
                SaturatedFats = userFood.SaturatedFats,
                MonounsaturatedFats = userFood.MonounsaturatedFats,
                PolyunsaturatedFats = userFood.PolyunsaturatedFats,
                Carbohydrates = userFood.Carbohydrates,
                Sugars = userFood.Sugars,
                Protein = userFood.Protein,
                DietaryFiber = userFood.DietaryFiber,
                Water = userFood.Water,
                Cholesterol = userFood.Cholesterol,
                Sodium = userFood.Sodium,
                VitaminA = userFood.VitaminA,
                VitaminB1Thiamine = userFood.VitaminB1Thiamine,
                VitaminB11FolicAcid = userFood.VitaminB11FolicAcid,
                VitaminB12 = userFood.VitaminB12,
                VitaminB2Riboflavin = userFood.VitaminB2Riboflavin,
                VitaminB3Niacin = userFood.VitaminB3Niacin,
                VitaminB5PantothenicAcid = userFood.VitaminB5PantothenicAcid,
                VitaminB6 = userFood.VitaminB6,
                VitaminC = userFood.VitaminC,
                VitaminD = userFood.VitaminD,
                VitaminE = userFood.VitaminE,
                VitaminK = userFood.VitaminK,
                Calcium = userFood.Calcium,
                Copper = userFood.Copper,
                Iron = userFood.Iron,
                Magnesium = userFood.Magnesium,
                Manganese = userFood.Manganese,
                Phosphorus = userFood.Phosphorus,
                Potassium = userFood.Potassium,
                Selenium = userFood.Selenium,
                Zinc = userFood.Zinc,
                NutritionDensity = userFood.NutritionDensity,
                ModifiedAt = userFood.ModifiedAt,
                Deleted = userFood.Deleted
            };
        }

        public static UserFood ToUserFood(this AddUserFoodRequestDto requestDto, string userId)
        {
            return new UserFood
            {
                Id = requestDto.Id,
                UserId = userId,
                Name = requestDto.Name,
                CaloricValue = requestDto.CaloricValue,
                Fat = requestDto.Fat,
                SaturatedFats = requestDto.SaturatedFats,
                MonounsaturatedFats = requestDto.MonounsaturatedFats,
                PolyunsaturatedFats = requestDto.PolyunsaturatedFats,
                Carbohydrates = requestDto.Carbohydrates,
                Sugars = requestDto.Sugars,
                Protein = requestDto.Protein,
                DietaryFiber = requestDto.DietaryFiber,
                Water = requestDto.Water,
                Cholesterol = requestDto.Cholesterol,
                Sodium = requestDto.Sodium,
                VitaminA = requestDto.VitaminA,
                VitaminB1Thiamine = requestDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = requestDto.VitaminB11FolicAcid,
                VitaminB12 = requestDto.VitaminB12,
                VitaminB2Riboflavin = requestDto.VitaminB2Riboflavin,
                VitaminB3Niacin = requestDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = requestDto.VitaminB5PantothenicAcid,
                VitaminB6 = requestDto.VitaminB6,
                VitaminC = requestDto.VitaminC,
                VitaminD = requestDto.VitaminD,
                VitaminE = requestDto.VitaminE,
                VitaminK = requestDto.VitaminK,
                Calcium = requestDto.Calcium,
                Copper = requestDto.Copper,
                Iron = requestDto.Iron,
                Magnesium = requestDto.Magnesium,
                Manganese = requestDto.Manganese,
                Phosphorus = requestDto.Phosphorus,
                Potassium = requestDto.Potassium,
                Selenium = requestDto.Selenium,
                Zinc = requestDto.Zinc,
                NutritionDensity = requestDto.NutritionDensity,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static UserFood ToUserFood(this UserFoodDto userFoodDto, string userId)
        {
            return new UserFood
            {
                UserId = userId,
                Id = userFoodDto.Id,
                Name = userFoodDto.Name,
                CaloricValue = userFoodDto.CaloricValue,
                Fat = userFoodDto.Fat,
                SaturatedFats = userFoodDto.SaturatedFats,
                MonounsaturatedFats = userFoodDto.MonounsaturatedFats,
                PolyunsaturatedFats = userFoodDto.PolyunsaturatedFats,
                Carbohydrates = userFoodDto.Carbohydrates,
                Sugars = userFoodDto.Sugars,
                Protein = userFoodDto.Protein,
                DietaryFiber = userFoodDto.DietaryFiber,
                Water = userFoodDto.Water,
                Cholesterol = userFoodDto.Cholesterol,
                Sodium = userFoodDto.Sodium,
                VitaminA = userFoodDto.VitaminA,
                VitaminB1Thiamine = userFoodDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = userFoodDto.VitaminB11FolicAcid,
                VitaminB12 = userFoodDto.VitaminB12,
                VitaminB2Riboflavin = userFoodDto.VitaminB2Riboflavin,
                VitaminB3Niacin = userFoodDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = userFoodDto.VitaminB5PantothenicAcid,
                VitaminB6 = userFoodDto.VitaminB6,
                VitaminC = userFoodDto.VitaminC,
                VitaminD = userFoodDto.VitaminD,
                VitaminE = userFoodDto.VitaminE,
                VitaminK = userFoodDto.VitaminK,
                Calcium = userFoodDto.Calcium,
                Copper = userFoodDto.Copper,
                Iron = userFoodDto.Iron,
                Magnesium = userFoodDto.Magnesium,
                Manganese = userFoodDto.Manganese,
                Phosphorus = userFoodDto.Phosphorus,
                Potassium = userFoodDto.Potassium,
                Selenium = userFoodDto.Selenium,
                Zinc = userFoodDto.Zinc,
                NutritionDensity = userFoodDto.NutritionDensity,
                ModifiedAt = userFoodDto.ModifiedAt,
                Deleted = userFoodDto.Deleted
            };
        }

        public static UserFood ToUserFood(this UpdateUserFoodRequestDto requestDto, string userId)
        {
            return new UserFood
            {
                UserId = userId,
                Id = requestDto.Id,
                Name = requestDto.Name,
                CaloricValue = requestDto.CaloricValue,
                Fat = requestDto.Fat,
                SaturatedFats = requestDto.SaturatedFats,
                MonounsaturatedFats = requestDto.MonounsaturatedFats,
                PolyunsaturatedFats = requestDto.PolyunsaturatedFats,
                Carbohydrates = requestDto.Carbohydrates,
                Sugars = requestDto.Sugars,
                Protein = requestDto.Protein,
                DietaryFiber = requestDto.DietaryFiber,
                Water = requestDto.Water,
                Cholesterol = requestDto.Cholesterol,
                Sodium = requestDto.Sodium,
                VitaminA = requestDto.VitaminA,
                VitaminB1Thiamine = requestDto.VitaminB1Thiamine,
                VitaminB11FolicAcid = requestDto.VitaminB11FolicAcid,
                VitaminB12 = requestDto.VitaminB12,
                VitaminB2Riboflavin = requestDto.VitaminB2Riboflavin,
                VitaminB3Niacin = requestDto.VitaminB3Niacin,
                VitaminB5PantothenicAcid = requestDto.VitaminB5PantothenicAcid,
                VitaminB6 = requestDto.VitaminB6,
                VitaminC = requestDto.VitaminC,
                VitaminD = requestDto.VitaminD,
                VitaminE = requestDto.VitaminE,
                VitaminK = requestDto.VitaminK,
                Calcium = requestDto.Calcium,
                Copper = requestDto.Copper,
                Iron = requestDto.Iron,
                Magnesium = requestDto.Magnesium,
                Manganese = requestDto.Manganese,
                Phosphorus = requestDto.Phosphorus,
                Potassium = requestDto.Potassium,
                Selenium = requestDto.Selenium,
                Zinc = requestDto.Zinc,
                NutritionDensity = requestDto.NutritionDensity,
                ModifiedAt = requestDto.ModifiedAt,
                Deleted = requestDto.Deleted
            };
        }

        public static SyncUserFoodResponseDto ToSyncUserFoodResponseDto(
            this IEnumerable<UserFood> userFoods,
            DateTime syncedAt
        )
        {
            return new SyncUserFoodResponseDto
            {
                LastSyncedAt = syncedAt,
                UserFoods = userFoods.Select(uf => uf.ToUserFoodDto()).ToList()
            };
        }
    }
}
