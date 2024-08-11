namespace Recipe.Core.Interfaces.DTOs.Filter;

public interface IFilterDateRange
{
    DateTime? StartDateTime { get; set; }
    DateTime? EndDateTime { get; set; }
}

public interface IFilterPagination
{
    int Limit { get; set; }
    int Page { get; set; }
}