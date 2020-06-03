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

        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementSymbol { get; set; }

        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}
