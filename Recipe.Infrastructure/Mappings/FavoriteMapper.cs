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
                RecipeId = entity.RecipeId,
                UserId = entity.UserId,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static FavoriteMyDto? ToMyDto(Favorite? entity)
        {
            if (entity is null) return null;

            return new FavoriteMyDto
            {
                Id = entity.Id,
                Recipe = RecipeMapper.ToSummaryDto(entity.Recipe),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static List<FavoriteDto?>? ToListDto(IEnumerable<Favorite>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        public static List<FavoriteMyDto?>? ToMyListDto(IEnumerable<Favorite>? entities)
        {
            return entities?.Select(ToMyDto).ToList();
        }

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