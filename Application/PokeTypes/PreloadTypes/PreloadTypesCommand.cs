using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Shared;
using Domain.PokeTypes;
using Microsoft.Extensions.Logging;

namespace Application.PokeTypes.PreloadTypes
{
    public sealed record PreloadTypesCommand : ICommand;

    internal sealed class PreloadTypesCommandHandler(IPokeTypeRepository pokeTypeRepository, ILogger<PreloadTypesCommandHandler> logger) : ICommandHandler<PreloadTypesCommand>
    {
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;
        private readonly ILogger<PreloadTypesCommandHandler> _logger = logger;

        public async Task<Result> Handle(PreloadTypesCommand command, CancellationToken cancellationToken)
        {
            await _pokeTypeRepository.AddPokeType(PokeType.Create(1, "fire", "test"), cancellationToken);

            var existingType = await _pokeTypeRepository.GetPokeTypeById(1, cancellationToken);

            if (existingType != null)
            {
                _logger.LogError(PreloadTypesErrors.TypeAlreadyExists.Description, existingType);
                return PreloadTypesErrors.TypeAlreadyExists;
            }

            await _pokeTypeRepository.AddPokeType(PokeType.Create(1, "fire", "test"), cancellationToken);

            return Result.Success();
        }
    }

    internal static class PreloadTypesErrors
    {
        public static readonly Error TypeAlreadyExists = new(
            "PreloadTypes.AlreadyExists",
            "This type already exists in the database");
    }
}
