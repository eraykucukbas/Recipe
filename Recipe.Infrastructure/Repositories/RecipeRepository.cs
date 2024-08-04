using Recipe.Core.Interfaces.Repositories;
using Recipe.Infrastructure.Models;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.Infrastructure.Repositories
{
    public class RecipeRepository : GenericRepository<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext context) : base(context)
        {
            
        }
        // public async Task<RecipeEntity> GetByIdWithTodoItems(int id)
        // {
        //     var todoList = await _context.TodoLists
        //         .Include(x => x.TodoItems)
        //         .FirstOrDefaultAsync(t => t.Id == id); 
        //
        //     if (todoList == null)
        //     {
        //         return null; // or handle not found case as needed
        //     }
        //
        //     todoList.TodoItems = todoList.TodoItems
        //         .OrderBy(i => i.IsCompleted)
        //         .ThenByDescending(t => t.CreatedDate)
        //         .ToList();
        //
        //     return todoList;
        // }
    }
}