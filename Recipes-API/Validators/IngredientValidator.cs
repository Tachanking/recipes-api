using FluentValidation;

namespace Recipes_API.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            //RuleFor(ingredient => ingredient.Name).NotNull().MaximumLength(64);
            //RuleFor(ingredient => ingredient.MeasurementId).NotNull();

            //RuleForEach(recipeIngredient => recipeIngredient.RecipeIngredient).SetValidator(new RecipeIngredientValidator());
        }
    }
}
