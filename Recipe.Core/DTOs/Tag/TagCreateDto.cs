using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Tag;

public class TagCreateDto
{
    public string Name { get; set; } = null!;
    public int RecipeId { get; set; }
}