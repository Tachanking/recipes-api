using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeToolDtoValidator : AbstractValidator<RecipeToolDto>
    {
        public RecipeToolDtoValidator()
        {
            RuleFor(recipeTool => recipeTool.RecipeId).NotNull();
            RuleFor(recipeTool => recipeTool.RecipeId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(recipeTool => recipeTool.ToolId).NotNull();
            RuleFor(recipeTool => recipeTool.ToolId).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(recipeTool => recipeTool.Quantity).NotNull();
            RuleFor(recipeTool => recipeTool.Quantity).GreaterThanOrEqualTo(Constants.ToolMinimumQuantity);
        }
    }
}