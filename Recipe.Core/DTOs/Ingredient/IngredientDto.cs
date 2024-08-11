using Recipe.Core.DTOs.Base;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Ingredient;

public class IngredientDto : BaseDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public Entities.Unit Unit { get; set; }
}