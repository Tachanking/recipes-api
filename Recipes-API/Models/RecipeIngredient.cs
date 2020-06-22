using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }        
        public double Quantity { get; set; }
        public bool IsOptional { get; set; }
    }
}
