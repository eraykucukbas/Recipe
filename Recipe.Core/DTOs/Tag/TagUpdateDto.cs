using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Tag;

public class TagUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}