using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PokeTypeRepository(PokeDbContext pokeDbContext) : IPokeTypeRepository
    {
        private readonly PokeDbContext _pokeDbContext = pokeDbContext;

        public async Task<ICollection<PokeType>> GetPokeTypes()
        {
            return await _pokeDbContext.PokeTypes.ToListAsync();
        }
    }
}
