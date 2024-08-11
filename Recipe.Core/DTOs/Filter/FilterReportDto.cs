using Recipe.Core.Interfaces.DTOs.Filter;

namespace Recipe.Core.DTOs.Filter;
public class FilterPaginationDto : IFilterPagination
{
    public int Limit { get; set; }
    public int Page { get; set; }
}

public class FilterDateDto : FilterPaginationDto, IFilterDateRange
{
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}

public class FilterReportDto : FilterDateDto
{
    public string? UserId { get; set; }
    public int? RecipeId { get; set; }
}

public class FilterRecipeDto : FilterDateDto
{
    public string? Name { get; set; }
    public string? Tag { get; set; }
    public int? CategoryId { get; set; }
    public string? UserId { get; set; }

}