using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeDtoValidator : AbstractValidator<RecipeDto>
    {
        public RecipeDtoValidator()
        {
            RuleFor(recipe => recipe.Id).NotNull();
            RuleFor(recipe => recipe.Id).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(recipe => recipe.Name).NotEmpty();
            RuleFor(recipe => recipe.Name).MaximumLength(Constants.NameMaximumLength);
        }
    }
}
