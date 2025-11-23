using Domain.PokeTypeRelations;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRelationRepository
    {
        Task<ICollection<DamageRelation>> GetOffensiveRelations(int pokeTypeId);
        Task<ICollection<DamageRelation>> GetDefensiveRelations(int pokeTypeId);
        Task AddRelations(IEnumerable<DamageRelation> relations);
    }
}
