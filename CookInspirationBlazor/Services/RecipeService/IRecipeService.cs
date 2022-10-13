using RecipeModelLibrary.Models;

namespace CookInspiration.Services.RecipeService
{
    public interface IRecipeService
    {
        List<Recipe> Recipes { get; set; }
        List<Ingredient> Ingredients { get; set; }

        Task<Recipe> GetSingleRecipe(int id);
        Task GetRecipes();
        Task CreateRecipe(Recipe recipe);
        Task DeleteRecipe(int id);
        Task UpdateRecipe(Recipe recipe);
    }
}
