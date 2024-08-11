using Recipe.Core.DTOs.Filter;
using Recipe.Core.Entities;
using Recipe.Core.Interfaces.DTOs.Filter;
using RecipeEntity = Recipe.Core.Entities.Recipe;

namespace Recipe.Api.Helpers;
public static class QueryFilterHelper
{
    public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, object filterDto) where T : BaseEntity
    {
        if (filterDto is IFilterDateRange dateRangeFilter)
        {
            if (dateRangeFilter.StartDateTime.HasValue)
            {
                query = query.Where(f => f.CreatedDate >= dateRangeFilter.StartDateTime.Value);
            }

            if (dateRangeFilter.EndDateTime.HasValue)
            {
                query = query.Where(f => f.CreatedDate <= dateRangeFilter.EndDateTime.Value);
            }
        }

        if (filterDto is FilterReportDto reportFilter)
        {
            if (!string.IsNullOrEmpty(reportFilter.UserId) && typeof(IUserOwnedEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(f => ((IUserOwnedEntity)f).UserId == reportFilter.UserId);
            }

            if (reportFilter.RecipeId.HasValue && typeof(IRecipeOwnedEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(f => ((IRecipeOwnedEntity)f).RecipeId == reportFilter.RecipeId);
            }
        }
        else if (filterDto is FilterRecipeDto recipeFilter)
        {
            if (!string.IsNullOrEmpty(recipeFilter.Name))
            {
                query = query.Where(f => ((RecipeEntity)(object)f).Title.Contains(recipeFilter.Name));
            }

            if (!string.IsNullOrEmpty(recipeFilter.Tag))
            {
                query = query.Where(f => ((RecipeEntity)(object)f).Tags.Any(t => t.Name == recipeFilter.Tag));
            }

            if (recipeFilter.CategoryId.HasValue)
            {
                query = query.Where(f => ((RecipeEntity)(object)f).CategoryId == recipeFilter.CategoryId.Value);
            }
            
            if (!string.IsNullOrEmpty(recipeFilter.UserId) && typeof(IUserOwnedEntity).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(f => ((IUserOwnedEntity)f).UserId == recipeFilter.UserId);
            }
        }

        return query;
    }
}