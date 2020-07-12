using Recipes_API.Models;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Measurement
    {
        public Measurement()
        {
            RecipeIngredientMeasurement = new HashSet<RecipeIngredientMeasurement>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
    }
}
