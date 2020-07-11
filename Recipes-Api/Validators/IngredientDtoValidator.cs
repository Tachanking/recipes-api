using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class IngredientDtoValidator : AbstractValidator<IngredientDto>
    {
        public IngredientDtoValidator()
        {
            RuleFor(ingredient => ingredient.Name).NotNull().MaximumLength(Constants.NameMaxLength);
        }
    }
}
