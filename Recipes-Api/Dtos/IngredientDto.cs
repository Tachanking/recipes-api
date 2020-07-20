using System.Collections.Generic;

namespace Recipes_Api.Dto
{
    public class IngredientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public IList<MeasurementDto> Measurements { get; set; }
    }
}
