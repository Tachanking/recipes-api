using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class RecipeToolDtoValidator : AbstractValidator<RecipeToolDto>
    {
        public RecipeToolDtoValidator()
        {
            //RuleFor(tool => tool.ToolName).NotEmpty();
            //RuleFor(tool => tool.ToolName).MaximumLength(Constants.NameMaxLength);
        }
    }
}