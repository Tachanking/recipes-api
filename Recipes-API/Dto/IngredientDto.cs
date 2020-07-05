namespace Recipes_API.Dto
{
    public class IngredientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        // todo : public ICollection<IngredientMeasurementDto> IngredientMeasurement;
        public MeasurementDto Measurement { get; set; }
    }
}
