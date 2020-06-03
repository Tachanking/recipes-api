using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            RecipeIngredients = new HashSet<RecipeIngredients>();
        }

        public int IngredientId { get; set; }
        public int MeasurementId { get; set; }
        public string IngredientName { get; set; }

        public virtual Measurements Measurement { get; set; }
        public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; }
    }
}
