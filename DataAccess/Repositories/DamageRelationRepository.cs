using Application.Abstractions.Repositories;
using Domain.PokeTypeRelations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DamageRelationRepository(PokeDbContext dbContext) : IDamageRelationRepository
    {
        private readonly PokeDbContext _dbContext = dbContext;

        public async Task AddRelations(ICollection<DamageRelation> relations, CancellationToken cancellationToken)
        {
            _dbContext.DamageRelations.AddRange(relations);
            await  _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<DamageRelation>> GetDefensiveRelations(int pokeTypeId)
        {
            return await _dbContext.DamageRelations.Where(relation => relation.DefendingTypeId == pokeTypeId).ToListAsync();
        }

        public async Task<List<DamageRelation>> GetOffensiveRelations(int pokeTypeId)
        {
            return await _dbContext.DamageRelations.Where(relation => relation.AttackingTypeId == pokeTypeId).ToListAsync();
        }

        public async Task<bool> HasAnyDamageRelationsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.DamageRelations.AnyAsync();
        }
    }
}
