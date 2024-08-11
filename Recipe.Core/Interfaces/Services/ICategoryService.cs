using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(FilterPaginationDto filterPaginationDto);
        Task<CustomResponseDto<CategoryDto>> Create(CategoryCreateDto categoryCreateDto);
        Task<CustomResponseDto<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
    }
}