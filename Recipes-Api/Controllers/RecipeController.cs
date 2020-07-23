using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Recipes_Api.Dto;
using Recipes_Api.Models;
using Recipes_API.Services;

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
                return NotFound();

            return _mapper.Map<RecipeDto>(recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(long id, RecipeDto recipeDto)
        {
            if (id != recipeDto.Id)
                return BadRequest();

            var recipe = _mapper.Map<Recipe>(recipeDto);
            var isUpdated = await _recipeService.PutRecipe(id, recipe);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        // POST: api/Recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> PostRecipe(RecipeDto recipeDto)
        {
            // todo : automapper...
            var recipeIngredientMeasurements = new List<RecipeIngredientMeasurement>();
            foreach(var ingredient in recipeDto.Ingredients)
            {
                foreach(var measurement in ingredient.Measurements)
                {
                    recipeIngredientMeasurements.Add(new RecipeIngredientMeasurement()
                    {
                        RecipeId = recipeDto.Id,
                        IngredientId = ingredient.Id,
                        MeasurementId = measurement.Id,
                        Quantity = measurement.Quantity
                    });
                }
            }

            var recipeTools = new List<RecipeTool>();
            foreach(var tool in recipeDto.Tools)
            {
                recipeTools.Add(new RecipeTool()
                {
                    RecipeId = recipeDto.Id,
                    ToolId = tool.Id,
                    Quantity = tool.Quantity
                });
            }

            var recipe = new Recipe()
            {
                Id = recipeDto.Id,
                Name = recipeDto.Name,
                RecipeIngredientMeasurement = recipeIngredientMeasurements,
                RecipeTool = recipeTools
            };

            await _recipeService.PostRecipe(recipe);

            return CreatedAtAction(nameof(PostRecipe), recipeDto);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> DeleteRecipe(long id)
        {            
            var isDeleted = await _recipeService.DeleteRecipe(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

        // GET: api/Recipes/5/Tools
        [HttpGet("{recipeId}/Tools")]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetRecipeTools(long recipeId)
        {
            var recipeTools = await _recipeService.GetRecipeTools(recipeId);
            return _mapper.Map<List<ToolDto>>(recipeTools);
        }

        // GET: api/Recipes/5/Tools/4
        [HttpGet("{recipeId}/Tools/{toolId}")]
        public async Task<ActionResult<ToolDto>> GetRecipeTool(long recipeId, long toolId)
        {
            var tool = await _recipeService.GetRecipeTool(recipeId, toolId);

            if (tool is null)
                return NotFound();

            return _mapper.Map<ToolDto>(tool);
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements
        [HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements")]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        {
            var recipeIngredientMeasurements = await _recipeService.GetRecipeIngredientMeasurements(recipeId, ingredientId);
            return _mapper.Map<List<MeasurementDto>>(recipeIngredientMeasurements);
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements/5
        [HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements/{measurementId}")]
        public async Task<ActionResult<MeasurementDto>> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            var measurement = await _recipeService.GetRecipeIngredientMeasurement(recipeId, ingredientId, measurementId);

            if (measurement == null)
                return NotFound();

            return _mapper.Map<MeasurementDto>(measurement);
        }
    }
}
