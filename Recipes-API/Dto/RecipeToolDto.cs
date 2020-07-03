namespace Recipes_API.Dto
{
    public class RecipeToolDto
    {
        public RecipeDto Recipe {get;set;}
        public ToolDto Tool { get; set; }

        public int Quantity { get; set; }
    }
}
