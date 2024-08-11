using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Comment;

public class CommentCreateDto
{
    public int RecipeId { get; set; }
    public string Message { get; set; }
    public int? Rate { get; set; }
    public int? ParentComment { get; set; }    
    [JsonIgnore] public string? UserId { get; set; }
}