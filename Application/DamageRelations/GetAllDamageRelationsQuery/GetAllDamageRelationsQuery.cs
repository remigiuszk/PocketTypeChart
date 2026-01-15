using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Shared;
using Domain.PokeTypeRelations;

namespace Application.DamageRelations.GetTypingEffectivenessQuery
{
    public sealed record GetAllDamageRelationsQuery() : IQuery<List<DamageRelation>>;

    internal class GetAllDamageRelationsQueryHandler(
        IDamageRelationRepository repository
        ) : IQueryHandler<GetAllDamageRelationsQuery, List<DamageRelation>>
    {
        private readonly IDamageRelationRepository _repository = repository;

        public async Task<Result<List<DamageRelation>>> Handle(GetAllDamageRelationsQuery request, CancellationToken cancellationToken)
        {
            var damageRelations = await _repository.GetAll(cancellationToken);
            return Result.Success(damageRelations);
        }
    }
}
