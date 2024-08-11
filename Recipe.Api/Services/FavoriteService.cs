using Microsoft.EntityFrameworkCore;
using Recipe.Api.Helpers;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.API.Services
{
    public class FavoriteService : Service<Favorite>, IFavoriteService
    {
        private readonly IRecipeService _recipeService;

        public FavoriteService(IGenericRepository<Favorite> repository, IUnitOfWork unitOfWork,
            IRecipeService recipeService
        )
            : base(repository, unitOfWork)
        {
            _recipeService = recipeService;
        }

        public async Task<CustomResponseDto<List<FavoriteDto>>> GetAllAsync(FilterReportDto filterReportDto)
        {
            var favoritesQuery = GetAll()
                .Include(f => f.Recipe)
                .Include(f => f.User)
                .AsQueryable();
            favoritesQuery = QueryFilterHelper.ApplyFilters(favoritesQuery, filterReportDto);
            favoritesQuery = favoritesQuery.OrderByDescending(f => f.CreatedDate);
            var (favoriteList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(favoritesQuery, filterReportDto);
            var favoriteListDto = FavoriteMapper.ToListDto(favoriteList);
            return CustomResponseDto<List<FavoriteDto>>.Success(200, favoriteListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<List<FavoriteMyDto>>> GetMyAsync(FilterPaginationDto filterPaginationDto,
            UserApp activeUser)
        {
            var favoritesQuery = Where(f => f.UserId == activeUser.Id).Include(f => f.Recipe).AsQueryable();
            favoritesQuery = favoritesQuery.OrderByDescending(f => f.CreatedDate);
            var (favoriteList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(favoritesQuery, filterPaginationDto);
            var favoriteListDto = FavoriteMapper.ToMyListDto(favoriteList);
            return CustomResponseDto<List<FavoriteMyDto>>.Success(200, favoriteListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<FavoriteDto>> Create(FavoriteCreateDto favoriteCreateDto,
            UserApp activeUser)
        {
            favoriteCreateDto.UserId = activeUser.Id;
            await _recipeService.EnsureExistsOrThrow(t => t.Id == favoriteCreateDto.RecipeId);
            await EnsureNotExistsOrThrow(t =>
                t.UserId == favoriteCreateDto.UserId && t.RecipeId == favoriteCreateDto.RecipeId);
            var favorite = FavoriteMapper.ToEntity(favoriteCreateDto);
            var createdFavorite = await AddAsync(favorite);
            var favoriteDto = FavoriteMapper.ToDto(createdFavorite);
            return CustomResponseDto<FavoriteDto>.Success(201, favoriteDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id, UserApp activeUser)
        {
            var entityToRemove = await CheckUserAccess(id, activeUser.Id);
            await RemoveAsync(entityToRemove);
            return CustomResponseDto<NoContentDto>.Success(204);
        }
        
    }
}