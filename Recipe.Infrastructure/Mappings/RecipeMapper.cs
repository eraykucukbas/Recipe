using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
// using Recipe.Core.Interfaces.Mappings;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.Infrastructure.Mappings
{
    public static class RecipeMapper
    {
        public static RecipeDto? ToDto(RecipeEntity? entity)
        {
            if (entity is null) return null;

            return new RecipeDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                UserId = entity.UserId,
                CategoryId = entity.CategoryId,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static RecipeDetailDto? ToDetailDto(RecipeEntity? entity)
        {
            if (entity is null) return null;

            return new RecipeDetailDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                User = entity.User,
                Category = entity.Category,
                Ingredients = entity.Ingredients,
                Instructions = entity.Instructions,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static List<RecipeDto?>? ToListDto(IEnumerable<RecipeEntity>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        public static RecipeEntity ToEntity(RecipeCreateDto recipeCreateDto)
        {
            return new RecipeEntity
            {
                Title = recipeCreateDto.Title,
                Description = recipeCreateDto.Description,
                ImageUrl = recipeCreateDto.ImageUrl,
                UserId = recipeCreateDto.UserId,
                CategoryId = recipeCreateDto.CategoryId
            };
        }
    }
}