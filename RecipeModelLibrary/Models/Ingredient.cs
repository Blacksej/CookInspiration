using System.Text.Json.Serialization;

namespace RecipeModelLibrary.Models
{
    public class Ingredient
    {
        [JsonPropertyName("ingredientId")]
        public int IngredientId { get; set; }

        [JsonPropertyName("nameAndAmount")]
        public string NameAndAmount { get; set; }

        [JsonPropertyName("recipeId")]
        public int? RecipeId { get; set; }

        [JsonPropertyName("recipe")]
        public Recipe? Recipe { get; set; }
    }
}
