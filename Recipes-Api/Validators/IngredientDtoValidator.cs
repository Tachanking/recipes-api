using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
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
