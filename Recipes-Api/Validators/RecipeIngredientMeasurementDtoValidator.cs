using FluentValidation;
using Recipes_Api.Dto;
using Recipes_Api.Utility;

namespace Recipes_Api.Validators
{
    public class RecipeIngredientMeasurementDtoValidator : AbstractValidator<RecipeIngredientMeasurementDto>
    {
        public RecipeIngredientMeasurementDtoValidator()
        {
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.RecipeId).NotNull();
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.RecipeId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.IngredientId).NotNull();
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.IngredientId).GreaterThanOrEqualTo(Constants.IdMinimumValue);
            
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.MeasurementId).NotNull();
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.MeasurementId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.Quantity).NotNull();
            RuleFor(recipeIngredientMeasurement => recipeIngredientMeasurement.Quantity).GreaterThanOrEqualTo(Constants.IngredientMinimumQuantity);
        }
    }
}
