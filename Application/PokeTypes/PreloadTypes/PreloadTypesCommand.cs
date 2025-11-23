using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.External.Dto;
using Application.External.Mappers;
using Application.PokeTypes.PreloadTypes.Services.Dto;
using Application.Shared;
using Domain.PokeTypeRelations;
using Domain.PokeTypes;
using Microsoft.Extensions.Logging;

namespace Application.PokeTypes.PreloadTypes
{
    public sealed record PreloadTypesCommand : ICommand;

    internal sealed class PreloadTypesCommandHandler(IPokeTypeRepository pokeTypeRepository, ILogger<PreloadTypesCommandHandler> logger, IPokeApiHttpService pokeApiHttpService) : ICommandHandler<PreloadTypesCommand>
    {
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;
        private readonly ILogger<PreloadTypesCommandHandler> _logger = logger;
        private readonly IPokeApiHttpService _pokeApiHttpService = pokeApiHttpService;
        private readonly int AMOUNT_OF_TYPES = 18;

        private ICollection<PokeType> _typesToSave = new List<PokeType>();
        private ICollection<DamageRelation> _damageRelationsToSave = new List<DamageRelation>();

        public async Task<Result> Handle(PreloadTypesCommand command, CancellationToken cancellationToken)
        {
            await GetAllPokeTypesFromPokeApi();

            //var existingType = await _pokeTypeRepository.GetPokeTypeById(1, cancellationToken);

            //if (existingType != null)
            //{
            //    _logger.LogError(PreloadTypesErrors.TypeAlreadyExists.Description, existingType);
            //    return PreloadTypesErrors.TypeAlreadyExists;
            //}

            //await _pokeTypeRepository.AddPokeType(PokeType.Create(1, "fire", "test"), cancellationToken);

            return Result.Success();
        }

        private async Task GetAllPokeTypesFromPokeApi()
        {
            for (var i = 1; i <= AMOUNT_OF_TYPES; i++)
            {
                await GetPokeTypeFromPokeApiAsync(i);

                _typesToSave.Add(pokeTypeDto.ToDomain());
            }
        }

        private async Task<PokeTypeDto> GetPokeTypeFromPokeApiAsync(int i)
        {
            var externalDto = await _pokeApiHttpService.GetPokeTypeAsync(i);

            var damageRelations = PrepareDamageRelations
        }
    }
}
