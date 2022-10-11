using RecipeModelLibrary.Models;

namespace CookInspiration.Services.RecipeService
{
    public interface IRecipeService
    {
        List<Recipe> Recipes { get; set; }

        Task GetRecipes();
        Task CreateRecipe(Recipe recipe);
        Task DeleteRecipe(int id);
        Task UpdateRecipe(Recipe recipe);
    }
}
