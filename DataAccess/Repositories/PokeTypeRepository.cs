using Application.Abstractions.Repositories;
using Domain.PokeTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PokeTypeRepository(PokeDbContext pokeDbContext) : IPokeTypeRepository
    {
        private readonly PokeDbContext _dbContext = pokeDbContext;

        public async Task<ICollection<PokeType>> GetPokeTypes(CancellationToken cancellationToken)
        {
            return await _dbContext.PokeTypes.ToListAsync(cancellationToken);
        }

        public async Task AddPokeType(PokeType pokeType, CancellationToken cancellationToken)
        {
            _dbContext.PokeTypes.Add(pokeType);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PokeType?> GetPokeTypeById(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.PokeTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
