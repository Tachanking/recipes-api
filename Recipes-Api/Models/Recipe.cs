﻿using System.Collections.Generic;

namespace Recipes_Api.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeIngredientMeasurement = new HashSet<RecipeIngredientMeasurement>();
            RecipeTool = new HashSet<RecipeTool>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
