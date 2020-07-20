using FluentValidation;
using Recipes_Api.Dto;
using Recipes_Api.Utility;

namespace Recipes_Api.Validators
{
    public class ToolDtoValidator : AbstractValidator<ToolDto>
    {
        public ToolDtoValidator()
        {
            RuleFor(tool => tool.Id).NotNull();
            RuleFor(tool => tool.Id).GreaterThanOrEqualTo(Constants.IdMinimumValue);

            RuleFor(tool => tool.Name).NotEmpty();
            RuleFor(tool => tool.Name).MaximumLength(Constants.NameMaximumLength);
        }
    }
}
