using Microsoft.EntityFrameworkCore;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;
using RecipEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.API.Services
{
    public class RecipeService : Service<RecipEntity>, IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IInstructionRepository _instructionRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IGenericRepository<RecipEntity> repository, IUnitOfWork unitOfWork,
            IRecipeRepository recipeRepository,
            IInstructionRepository instructionRepository,
            IIngredientRepository ingredientRepository
        )
            : base(repository, unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _instructionRepository = instructionRepository;
            _ingredientRepository = ingredientRepository;
            _unitOfWork = unitOfWork;
        }

        private async Task<CustomResponseDto<RecipEntity>> CheckUserAccess(int recipeId, string userId)
        {
            var recipe = await _recipeRepository.Where(t => t.Id == recipeId)
                .FirstOrDefaultAsync(t => t.UserId == userId);
            if (recipe == null)
            {
                return CustomResponseDto<RecipEntity>.Fail(403, "You don't have access for this transaction");
            }

            return CustomResponseDto<RecipEntity>.Success(200, recipe);
        }

        public async Task<CustomResponseDto<List<RecipeDto>>> GetAll()
        {
            var recipes = _recipeRepository.GetAll();

            var recipeDto = RecipeMapper.ToListDto(await recipes.ToListAsync());

            return CustomResponseDto<List<RecipeDto>>.Success(200, recipeDto);
        }

        public async Task<CustomResponseDto<RecipeDetailDto>> GetById(int id)
        {
            var recipe = await _recipeRepository
                .Where(r => r.Id == id)
                .Include(r => r.User)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .Include(r => r.Instructions)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return CustomResponseDto<RecipeDetailDto>.Fail(404, "Recipe not found");
            }

            var recipeDetailDto = RecipeMapper.ToDetailDto(recipe);
            return CustomResponseDto<RecipeDetailDto>.Success(200, recipeDetailDto);
        }

        public async Task<CustomResponseDto<RecipeDto>> Create(RecipeCreateDto recipeCreateDto, UserApp activeUser)
        {
            var recipe = RecipeMapper.ToEntity(recipeCreateDto);
            recipe.UserId = activeUser.Id;

            await _recipeRepository.AddAsync(recipe);
            await _unitOfWork.CommitAsync();

            recipeCreateDto.Ingredients.ForEach(i => i.RecipeId = recipe.Id);
            recipeCreateDto.Instructions.ForEach(i => i.RecipeId = recipe.Id);

            var ingredientList = IngredientMapper.ToEntityList(recipeCreateDto.Ingredients);
            var instructionList = InstructionMapper.ToEntityList(recipeCreateDto.Instructions);

            await _ingredientRepository.AddRangeAsync(ingredientList);
            await _instructionRepository.AddRangeAsync(instructionList);

            var recipeDto = RecipeMapper.ToDto(recipe);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<RecipeDto>.Success(201, recipeDto);
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