using Domain.PokeTypeRelations;

namespace Application.Abstractions.Repositories
{
    public interface IDamageRelationRepository
    {
        Task<List<DamageRelation>> GetAll(CancellationToken cancellationToken);
        Task<List<DamageRelation>> GetOffensiveRelations(int pokeTypeId);
        Task<List<DamageRelation>> GetDefensiveRelations(int pokeTypeId);
        Task AddRelations(ICollection<DamageRelation> relations, CancellationToken cancellationToken);
        Task<bool> HasAnyDamageRelationsAsync(CancellationToken cancellationToken);
    }
}
