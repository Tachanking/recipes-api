using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class RecipeIngredients
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }

        public virtual Ingredients Ingredient { get; set; }
        public virtual Recipes Recipe { get; set; }
    }
}
