using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.PokeTypes.PreloadTypes;
using Application.Shared;
using Domain.PokeTypeRelations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.DamageRelations.GetDamageRelationsForSelectedTypes
{
    public sealed record GetDamageRelationsForSelectedTypesCommand(List<int> SelectedTypesId) : ICommand<List<DamageRelation>>;
    internal class GetDamageRelationsForSelectedTypesCommandHandler(
        IDamageRelationRepository damageRelationRepository,
        IPokeTypeRepository pokeTypeRepository,
        ILogger<PreloadTypesCommandHandler> logger
        ) : ICommandHandler<GetDamageRelationsForSelectedTypesCommand, List<DamageRelation>>
    {
        private readonly IDamageRelationRepository _damageRelationRepository = damageRelationRepository;
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;
        private readonly ILogger<PreloadTypesCommandHandler> _logger = logger;

        private List<DamageRelation> _defensiveRelationsToMap;
        private List<DamageRelation> _offensiveRelationsToMap;

        public async Task<Result<List<DamageRelation>>> Handle(GetDamageRelationsForSelectedTypesCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.SelectedTypesId)
            {
                _defensiveRelationsToMap.AddRange(await damageRelationRepository.GetDefensiveRelations(id));
                _offensiveRelationsToMap.AddRange(await _damageRelationRepository.GetOffensiveRelations(id));
            }


        }
    }
}
