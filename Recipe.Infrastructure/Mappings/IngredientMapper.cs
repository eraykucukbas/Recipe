using Recipe.Core.DTOs.Auth;
using Recipe.Core.DTOs.Ingredient;
using Recipe.Core.DTOs.Token;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class IngredientMapper
    {
        public static IngredientDto? ToDto(Ingredient? entity)
        {
            if (entity is null) return null;

            return new IngredientDto
            {
                Name = entity.Name,
                Quantity = entity.Quantity,
                Unit = entity.Unit
            };
        }

        public static Ingredient ToEntity(IngredientCreateDto dto)
        {
            return new Ingredient
            {
                Name = dto.Name,
                Quantity = dto.Quantity,
                UnitId = dto.UnitId,
                RecipeId = dto.RecipeId,
            };
        }

        public static List<Ingredient?> ToEntityList(IEnumerable<IngredientCreateDto>? dto)
        {
            return dto.Select(ToEntity).ToList();
        }
        
        public static List<IngredientDto?> ToSummaryListdto(ICollection<Ingredient>? entities)
        {
            if (entities is null) return null;
            return entities.Select(ToDto).ToList();
        }
    }
}