using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Recipes
    {
        public Recipes()
        {
            RecipeIngredients = new HashSet<RecipeIngredients>();
            RecipeTools = new HashSet<RecipeTools>();
        }

        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; }
        public virtual ICollection<RecipeTools> RecipeTools { get; set; }
    }
}
