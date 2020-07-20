using FluentValidation;
using Recipes_Api.Dto;
using Recipes_Api.Utility;

namespace Recipes_Api.Validators
{
    public class IngredientDtoValidator : AbstractValidator<IngredientDto>
    {
        public IngredientDtoValidator()
        {
            RuleFor(ingredient => ingredient.Id).NotNull();
            RuleFor(ingredient => ingredient.Id).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(ingredient => ingredient.Name).NotEmpty();
            RuleFor(ingredient => ingredient.Name).MaximumLength(Constants.NameMaximumLength);
        }
    }
}
