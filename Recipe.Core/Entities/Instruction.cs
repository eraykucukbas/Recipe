namespace Recipe.Core.Entities;

public class Instruction : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; }
    public int RecipeId { get; set; }
    
    public Recipe Recipe { get; set; }
}