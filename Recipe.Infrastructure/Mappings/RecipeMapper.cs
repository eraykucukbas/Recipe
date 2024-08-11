using System.Text.Json;
using System.Text.Json.Serialization;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Instruction;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.Tag;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;
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
                Tags = TagMapper.ToSummaryListdto(entity.Tags),
                AverageRating = entity.Comments
                    .Where(c => c.ParentComment == null && c.Rate.HasValue)
                    .Average(c => (double?)c.Rate),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static RecipeCreateDto ToCreateDto(RecipeCreateFormDataDto? formData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return new RecipeCreateDto
            {
                Title = formData.Title,
                Description = formData.Description,
                UserId = formData.UserId,
                CategoryId = formData.CategoryId,
                Ingredients = string.IsNullOrEmpty(formData.Ingredients)
                    ? new List<IngredientCreateDto>()
                    : JsonSerializer.Deserialize<List<IngredientCreateDto>>(formData.Ingredients, options),
                Instructions = string.IsNullOrEmpty(formData.Instructions)
                    ? new List<InstructionCreateDto>()
                    : JsonSerializer.Deserialize<List<InstructionCreateDto>>(formData.Instructions, options),
                Tags = string.IsNullOrEmpty(formData.Tags)
                    ? new List<TagCreateDto>()
                    : JsonSerializer.Deserialize<List<TagCreateDto>>(formData.Tags, options),
            };
        }

        public static RecipeUpdateDto ToUpdateDto(RecipeUpdateFormDataDto? formData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return new RecipeUpdateDto
            {
                Id = formData.Id,
                Title = formData.Title,
                Description = formData.Description,
                UserId = formData.UserId,
                CategoryId = formData.CategoryId,
                Ingredients = string.IsNullOrEmpty(formData.Ingredients)
                    ? new List<IngredientCreateDto>()
                    : JsonSerializer.Deserialize<List<IngredientCreateDto>>(formData.Ingredients, options),
                Instructions = string.IsNullOrEmpty(formData.Instructions)
                    ? new List<InstructionCreateDto>()
                    : JsonSerializer.Deserialize<List<InstructionCreateDto>>(formData.Instructions, options),
                Tags = string.IsNullOrEmpty(formData.Tags)
                    ? new List<TagCreateDto>()
                    : JsonSerializer.Deserialize<List<TagCreateDto>>(formData.Tags, options),
            };
        }

        public static RecipeSummaryDto ToSummaryDto(RecipeEntity entity)
        {
            if (entity is null) throw new NotFoundException("Recipe not found");

            return new RecipeSummaryDto
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static RecipeDetailDto ToDetailDto(RecipeEntity? entity)
        {
            if (entity is null) throw new NotFoundException("Recipe not found");

            return new RecipeDetailDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                User = UserMapper.ToDto(entity.User),
                Category = CategoryMapper.ToDto(entity.Category),
                Ingredients = IngredientMapper.ToSummaryListdto(entity.Ingredients),
                Instructions = InstructionMapper.ToSummaryListdto(entity.Instructions),
                Tags = TagMapper.ToSummaryListdto(entity.Tags),
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

        public static void ToEntity(RecipeUpdateDto recipeUpdateDto, RecipeEntity entity)
        {
            entity.Title = recipeUpdateDto.Title;
            entity.Description = recipeUpdateDto.Description;
            entity.ImageUrl = recipeUpdateDto.ImageUrl;
            entity.CategoryId = recipeUpdateDto.CategoryId;
        }
    }
}