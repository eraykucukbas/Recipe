using Recipe.Core.DTOs.Base;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Favorite;

public class FavoriteDto : BaseDto
{
    public string UserId { get; set; }
    public Entities.Recipe Recipe { get; set; }
}