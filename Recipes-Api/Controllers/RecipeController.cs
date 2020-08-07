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

        //// GET: api/Recipes/5/Tools
        //[HttpGet("{recipeId}/Tools")]
        //public async Task<ActionResult<IEnumerable<ToolDto>>> GetRecipeTools(long recipeId)
        //{
        //    var recipeTools = await _recipeService.GetRecipeTools(recipeId);
        //    return _mapper.Map<List<ToolDto>>(recipeTools);
        //}

        //// GET: api/Recipes/5/Tools/4
        //[HttpGet("{recipeId}/Tools/{toolId}")]
        //public async Task<ActionResult<ToolDto>> GetRecipeTool(long recipeId, long toolId)
        //{
        //    var tool = await _recipeService.GetRecipeTool(recipeId, toolId);

        //    if (tool is null)
        //        return NotFound(); // todo : error handling

        //    return _mapper.Map<ToolDto>(tool);
        //}

        //// GET: api/Recipes/5/Ingredients/5/Measurements
        //[HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements")]
        //public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        //{
        //    var recipeIngredientMeasurements = await _recipeService.GetRecipeIngredientMeasurements(recipeId, ingredientId);
        //    return _mapper.Map<List<MeasurementDto>>(recipeIngredientMeasurements);
        //}

        //// GET: api/Recipes/5/Ingredients/5/Measurements/5
        //[HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements/{measurementId}")]
        //public async Task<ActionResult<MeasurementDto>> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        //{
        //    var measurement = await _recipeService.GetRecipeIngredientMeasurement(recipeId, ingredientId, measurementId);

        //    if (measurement == null)
        //        return NotFound(); // todo : error handling

        //    return _mapper.Map<MeasurementDto>(measurement);
        //}

        //// FROM HERE -------------------------------------------------------------

        //// PUT: api/Recipes/5/Ingredients/5/Measurements/5
        //[HttpPut("{recipeId}/Ingredients/{ingredientId}/Measurements/{measurementId}")]
        //public async Task<IActionResult> PutRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId, MeasurementDto measurementDto)
        //{
        //    if (measurementId != measurementDto.Id)
        //        return BadRequest();            // todo : error handling

        //    var measurement = _mapper.Map<Measurement>(measurementDto);
        //    await _recipeService.PutRecipeIngredientMeasurement(recipeId, ingredientId, measurementId, measurement);

        //    return NoContent();
        //}

        //// POST: api/Recipes/5/Ingredients/5/Measurements
        //[HttpPost("{recipeId}/Ingredients/{ingredientId}/Measurements")]
        //public async Task<ActionResult<MeasurementDto>> PostRecipeIngredientMeasurement(long recipeId, long ingredientId, MeasurementDto measurementDto)
        //{
        //    var measurement = _mapper.Map<Measurement>(measurementDto);
        //    await _recipeService.PostRecipeIngredientMeasurement(recipeId, ingredientId, measurement);
        //    return CreatedAtAction("GetRecipeIngredientMeasurement", new { measurementId = measurementDto.Id }, measurementDto);
        //}

        //// DELETE: api/Recipes/5/Ingredients/5/Measurements/5
        //[HttpDelete("{recipeId}/Ingredients/{ingredientId}/Measurements/{measurementId}")]
        //public async Task<ActionResult<MeasurementDto>> DeleteRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        //{
        //    var measurement = await _recipeService.DeleteRecipeIngredientMeasurement(recipeId, ingredientId, measurementId);
        //    return _mapper.Map<MeasurementDto>(measurement);
        //}

        //// PUT: api/Recipes/5/Tools/4
        //[HttpPut("{recipeId}/Tools/{toolId}")]
        //public async Task<IActionResult> PutRecipeTool(long recipeId, long toolId, ToolDto toolDto)
        //{
        //    var ok = recipeId;
        //    if (toolId != toolDto.Id)
        //        return BadRequest(); // toto : error handling

        //    var tool = _mapper.Map<Tool>(toolDto);
        //    await _recipeService.PostRecipeTool(tool);

        //    return NoContent();
        //}

        //// POST: api/Recipes/5/Tools
        //[HttpPost("{recipeId}/Tools/")]
        //public async Task<ActionResult<ToolDto>> PostRecipeTool(long recipeId, ToolDto toolDto)
        //{
        //    var tool = _mapper.Map<Tool>(toolDto);
        //    await _recipeService.PostRecipeTool(tool);
        //    return CreatedAtAction("GetRecipeTool", toolDto);
        //}

        //// DELETE: api/Recipes/5/Tools/5
        //[HttpDelete("{recipeId}/Tools/{toolId}")]
        //public async Task<ActionResult<ToolDto>> DeleteRecipeTool(long recipeId, long toolId)
        //{
        //    var tool = await _recipeService.DeleteRecipeTool(recipeId, toolId);
        //    return _mapper.Map<ToolDto>(tool);
        //}

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
