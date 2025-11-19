using Domain.PokeTypes;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRepository
    {
        Task<ICollection<PokeType>> GetPokeTypes(CancellationToken cancellationToken);
        Task AddPokeType(PokeType pokeType, CancellationToken cancellationToken);
        Task<PokeType?> GetPokeTypeById(int id, CancellationToken cancellationToken);
    }
}
