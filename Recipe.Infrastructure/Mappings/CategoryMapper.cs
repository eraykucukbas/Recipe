using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Favorite;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class CategoryMapper
    {
        public static CategoryDto? ToDto(Category? entity)
        {
            if (entity is null) return null;

            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static List<CategoryDto?>? ToListDto(IEnumerable<Category>? entities)
        {
            return entities?.Select(ToDto).ToList();
        }

        public static Category ToEntity(CategoryCreateDto categoryCreateDto)
        {
            return new Category
            {
                Name = categoryCreateDto.Name
            };
        }
        public static void ToEntity(CategoryUpdateDto categoryUpdateDto, Category entity)
        {
            entity.Name = categoryUpdateDto.Name;
        }
    }
}