using Microsoft.AspNetCore.Mvc;
using RecipeModelLibrary.Data;
using Microsoft.EntityFrameworkCore;
using RecipeModelLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CookInspirationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneRecipe(int id)
        {
            var recipe = await _context.Recipes
                .Include("Ingredients")
                .Include("Steps")
                .FirstOrDefaultAsync(recipe => recipe.RecipeId == id);

            return Ok(recipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _context.Recipes
                .Include("Ingredients")
                .Include("Steps")
                .ToListAsync();

            return Ok(recipes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRecipe(int id, Recipe recipe)
        {
            var dbRecipe = await _context.Recipes.AsNoTracking()
                .Include("Ingredients")
                .Include("Steps")
                .FirstOrDefaultAsync(x => x.RecipeId == id);

            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            dbRecipe.Name = recipe.Name;
            dbRecipe.Description = recipe.Description;
            //dbRecipe.Steps = recipe.Steps;

            dbRecipe.Image = recipe.Image;
            dbRecipe.Ingredients = recipe.Ingredients;
            

            _context.Entry(recipe).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(await GetAllRecipes());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        // FIX PLIZ: CAN'T DELETE RECIPE BECAUSE OF FOREIGN KEY LIMTIS
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(recipe => recipe.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe recipe)
        {
            List<Ingredient> tempIngredients = new List<Ingredient>();

            if(recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    var ingredientFromId = await _context.Ingredients.FirstOrDefaultAsync(x => x.IngredientId == ingredient.IngredientId);
                    tempIngredients.Add(ingredientFromId);
                }

                recipe.Ingredients = tempIngredients;
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecipe), new { id = recipe.RecipeId }, recipe);
        }

        //[HttpGet("ingredients")]
        //public async Task<IActionResult> GetIngredientsForRecipe()
        //{
        //    var recipe = await _context.Recipes
        //        .FirstOrDefaultAsync(recipe => recipe.RecipeId == id);

        //    return Ok(recipes);
        //}
    }
}
