namespace Recipe.Core.Entities;

public class Comment : BaseEntity
{
    public string Message { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int Rate { get; set; }
    public int RecipeId { get; set; }
    
    public Recipe Recipe { get; set; } = null!;
    public UserApp User { get; set; } = null!;
}