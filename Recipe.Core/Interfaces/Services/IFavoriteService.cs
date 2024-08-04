using Recipe.Core.DTOs;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IFavoriteService : IService<Favorite>
    {
        // Task<CustomResponseDto<Entities.Recipe>> CheckUserAccess(int todoListId, string userId);
        Task<CustomResponseDto<List<FavoriteDto>>> GetAll();
        Task<CustomResponseDto<FavoriteDto>> Create(FavoriteCreateDto favoriteCreateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> IsCompletedTrigger(int id, UserApp activeUser);
        // Task<CustomResponseDto<TodoListDto>> CreateTodoList(TodoListCreateDto todoListCreateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> UpdateTodoList(TodoListUpdateDto todoListUpdateDto, UserApp activeUser);
        // Task<CustomResponseDto<NoContentDto>> RemoveTodoList(int id, UserApp activeUser);
    }
}
