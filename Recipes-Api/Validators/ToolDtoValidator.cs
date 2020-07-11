using FluentValidation;
using Recipes_API.Dto;
using Recipes_API.Utility;

namespace Recipes_API.Validators
{
    public class ToolDtoValidator : AbstractValidator<ToolDto>
    {
        public ToolDtoValidator()
        {
            RuleFor(tool => tool.Name).NotEmpty();
            RuleFor(tool => tool.Name).MaximumLength(Constants.NameMaxLength);
        }
    }
}
