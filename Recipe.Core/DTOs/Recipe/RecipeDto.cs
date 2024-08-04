using Recipe.Core.DTOs.Base;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string UserId { get; set; } = null!;
    public int CategoryId { get; set; }
}