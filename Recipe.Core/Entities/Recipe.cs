using Microsoft.EntityFrameworkCore;

namespace Recipe.Core.Entities
{
    public class Recipe : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }

        public UserApp User { get; set; }
        public Category Category { get; set; }

        public ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}