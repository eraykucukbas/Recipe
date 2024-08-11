using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Favorite;

public class FavoriteMyDto : BaseDto
{
    public RecipeSummaryDto Recipe { get; set; } = null!;
}