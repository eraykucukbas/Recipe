using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Category;

public class CategoryCreateDto
{
    public string Name { get; set; } = null!;
}