using Application.Abstractions.Queries;
using Application.Abstractions.Queries.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Queries
{
    public class DamageRelationQueries(PokeDbContext dbContext) : IDamageRelationQueries
    {
        private readonly PokeDbContext _dbContext = dbContext;

        public async Task<List<DamageRelationDto>> GetAllDamageRelationsForSelectedTypes(List<int> pokeTypeIds)
        {
           return await
                (from relations in _dbContext.DamageRelations
                 join attackingType in _dbContext.PokeTypes on relations.AttackingTypeId equals attackingType.Id
                 join defensiveType in _dbContext.PokeTypes on relations.DefendingTypeId equals defensiveType.Id
                 where pokeTypeIds.Contains(relations.AttackingTypeId) || pokeTypeIds.Contains(relations.DefendingTypeId)
                 select new DamageRelationDto(
                     PokeTypeDto.CreateFromDomain(attackingType),
                     PokeTypeDto.CreateFromDomain(defensiveType),
                     relations.Multiplier))
                .ToListAsync();
        }
    }
}
