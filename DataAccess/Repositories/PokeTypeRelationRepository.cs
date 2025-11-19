using Application.Abstractions.Repositories;
using Domain.PokeTypeRelations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PokeTypeRelationRepository(PokeDbContext dbContext) : IPokeTypeRelationRepository
    {
        private readonly PokeDbContext _dbContext = dbContext;

        public async Task AddRelations(IEnumerable<PokeTypeRelation> relations)
        {
            await _dbContext.PokeTypeRelations.AddRangeAsync(relations);
        }

        public async Task<ICollection<PokeTypeRelation>> GetDefensiveRelations(int pokeTypeId)
        {
            return await _dbContext.PokeTypeRelations.Where(relation => relation.DefendingTypeId == pokeTypeId).ToListAsync();
        }

        public async Task<ICollection<PokeTypeRelation>> GetOffensiveRelations(int pokeTypeId)
        {
            return await _dbContext.PokeTypeRelations.Where(relation => relation.AttackingTypeId == pokeTypeId).ToListAsync();
        }
    }
}
