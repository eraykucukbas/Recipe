using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IFavoriteService : IService<Favorite>
    {
        // Task<CustomResponseDto<Entities.Recipe>> CheckUserAccess(int todoListId, string userId);
        Task<CustomResponseDto<List<FavoriteDto>>> GetAllAsync(FilterReportDto filterReportDto);

        Task<CustomResponseDto<List<FavoriteMyDto>>> GetMyAsync(FilterPaginationDto filterPaginationDto,
            UserApp activeUser);

        Task<CustomResponseDto<FavoriteDto>> Create(FavoriteCreateDto favoriteCreateDto, UserApp activeUser);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id, UserApp activeUser);
    }
}