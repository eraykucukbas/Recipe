using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class UnitMapper
    {
        public static UnitDto? ToDto(Unit? entity)
        {
            if (entity is null) return null;

            return new UnitDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static List<UnitDto?>? ToListDto(IEnumerable<Unit>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        public static Unit ToEntity(UnitCreateDto unitCreateDto)
        {
            return new Unit
            {
                Name = unitCreateDto.Name
            };
        }
        
        public static void ToEntity(UnitUpdateDto unitUpdateDto, Unit entity)
        {
            entity.Name = unitUpdateDto.Name;
        }
    }
}