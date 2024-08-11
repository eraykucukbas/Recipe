using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Favorite;

public class FavoriteDto : BaseDto
{
    public int RecipeId { get; set; }
    public string UserId { get; set; } = null!;
    public UserAppSummaryDto User { get; set; } = null!;
    public RecipeSummaryDto Recipe { get; set; } = null!;
}