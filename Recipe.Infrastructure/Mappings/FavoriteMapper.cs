using Recipe.Core.DTOs.Favorite;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class FavoriteMapper
    {
        public static FavoriteDto? ToDto(Favorite? entity)
        {
            if (entity is null) return null;

            return new FavoriteDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Recipe = entity.Recipe,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        // public static RecipeDetailDto? ToDetailDto(RecipeEntity? entity)
        // {
        //     if (entity is null) return null;
        //
        //     return new RecipeDetailDto
        //     {
        //         Id = entity.Id,
        //         Title = entity.Title,
        //         Description = entity.Description,
        //         ImageUrl = entity.ImageUrl,
        //         User = entity.User,
        //         Category = entity.Category,
        //         Ingredients = entity.Ingredients,
        //         Instructions = entity.Instructions,
        //         CreatedDate = entity.CreatedDate,
        //         UpdatedDate = entity.UpdatedDate
        //     };
        // }
        //
        public static List<FavoriteDto?>? ToListDto(IEnumerable<Favorite>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        //
        public static Favorite ToEntity(FavoriteCreateDto favoriteCreateDto)
        {
            return new Favorite
            {
                UserId = favoriteCreateDto.UserId,
                RecipeId = favoriteCreateDto.RecipeId
            };
        }
    }
}