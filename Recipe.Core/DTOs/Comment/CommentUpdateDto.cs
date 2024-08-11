using System.Text.Json.Serialization;

namespace Recipe.Core.DTOs.Comment;

public class CommentUpdateDto
{
    public int Id { get; set; }
    public string Message { get; set; } = null!;
    public int? Rate { get; set; }
    [JsonIgnore] public string? UserId { get; set; }
}