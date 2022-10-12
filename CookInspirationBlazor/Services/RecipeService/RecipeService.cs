using Microsoft.AspNetCore.Components;
using RecipeModelLibrary.Models;
using System.Text.Json;

namespace CookInspiration.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        HttpClient _httpClient;
        NavigationManager _navigationManager;
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        //public RecipeService(HttpClient http, NavigationManager navigationManager)
        //{
        //    _httpClient = http;
        //    _navigationManager = navigationManager;
        //}

        public async Task GetRecipes()
        {
            _httpClient = new HttpClient();

            using HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7043/api/Recipes");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonResponse);

            if(recipes != null)
            {
                Recipes = recipes;
            }
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Recipes", recipe);
            var response = await result.Content.ReadFromJsonAsync<List<Recipe>>();
            Recipes = response;
        }

        public Task DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }

        private async Task SetRecipes(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Recipe>>();
            Recipes = response;
            _navigationManager.NavigateTo("recipes");
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/Recipes/{recipe.RecipeId}", recipe);
            var response = await result.Content.ReadFromJsonAsync<List<Recipe>>();
            await SetRecipes(result);
        }
    }
}
