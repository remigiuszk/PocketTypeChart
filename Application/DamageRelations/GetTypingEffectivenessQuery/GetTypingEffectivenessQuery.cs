using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.DamageRelations.GetTypingEffectivenessQuery.Mappers;
using Application.DamageRelations.GetTypingEffectivenessQuery.ReadModel.DamageRelation;
using Application.PokeTypes.PreloadTypes;
using Application.Shared;
using Domain.PokeTypeRelations;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Application.DamageRelations.GetDamageRelationsForSelectedTypes
{
    public sealed record GetTypingEffectivenessQuery(List<int> SelectedTypesId) : IQuery<List<DamageRelation>>;
    internal class GetTypingEffectivenessQueryHandler(
        IDamageRelationRepository damageRelationRepository,
        IPokeTypeRepository pokeTypeRepository,
        ILogger<PreloadTypesCommandHandler> logger
        ) : IQueryHandler<GetTypingEffectivenessQuery, List<DamageRelation>>
    {
        private readonly IDamageRelationRepository _damageRelationRepository = damageRelationRepository;
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;
        private readonly ILogger<PreloadTypesCommandHandler> _logger = logger;

        private List<DefensiveDamageRelationReadModel> _defRelations = [];
        private List<DamageRelation> _offensiveRelationsToMap;
        private List<DamageRelation> _defensiveRelationsToMap;

        public async Task<Result<List<DamageRelation>>> Handle(GetTypingEffectivenessQuery request, CancellationToken cancellationToken)
        {

            // chyba bedzie nieoptymalne tak za kazdym razem pobierac typ, trzeba chyba bedzie zrobic jednak FK
            await GetRelationsFromDb(request);

            var defensiveRelationsLookup = _defensiveRelationsToMap.ToLookup(x => x.AttackingTypeId);

            foreach (var grouping in defensiveRelationsLookup)
            {
                var typeEntity = await _pokeTypeRepository.GetPokeTypeById(relationLookup.Key, cancellationToken);


                readModel.AtackingType = typeEntity.ToReadModel();
                readModel.Multiplier = relationLookup.Select(x => x.Multiplier).Aggregate(1.0, (totalMultiPlayer, currentMultiplier) => totalMultiPlayer * currentMultiplier);
                _defRelations.Add(readModel);
            }
        }

        private async Task GetRelationsFromDb(GetTypingEffectivenessQuery request)
        {
            foreach (var id in request.SelectedTypesId)
            {
                _defensiveRelationsToMap.AddRange(await _damageRelationRepository.GetDefensiveRelations(id));
                _offensiveRelationsToMap.AddRange(await _damageRelationRepository.GetOffensiveRelations(id));
            }
        }

        private void HandleDefensiveRelations()
        {
            var defensiveRelationsLookup = _defensiveRelationsToMap.ToLookup(x => x.AttackingTypeId);

            foreach (var grouping in defensiveRelationsLookup)
            {
                var typeEntity = await _pokeTypeRepository.GetPokeTypeById(relationLookup.Key, cancellationToken);
            }
    }
}
