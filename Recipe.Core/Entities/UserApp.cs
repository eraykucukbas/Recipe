using Microsoft.AspNetCore.Identity;

namespace Recipe.Core.Entities
{
    public class UserApp : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public override string UserName { get; set; } = null!;
        public bool IsActive { get; set; }
        public override string Email { get; set; } = null!;
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}