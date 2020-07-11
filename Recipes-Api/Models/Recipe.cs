using Recipes_API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes_API
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeIngredientMeasurement = new HashSet<RecipeIngredientMeasurement>();
            RecipeTool = new HashSet<RecipeTool>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
