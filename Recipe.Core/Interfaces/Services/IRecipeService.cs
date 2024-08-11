using Recipe.Core.DTOs;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.Entities;

namespace Recipe.Core.Interfaces.Services
{
    public interface IRecipeService : IService<Entities.Recipe>
    {
        // Task<CustomResponseDto<Entities.Recipe>> CheckUserAccess(int todoListId, string userId);
        Task<CustomResponseDto<List<RecipeDto>>> GetAllAsync(FilterRecipeDto filterRecipeDto);
        Task<CustomResponseDto<RecipeDetailDto>> GetById(int id);
        Task<CustomResponseDto< RecipeSummaryDto>> Create(RecipeCreateFormDataDto recipeCreateFormDataDto, UserApp activeUser);
        Task<CustomResponseDto< RecipeSummaryDto>> Update(RecipeUpdateFormDataDto recipeUpdateFormDataDto, UserApp activeUser);
        Task<CustomResponseDto<NoContentDto>> Remove(int id, UserApp activeUser);
     
    }
}
