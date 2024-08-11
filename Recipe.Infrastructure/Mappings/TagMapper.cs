using Recipe.Core.DTOs.Tag;
using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;
using Recipe.Core.Exceptions;

namespace Recipe.Infrastructure.Mappings
{
    public static class TagMapper
    {
        public static TagDto? ToDto(Tag? entity)
        {
            if (entity is null) return null;

            return new TagDto
            {
                Id = entity.Id,
                Name = entity.Name,
                RecipeId = entity.RecipeId,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }
        
        public static Tag ToEntity(TagCreateDto tagCreateDto)
        {
            return new Tag
            {
                Name = tagCreateDto.Name,
                RecipeId = tagCreateDto.RecipeId
            };
        }

        public static List<Tag> ToEntityList(IEnumerable<TagCreateDto>? dto)
        {
            // return entities?.Select(ToDto).ToList();
            return dto.Select(ToEntity).ToList();
        }
        
        public static List<TagDto?> ToSummaryListdto(ICollection<Tag>? entities)
        {
            if (entities is null) return null;
            return entities.Select(ToDto).ToList();
        }
    }
    //
    // public static void ToEntity(TagUpdateDto tagUpdateDto, Tag entity)
    // {
    //     entity.Name = tagUpdateDto.Name;
    // }

}