using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.External.PokeApi.Dto;
using Application.External.PokeApi.Mappers;
using Application.Shared;
using Domain.PokeTypeRelations;
using Domain.PokeTypes;
using Microsoft.Extensions.Logging;

namespace Application.PokeTypes.PreloadTypes
{
    public sealed record PreloadTypesCommand : ICommand;

    internal sealed class PreloadTypesCommandHandler(
        ILogger<PreloadTypesCommandHandler> logger,
        IPokeApiHttpService pokeApiHttpService,
        IPokeTypeRepository pokeTypeRepository,
        IDamageRelationRepository damageRelationRepository) : ICommandHandler<PreloadTypesCommand>
    {
        private readonly ILogger<PreloadTypesCommandHandler> _logger = logger;
        private readonly IPokeApiHttpService _pokeApiHttpService = pokeApiHttpService;
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;
        private readonly IDamageRelationRepository _damageRelationRepository = damageRelationRepository;
        private readonly List<PokeType> _typesToSave = [];
        private readonly List<DamageRelation> _damageRelationsToSave = [];
        private readonly int AMOUNT_OF_TYPES = 18;

        public async Task<Result> Handle(PreloadTypesCommand command, CancellationToken cancellationToken)
        {
            _typesToSave.Clear();
            _damageRelationsToSave.Clear();

            if(await _pokeTypeRepository.HasAnyPokeTypesAsync(cancellationToken))
            {
                _logger.LogError(PreloadTypesErrors.TypesInDb.Description);
                return PreloadTypesErrors.TypesInDb;
            }

            if (await _damageRelationRepository.HasAnyDamageRelationsAsync(cancellationToken))
            {
                _logger.LogError(PreloadTypesErrors.TypesInDb.Description);
                return PreloadTypesErrors.TypesInDb;
            }

            await GetAllPokeTypesFromPokeApiAsync();

            await SavePreparedDataAsync(cancellationToken);

            return Result.Success();
        }

        private async Task GetAllPokeTypesFromPokeApiAsync()
        {
            for (var i = 1; i <= AMOUNT_OF_TYPES; i++)
            {
                var pokeTypeDto = await _pokeApiHttpService.GetPokeTypeAsync(i);

                if(pokeTypeDto != null)
                    MapToDomainsAndAddToSave(pokeTypeDto);
            }
        }

        private void MapToDomainsAndAddToSave(PokeTypeDto pokeTypeDto)
        {
            _typesToSave.Add(pokeTypeDto.ToDomain());
            _damageRelationsToSave.AddRange(pokeTypeDto.MapDamageRelations());
        }

        private async Task SavePreparedDataAsync(CancellationToken cancellationToken)
        {
            await _pokeTypeRepository.AddPokeTypes(_typesToSave, cancellationToken);
            await _damageRelationRepository.AddRelations(_damageRelationsToSave, cancellationToken);
        }
    }
}
