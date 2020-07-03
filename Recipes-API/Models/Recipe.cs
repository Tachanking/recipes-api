using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes_API
{
    public partial class Recipe
    {
        public Recipe()
        {
            RecipeIngredient = new HashSet<RecipeIngredient>();
            RecipeTool = new HashSet<RecipeTool>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual ICollection<RecipeTool> RecipeTool { get; set; }
    }
}
