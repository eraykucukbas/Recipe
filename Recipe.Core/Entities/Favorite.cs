namespace Recipe.Core.Entities;

public class Favorite : BaseEntity
{
    public string UserId { get; set; } = null!;
    public int RecipeId { get; set; }
    
    public Recipe Recipe { get; set; }
    public UserApp User { get; set; }
}