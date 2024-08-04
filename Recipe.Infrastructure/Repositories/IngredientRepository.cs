using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Infrastructure.Models;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.Infrastructure.Repositories
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}