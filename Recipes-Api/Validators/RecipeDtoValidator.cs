using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeDtoValidator : AbstractValidator<RecipeDto>
    {
        public RecipeDtoValidator()
        {
            RuleFor(ingredient => ingredient.Name).NotNull();
            RuleFor(ingredient => ingredient.Name).MaximumLength(Constants.NameMaxLength);
        }
    }
}
