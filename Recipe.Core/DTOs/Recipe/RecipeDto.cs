using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Tag;
using Recipe.Core.DTOs.User;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeDto : BaseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string UserId { get; set; } = null!;
    public int? CategoryId { get; set; }
    
    public UserAppSummaryDto User { get; set; } = null!;
    public List<TagDto?> Tags { get; set; } = null!;
    public double? AverageRating { get; set; }
}