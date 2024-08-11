using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Unit;

public class UnitUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}