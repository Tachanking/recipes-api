namespace Recipes_API.Dto
{
    public class RecipeIngredientDto
    {
        public RecipeDto Recipe { get; set; }
        public IngredientDto Ingredient { get; set; }

        public double Quantity { get; set; }
    }
}
