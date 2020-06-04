using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Measurement
    {
        public Measurement()
        {
            Ingredient = new HashSet<Ingredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
