using Recipe.Core.Entities;
using Recipe.Core.Interfaces.Repositories;
using Recipe.Infrastructure.Models;

namespace Recipe.Infrastructure.Repositories
{
    public class InstructionRepository : GenericRepository<Instruction>, IInstructionRepository
    {
        public InstructionRepository(AppDbContext context) : base(context)
        {}
    }
}