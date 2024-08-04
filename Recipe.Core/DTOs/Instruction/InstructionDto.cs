using Recipe.Core.DTOs.Base;

namespace Recipe.Core.DTOs.Instruction;

public class InstructionDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Step { get; set; }
}