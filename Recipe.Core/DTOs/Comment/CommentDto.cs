using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Comment;

public class CommentDto : BaseDto
{
    // public int RecipeId { get; set; }
    // public string UserId { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int? Rate { get; set; }
    public int? ParentComment { get; set; }
    public UserAppSummaryDto User { get; set; } = null!;
    public RecipeSummaryDto? Recipe { get; set; }
}