using Microsoft.EntityFrameworkCore;
using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Filter;

namespace Recipe.Api.Helpers;

public static class PaginationHelper
{
    public static async Task<(List<T>, PaginationInfoDto)> ApplyPaginationAsync<T>(
        IQueryable<T> query, FilterPaginationDto paginationDto)
    {
        var count = await query.CountAsync();
        var page = paginationDto.Page > 0 ? paginationDto.Page : 1;
        var limit = paginationDto.Limit > 0 ? paginationDto.Limit : count;

        query = query.Skip((page - 1) * limit).Take(limit);

        var result = await query.ToListAsync();

        var paginationInfo = new PaginationInfoDto
        {
            Page = page,
            Limit = limit,
            Total = count
        };

        return (result, paginationInfo);
    }
}