using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Comment;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface ICommentService : IService<Comment>
    {
        Task<CustomResponseDto<List<CommentDto>>> GetAllAsync(FilterReportDto filterReportDto);
        Task<CustomResponseDto<List<CommentDto>>> GetParentCommentsByRecipe(int recipeId, FilterPaginationDto filterPaginationDto);
        Task<CustomResponseDto<List<CommentDto>>> GetCommentsByParent(int parentComment, FilterPaginationDto filterPaginationDto);
        Task<CustomResponseDto<CommentDto>> Create(CommentCreateDto favoriteCreateDto, UserApp activeUser);
        Task<CustomResponseDto<CommentDto>> Update(CommentUpdateDto commentUpdateDto, UserApp activeUser);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id, UserApp activeUser);
    }
}