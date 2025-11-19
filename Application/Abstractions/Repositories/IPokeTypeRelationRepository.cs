using Domain.PokeTypeRelations;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRelationRepository
    {
        Task<ICollection<PokeTypeRelation>> GetOffensiveRelations(int pokeTypeId);
        Task<ICollection<PokeTypeRelation>> GetDefensiveRelations(int pokeTypeId);
        Task AddRelations(IEnumerable<PokeTypeRelation> relations);
    }
}
