using Recipe.Core.DTOs.Base;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeDetailDto : BaseDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public UserApp User { get; set; }
    public Category Category { get; set; }
    public ICollection<Entities.Ingredient> Ingredients { get; set; }
    public ICollection<Entities.Instruction> Instructions { get; set; }
}