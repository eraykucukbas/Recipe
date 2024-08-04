using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Instruction;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeCreateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string UserId { get; set; } = null!;
    public int CategoryId { get; set; }
    public List<IngredientCreateDto> Ingredients { get; set; }
    public List<InstructionCreateDto> Instructions { get; set; }
}