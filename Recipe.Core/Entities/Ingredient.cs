namespace Recipe.Core.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public int UnitId { get; set; }
    public int RecipeId { get; set; }

    public virtual Unit Unit { get; set; }
    public Recipe Recipe { get; set; }
}