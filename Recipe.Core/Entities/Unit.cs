namespace Recipe.Core.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}