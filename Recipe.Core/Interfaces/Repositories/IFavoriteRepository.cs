using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Repositories;

public interface IFavoriteRepository : IGenericRepository<Favorite>
{
    // Task<TodoList> GetByIdWithTodoItems(int id);
}