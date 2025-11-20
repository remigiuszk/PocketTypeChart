using Domain.PokeTypeRelations;
using Domain.PokeTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class PokeDbContext : DbContext
    {
        public PokeDbContext(DbContextOptions<PokeDbContext> options)
            : base(options)
        {
        }

        public DbSet<PokeType> PokeTypes { get; set; } = null!;
        public DbSet<PokeTypeRelation> PokeTypeRelations { get; set; } = null!;
    }
}
