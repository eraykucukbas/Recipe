using System.Text.Json.Serialization;
using Recipe.Core.DTOs.Base;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Ingredient;

public class IngredientCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int UnitId { get; set; }
    
    [JsonIgnore]
    public int RecipeId { get; set; }
}