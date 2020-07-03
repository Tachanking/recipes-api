namespace Recipes_API.Dto
{
    public class IngredientDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MeasurementDto Measurement { get; set; }
    }
}
