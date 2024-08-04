using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Infrastructure.Models;

namespace Recipe.Infrastructure.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}