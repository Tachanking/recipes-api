using System;
using System.Collections.Generic;

namespace Recipes_API
{
    public partial class Measurements
    {
        public Measurements()
        {
            Ingredients = new HashSet<Ingredients>();
        }

        public int MeasurementId { get; set; }
        public string MeasurementName { get; set; }
        public string MeasurementSymbol { get; set; }

        public virtual ICollection<Ingredients> Ingredients { get; set; }
    }
}
