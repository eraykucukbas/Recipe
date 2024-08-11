using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Favorite;

public class FavoriteCreateDto
{
    public int RecipeId { get; set; }

    [JsonIgnore] public string? UserId { get; set; }
}