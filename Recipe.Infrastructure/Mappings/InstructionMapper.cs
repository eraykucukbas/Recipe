using Recipe.Core.DTOs.Instruction;
using Recipe.Core.Entities;

namespace Recipe.Infrastructure.Mappings
{
    public static class InstructionMapper
    {
        public static InstructionDto? ToDto(Instruction? entity)
        {
            if (entity is null) return null;

            return new InstructionDto
            {
                Name = entity.Name,
                Description = entity.Description,
                Step = entity.Step
            };
        }

        // public static List<RecipeDto?>? ToListDto(IEnumerable<RecipeEntity>? entities)
        // {
        //     return entities?.Select(ToDto).ToList();
        // }

        public static Instruction ToEntity(InstructionCreateDto dto)
        {
            return new Instruction
            {
                Name = dto.Name,
                Description = dto.Description,
                Step = dto.Step,
                RecipeId = dto.RecipeId,
            };
        }

        public static List<Instruction?>? ToEntityList(IEnumerable<InstructionCreateDto>? dto)
        {
            // return entities?.Select(ToDto).ToList();
            return dto.Select(ToEntity).ToList();
        }
    }
}