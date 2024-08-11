using Recipe.Core.DTOs.Base;

namespace Recipe.Core.DTOs.Recipe;

public class RecipeSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
}