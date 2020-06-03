using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Recipes_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly ILogger<IngredientController> _logger;

        public IngredientController(ILogger<IngredientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Ingredient> GetIngredients()
        {
            IEnumerable<Ingredient> ingredients;
            using (var context = new RecipesContext())
            {
                ingredients = context.Ingredients.ToList();
            }

            return ingredients;
        }
    }
}
