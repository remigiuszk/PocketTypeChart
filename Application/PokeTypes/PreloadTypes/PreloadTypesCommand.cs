using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Domain.Models;
using MediatR;

namespace Application.PokeTypes.PreloadTypes
{
    public sealed record PreloadTypesCommand : ICommand;

    internal sealed class PreloadTypesCommandHandler(IPokeTypeRepository pokeTypeRepository) : ICommandHandler<PreloadTypesCommand>
    {
        private readonly IPokeTypeRepository _pokeTypeRepository = pokeTypeRepository;

        public async Task Handle(PreloadTypesCommand command, CancellationToken cancellationToken)
        {
            await _pokeTypeRepository.AddPokeType(new PokeType() { Id = 1, Name = "Fire", Image = "test" }, cancellationToken);
        }
    }
}
