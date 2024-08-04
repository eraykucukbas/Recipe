using Recipe.Core.DTOs;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IRecipeService : IService<Entities.Recipe>
    {
        // Task<CustomResponseDto<Entities.Recipe>> CheckUserAccess(int todoListId, string userId);
        Task<CustomResponseDto<List<RecipeDto>>> GetAll();
        Task<CustomResponseDto<RecipeDetailDto>> GetById(int id);
        Task<CustomResponseDto<RecipeDto>> Create(RecipeCreateDto recipeCreateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> IsCompletedTrigger(int id, UserApp activeUser);
        // Task<CustomResponseDto<TodoListDto>> CreateTodoList(TodoListCreateDto todoListCreateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> UpdateTodoList(TodoListUpdateDto todoListUpdateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> RemoveTodoList(int id, UserApp activeUser);
    }
}
