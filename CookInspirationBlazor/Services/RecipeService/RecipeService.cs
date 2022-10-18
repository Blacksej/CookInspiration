using Microsoft.AspNetCore.Components;
using RecipeModelLibrary.Models;
using System.Text.Json;

namespace CookInspiration.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public List<Ingredient> RecipeIngredients { get; set; } = new List<Ingredient>();
        public List<Ingredient> AllIngredients { get; set; } = new List<Ingredient>();

        public RecipeService(HttpClient http, NavigationManager navigationManager)
        {
            _httpClient = http;
            _navigationManager = navigationManager;
        }

        public async Task GetRecipes()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7043/api/Recipes");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonResponse);

            if(recipes != null)
            {
                Recipes = recipes;
            }
        }

        public async Task GetIngredients()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7043/api/Ingredients");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(jsonResponse);

            if (ingredients != null)
            {
                AllIngredients = ingredients;
            }
        }

        public async Task<Recipe> GetSingleRecipe(int id)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7043/api/Recipes/{id}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var recipe = JsonSerializer.Deserialize<Recipe>(jsonResponse);

            RecipeIngredients = recipe.Ingredients;

            if (recipe != null)
            {
                return recipe;
            }

            throw new Exception("Recipe not found!");
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7043/api/Recipes", recipe);
            _navigationManager.NavigateTo("recipes");
            //var returnRecipe = await result.Content.ReadFromJsonAsync<List<Recipe>>();
            //Recipes = returnRecipe;
        }

        public Task DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            var recipeResult = await _httpClient.PutAsJsonAsync($"https://localhost:7043/api/Recipes/{recipe.RecipeId}", recipe);

            _navigationManager.NavigateTo("recipes");
        }

        //public async Task SetRecipes(HttpResponseMessage result)
        //{
        //    var jsonResponse = await result.Content.ReadFromJsonAsync<Recipe>();

        //    _navigationManager.NavigateTo("recipes");
        //}
    }
}
