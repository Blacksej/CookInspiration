using RecipeModelLibrary.Models;

namespace CookInspiration.Services.RecipeService
{
    public interface IRecipeService
    {
        List<Recipe> Recipes { get; set; }
        List<Ingredient> RecipeIngredients { get; set; }
        List<Ingredient> AllIngredients { get; set; }

        Task GetIngredients();
        Task<Recipe> GetSingleRecipe(int id);
        Task GetRecipes();
        Task CreateRecipe(Recipe recipe);
        Task DeleteRecipe(int id);
        Task UpdateRecipe(Recipe recipe);
    }
}
