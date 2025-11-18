using Domain.Models;

namespace Application.Abstractions.Repositories
{
    public interface IPokeTypeRepository
    {
        Task<ICollection<PokeType>> GetPokeTypes(CancellationToken cancellationToken);
        Task AddPokeType(PokeType pokeType, CancellationToken cancellationToken);
    }
}
