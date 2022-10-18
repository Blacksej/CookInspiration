using System.Text.Json.Serialization;

namespace RecipeModelLibrary.Models
{
    public class Ingredient
    {
        [JsonPropertyName("ingredientId")]
        public int IngredientId { get; set; }

        [JsonPropertyName("nameAndAmount")]
        public string NameAndAmount { get; set; }

        [JsonPropertyName("recipes")]
        public List<Recipe>? Recipes { get; set; }

    }
}
