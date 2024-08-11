using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Category;

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}