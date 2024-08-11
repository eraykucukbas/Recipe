using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IUnitService : IService<Unit>
    {
        Task<CustomResponseDto<List<UnitDto>>> GetAllAsync(FilterPaginationDto filterPaginationDto);
        Task<CustomResponseDto<UnitDto>> Create(UnitCreateDto unitCreateDto);
        Task<CustomResponseDto<UnitDto>> Update(UnitUpdateDto unitUpdateDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
    }
}