using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Shared;
using Domain.PokeTypes;
using Microsoft.Extensions.Logging;

namespace Application.PokeTypes.BackfillSpriteImages
{
    public sealed record BackfillSpriteImagesCommand : ICommand;

    internal sealed class BackfillSpriteImagesCommandHandler(
        ILogger<BackfillSpriteImagesCommandHandler> logger,
        IPokeApiHttpService pokeApiHttpService,
        IPokeTypeRepository pokeTypeRepository) : ICommandHandler<BackfillSpriteImagesCommand>
    {
        private readonly ILogger<BackfillSpriteImagesCommandHandler> _logger = logger;
        private readonly IPokeApiHttpService _pokeApiHttpService = pokeApiHttpService;
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;

        public async Task<Result> Handle(BackfillSpriteImagesCommand command, CancellationToken cancellationToken)
        {
            var pokeTypes = await _pokeTypeRepository.GetPokeTypes(cancellationToken);
            var updated = new List<PokeType>();

            foreach (var pokeType in pokeTypes)
            {
                if (pokeType.SpriteImage is { Length: > 0 })
                    continue;

                if (string.IsNullOrWhiteSpace(pokeType.Sprite))
                {
                    _logger.LogWarning("PokeType {Id} has no source sprite URL to backfill from.", pokeType.Id);
                    continue;
                }

                pokeType.SpriteImage = await _pokeApiHttpService.DownloadSpriteAsync(pokeType.Sprite);
                updated.Add(pokeType);
            }

            if (updated.Count > 0)
                await _pokeTypeRepository.UpdatePokeTypesAsync(updated, cancellationToken);

            return Result.Success();
        }
    }
}
