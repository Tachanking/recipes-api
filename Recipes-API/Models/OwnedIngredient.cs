using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class OwnedIngredient
    {
        public OwnedIngredient()
        {
        }

        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
