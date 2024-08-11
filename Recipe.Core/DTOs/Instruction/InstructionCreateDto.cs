using System.Text.Json.Serialization;
using Recipe.Core.DTOs.Base;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Instruction;

public class InstructionCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    [JsonIgnore] public int Step { get; set; }
    
    [JsonIgnore] public int RecipeId { get; set; }
}