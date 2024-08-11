namespace Recipe.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}

public interface IUserOwnedEntity
{
    string UserId { get; set; }
}

public interface IRecipeOwnedEntity
{
    int RecipeId { get; set; }
}

public interface IUserAndRecipeOwnedEntity : IUserOwnedEntity, IRecipeOwnedEntity
{
}