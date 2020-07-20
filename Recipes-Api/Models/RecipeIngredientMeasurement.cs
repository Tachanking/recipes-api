namespace Recipes_Api.Models
{
    public class RecipeIngredientMeasurement
    {
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public long MeasurementId { get; set; }
        public virtual Measurement Measurement { get; set; }

        public double Quantity { get; set; }
    }
}
