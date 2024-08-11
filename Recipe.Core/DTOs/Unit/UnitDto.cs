using Recipe.Core.DTOs.Base;
using Recipe.Core.DTOs.Recipe;
using Recipe.Core.DTOs.User;
using Recipe.Core.Entities;

namespace Recipe.Core.DTOs.Unit;

public class UnitDto : BaseDto
{
    public string Name { get; set; } = null!;
}