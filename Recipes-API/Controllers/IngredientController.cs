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
        public IEnumerable<Ingredients> Get()
        {
            IEnumerable<Ingredients> ingredients;
            using (var context = new PostgresContext())
            {
                ingredients = context.Ingredients.ToList();
            }

            return ingredients;
        }
    }
}
