namespace Recipes_API
{
    public partial class RecipeTool
    {
        public long RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public long ToolId { get; set; }
        public virtual Tool Tool { get; set; }

        public int Quantity { get; set; }
    }
}
