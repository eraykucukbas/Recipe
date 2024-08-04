namespace Recipe.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}