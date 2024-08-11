using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Unit;

public class UnitCreateDto
{
    public string Name { get; set; } = null!;
}