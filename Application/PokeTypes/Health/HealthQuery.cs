using Application.Abstractions.Messaging;
using Application.Abstractions.Queries;
using Application.Abstractions.Repositories;
using Application.Shared;
using Domain.PokeTypes;

namespace Application.PokeTypes.GetAllTypes
{
    public sealed record HealthQuery : IQuery<int>;

    internal sealed class HealthQueryHandler(IDamageRelationQueries damageRelationQueries) : IQueryHandler<HealthQuery, int>
    {
        private readonly IDamageRelationQueries _queries = damageRelationQueries;

        public async Task<Result<int>> Handle(HealthQuery request, CancellationToken cancellationToken)
        {
            await _queries.WarmUpDb();
            return Result.Success(1);
        }
    }
}
