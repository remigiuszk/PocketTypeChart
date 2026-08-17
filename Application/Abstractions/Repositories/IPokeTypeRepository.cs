using Domain.PokeTypes;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRepository
    {
        Task<List<PokeType>> GetPokeTypes(CancellationToken cancellationToken);
        Task AddPokeTypes(ICollection<PokeType> pokeTypes, CancellationToken cancellationToken);
        Task UpdatePokeTypesAsync(ICollection<PokeType> pokeTypes, CancellationToken cancellationToken);
        Task<PokeType?> GetPokeTypeById(int id, CancellationToken cancellationToken);
        Task<byte[]?> GetSpriteImageAsync(int id, CancellationToken cancellationToken);
        Task<bool> HasAnyPokeTypesAsync(CancellationToken cancellationToken);
    }
}
