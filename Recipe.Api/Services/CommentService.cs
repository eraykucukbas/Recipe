using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recipe.Api.Helpers;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Comment;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;

namespace Recipe.API.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<UserApp> _userManager;

        public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork,
            IRecipeService recipeService, UserManager<UserApp> userManager
        )
            : base(repository, unitOfWork)
        {
            _userManager = userManager;
            _recipeService = recipeService;
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetAllAsync(FilterReportDto filterReportDto)
        {
            var query = GetAll()
                .Include(f => f.Recipe)
                .Include(f => f.User)
                .AsQueryable();
            query = QueryFilterHelper.ApplyFilters(query, filterReportDto);
            query = query.OrderByDescending(f => f.CreatedDate);
            var (queryList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterReportDto);
            var queryListDto = CommentMapper.ToListDtoWithRecipe(queryList);
            return CustomResponseDto<List<CommentDto>>.Success(200, queryListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetParentCommentsByRecipe(int recipeId,
            FilterPaginationDto filterPaginationDto)
        {
            var query = Where(t => t.RecipeId == recipeId).Where(t => t.ParentComment == null)
                .Include(f => f.Recipe)
                .Include(f => f.User)
                .AsQueryable();
            query = query.OrderByDescending(f => f.CreatedDate);
            var (queryList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterPaginationDto);
            var queryListDto = CommentMapper.ToListDto(queryList);
            return CustomResponseDto<List<CommentDto>>.Success(200, queryListDto, paginationInfo);
        }
        
        public async Task<CustomResponseDto<List<CommentDto>>> GetCommentsByParent(int parentComment,
            FilterPaginationDto filterPaginationDto)
        {
            var query = Where(t => t.ParentComment == parentComment)
                .Include(f => f.Recipe)
                .Include(f => f.User)
                .AsQueryable();
            query = query.OrderByDescending(f => f.CreatedDate);
            var (queryList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterPaginationDto);
            var queryListDto = CommentMapper.ToListDtoWithRecipe(queryList);
            return CustomResponseDto<List<CommentDto>>.Success(200, queryListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<CommentDto>> Create(CommentCreateDto commentCreateDto,
            UserApp activeUser)
        {
            commentCreateDto.UserId = activeUser.Id;
            await _recipeService.EnsureExistsOrThrow(t => t.Id == commentCreateDto.RecipeId);
            // await EnsureNotExistsOrThrow(t =>
            //     t.UserId == commentCreateDto.UserId && t.RecipeId == commentCreateDto.RecipeId);
            var entity = CommentMapper.ToEntity(commentCreateDto);
            var createdEntity = await AddAsync(entity);
            var entityDto = CommentMapper.ToDto(createdEntity);
            return CustomResponseDto<CommentDto>.Success(201, entityDto);
        }

        public async Task<CustomResponseDto<CommentDto>> Update(CommentUpdateDto updateDto,
            UserApp activeUser)
        {
            var entity = await CheckUserAccess(updateDto.Id, activeUser.Id);
            CommentMapper.ToEntity(updateDto, entity);
            var updatedEntity = await UpdateAsync(entity);
            var entityDto = CommentMapper.ToDto(updatedEntity);
            return CustomResponseDto<CommentDto>.Success(201, entityDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id, UserApp activeUser)
        {
            var isAdmin = await IsAdminAsync(activeUser);

            var entityToRemove = isAdmin
                ? await GetByIdAsync(id)
                : await CheckUserAccess(id, activeUser.Id);

            await RemoveAsync(entityToRemove);

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        private async Task<bool> IsAdminAsync(UserApp user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Admin");
        }
    }
}