using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeIngredientMeasurementDtoValidator : AbstractValidator<RecipeIngredientMeasurementDto>
    {
        public RecipeIngredientMeasurementDtoValidator()
        {
            RuleFor(tool => tool.RecipeId).NotNull();
            RuleFor(tool => tool.RecipeId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(tool => tool.IngredientId).NotNull();
            RuleFor(tool => tool.IngredientId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(tool => tool.MeasurementId).NotNull();
            RuleFor(tool => tool.MeasurementId).GreaterThanOrEqualTo(Constants.IdMinimumValue);
        }
    }
}
