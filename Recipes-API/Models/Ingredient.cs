using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes_API
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            RecipeIngredient = new HashSet<RecipeIngredient>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public long MeasurementId { get; set; }
        public virtual Measurement Measurement { get; set; }

        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
