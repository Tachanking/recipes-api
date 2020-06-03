using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeIngredient = new HashSet<RecipeIngredient>();
            RecipeTool = new HashSet<RecipeTool>();
        }

        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
