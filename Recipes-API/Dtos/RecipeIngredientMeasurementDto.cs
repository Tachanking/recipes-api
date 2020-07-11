namespace Recipes_API.Dto
{
    public class RecipeIngredientMeasurementDto
    {
        public long RecipeId { get; set; }
        public long IngredientId { get; set; }
        public long MeasurementId { get; set; }

        public double Quantity { get; set; }
    }
}
