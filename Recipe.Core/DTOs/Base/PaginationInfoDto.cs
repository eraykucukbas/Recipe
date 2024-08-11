namespace Recipe.Core.DTOs.Base;

public class PaginationInfoDto
{
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Page { get; set; }
}