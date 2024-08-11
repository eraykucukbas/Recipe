using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Instruction;
using Recipe.Core.DTOs.Tag;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeCreateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [JsonIgnore] public string? ImageUrl { get; set; }

    // public IFormFile Image { get; set; }
    [JsonIgnore] public string? UserId { get; set; }
    public int CategoryId { get; set; }
    public List<IngredientCreateDto> Ingredients { get; set; }
    public List<InstructionCreateDto> Instructions { get; set; }
    public List<TagCreateDto> Tags { get; set; }
}

public class RecipeCreateFormDataDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public IFormFile Image { get; set; }
    [JsonIgnore] public string? UserId { get; set; }
    public int CategoryId { get; set; }
    public string? Ingredients { get; set; }
    public string? Instructions { get; set; }
    public string? Tags { get; set; }
}