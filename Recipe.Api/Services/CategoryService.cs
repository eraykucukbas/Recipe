using Microsoft.EntityFrameworkCore;
using Recipe.Api.Helpers;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;

namespace Recipe.API.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork
        )
            : base(repository, unitOfWork)
        {
        }

        public async Task<CustomResponseDto<List<CategoryDto>>> GetAllAsync(FilterPaginationDto filterPaginationDto)
        {
            var query = GetAll()
                .AsQueryable();
            query = query.OrderByDescending(f => f.CreatedDate);
            var (categoryList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterPaginationDto);
            var categoryListDto = CategoryMapper.ToListDto(categoryList);
            return CustomResponseDto<List<CategoryDto>>.Success(200, categoryListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<CategoryDto>> Create(CategoryCreateDto categoryCreateDto)
        {
            await EnsureNotExistsOrThrow(c => c.Name == categoryCreateDto.Name);
            var entity = CategoryMapper.ToEntity(categoryCreateDto);
            var entityDto = CategoryMapper.ToDto(await AddAsync(entity));
            return CustomResponseDto<CategoryDto>.Success(201, entityDto);
        }

        public async Task<CustomResponseDto<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var entity = await GetByIdAsync(categoryUpdateDto.Id);
            CategoryMapper.ToEntity(categoryUpdateDto, entity);
            var updatedEntity = await UpdateAsync(entity);
            var entityDto = CategoryMapper.ToDto(updatedEntity);
            return CustomResponseDto<CategoryDto>.Success(201, entityDto);
        }


        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await RemoveAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}