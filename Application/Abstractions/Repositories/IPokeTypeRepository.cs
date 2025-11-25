using Domain.PokeTypes;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRepository
    {
        Task<List<PokeType>> GetPokeTypes(CancellationToken cancellationToken);
        Task AddPokeTypes(ICollection<PokeType> pokeTypes, CancellationToken cancellationToken);
        Task<PokeType?> GetPokeTypeById(int id, CancellationToken cancellationToken);
        Task<bool> HasAnyPokeTypesAsync(CancellationToken cancellationToken);
    }
}
