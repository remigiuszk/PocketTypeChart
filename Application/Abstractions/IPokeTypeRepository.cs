using Domain.Models;

namespace Application.Abstractions
{
    public interface IPokeTypeRepository
    {
        Task<ICollection<PokeType>> GetPokeTypes();
    }
}
