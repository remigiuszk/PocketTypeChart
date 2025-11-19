using Domain.PokeTypeRelations;
using Domain.PokeTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PokeDbContext(DbContextOptions opt) : DbContext(opt)
    {
        public DbSet<PokeType> PokeTypes { get; set; }
        public DbSet<PokeTypeRelation> PokeTypeRelations { get; set; }
    }
}
