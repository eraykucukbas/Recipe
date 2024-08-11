using Microsoft.EntityFrameworkCore;
using Recipe.Api.Helpers;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Category;
using Recipe.Core.DTOs.Filter;
using Recipe.Core.DTOs.Unit;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Core.Interfaces.Services;
using Recipe.Core.Interfaces.UnitOfWorks;
using Recipe.Infrastructure.Mappings;

namespace Recipe.API.Services
{
    public class UnitService : Service<Unit>, IUnitService
    {
        public UnitService(IGenericRepository<Unit> repository, IUnitOfWork unitOfWork
        )
            : base(repository, unitOfWork)
        {
        }

        public async Task<CustomResponseDto<List<UnitDto>>> GetAllAsync(FilterPaginationDto filterPaginationDto)
        {
            var query = GetAll()
                .AsQueryable();
            query = query.OrderByDescending(f => f.CreatedDate);
            var (unitList, paginationInfo) =
                await PaginationHelper.ApplyPaginationAsync(query, filterPaginationDto);
            var unitListDto = UnitMapper.ToListDto(unitList);
            return CustomResponseDto<List<UnitDto>>.Success(200, unitListDto, paginationInfo);
        }

        public async Task<CustomResponseDto<UnitDto>> Create(UnitCreateDto unitCreateDto)
        {
            await EnsureNotExistsOrThrow(c => c.Name == unitCreateDto.Name);
            var entity = UnitMapper.ToEntity(unitCreateDto);
            var entityDto = UnitMapper.ToDto(await AddAsync(entity));
            return CustomResponseDto<UnitDto>.Success(201, entityDto);
        }

        public async Task<CustomResponseDto<UnitDto>> Update(UnitUpdateDto unitUpdateDto)
        {
            var entity = await GetByIdAsync(unitUpdateDto.Id);
            UnitMapper.ToEntity(unitUpdateDto, entity);
            var updatedEntity = await UpdateAsync(entity);
            var entityDto = UnitMapper.ToDto(updatedEntity);
            return CustomResponseDto<UnitDto>.Success(201, entityDto);
        }


        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await RemoveAsync(entity);
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}