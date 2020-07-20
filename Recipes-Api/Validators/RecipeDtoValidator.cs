using FluentValidation;
using Recipes_Api.Dto;
using Recipes_Api.Utility;

namespace Recipes_Api.Validators
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
