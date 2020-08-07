using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using Recipes_Api.Services;

namespace Recipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecipeService _recipeService;

        public RecipeController(IMapper mapper, IRecipeService recipeService)
        {
            _mapper = mapper;
            _recipeService = recipeService;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            var recipes = await _recipeService.GetRecipes();
            return _mapper.Map<List<RecipeDto>>(recipes);
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(long id)
        {
            var recipe = await _recipeService.GetRecipe(id);

            if (recipe is null)
                return NotFound(); // todo : error handling

            return _mapper.Map<RecipeDto>(recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(long id, RecipeDto recipeDto)
        {
            // only recipe name was updated

            if (id != recipeDto.Id)
                return BadRequest(); // todo : error handling

            var recipe = MapDtoToRecipe(recipeDto); // todo : fix automapper profile...
            var isUpdated = await _recipeService.PutRecipe(id, recipe);

            if (!isUpdated)
                return NotFound(); // todo : error handling

            return NoContent();
        }

        // POST: api/Recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> PostRecipe(RecipeDto recipeDto)
        {
            // todo : automapper profile...
            // todo : fix entity tracking issue
            // todo : error handling
            var recipe = MapDtoToRecipe(recipeDto);
            await _recipeService.PostRecipe(recipe);
            return CreatedAtAction(nameof(PostRecipe), recipeDto);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> DeleteRecipe(long id)
        {            
            var isDeleted = await _recipeService.DeleteRecipe(id);

            if (!isDeleted) // todo : error handling
                return NotFound();

            return NoContent();
        }

        private Recipe MapDtoToRecipe(RecipeDto recipeDto)
        {
            var recipeIngredientMeasurements = new List<RecipeIngredientMeasurement>();
            foreach (var ingredient in recipeDto.Ingredients)
            {
                foreach (var measurement in ingredient.Measurements)
                {
                    recipeIngredientMeasurements.Add(new RecipeIngredientMeasurement()
                    {
                        IngredientId = ingredient.Id,
                        Ingredient = new Ingredient()
                        {
                            Id = ingredient.Id,
                            Name = ingredient.Name
                        },
                        MeasurementId = measurement.Id,
                        Measurement = new Measurement()
                        {
                            Id = measurement.Id,
                            Name = measurement.Name,
                            Symbol = measurement.Symbol
                        },
                        Quantity = measurement.Quantity
                    });
                }
            }

            var recipeTools = new List<RecipeTool>();
            foreach (var tool in recipeDto.Tools)
            {
                recipeTools.Add(new RecipeTool()
                {
                    Tool = new Tool()
                    {
                        Id = tool.Id,
                        Name = tool.Name,
                    },
                    Quantity = tool.Quantity
                });
            }

            return new Recipe()
            {
                Id = recipeDto.Id,
                Name = recipeDto.Name,
                RecipeIngredientMeasurement = recipeIngredientMeasurements,
                RecipeTool = recipeTools
            };
        }
    }
}
