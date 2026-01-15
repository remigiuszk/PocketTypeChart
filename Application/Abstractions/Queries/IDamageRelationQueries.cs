using Application.Abstractions.Queries.Dto;

namespace Application.Abstractions.Queries
{
    public interface IDamageRelationQueries
    {
        public Task<List<DamageRelationDto>> GetAllDamageRelationsForSelectedTypes(List<int> pokeTypeIds);
        public Task WarmUpDb();
    }
}
