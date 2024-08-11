using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Recipe.Api.Helpers;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Ingredient;
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
    public class RecipeService : Service<RecipeEntity>, IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Ingredient> _ingredientRepository;
        private readonly IGenericRepository<Instruction> _instructionRepository;
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IWebHostEnvironment _env;

        public RecipeService(
            IGenericRepository<RecipeEntity> repository,
            IGenericRepository<Ingredient> ingredientRepository,
            IGenericRepository<Instruction> instructionRepository,
            IGenericRepository<Tag> tagRepository,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment env
        )
            : base(repository, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ingredientRepository = ingredientRepository;
            _instructionRepository = instructionRepository;
            _tagRepository = tagRepository;
            _env = env;
        }

        public async Task<CustomResponseDto<List<RecipeDto>>> GetAllAsync(FilterRecipeDto filterRecipeDto)
        {
            var query = GetAll()
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(t => t.Tags)
                .Include(t => t.Comments)
                .AsQueryable();

            query = QueryFilterHelper.ApplyFilters(query, filterRecipeDto);
            query = query.OrderByDescending(f => f.CreatedDate);
            var (queryList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterRecipeDto);
            var queryListDto = RecipeMapper.ToListDto(queryList);
            return CustomResponseDto<List<RecipeDto>>.Success(200, queryListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<RecipeDetailDto>> GetById(int id)
        {
            var query = await Where(t => t.Id == id)
                .Include(t => t.Ingredients)
                .Include(t => t.Instructions)
                .Include(t => t.Category)
                .Include(t => t.User)
                .Include(t => t.Tags)
                .Include(t => t.Comments)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                throw new NotFoundException("Recipe not found");
            }

            var averageRating = query.Comments
                .Where(c => c.ParentComment == null && c.Rate.HasValue)
                .Average(c => (double?)c.Rate);

            var queryDto = RecipeMapper.ToDetailDto(query);
            queryDto.AverageRating = averageRating;
            return CustomResponseDto<RecipeDetailDto>.Success(200, queryDto);
        }

        public async Task<CustomResponseDto<RecipeSummaryDto>> Create(RecipeCreateFormDataDto recipeCreateFormDataDto,
            UserApp activeUser)
        {
            var recipeCreateDto = RecipeMapper.ToCreateDto(recipeCreateFormDataDto);
            recipeCreateDto.UserId = activeUser.Id;

            if (recipeCreateFormDataDto.Image != null && recipeCreateFormDataDto.Image.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "images");
                var imageUrl = await FileHelper.SaveFileAsync(recipeCreateFormDataDto.Image, uploadPath);
                recipeCreateDto.ImageUrl = imageUrl;
            }

            var recipe = RecipeMapper.ToEntity(recipeCreateDto);
            await AddAsync(recipe);

            recipeCreateDto.Tags.ForEach(i => i.RecipeId = recipe.Id);
            recipeCreateDto.Ingredients.ForEach(i => i.RecipeId = recipe.Id);
            recipeCreateDto.Instructions = recipeCreateDto.Instructions
                .Select((i, index) =>
                {
                    i.RecipeId = recipe.Id;
                    i.Step = index + 1;
                    return i;
                }).ToList();

            var ingredientList = IngredientMapper.ToEntityList(recipeCreateDto.Ingredients);
            var instructionList = InstructionMapper.ToEntityList(recipeCreateDto.Instructions);
            var tagList = TagMapper.ToEntityList(recipeCreateDto.Tags);

            await _ingredientRepository.AddRangeAsync(ingredientList);
            await _instructionRepository.AddRangeAsync(instructionList);
            await _tagRepository.AddRangeAsync(tagList);
            await _unitOfWork.CommitAsync();

            var recipeDto = RecipeMapper.ToSummaryDto(recipe);
            return CustomResponseDto<RecipeSummaryDto>.Success(201, recipeDto);
            // return CustomResponseDto<RecipeSummaryDto>.Success(201);
        }

        public async Task<CustomResponseDto<RecipeSummaryDto>> Update(RecipeUpdateFormDataDto recipeUpdateFormDataDto,
            UserApp activeUser)
        {
            var recipeUpdateDto = RecipeMapper.ToUpdateDto(recipeUpdateFormDataDto);
            var entity = await Where(t => t.Id == recipeUpdateDto.Id).Where(t => t.UserId == activeUser.Id)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new NotFoundException("Recipe not found");
            }

            if (recipeUpdateFormDataDto.Image != null && recipeUpdateFormDataDto.Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(entity.ImageUrl))
                {
                    await FileHelper.DeleteFileAsync(entity.ImageUrl);
                }

                var uploadPath = Path.Combine(_env.WebRootPath, "images");
                var imageUrl = await FileHelper.SaveFileAsync(recipeUpdateFormDataDto.Image, uploadPath);
                recipeUpdateDto.ImageUrl = imageUrl;
            }

            RecipeMapper.ToEntity(recipeUpdateDto, entity);
            var updatedEntity = await UpdateAsync(entity);

            // Tags
            recipeUpdateDto.Tags.ForEach(i => i.RecipeId = updatedEntity.Id);
            var tagList = TagMapper.ToEntityList(recipeUpdateDto.Tags);
            var oldTagList = _tagRepository.Where(t => t.RecipeId == updatedEntity.Id);
            _tagRepository.RemoveRange(oldTagList);
            await _tagRepository.AddRangeAsync(tagList);

            // Ingredients
            recipeUpdateDto.Ingredients.ForEach(i => i.RecipeId = updatedEntity.Id);
            var ingredientList = IngredientMapper.ToEntityList(recipeUpdateDto.Ingredients);
            var oldIngredientList = _ingredientRepository.Where(t => t.RecipeId == updatedEntity.Id);
            _ingredientRepository.RemoveRange(oldIngredientList);
            await _ingredientRepository.AddRangeAsync(ingredientList);

            // Instructions
            recipeUpdateDto.Instructions = recipeUpdateDto.Instructions
                .Select((i, index) =>
                {
                    i.RecipeId = updatedEntity.Id;
                    i.Step = index + 1;
                    return i;
                }).ToList();
            var instructionList = InstructionMapper.ToEntityList(recipeUpdateDto.Instructions);
            var oldInstructionsList = _instructionRepository.Where(t => t.RecipeId == updatedEntity.Id);
            _instructionRepository.RemoveRange(oldInstructionsList);
            await _instructionRepository.AddRangeAsync(instructionList);

            await _unitOfWork.CommitAsync();
            var recipeDto = RecipeMapper.ToSummaryDto(updatedEntity);
            return CustomResponseDto<RecipeSummaryDto>.Success(201, recipeDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> Remove(int id, UserApp activeUser)
        {
            var entity = await Where(t => t.Id == id).Where(t => t.UserId == activeUser.Id)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new NotFoundException("Recipe not found");
            }

            if (!string.IsNullOrEmpty(entity.ImageUrl))
            {
                await FileHelper.DeleteFileAsync(entity.ImageUrl);
            }

            await RemoveAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}