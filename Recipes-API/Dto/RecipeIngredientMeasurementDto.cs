namespace Recipes_API.Dto
{
    public class RecipeIngredientMeasurementDto
    {
        public RecipeDto Recipe { get; set; }
        public IngredientDto Ingredient { get; set; }
        public MeasurementDto Measurement { get; set; }

        public double Quantity { get; set; }
    }
}
