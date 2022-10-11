using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecipeModelLibrary.Models
{
    public class Recipe
    {
        [JsonPropertyName("recipeid")]
        public int RecipeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("steps")]
        public List<Step>? Steps { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("ingredients")]
        public List<Ingredient>? Ingredients { get; set; }
    }
}
