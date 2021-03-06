﻿using System.Collections.Generic;

namespace Recipes_Api.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredientMeasurement = new HashSet<RecipeIngredientMeasurement>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
    }
}
