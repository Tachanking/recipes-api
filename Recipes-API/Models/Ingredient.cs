using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredient = new HashSet<RecipeIngredient>();
        }

        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }
        public string IngredientName { get; set; }

        public virtual Measurement Measurement { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
