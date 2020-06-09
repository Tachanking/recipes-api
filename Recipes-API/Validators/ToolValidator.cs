using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes_API.Validators
{
    public class ToolValidator : AbstractValidator<Tool>
    {
        public ToolValidator()
        {
            RuleFor(tool => tool.Name).NotNull();

            RuleForEach(tool => tool.RecipeTool).NotNull().SetValidator(new RecipeToolValidator());
        }
    }
}
