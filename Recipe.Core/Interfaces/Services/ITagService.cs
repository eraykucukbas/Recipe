using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Tag;
using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface ITagService : IService<Tag>
    {
        // Task<CustomResponseDto<List<TagDto>>> GetAllAsync(FilterPaginationDto filterPaginationDto);
        Task<CustomResponseDto<TagDto>> Create(TagCreateDto tagCreateDto);
        // Task<CustomResponseDto<TagDto>> Update(TagUpdateDto tagUpdateDto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id);
    }
}