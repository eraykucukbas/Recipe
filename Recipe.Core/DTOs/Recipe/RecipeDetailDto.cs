using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Instruction;
using Recipe.Core.DTOs.Tag;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeDetailDto : BaseDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public UserAppDto User { get; set; }
    public CategoryDto Category { get; set; }
    public ICollection<IngredientDto> Ingredients { get; set; }
    public ICollection<InstructionDto> Instructions { get; set; }
    public ICollection<TagDto> Tags { get; set; }
    public double? AverageRating { get; set; }

}
