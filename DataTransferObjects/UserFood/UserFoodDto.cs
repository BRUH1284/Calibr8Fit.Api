namespace Calibr8Fit.Api.DataTransferObjects.UserFood
{
    public class UserFoodDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required float CaloricValue { get; set; }
        public required float Fat { get; set; }
        public required float SaturatedFats { get; set; }
        public required float MonounsaturatedFats { get; set; }
        public required float PolyunsaturatedFats { get; set; }
        public required float Carbohydrates { get; set; }
        public required float Sugars { get; set; }
        public required float Protein { get; set; }
        public required float DietaryFiber { get; set; }
        public required float Water { get; set; }
        public required float Cholesterol { get; set; }
        public required float Sodium { get; set; }
        public required float VitaminA { get; set; }
        public required float VitaminB1Thiamine { get; set; }
        public required float VitaminB11FolicAcid { get; set; }
        public required float VitaminB12 { get; set; }
        public required float VitaminB2Riboflavin { get; set; }
        public required float VitaminB3Niacin { get; set; }
        public required float VitaminB5PantothenicAcid { get; set; }
        public required float VitaminB6 { get; set; }
        public required float VitaminC { get; set; }
        public required float VitaminD { get; set; }
        public required float VitaminE { get; set; }
        public required float VitaminK { get; set; }
        public required float Calcium { get; set; }
        public required float Copper { get; set; }
        public required float Iron { get; set; }
        public required float Magnesium { get; set; }
        public required float Manganese { get; set; }
        public required float Phosphorus { get; set; }
        public required float Potassium { get; set; }
        public required float Selenium { get; set; }
        public required float Zinc { get; set; }
        public required float NutritionDensity { get; set; }
        public required DateTime ModifiedAt { get; set; }
        public required bool Deleted { get; set; }
    }
}
