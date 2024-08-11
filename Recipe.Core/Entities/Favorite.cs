namespace Recipe.Core.Entities;

public class Favorite : BaseEntity, IUserAndRecipeOwnedEntity
{
    public string UserId { get; set; } = null!;
    public int RecipeId { get; set; }

    public Recipe Recipe { get; set; } = null!;
    public UserApp User { get; set; } = null!;
}