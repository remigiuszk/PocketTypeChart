using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
