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

            var recipe = _mapper.Map<Recipe>(recipeDto);
            var isUpdated = await _recipeService.PutRecipe(id, recipe);

            if (!isUpdated)
                return NotFound(); // todo : error handling

            return NoContent();
        }

        // POST: api/Recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> PostRecipe(RecipeDto recipeDto)
        {
            // todo : fix entity tracking issue
            // todo : error handling
            var recipe = _mapper.Map<Recipe>(recipeDto);
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
    }
}
