namespace Recipes_API
{
    public partial class RecipeIngredient
    {
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public long IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public double Quantity { get; set; }
    }
}
