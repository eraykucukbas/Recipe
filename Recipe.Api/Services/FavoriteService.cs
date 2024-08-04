using Microsoft.EntityFrameworkCore;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.API.Services
{
    public class FavoriteService : Service<Favorite>, IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IGenericRepository<Favorite> repository, IUnitOfWork unitOfWork,
            IFavoriteRepository favoriteRepository,
            IRecipeRepository recipeRepository
        )
            : base(repository, unitOfWork)
        {
            _favoriteRepository = favoriteRepository;
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        // base service' de olmalı 
        private async Task<CustomResponseDto<Favorite>> CheckUserAccess(int favoriteId, string userId)
        {
            var favorite = await _favoriteRepository.Where(e => e.Id == favoriteId)
                .FirstOrDefaultAsync(e => e.UserId == userId);
            if (favorite == null)
            {
                throw new ForbiddenException();
            }

            return CustomResponseDto<Favorite>.Success(200, favorite);
        }


        public async Task<CustomResponseDto<List<FavoriteDto>>> GetAll()
        {
            try
            {
                var favorites = _favoriteRepository.GetAll().Include(x => x.Recipe);

                var favoriteListDto = FavoriteMapper.ToListDto(await favorites.ToListAsync());

                return CustomResponseDto<List<FavoriteDto>>.Success(200, favoriteListDto);
            }
            catch (Exception e)
            {
                throw new ServerException(e.Message);
            }
        }

        public async Task<CustomResponseDto<FavoriteDto>> Create(FavoriteCreateDto favoriteCreateDto,
            UserApp activeUser)
        {
            try
            {
                favoriteCreateDto.UserId = activeUser.Id;
                
                var favorite = FavoriteMapper.ToEntity(favoriteCreateDto);

                await _favoriteRepository.AddAsync(favorite);

                var favoriteDto = FavoriteMapper.ToDto(favorite);

                await _unitOfWork.CommitAsync();
                return CustomResponseDto<FavoriteDto>.Success(201, favoriteDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ServerException(e.Message);
            }
        }
        //
        // public async Task<CustomResponseDto<NoContentDto>> UpdateTodoList(TodoListUpdateDto todoListUpdateDto, UserApp activeUser)
        // {
        //     var accessCheck = await CheckUserAccess(todoListUpdateDto.Id, activeUser.Id);
        //     if (accessCheck.StatusCode == 403)
        //     {
        //         return CustomResponseDto<NoContentDto>.Fail(accessCheck.StatusCode, accessCheck.Errors);
        //     }
        //
        //     _mapper.Map(todoListUpdateDto, accessCheck.Data);
        //     await UpdateAsync(accessCheck.Data);
        //     return CustomResponseDto<NoContentDto>.Success(204);
        // }
        //
        // public async Task<CustomResponseDto<NoContentDto>> RemoveTodoList(int id, UserApp activeUser)
        // {
        //     var accessCheck = await CheckUserAccess(id, activeUser.Id);
        //     if (accessCheck.StatusCode == 403)
        //     {
        //         return CustomResponseDto<NoContentDto>.Fail(accessCheck.StatusCode, accessCheck.Errors);
        //     }
        //
        //     await RemoveAsync(accessCheck.Data);
        //     return CustomResponseDto<NoContentDto>.Success(204);
        // }
    }
}