using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeModelLibrary.Data;
using RecipeModelLibrary.Models;

namespace CookInspirationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(ingredient => ingredient.IngredientId == id);

            return Ok(ingredient);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            List<Ingredient> ingredients = await _context.Ingredients.ToListAsync();

            return Ok(ingredients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditIngredient(int id, [FromBody] Ingredient ingredient)
        {
            if (id != ingredient.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(ingredient => ingredient.IngredientId == id);

            if(ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateIngredient), new { id = ingredient.IngredientId }, ingredient);
        }
    }
}
