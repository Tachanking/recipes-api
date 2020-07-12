using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeToolDtoValidator : AbstractValidator<RecipeToolDto>
    {
        public RecipeToolDtoValidator()
        {
            RuleFor(tool => tool.RecipeId).NotNull();
            RuleFor(tool => tool.RecipeId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(tool => tool.ToolId).NotNull();
            RuleFor(tool => tool.ToolId).GreaterThanOrEqualTo(Constants.IdMinimumValue);
        }
    }
}